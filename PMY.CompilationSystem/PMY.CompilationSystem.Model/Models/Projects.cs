using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMY.CompilationSystem.Model.Models
{
    public class Projects
    {
        public int Id { get; set; }

        [Required]
        public double Sort { get; set; }

        [Required]
        [StringLength(50)]
        public string ProjectClassName { get; set; }

        [Required]
        [StringLength(50)]
        public string ProjectName { get; set; }

        [Required]
        [StringLength(100)]
        public string ProjectPath { get; set; }

        [Required]
        [StringLength(100)]
        public string ProjectParentPath { get; set; }

        [StringLength(100)]
        public string ProjectStartItem { get; set; }

        public DateTime? LastCompileTime { get; set; }

        public DateTime? LatestModifiTime { get; set; }

        [Required]
        [StringLength(100)]
        public string PublishPath { get; set; }

        [Required]
        public DateTime CreateTime { get; set; }

    }
}
