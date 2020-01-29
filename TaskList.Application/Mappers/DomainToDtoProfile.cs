using AutoMapper;
using DataTransferObjects;
using TaskList.Domain;

namespace TaskList.Application.Mappers
{
    public class DomainToDtoProfile : Profile
    {
        public DomainToDtoProfile()
        {
            CreateMap<Task, TaskDto>();
        }
    }
}