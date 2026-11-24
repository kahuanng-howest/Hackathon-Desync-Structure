using Hackathon_Desync_Structure.Core.Models;
namespace Hackathon_Desync_Structure.Core.Services.Interfaces
{
    public interface IMockService
    {
        Task<IEnumerable<TaskItem>> GetTasks();
    }
}
