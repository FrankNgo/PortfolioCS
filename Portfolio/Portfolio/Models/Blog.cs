using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio.Models
{
    [Table("Blogs")]
    public class Blog
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Copy { get; set; }
        public virtual string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual List<Comment> Comments { get; set; }
    }
}