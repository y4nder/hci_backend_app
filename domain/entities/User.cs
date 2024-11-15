﻿namespace domain.entities;

public partial class User
{
    public int UserId { get; set; }

    public string? Username { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();

    public virtual ICollection<ClubUser> ClubUsers { get; set; } = new List<ClubUser>();

    public virtual ICollection<Club> Clubs { get; set; } = new List<Club>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<SearchHistory> SearchHistories { get; set; } = new List<SearchHistory>();

    public virtual UserAdditionalInfo? UserAdditionalInfo { get; set; }

    public virtual ICollection<UserArticleBookmark> UserArticleBookmarks { get; set; } = new List<UserArticleBookmark>();

    public virtual ICollection<UserArticleRead> UserArticleReads { get; set; } = new List<UserArticleRead>();

    public virtual ICollection<UserArticleVote> UserArticleVotes { get; set; } = new List<UserArticleVote>();

    public virtual ICollection<UserCommentVote> UserCommentVotes { get; set; } = new List<UserCommentVote>();

    public virtual ICollection<UserFollow> UserFollowFollowers { get; set; } = new List<UserFollow>();

    public virtual ICollection<UserFollow> UserFollowFollowings { get; set; } = new List<UserFollow>();

    public virtual ICollection<UserTagFollow> UserTagFollows { get; set; } = new List<UserTagFollow>();

    public static User Create(string username, string email, string password)
    {
        return new User
        {
            Username = username,
            Email = email,
            Password = password
        };
    }
}
