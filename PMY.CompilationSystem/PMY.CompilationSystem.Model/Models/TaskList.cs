using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMY.CompilationSystem.Model.Models
{
    public class TaskList
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string CMDCommand { get; set; }

        [Required]
        public TaskType TaskType { get; set; }

        [Required]
        public TaskStatus TaskStatus { get; set; }

        [StringLength(100)]
        public string ActionPath { get; set; }

        [Required]
        public DateTime CreateTime { get; set; }

        public DateTime? FinishTime { get; set; }

        [Required]
        [StringLength(50)]
        public string Creator { get; set; }


    }
}
