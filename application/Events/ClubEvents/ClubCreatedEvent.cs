﻿using domain.entities;
using MediatR;

namespace application.Events.ClubEvents;

public class ClubCreatedEvent : INotification
{
    public ClubCreatedEvent(Club createdClub)
    {
        CreatedClub = createdClub;
    }

    public Club CreatedClub { get; set; }
}



