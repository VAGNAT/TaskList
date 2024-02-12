namespace TaskList.ResponseModels.Task
{
    internal record TaskResponseShort(Guid Id, string Name, string Description, DateTime AddDate, Guid TaskListId);
}
