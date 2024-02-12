using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskList.Domain.Abstractions;
using TaskList.Domain.Entities;

namespace TaskList.Infrastructure.Persistance
{
    internal static class SeedData
    {
        internal static IEnumerable<TaskListEntity> TaskLists{ get; }
        internal static IEnumerable<TaskEntity> Tasks { get; }
        internal static IEnumerable<CommentEntity> Comments { get; }
        internal static IEnumerable<StatusTaskHistory> StatusTasks { get; }

        static SeedData()
        {
            var taskList1 = new TaskListEntity
            {
                Id = Guid.NewGuid(),
                Name = "Первый список задач",
                Description = "Описание первого списка задач",
                Owner = "user@example.com"
            };
            var taskList2 = new TaskListEntity
            {
                Id = Guid.NewGuid(),
                Name = "Второй список задач",
                Description = "Описание второго списка задач",
                Owner = "user@example.com"
            };
            var taskList3 = new TaskListEntity
            {
                Id = Guid.NewGuid(),
                Name = "Третий список задач",
                Description = "Описание третьего списка задач",
                Owner = "user@example.com"
            };
            TaskLists = new List<TaskListEntity> { taskList1, taskList2, taskList3 };

            var random = new Random();
            var comments = new List<CommentEntity>();
            var statuses = new List<StatusTaskHistory>();
            var taskEntities = new List<TaskEntity>();
            var startDate = DateTime.SpecifyKind(new DateTime(2024, 02, 01), DateTimeKind.Utc);
            var endDate = DateTime.SpecifyKind(new DateTime(2024, 02, 10), DateTimeKind.Utc);

            foreach (var taskList in TaskLists)
            {
                for (int i = 0; i < 10; i++)
                {
                    var task = new TaskEntity
                    {
                        Id = Guid.NewGuid(),
                        Name = $"Задача {i + 1} листа {taskList.Name}",
                        Description = $"Описание задачи {i + 1} листа {taskList.Name}",
                        AddDate = startDate.AddDays(random.Next((endDate - startDate).Days)),
                        TaskListId = taskList.Id
                    };

                    taskEntities.Add(task);

                    for (int j = 0; j < random.Next(1,3); j++)
                    {
                        var comment = new CommentEntity
                        {
                            Id = Guid.NewGuid(),
                            Content = $"Комментарий {j + 1} к задаче {task.Name}",
                            TaskID = task.Id
                        };
                        comments.Add(comment);
                    }

                    var status = new StatusTaskHistory
                    {
                        Id = Guid.NewGuid(),
                        Status = StatusTask.Waiting,
                        AddDate = startDate.AddDays(random.Next((endDate - startDate).Days)),
                        TaskId = task.Id
                    };
                    statuses.Add(status);
                }
            }

            Tasks = taskEntities;
            Comments = comments;
            StatusTasks = statuses;
        }
    }
}
