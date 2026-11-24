using System;
using System.Collections.Generic;
using System.Text;

namespace Hackathon_Desync_Structure.Core.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";

        public int ProgrammeId { get; set; }
        public Program? Programme { get; set; }

        public List<Person> Teachers { get; set; } = new(); // many-to-many
        public List<TaskItem> Tasks { get; set; } = new();

    }
}
