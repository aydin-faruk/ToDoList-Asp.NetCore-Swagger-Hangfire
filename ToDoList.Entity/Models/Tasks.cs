using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoList.Entity.Models
{
    public partial class Tasks : BaseEntity
    {
        [Column("Name")]
        [StringLength(100)]
        [Required]
        public string Name { get; set; }

        [Column("DueDate")]
        [Required]
        public DateTime DueDate { get; set; }

        [Column("Status")]
        [Required]
        public bool Status { get; set; }

        [Column("CreativeId")]
        [Required]
        public int CreativeId { get; set; }
    }
}
