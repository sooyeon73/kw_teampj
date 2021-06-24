using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetCoreStudy.DataContext;
using AspnetCoreStudy.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


using System.IO;




// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspnetCoreStudy.Controllers
{


    public class Note3Controller : Controller
    {
        public int num;

        /// <summary>
        /// 게시판 리스트
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                //로그인이 안된 상태
                return RedirectToAction("Login", "Account");
            }

            using (var db = new AspnetNoteDbContext())
            {
                //ToList로 선언해서 리스트로 

                var list = db.Notes3.ToList();
                var list1 = db.Users.ToList();


                string temp = HttpContext.Session.GetInt32("USER_LOGIN_KEY").ToString();
                var currentUser = db.Users.FirstOrDefault(u => u.UserNo.ToString().Equals(temp));

                ViewBag.users = db.Users.ToList();
                ViewBag.notes = db.Notes3.ToList();
                ViewBag.currentUser = currentUser;

                return View();
            }
        }



        public IActionResult AddAnswer(int noteNo)
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                //로그인이 안된 상태
                return RedirectToAction("Login", "Account");
            }

            using (var db = new AspnetNoteDbContext())
            {
                var note = db.Notes3.FirstOrDefault(n => n.NoteNo.Equals(noteNo));
                note.ReadNum++;

   

                db.SaveChanges();
                return View(note);
            }
        }

        
        /*
        [HttpPost]
        public IActionResult AddAnswer(Note3 model)
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                //로그인이 안된 상태
                return RedirectToAction("Login", "Account");
            }




            //if (ModelState.IsValid)
            //{
            using (var db = new AspnetNoteDbContext())
            {
                var note = db.Notes3.FirstOrDefault(n => n.NoteNo.Equals(model.NoteNo));

                DateTime date = DateTime.Now;
                // note.NoteAnswerTimeStamp = date.ToString("yyyy-MM-dd");
                DateTime dateD = Convert.ToDateTime(model.DeadLine);

            

                note.AssignmentContents = model.AssignmentContents;
                db.SaveChanges();



            }
            return Read(model.NoteNo);
        }
        */

        public IActionResult Read(int noteNo)
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                //로그인이 안된 상태
                return RedirectToAction("Login", "Account");
            }

            using (var db = new AspnetNoteDbContext())
            {
                var note = db.Notes3.FirstOrDefault(n => n.NoteNo.Equals(noteNo));
                note.ReadNum++;
                db.SaveChanges();
                return View(note);
            }
        }

        /// <summary>
        /// 게시물 추가
        /// </summary>
        /// <returns></returns>
        public IActionResult Add()
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                //로그인이 안된 상태
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        
        [HttpPost]
        public IActionResult Add(Note3 model)
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                //로그인이 안된 상태
                return RedirectToAction("Login", "Account");
            }
            model.UserNo = int.Parse(HttpContext.Session.GetInt32("USER_LOGIN_KEY").ToString());
            if (ModelState.IsValid)
            {
                using (var db = new AspnetNoteDbContext())
                {
                    // model.NoteTimeStamp = 
                    DateTime date = DateTime.Now;
                    model.NoteTimeStamp = date.ToString("yyyy-MM-dd");


                    db.Notes3.Add(model);

                    if (db.SaveChanges() > 0)
                    {
                        //동일한 위치의 Index로 이동
                        return Redirect("Index");
                        //return RedirectToAction("Index","Home");
                    }
                }
                ModelState.AddModelError(string.Empty, "게시물을 저장할 수 없습니다.");
            }
            return View(model);
        }
        

        [HttpPost]
        public IActionResult Add_(IFormFile files, Note3 model)
        {

            using (var db = new AspnetNoteDbContext())
            {


                if (files != null)
                {
                    //Getting FileName
                    var fileName = Path.GetFileName(files.FileName);
                    //Getting file 확장자
                    var fileExtension = Path.GetExtension(fileName);
                    // concatenating  FileName + FileExtension
                    var newFileName = String.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);
                    int n = model.NoteNo;

                    Files ob = null;
                    foreach (Files f in db.Files)
                    {
                        if (f.NoteNo == n && f.BoardNo == 3) // board num 1
                        {
                            ob = f;
                        }
                    }
                    if (ob == null)
                    {
                        var objfiles = new Files()
                        {
                            BoardNo = 3,
                            NoteNo = n,
                            Name = newFileName,
                            FileType = fileExtension,
                            UserNo = int.Parse(HttpContext.Session.GetInt32("USER_LOGIN_KEY").ToString())

                        };
                        using (var target = new MemoryStream())
                        {
                            files.CopyTo(target);
                            objfiles.DataFiles = target.ToArray();
                        }
                        db.Files.Add(objfiles);

                    }
                    else
                    {
                        ob.Name = newFileName;
                        ob.FileType = fileExtension;
                        using (var target = new MemoryStream())
                        {
                            files.CopyTo(target);
                            ob.DataFiles = target.ToArray();
                        }
                    }
                    db.SaveChanges();
                    return Redirect("Index");

                }
                ModelState.AddModelError(string.Empty, "파일을 첨부해 주세요.");

                return AddAnswer(model.NoteNo);
            }
        }

    }
}



