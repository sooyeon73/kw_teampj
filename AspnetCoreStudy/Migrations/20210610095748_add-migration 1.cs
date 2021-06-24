using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AspnetCoreStudy.Migrations
{
    public partial class addmigration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    FileNo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BoardNo = table.Column<int>(nullable: false),
                    DataFiles = table.Column<byte[]>(nullable: true),
                    FileType = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    NoteNo = table.Column<int>(nullable: false),
                    UserNo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.FileNo);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserNo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IfStudent = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: false),
                    UserPassword = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserNo);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    NoteNo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NoteContents = table.Column<string>(nullable: false),
                    NoteTimeStamp = table.Column<string>(nullable: true),
                    NoteTitle = table.Column<string>(nullable: false),
                    ReadNum = table.Column<int>(nullable: false),
                    UserNo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.NoteNo);
                    table.ForeignKey(
                        name: "FK_Notes_Users_UserNo",
                        column: x => x.UserNo,
                        principalTable: "Users",
                        principalColumn: "UserNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notes1",
                columns: table => new
                {
                    NoteNo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Comment1 = table.Column<string>(nullable: true),
                    NoteContents = table.Column<string>(nullable: false),
                    NoteTimeStamp = table.Column<string>(nullable: true),
                    NoteTitle = table.Column<string>(nullable: false),
                    ReadNum = table.Column<int>(nullable: false),
                    UserNo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes1", x => x.NoteNo);
                    table.ForeignKey(
                        name: "FK_Notes1_Users_UserNo",
                        column: x => x.UserNo,
                        principalTable: "Users",
                        principalColumn: "UserNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notes2",
                columns: table => new
                {
                    NoteNo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NoteAnswer = table.Column<string>(nullable: true),
                    NoteAnswerTimeStamp = table.Column<string>(nullable: true),
                    NoteContents = table.Column<string>(nullable: false),
                    NoteTimeStamp = table.Column<string>(nullable: true),
                    NoteTitle = table.Column<string>(nullable: false),
                    ReadNum = table.Column<int>(nullable: false),
                    UserNo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes2", x => x.NoteNo);
                    table.ForeignKey(
                        name: "FK_Notes2_Users_UserNo",
                        column: x => x.UserNo,
                        principalTable: "Users",
                        principalColumn: "UserNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notes3",
                columns: table => new
                {
                    NoteNo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AssignmentContents = table.Column<string>(nullable: true),
                    DeadLine = table.Column<string>(nullable: true),
                    NoteContents = table.Column<string>(nullable: false),
                    NoteTimeStamp = table.Column<string>(nullable: true),
                    NoteTitle = table.Column<string>(nullable: false),
                    ReadNum = table.Column<int>(nullable: false),
                    UserNo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes3", x => x.NoteNo);
                    table.ForeignKey(
                        name: "FK_Notes3_Users_UserNo",
                        column: x => x.UserNo,
                        principalTable: "Users",
                        principalColumn: "UserNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CommentContents = table.Column<string>(nullable: true),
                    Note1NoteNo = table.Column<int>(nullable: true),
                    NoteID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentID);
                    table.ForeignKey(
                        name: "FK_Comments_Notes1_Note1NoteNo",
                        column: x => x.Note1NoteNo,
                        principalTable: "Notes1",
                        principalColumn: "NoteNo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_Note1NoteNo",
                table: "Comments",
                column: "Note1NoteNo");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_UserNo",
                table: "Notes",
                column: "UserNo");

            migrationBuilder.CreateIndex(
                name: "IX_Notes1_UserNo",
                table: "Notes1",
                column: "UserNo");

            migrationBuilder.CreateIndex(
                name: "IX_Notes2_UserNo",
                table: "Notes2",
                column: "UserNo");

            migrationBuilder.CreateIndex(
                name: "IX_Notes3_UserNo",
                table: "Notes3",
                column: "UserNo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "Notes2");

            migrationBuilder.DropTable(
                name: "Notes3");

            migrationBuilder.DropTable(
                name: "Notes1");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
