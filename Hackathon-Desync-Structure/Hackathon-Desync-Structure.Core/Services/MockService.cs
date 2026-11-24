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
    public List<PersonTask> PersonTasks { get; }
    public MockService(IWebHostEnvironment env)
    {
        personTasks = new List<PersonTask>();
        string path = Path.Combine(env.ContentRootPath, "..", "Hackathon-Desync-Structure.Core", "Data", "structure.json"); JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        };
        personTasks = JsonSerializer.Deserialize<List<PersonTask>>(File.ReadAllText(path), options) ?? new List<PersonTask>();
    }
    public async Task<IEnumerable<PersonTask>> GetTasksFromPerson(int id)
    {
        return PersonTasks.Where(t => t.PersonId == id);
    }
}