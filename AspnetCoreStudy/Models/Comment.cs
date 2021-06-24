using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace AspnetCoreStudy.Models
{
    public class Comment
    {
        [Key]
        public int CommentID { get; set; }
        public int NoteID { get; set; }
        public string CommentContents { get; set; }
    }
}
