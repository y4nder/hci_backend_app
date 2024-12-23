﻿using domain.entities;
using domain.pagination;

namespace domain.interfaces;

public interface IArticleRepository
{
    void AddArticle(Article article);
    
    Task<Article?> GetArticleByIdAsync(int articleId);
    
    void Update(Article article);
    
    Task<bool> ArticleExistsAsync(int articleId);
    
    Task<bool> ArticleExistsByIdIgnoreArchived(int articleId);

    Task<Article?> GetArticleByIdNoTracking(int articleId);
    
    Task<PaginatedResult<ArticleResponseDto>> GetPaginatedHomeArticlesByTagIdsAsync(int userId, List<int> tagIds,
        int pageNumber, int pageSize);
    Task<Article?> QuerySingleArticleByIdAsync(int articleId);
    
    Task<PaginatedResult<ArticleResponseDto>> GetPaginatedDiscoverArticlesAsync(int userId, int pageNumber,
        int pageSize);

    Task<PaginatedResult<ArticleResponseDto>> GetPaginatedArticlesBySearchQueryAsync(string searchQuery, int userId,
        int pageNumber, int pageSize);

    Task<PaginatedResult<ArticleResponseDto>> GetPaginatedArticlesByClubIdAsync(int clubId, int pageNumber,
        int pageSize, int userId);
    
    Task<PaginatedResult<ArticleResponseDto>> GetPaginatedArticlesByClubIdForModeratorAsync(int clubId, int pageNumber,
        int pageSize, int userId);
    
    Task<PaginatedResult<ArticleResponseDto>> GetPaginatedArticlesByUserId(int authorId, int pageNumber, int pageSize);
    
    Task<PaginatedResult<ArticleResponseDto>> GetPaginatedUpVotedArticles(int userId, int pageNumber, int pageSize);
    
    Task<PaginatedResult<ArticleResponseDto>> GetPaginatedBookmarkedArticles(int userId, int pageNumber, int pageSize);
    
    Task<PaginatedResult<ArticleResponseDto>> GetPaginatedReadArticles(int userId, int pageNumber, int pageSize);
    
    Task<PaginatedResult<ArticleResponseDto>> GetPaginatedArticlesByTagId(int userId, int tagId, int pageNumber, int pageSize);
    
    Task<bool> IsAuthor(int userId, int articleId);
    Task<ArticleResponseForEditDto> GetArticleForEditByIdAsync(int articleId);
    
    Task<List<ArticleSuggestionDto>> GetArticleSuggestions(string searchQuery);
}