namespace TaskList.ResponseModels.TaskList
{
    internal record TaskListResponseShort
        (
        Guid Id,
        string Name,
        string Description,
        string Owner
        );
}
