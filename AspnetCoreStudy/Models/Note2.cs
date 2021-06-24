using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using AspnetCoreStudy.DataContext;

namespace AspnetCoreStudy.Models
{
    public class Note2
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
        /// 게시물 내용 질문
        /// </summary>
        [Required(ErrorMessage = "내용을 입력하세요.")] //  Not Null 설정
        public string NoteContents { get; set; }

        public string NoteTimeStamp { get; set; }

        public int ReadNum { get; set; }

        /// <summary>
        /// 게시물 내용 답변
        /// </summary>
        public string NoteAnswer { get; set; }

        public string NoteAnswerTimeStamp { get; set; }





        /// <summary>
        /// 작성자 번호
        /// </summary>
        public int UserNo { get; set; }


        [ForeignKey("UserNo")]
        public virtual User User { get; set; }

    }
}
