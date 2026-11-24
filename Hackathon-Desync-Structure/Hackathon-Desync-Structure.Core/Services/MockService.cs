using Hackathon_Desync_Structure.Core.Models;
using Hackathon_Desync_Structure.Core.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using System.Text.Json;
using System.Text.Json.Serialization;

public class MockService : IMockService
{
    private List<PersonTask> personTasks = new();
    private List<TaskItem> tasks = new();
    private List<Subject> subjects = new();
    private List<Person> persons = new();
    public List<PersonTask> PersonTasks { get { return personTasks; } }
    private List<TaskItem> teachersTasks = new();
    public List<TaskItem> TeachersTasks { get { return teachersTasks; } }

    private class Root
    {
        public List<Subject> Subjects { get; set; } = new();
        public List<Person> Persons { get; set; } = new();
        public List<TaskItem> Tasks { get; set; } = new();
        public List<PersonTask> PersonTasks { get; set; } = new();
    }

    public MockService(IWebHostEnvironment env)
    {
        string path = Path.Combine(env.ContentRootPath, "..", "Hackathon-Desync-Structure.Core", "Data", "structure.json");
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        };

        var data = JsonSerializer.Deserialize<Root>(File.ReadAllText(path), options) ?? new Root();
        subjects = data.Subjects;
        persons = data.Persons;
        tasks = data.Tasks;
        personTasks = data.PersonTasks;

        foreach (var t in tasks)
            t.Subject = subjects.FirstOrDefault(s => s.Id == t.SubjectId);

        foreach (var pt in personTasks)
        {
            pt.Person = persons.FirstOrDefault(p => p.Id == pt.PersonId);
            pt.Task = tasks.FirstOrDefault(t => t.Id == pt.TaskId);
        }
    }

    public async Task<IEnumerable<PersonTask>> GetTasksFromPerson(int id)
    {
        return PersonTasks.Where(t => t.PersonId == id);
    }

    public Task<IEnumerable<Subject>> GetSubjectsFromTeacher(int teacherId)
    {
        var teacher = persons.First(p => p.Id == teacherId);
        return Task.FromResult(subjects.Where(s => teacher.SubjectIds.Contains(s.Id)));
    }

    public Task<TaskItem> AddTaskAsync(TaskItem task)
    {
        var teacher = persons.First(p => p.Id == task.TeacherId);
        if (!teacher.SubjectIds.Contains(task.SubjectId))
            throw new InvalidOperationException("Deze leerkracht geeft dit vak niet.");

        var subject = subjects.First(s => s.Id == task.SubjectId);

        task.Id = tasks.Count == 0 ? 1 : tasks.Max(t => t.Id) + 1;
        task.Subject = subject;
        task.Teacher = teacher;
        tasks.Add(task);

        // één rij per student van de opleiding waar het vak bijhoort
        var students = persons.Where(p => p.Role == "Student" && p.ProgramId == subject.ProgramId);
        foreach (var student in students)
        {
            var pt = new PersonTask { PersonId = student.Id, Person = student, TaskId = task.Id, Task = task };
            personTasks.Add(pt);
            task.PersonTasks.Add(pt);
        }

        return Task.FromResult(task);
    }

    public async Task<Person?> GetPersonByName(string name)
    {
        return persons.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    public async Task<IEnumerable<TaskItem>> GetTasksFromTeacher(int id)
    {
        teachersTasks = tasks.Where(t => t.TeacherId == id).ToList();
        return TeachersTasks;
    }
}