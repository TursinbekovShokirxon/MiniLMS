using MiniLMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniLMS.Domain.Models.CourseDTO
{
    public class CourseCreateDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
