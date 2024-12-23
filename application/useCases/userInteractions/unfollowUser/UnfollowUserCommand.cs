﻿using MediatR;

namespace application.useCases.userInteractions.unfollowUser;

public class UnfollowUserCommand : IRequest<UnfollowUserResponse>
{
    public int FollowingId { get; set; }
}

public class UnfollowUserResponse
{    
    public string Message { get; set; } = null!;
}