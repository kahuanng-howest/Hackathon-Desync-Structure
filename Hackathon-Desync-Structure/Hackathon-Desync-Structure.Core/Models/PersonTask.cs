using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hackathon_Desync_Structure.Core.Models
{
    public class PersonTask
    {
        public int PersonId { get; set; }
        public Person? Person { get; set; }
        public int TaskId { get; set; }
        public TaskItem? Task { get; set; }
        public bool Completed { get; set; }
    }
}
