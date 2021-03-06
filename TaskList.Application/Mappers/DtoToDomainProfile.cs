﻿using AutoMapper;
using DataTransferObjects;
using TaskList.Domain;

namespace TaskList.Application.Mappers
{
    public class DtoToDomainProfile : Profile
    {
        public DtoToDomainProfile()
        {
            CreateMap<TaskDto, Task>();
        }
    }
}