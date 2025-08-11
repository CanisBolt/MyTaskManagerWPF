using System.ComponentModel;
using System.Text.Json.Serialization;

namespace MyTaskManagerWPF.Model
{
    public class UserTask
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime Created { get; set; }
        public DateTime Completed { get; set; }
        public Priority TaskPriority { get; set; }
        public bool IsCompleted { get; set; }

        [JsonConstructor]
        public UserTask(string name, string description, DateTime created, Priority taskPriority)
        {
            Name = name;
            Description = description;
            Created = created;
            TaskPriority = taskPriority;
            IsCompleted = false;
        }

        public UserTask(string name, string description, DateTime created, DateTime completed)
        {
            Name = name;
            Description = description;
            Created = created;
            Completed = completed;
            IsCompleted = true;
        }

        public static Priority GetTaskPriority(string priority)
        {
            switch (priority)
            {
                case "Низкая":
                    return Priority.Низкая;
                case "Средняя":
                    return Priority.Средняя;
                case "Высокая":
                    return Priority.Высокая;
                case "Срочная":
                    return Priority.Срочная;
                default:
                    return Priority.Средняя;
            }
        }

        public enum Priority
        {
            Низкая, Средняя, Высокая, Срочная
        }
    }
}
