using AspnetCoreStudy.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreStudy.DataContext
{
    public class AspnetNoteDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; } // 공지사항
        public DbSet<Note1> Notes1 { get; set; } //자료게시판

        public DbSet<Note2> Notes2 { get; set; } //질문답변게시판

        public DbSet<Note3> Notes3 { get; set; } //과제게시판

        public DbSet<Files> Files { get; set; }


        public DbSet<Comment> Comments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
                  optionsBuilder.UseSqlServer(@"Server=localhost; Database =AspnetNoteDb; User Id =sa;Password =1234;");
        }
    }
}
