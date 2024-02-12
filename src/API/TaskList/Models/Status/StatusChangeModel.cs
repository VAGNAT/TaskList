using System.ComponentModel.DataAnnotations;
using TaskList.ResponseModels.Status;

namespace TaskList.Models.Status
{
    public class StatusChangeModel
    {
        [Required]
        public Guid TaskId { get; set; }

        [Required]
        [EnumDataType(typeof(StatusTaskResponse))]
        public StatusTaskResponse Status { get; set; }
    }
}
