using AutoMapper;
using TaskList.Domain.EntitiesDto;
using TaskList.Models.Comment;
using TaskList.Models.Task;
using TaskList.ResponseModels.Comment;
using TaskList.ResponseModels.Task;
using TaskList.ResponseModels.TaskList;

namespace TaskList.Mapping
{
    internal sealed class CommentUiProfile : Profile
    {
        public CommentUiProfile()
        {
            CreateMap<CommentDto, CommentResponseShort>();

            CreateMap<CommentCreateModel, CommentDto>()
                .ForMember(x => x.Id, map => map.Ignore())
                .ForMember(x => x.Task, map => map.Ignore());

            CreateMap<CommentUpdateModel, CommentDto>()
                .ForMember(x => x.TaskId, map => map.Ignore())
                .ForMember(x => x.Task, map => map.Ignore());
        }
    }
}
