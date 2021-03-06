using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using AspnetCoreStudy.DataContext;
using System.Linq;


namespace AspnetCoreStudy.Models
{
    public class Note
    {
        /// <summary>
        /// 게시물 번호
        /// </summary>
        [Key]  // PK 설정
        public int NoteNo { get; set; }

        /// <summary>
        /// 게시물 제목
        /// </summary>
        [Required(ErrorMessage = "제목을 입력하세요.")] //  Not Null 설정
        public string NoteTitle { get; set; }

        /// <summary>
        /// 게시물 내용
        /// </summary>
        [Required(ErrorMessage = "내용을 입력하세요.")] //  Not Null 설정
        public string NoteContents { get; set; }

        public string NoteTimeStamp { get; set; }

        public int ReadNum { get; set; }


        public Files getFiles() {
            var db = new AspnetNoteDbContext();
            var a = new Files();
            foreach (Files f in db.Files)
            {
                if (f.NoteNo == this.NoteNo && f.BoardNo==0) // board num 0
                {
                    return f;
                }
            }
            return null;
        }


        /// <summary>
        /// 작성자 번호
        /// </summary>
        public int UserNo { get; set; }


        [ForeignKey("UserNo")]
        public virtual User User { get; set; }

    }
}
