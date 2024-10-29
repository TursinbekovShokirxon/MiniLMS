using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MiniLMS.Domain.Entities
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id  { get; set; }
        public string Title  { get; set; }
        public string Description { get; set; }
        public IList<Student>? Students { get; set; }
        public int? TeacherId { get; set; }
        public Teacher? Teacher { get; set; }
    }
}
