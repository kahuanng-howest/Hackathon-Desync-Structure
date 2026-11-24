using Hackathon_Desync_Structure.Core.Models;
using Hackathon_Desync_Structure.Core.Services.Interfaces;
namespace Hackathon_Desync_Structure.Core.Services;

using Microsoft.AspNetCore.Hosting;
using System.Text.Json;
using System.Text.Json.Serialization;

public class MockService : IMockService
{
    public IMockService _mockService;
    private List<PersonTask> personTasks;
    public List<PersonTask> PersonTasks { get { return personTasks; } } 

    private class Root
    {
        public List<Subject> Subjects { get; set; } = new();
        public List<Person> Persons { get; set; } = new();
        public List<TaskItem> Tasks { get; set; } = new();
        public List<PersonTask> PersonTasks { get; set; } = new();
    }

    public MockService(IWebHostEnvironment env)
    {
        personTasks = new List<PersonTask>();
        string path = Path.Combine(env.ContentRootPath, "..", "Hackathon-Desync-Structure.Core", "Data", "structure.json");
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        };

        // 3. Root inlezen in plaats van List<PersonTask>
        var data = JsonSerializer.Deserialize<Root>(File.ReadAllText(path), options) ?? new Root();
        personTasks = data.PersonTasks;

        // koppelingen, zodat pt.Task.Title en pt.Person niet null zijn
        foreach (var t in data.Tasks)
            t.Subject = data.Subjects.FirstOrDefault(s => s.Id == t.SubjectId);

        foreach (var pt in personTasks)
        {
            pt.Person = data.Persons.FirstOrDefault(p => p.Id == pt.PersonId);
            pt.Task = data.Tasks.FirstOrDefault(t => t.Id == pt.TaskId);
        }
    }

    public async Task<IEnumerable<PersonTask>> GetTasksFromPerson(int id)
    {
        return PersonTasks.Where(t => t.PersonId == id);
    }
}