﻿namespace domain.entities;

public partial class Article
{
    public int ArticleId { get; set; }

    public int ArticleAuthorId { get; set; }

    public int ClubId { get; set; }

    public string ArticleTitle { get; set; } = null!;
    
    public string NormalizedArticleTitle { get; set; } = null!;

    public string? ArticleThumbnailUrl { get; set; }

    public DateTime? CreatedDateTime { get; set; }

    public DateTime? UpdateDateTime { get; set; }

    public string Status { get; set; } = null!;

    public bool IsDrafted { get; set; }

    public bool Archived { get; set; }
    public bool Pinned { get; set; } 

    public virtual User? ArticleAuthor { get; set; }

    public virtual Club? Club { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<UserArticleBookmark> UserArticleBookmarks { get; set; } = new List<UserArticleBookmark>();

    public virtual ICollection<UserArticleRead> UserArticleReads { get; set; } = new List<UserArticleRead>();

    public virtual ICollection<UserArticleVote> UserArticleVotes { get; set; } = new List<UserArticleVote>();

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();

    public ICollection<ReportedArticle> Reports { get; set; } = new List<ReportedArticle>();
    
    
    public static Article CreateDraft(ArticleDto dto)
    {
        return new Article
        {
            ArticleAuthorId = dto.AuthorId,
            ClubId = dto.ClubId,
            ArticleTitle = dto.ArticleTitle,
            NormalizedArticleTitle = dto.ArticleTitle.ToUpper(),
            ArticleThumbnailUrl = dto.ArticleThumbnailUrl,
            CreatedDateTime = DateTime.Now,
            UpdateDateTime = DateTime.Now,
            Status = ArticleStatusDefaults.Draft,
            IsDrafted = true,
            Tags = dto.Tags
        };
    }
    
    public static Article CreatePublished(ArticleDto dto)
    {
        return new Article
        {
            ArticleAuthorId = dto.AuthorId,
            ClubId = dto.ClubId,
            ArticleTitle = dto.ArticleTitle,
            NormalizedArticleTitle = dto.ArticleTitle.ToUpper(),
            ArticleThumbnailUrl = dto.ArticleThumbnailUrl,
            CreatedDateTime = DateTime.Now,
            UpdateDateTime = DateTime.Now,
            Status = ArticleStatusDefaults.Published,
            IsDrafted = false,
            Tags = dto.Tags
        };
    }

    public void UpdateToRemoved()
    {
        Status = ArticleStatusDefaults.Removed;
    }

    public void UpdateToPublished()
    {
        Status = ArticleStatusDefaults.Published;
    }

    public void UpdatePin(bool pinned) => Pinned = pinned;
}

public class ArticleDto
{
    public int AuthorId { get; set; }
    public int ClubId { get; set; }
    public string ArticleTitle { get; set; } = null!;
    public string? ArticleThumbnailUrl { get; set; }
    public List<Tag> Tags { get; set; } = new List<Tag>();
}

public static class ArticleStatusDefaults
{
    public const string Draft = "Drafted";
    public const string Published = "Published";
    public const string Archived = "Archived";
    public const string Removed = "Removed";
}

public class ArticleResponseDto
{
    public int ArticleId { get; set; }
    public string ClubImageUrl { get; set; } = null!;
    public int AuthorId { get; set; }
    public string AuthorName { get; set; } = null!;
    public string UserImageUrl { get; set; } = null!;
    public int ClubId { get; set; }
    public string ClubName { get; set; } = null!;
    public string ArticleTitle { get; set; } = null!;
    public List<TagDto> Tags { get; set; } = null!;
    public DateTime? CreatedDateTime { get; set; }
    public string ArticleThumbnailUrl { get; set; } = null!;
    public int VoteCount { get; set; } = -1;
    public int CommentCount { get; set; } = -1;
    public int VoteType { get; set; }
    public bool Bookmarked { get; set; }
    public string Status { get; set; } = null!;
    public bool Pinned { get; set; }

}

public class ArticleResponseForEditDto
{
    public int ArticleId { get; set; }
    public int ClubId { get; set; }
    public string ArticleTitle { get; set; } = null!;
    public string ArticleThumbnail { get; set; } = null!;
    public string ArticleContent { get; set; } = null!;
    public List<TagDto> Tags { get; set; } = new();
}

public class ArticleSuggestionDto
{
    public int ArticleId { get; set; }
    public string ArticleTitle { get; set; } = null!;
}