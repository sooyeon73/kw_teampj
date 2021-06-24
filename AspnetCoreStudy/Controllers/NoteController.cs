using AspnetCoreStudy.DataContext;
using AspnetCoreStudy.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;


namespace AspnetCoreStudy.Controllers
{


    public class NoteController : Controller
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

                var list = db.Notes.ToList();
                var list1 = db.Users.ToList();


                string temp = HttpContext.Session.GetInt32("USER_LOGIN_KEY").ToString();
                var currentUser = db.Users.FirstOrDefault(u => u.UserNo.ToString().Equals(temp));

                ViewBag.users = db.Users.ToList();
                ViewBag.notes = db.Notes.ToList();
                ViewBag.currentUser = currentUser;

                return View();
            }
        }


        /// <summary>
        /// 게사판 상세
        /// </summary>
        /// <param name="noteNo"></param>
        /// <returns></returns>
        public IActionResult Detail(int noteNo)
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                //로그인이 안된 상태
                return RedirectToAction("Login", "Account");
            }

            using (var db = new AspnetNoteDbContext())
            {
                var note = db.Notes.FirstOrDefault(n => n.NoteNo.Equals(noteNo));

                string temp = HttpContext.Session.GetInt32("USER_LOGIN_KEY").ToString();
                var currentUser = db.Users.FirstOrDefault(u => u.UserNo.ToString().Equals(temp));

                ViewBag.currentUser = currentUser;

                return View(note);
            }
        }


        public IActionResult Read(int noteNo)
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                //로그인이 안된 상태
                return RedirectToAction("Login", "Account");
            }

            using (var db = new AspnetNoteDbContext())
            {
                var note = db.Notes.FirstOrDefault(n => n.NoteNo.Equals(noteNo));
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
        public IActionResult Add(Note model)
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

                    db.Notes.Add(model);

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


        /// <summary>
        /// 게시물 수정
        /// </summary>
        /// <returns></returns>
        public IActionResult Edit(int noteNo)
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                //로그인이 안된 상태
                return RedirectToAction("Login", "Account");
            }

            using (var db = new AspnetNoteDbContext())
            {
                var note = db.Notes.FirstOrDefault(n => n.NoteNo.Equals(noteNo));
                /*if (note.UserNo != int.Parse(HttpContext.Session.GetInt32("USER_LOGIN_KEY").ToString())){
                      return Redirect("Index");
                  }*/
                return View(note);
            }
        }

        ////
        [HttpPost]
        public IActionResult Edit(Note model)
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                //로그인이 안된 상태
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                using (var db = new AspnetNoteDbContext())
                {

                    db.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    if (db.SaveChanges() > 0)
                    {
                        //동일한 위치의 Index로 이동
                        return Redirect("Index");

                    }
                }
                ModelState.AddModelError(string.Empty, "에러.");
            }
            return View(model);
        }

        /// <summary>
        /// 게시물 삭제
        /// </summary>
        /// <returns></returns>
        public IActionResult Delete(int noteNo)
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                //로그인이 안된 상태
                return RedirectToAction("Login", "Account");
            }



            if (ModelState.IsValid)
            {
                using (var db = new AspnetNoteDbContext())
                {
                    Note note = db.Notes.Find(noteNo);
                    /*   if (note.UserNo != int.Parse(HttpContext.Session.GetInt32("USER_LOGIN_KEY").ToString()))
                       {
                           return Redirect("Index");
                       }*/

                    db.Notes.Remove(note);
                    db.SaveChanges();
                    return Redirect("Index");
                }
                ModelState.AddModelError(string.Empty, "에러.");

            }
            return Redirect("Index");

        }

        public IActionResult Download(int fileno)
        {
            using (var db = new AspnetNoteDbContext())
            {
                var files = db.Files.FirstOrDefault(n => n.FileNo.Equals(fileno));

                if (files != null)
                {
                    files.FileType = files.FileType.Remove(0, 1);
                    return File(files.DataFiles, "application/" + files.FileType, files.Name);
                }
                return Redirect("Read");
            }
        }
                    
       



        [HttpPost]
        public IActionResult Add_(IFormFile files)
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
                   
                    int n;
                    if (db.Notes.Count() == 0)
                    {
                        n = 1;

                    }
                    else
                    {
                        n = db.Notes.Last().NoteNo + 1; // 새로운 노트 번호
                    }

                    Files ob = null;

                    foreach (Files f in db.Files)
                    {
                        if (f.NoteNo == n && f.BoardNo == 0) // board num 0
                        {
                            ob = f;
                        }
                    }

                if (ob == null)
                    {
                        var objfiles = new Files()
                        {
                            BoardNo = 0,
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
                    return Redirect("Add");

                }
                return  Redirect("Add");
            }
        }
    }
    

}
