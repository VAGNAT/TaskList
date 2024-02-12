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
    public sealed class StatusProfile : Profile
    {
        public StatusProfile()
        {
            CreateMap<StatusTaskHistory, StatusTaskHistoryDto>();
            CreateMap<StatusTaskHistoryDto, StatusTaskHistory>();
            
        }
    }
}
