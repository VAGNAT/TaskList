using System.ComponentModel.DataAnnotations;

namespace TaskList.Models.Task
{
    public class TaskFilterModel
    {
        //filter       
        public string? Name { get; set; }
        
        public string? Description { get; set; }

        //sort
        public bool SortByAddDate { get; set; }

        //pagination
        [Required]
        public int ItemsPerPage { get; set; }

        [Required]
        public int Page { get; set; }
    }
}
