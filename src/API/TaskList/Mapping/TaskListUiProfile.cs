using AutoMapper;
using TaskList.Domain.EntitiesDto;
using TaskList.Models.TaskList;
using TaskList.ResponseModels.TaskList;

namespace TaskList.Mapping
{
    internal sealed class TaskListUiProfile : Profile
    {
        public TaskListUiProfile()
        {
            CreateMap<TaskListDto, TaskListResponseShort>();
            CreateMap<TaskListCreateModel, TaskListDto>()
                .ForMember(x => x.Id, map => map.Ignore())
                .ForMember(x => x.Owner, map => map.Ignore())
                .ForMember(x => x.Tasks, map => map.Ignore());

            CreateMap<TaskListUpdateModel, TaskListDto>()                
                .ForMember(x => x.Owner, map => map.Ignore())
                .ForMember(x => x.Tasks, map => map.Ignore());
        }
    }
}
