using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMY.CompilationSystem.Model.Models
{
    public class ProjectClass
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string ClassName { get; set; }

        [Required]
        public DateTime CreateTime { get; set; }
    }
}
