using System;
using System.Collections.Generic;
using System.Text;

namespace Hackathon_Desync_Structure.Core.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }

        public int SubjectId { get; set; }
        public Subject? Subject { get; set; }

        public int TeacherId { get; set; }
        public Person? Teacher { get; set; }

        public List<PersonTask> PersonTasks { get; set; } = new();
    }

}
