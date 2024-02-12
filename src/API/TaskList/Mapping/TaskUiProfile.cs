using AutoMapper;
using TaskList.Domain.EntitiesDto;
using TaskList.Models.Task;
using TaskList.ResponseModels.Task;
using TaskList.ResponseModels.TaskList;

namespace TaskList.Mapping
{
    internal sealed class TaskUiProfile : Profile
    {
        public TaskUiProfile()
        {
            CreateMap<TaskFilterModel, TaskFilterDto>();
            CreateMap<TaskDto, TaskResponseShort>();
            CreateMap<TaskCreateModel, TaskDto>()
                .ForMember(x => x.Id, map => map.Ignore())
                .ForMember(x => x.TaskList, map => map.Ignore())
                .ForMember(x => x.AddDate, map => map.Ignore());

            CreateMap<TaskUpdateModel, TaskDto>()
                .ForMember(x => x.TaskList, map => map.Ignore())
                .ForMember(x => x.AddDate, map => map.Ignore())
                .ForMember(x => x.TaskListId, map => map.Ignore());

            CreateMap<TaskMoveModel, TaskMoveDto>();
        }
    }
}
