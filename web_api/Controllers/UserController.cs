﻿using application.useCases.userInteractions.Queries.SelfMinimalQuery;
using application.useCases.userInteractions.Queries.UserAdditionalInfoQuery;
using application.useCases.userInteractions.updateUserInfo;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using web_api.utils;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace web_api.Controllers;

public class UserController : Controller
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("/me")]
    public async Task<IActionResult> GetMe([FromQuery] SelfMinimalQuery query)
    {
        try
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return ErrorFactory.CreateErrorResponse(ex);
        }
    }
    
    [HttpGet("/me/profile")]
    public async Task<IActionResult> GetUserAdditionalInfo([FromQuery] SelfUserAdditionalInfoQuery query)
    {
        try
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return ErrorFactory.CreateErrorResponse(ex);
        }
    }

    [Authorize]
    [HttpPatch("/me/profile/update")]
    public async Task<IActionResult> UpdateUserAdditinalInfo([FromBody] UpdateUserCommand command)
    {
        try
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return ErrorFactory.CreateErrorResponse(ex);
        }
    }
}