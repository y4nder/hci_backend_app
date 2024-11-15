﻿using domain.interfaces;
using infrastructure.repositories;
using Microsoft.Extensions.DependencyInjection;

namespace application.configurations;

public static class RepositoryConfigurations
{
    public static IServiceCollection AddRepositoryConfigurations(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserAdditionalInfoRepository, UserAdditionalInfoRepository>();
        
        services.AddScoped<ITagRepository, TagRepository>();
        services.AddScoped<IUserTagFollowRepository, UserTagFollowRepository>();

        services.AddScoped<IClubCategoryRepository, ClubCategoryRepository>();
        services.AddScoped<IClubRepository, ClubRepository>();

        services.AddScoped<IClubAdditionalInfoRepository, ClubAdditionalInfoRepository>();

        services.AddScoped<IClubUserRepository, ClubUserRepository>();

        services.AddScoped<IUserFollowRepository, UserFollowRepository>();
        
        return services;
    }
}