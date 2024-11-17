﻿using application.Events.ArticleEvents;
using application.Exceptions.TagExceptions;
using application.utilities.ImageUploads.Article;
using domain.entities;
using domain.interfaces;
using FluentValidation;
using infrastructure.services.worker;
using MediatR;

namespace application.useCases.articleInteractions.CreateArticle;

public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, CreateArticleResponse>
{
    private readonly IValidator<CreateArticleCommand> _validator;
    private readonly IUserRepository _userRepository;
    private readonly IClubRepository _clubRepository;
    private readonly IArticleImageService _articleImageService;
    private readonly IArticleRepository _articleRepository;
    private readonly ITagRepository _tagRepository;
    private readonly IClubUserRepository _clubUserRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediator _mediator;

    public CreateArticleCommandHandler(
        IValidator<CreateArticleCommand> validator,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IClubRepository clubRepository, 
        IArticleImageService articleImageService, 
        ITagRepository tagRepository, 
        IArticleRepository articleRepository, 
        IMediator mediator, 
        IClubUserRepository clubUserRepository)
    {
        _validator = validator;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _clubRepository = clubRepository;
        _articleImageService = articleImageService;
        _tagRepository = tagRepository;
        _articleRepository = articleRepository;
        _mediator = mediator;
        _clubUserRepository = clubUserRepository;
    }

    public async Task<CreateArticleResponse> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        // validate author id
        if(! await _userRepository.CheckIdExists(request.AuthorId))
            throw new KeyNotFoundException("User does not exist");
        
        // validate club id
        var club = await _clubRepository.GetClubByIdNoTracking(request.ClubId)??
            throw new KeyNotFoundException("Club does not exist");
        
        //check if joined
        if(! await _clubUserRepository.ClubJoined(request.ClubId, request.AuthorId))
            throw new UnauthorizedAccessException("You do not have permission to access this club");
        
        // resolve permissions 
        await ValidateClubPermissions(club, request.AuthorId);
        
        // validate tags
        await ValidateTags(request.TagIds);
        
        // upload article thumbnail
        var articleUploadResponse = await _articleImageService.UploadThumbnail(request.ArticleThumbnail!)??
                                    throw new Exception("Thumbnail upload failed");
        
        // get list of tags
        var tags = await _tagRepository.GetTagsManyAsync(request.TagIds!);

        // create dto
        var articleDto = new ArticleDto
        {
            AuthorId = request.AuthorId,
            ClubId = request.ClubId,
            ArticleTitle = request.ArticleTitle,
            ArticleThumbnailUrl = articleUploadResponse.ImageSecureUrl,
            Tags = tags
        };
        
        // create article object
        var article = request.IsDrafted ? 
            Article.CreateDraft(articleDto) : 
            Article.CreatePublished(articleDto);
        
        // add article
        _articleRepository.AddArticle(article);
        
        // save unit of work
        await _unitOfWork.CommitAsync(cancellationToken);
        
        // create event article event created
        await _mediator.Publish(new ArticleCreatedEvent(article, request.ArticleContent), cancellationToken);
        
        // return response
        return new CreateArticleResponse
        {
            Message = "Article created",
        };
    }

    private async Task ValidateClubPermissions(Club club, int authorId)
    {
        var clubUserRecords = await _clubUserRepository.GetClubUserRecord(authorId);
        var roles = clubUserRecords!.Select(x => x.Role).Distinct();

        if (club.PostPermission == (short)PermissionType.Moderators)
        {
            bool allowed = roles.Any(r => r!.RoleId == (int)DefaultRoles.Moderator);
            if(!allowed)
                throw new UnauthorizedAccessException("You do not have permission to post an article in this club");
        }
    }

    private async Task ValidateTags(List<int>? requestTagIds)
    {
        if(requestTagIds == null || requestTagIds.Count == 0)
            throw new ArgumentException("No tags provided");
        
        if (requestTagIds is [0] && requestTagIds.Count == 1)
        {
            throw new ArgumentException("No tags provided");
        }
        
        if (!TagsUnique(requestTagIds))
        {
            throw TagException.DuplicateTags();
        }

        if (!await EnsureAllTagsExist(requestTagIds))
        {
            throw TagException.InvalidTagIds();
        }
        
    }
    
    private bool TagsUnique(List<int> tagIds)
    {
        HashSet<int> seedTagIds = new HashSet<int>();
        bool isUnique = true;

        foreach (var number in tagIds)
        {
            if (!seedTagIds.Add(number))
            {
                isUnique = false;
                break;
            }
        }
        return isUnique;
    }

    private async Task<bool> EnsureAllTagsExist(List<int> tagIds)
    {
        return await _tagRepository.AreAllIdsValidAsync(tagIds);
    }
}