namespace Hackathon_Desync_Structure.Core.Models
{
    public class Program
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Subject> Subjects { get; set; } = new();
        public List<Person> Students { get; set; } = new();

    }
}