using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMY.CompilationSystem.Model.Models
{
    public class SVNFolders
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FolderName { get; set; }

        [Required]
        [StringLength(100)]
        public string FolderPath { get; set; }

        [Required]
        public double Sort { get; set; }

        [Required]
        public DateTime CreateTime { get; set; }


    }
}
