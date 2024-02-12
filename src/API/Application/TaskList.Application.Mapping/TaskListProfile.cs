using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskList.Domain.Entities;
using TaskList.Domain.EntitiesDto;

namespace TaskList.Application.Mapping
{
    public sealed class TaskListProfile : Profile
    {
        public TaskListProfile()
        {
            CreateMap<TaskListEntity, TaskListDto>();
            CreateMap<TaskListDto, TaskListEntity>()
                .ForMember(x => x.Tasks, map => map.Ignore())
                .ForMember(x => x.Owner, map => map.Ignore())
                .ForMember(dest => dest.Name,
                    opt => opt.Condition(src => !string.IsNullOrEmpty(src.Name)))
                .ForMember(dest => dest.Description,
                    opt => opt.Condition(src => !string.IsNullOrEmpty(src.Description)));
        }
    }
}
