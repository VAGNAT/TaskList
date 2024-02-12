namespace TaskList.ResponseModels.Status
{
    public class StatusResponseShort
    {
        public Guid TaskId { get; set; }

        public required StatusTaskResponse Status { get; set; }

        public DateTime AddDate { get; set; }
    }
}
