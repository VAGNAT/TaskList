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
    public sealed class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<TaskEntity, TaskDto>();
            CreateMap<TaskDto, TaskEntity>()
                .ForMember(dest => dest.TaskListId,
                    opt => opt.Condition(src => src.TaskListId != Guid.Empty))
                .ForMember(dest => dest.AddDate,
                    opt => opt.Condition(src => src.AddDate != DateTime.MinValue))
                .ForMember(dest => dest.Name,
                    opt => opt.Condition(src => !string.IsNullOrEmpty(src.Name)))
                .ForMember(dest => dest.Description,
                    opt => opt.Condition(src => !string.IsNullOrEmpty(src.Description)));
        }
    }
}
