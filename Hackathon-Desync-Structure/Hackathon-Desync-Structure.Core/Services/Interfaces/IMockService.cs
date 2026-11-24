using Hackathon_Desync_Structure.Core.Models;
namespace Hackathon_Desync_Structure.Core.Services.Interfaces
{
    public interface IMockService
    {
        Task<IEnumerable<PersonTask>> GetTasksFromPerson(int id);
        Task<IEnumerable<Subject>> GetSubjectsFromTeacher(int teacherId);
        Task<TaskItem> AddTaskAsync(TaskItem task);
        Task<Person?> GetPersonByName(string name);
    }
}
