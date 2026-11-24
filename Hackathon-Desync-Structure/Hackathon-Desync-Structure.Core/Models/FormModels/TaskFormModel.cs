using System;
using System.Collections.Generic;
using System.Text;

namespace Hackathon_Desync_Structure.Core.Models.FormModels
{
    using System.ComponentModel.DataAnnotations;

    namespace Hackathon_Desync_Structure.Core.Models.FormModels
    {
        public class TaskFormModel
        {
            [Required(ErrorMessage = "Titel is verplicht.")]
            [StringLength(100, ErrorMessage = "Titel mag max. 100 tekens bevatten.")]
            public string Title { get; set; } = string.Empty;

            [Range(1, int.MaxValue, ErrorMessage = "Kies een vak.")]
            public int SubjectId { get; set; }

            public DateTime StartDate { get; set; } = DateTime.Today;
            public DateTime DueDate { get; set; } = DateTime.Today.AddDays(7);

            public IEnumerable<ValidationResult> Validate(ValidationContext context)
            {
                if (DueDate < StartDate)
                    yield return new ValidationResult(
                        "Deadline mag niet voor de startdatum liggen.", new[] { nameof(DueDate) });
            }

            public TaskItem ToEntity(int teacherId) => new()
            {
                Title = Title,
                StartDate = StartDate,
                DueDate = DueDate,
                SubjectId = SubjectId,
                TeacherId = teacherId
            };
        }
    }
}
