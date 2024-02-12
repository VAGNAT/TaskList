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
    public sealed class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<CommentEntity, CommentDto>();
            CreateMap<CommentDto, CommentEntity>()
                .ForMember(dest => dest.TaskID,
                    opt => opt.Condition(src => src.TaskId != Guid.Empty))
                .ForMember(dest => dest.Content,
                    opt => opt.Condition(src => !string.IsNullOrEmpty(src.Content)));
        }
    }
}
