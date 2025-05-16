namespace Fitness.Models
{
    public class Teacher : BaseEntity
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public IEnumerable<Course> Courses { get; set; }
    }
}
