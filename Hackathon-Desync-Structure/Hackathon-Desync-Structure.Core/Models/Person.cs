using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Hackathon_Desync_Structure.Core.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }

        public int? ProgramId { get; set; }
        public Program? Program { get; set; }

        public List<Subject> Subjects { get; set; } = new();

        public List<PersonTask> PersonTasks { get; set; } = new();
    }
}
