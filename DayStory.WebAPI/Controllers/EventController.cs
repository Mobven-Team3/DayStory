﻿using DayStory.Application.Interfaces;
using DayStory.Application.Pagination;
using DayStory.Common.DTOs;
using DayStory.WebAPI.Helpers;
using DayStory.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DayStory.WebAPI.Controllers;

[Route("api/[controller]s")]
[ApiController]
public class EventController : Controller
{
    private readonly IEventService _eventService;
    public EventController(IEventService service)
    {
        _eventService = service;
    }

    [Authorize(Roles = "Admin, User")]
    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateEventContract request)
    {
        request.UserId = int.Parse(JwtHelper.GetUserIdFromToken(HttpContext));
        await _eventService.AddEventAsync(request);
        return Ok(new ResponseModel("Successfully Created"));
    }

    [Authorize(Roles = "Admin, User")]
    [HttpPut]
    public async Task<IActionResult> UpdateAsync(UpdateEventContract request)
    {
        await _eventService.UpdateEventAsync(request);
        return Ok(new ResponseModel("Successfully Updated"));
    }

    [Authorize(Roles = "Admin, User")]
    [HttpGet("all")]
    public async Task<IActionResult> GetAllAsync()
    {
        var userId = int.Parse(JwtHelper.GetUserIdFromToken(HttpContext));
        var responseModel = await _eventService.GetEventsAsync(userId);
        return Ok(new ResponseModel<List<GetEventContract>>(responseModel));
    }

    [Authorize(Roles = "Admin, User")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var responseModel = await _eventService.GetEventByIdAsync(id);
        return Ok(new ResponseModel<GetEventContract>(responseModel));
    }

    [Authorize(Roles = "Admin, User")]
    [HttpGet("day")]
    public async Task<IActionResult> GetEventsByDayAsync(GetEventsByDayContract request)
    {
        request.UserId = int.Parse(JwtHelper.GetUserIdFromToken(HttpContext));
        var responseModel = await _eventService.GetEventsByDayAsync(request);
        return Ok(new ResponseModel<List<GetEventContract>>(responseModel));
    }

    [Authorize(Roles = "Admin, User")]
    [HttpGet("month")]
    public async Task<IActionResult> GetEventsByMonthAsync(GetEventsByMonthContract request)
    {
        request.UserId = int.Parse(JwtHelper.GetUserIdFromToken(HttpContext));
        var responseModel = await _eventService.GetEventsByMonthAsync(request);
        return Ok(new ResponseModel<List<GetEventContract>>(responseModel));
    }

    [Authorize(Roles = "Admin, User")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _eventService.RemoveEventByIdAsync(id);
        return Ok(new ResponseModel("Successfully Deleted"));
    }

    //[Authorize(Roles = "Admin, User")]
    //[HttpGet("pages")]
    //public async Task<IActionResult> GetAllPagedAsync([FromQuery] PaginationFilter filter)
    //{
    //    PaginationFilter paginationFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
    //    var responseModel = await _eventService.GetPagedDataAsync(paginationFilter.PageNumber, paginationFilter.PageSize);
    //    return Ok(responseModel);
    //}
}
