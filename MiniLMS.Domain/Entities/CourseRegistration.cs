using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniLMS.Domain.Entities
{
        public class CourseRegistration
        {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Id { get; set; }
        public int StudentId { get; set; } 
        public virtual Student Student { get; set; }
        public int CourseId { get; set; }
        public virtual Course Course { get; set; } 
    }
}
