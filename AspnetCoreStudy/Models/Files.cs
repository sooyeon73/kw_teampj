using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using AspnetCoreStudy.DataContext;

namespace AspnetCoreStudy.Models
{
    public class Files
    {
        /// <summary>
        /// 게시물 번호
        /// </summary>
        [Key]  // PK 설정
        public int FileNo { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public string FileType { get; set; }

        public byte[] DataFiles { get; set; }
 
        public int BoardNo { get; set; }

        public int NoteNo { get; set; }

        public int UserNo { get; set; }



    }
}
