using AutoMapper;
using TaskList.Domain.Abstractions;
using TaskList.Domain.EntitiesDto;
using TaskList.Models.Comment;
using TaskList.Models.Status;
using TaskList.Models.Task;
using TaskList.ResponseModels.Comment;
using TaskList.ResponseModels.Status;
using TaskList.ResponseModels.Task;
using TaskList.ResponseModels.TaskList;

namespace TaskList.Mapping
{
    internal sealed class StatusUiProfile : Profile
    {
        public StatusUiProfile()
        {
            CreateMap<StatusTaskHistoryDto, StatusResponseShort>();
            CreateMap<StatusTask, StatusTaskResponse>();
            CreateMap<StatusTaskResponse, StatusTask>();

            CreateMap<StatusChangeModel, StatusTaskHistoryDto>()
                .ForMember(x => x.Id, map => map.Ignore())
                .ForMember(x => x.Task, map => map.Ignore())
                .ForMember(x => x.AddDate, opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}
