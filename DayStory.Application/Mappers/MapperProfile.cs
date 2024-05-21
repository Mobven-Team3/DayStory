﻿using AutoMapper;
using DayStory.Application.DTOs;
using DayStory.Domain.Entities;
using DayStory.Domain.Pagination;

namespace DayStory.Application.Mappers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<User, UserContract>();
        CreateMap<User, UserContract>().ReverseMap();

        CreateMap<Event, EventContract>();
        CreateMap<Event, EventContract>().ReverseMap();

        CreateMap<DaySummary, DaySummaryContract>();
        CreateMap<DaySummary, DaySummaryContract>().ReverseMap();

        CreateMap<ArtStyle, ArtStyleContract>();
        CreateMap<ArtStyle, ArtStyleContract>().ReverseMap();

        CreateMap<PagedResponse<Event>, PagedResponse<EventContract>>();
        CreateMap<PagedResponse<Event>, PagedResponse<EventContract>>().ReverseMap();
        CreateMap<PagedResponse<DaySummary>, PagedResponse<DaySummaryContract>>();
        CreateMap<PagedResponse<DaySummary>, PagedResponse<DaySummaryContract>>().ReverseMap();
    }
}
