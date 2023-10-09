using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using master1.Models;
using Microsoft.AspNet.Identity;

namespace master1.Controllers
{
    public class RoshetasController : Controller
    {
        private master1Entities db = new master1Entities();

        // GET: Roshetas
        public ActionResult Index()
        {
           //accept doc ==1
          
                var roshetas = db.Roshetas.Include(r => r.AspNetUser);
                return View(roshetas.ToList());
           
       
        }

        public FileResult Download(string Rosheta_pic)
        {
            string name = "../photos/" + Rosheta_pic;
            string path = Server.MapPath(name);
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            return File(fileBytes, "application.pdf", Rosheta_pic);

        }

   

        // GET: Roshetas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rosheta rosheta = db.Roshetas.Find(id);
            if (rosheta == null)
            {
                return HttpNotFound();
            }
            return View(rosheta);
        }

        // GET: Roshetas/Create
        public ActionResult Create()
        {
           
            ViewBag.id = User.Identity.GetUserId();
            ViewBag.userid = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: Roshetas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Rosheta_id,userid,Rosheta_pic,City,age,note,Accept_doc,Accept_user")] Rosheta rosheta ,HttpPostedFileBase Rosheta_pic)
        {
          
            if (ModelState.IsValid)
            {
                string path = "~/photos/" + Path.GetFileName(Rosheta_pic.FileName);
                string path2 = Path.GetFileName(Rosheta_pic.FileName);
                Rosheta_pic.SaveAs(Server.MapPath(path));
                rosheta.Rosheta_pic = path2.ToString();
                db.Roshetas.Add(rosheta);
                db.SaveChanges();
                return RedirectToAction("Home", "container");
            }

            ViewBag.userid = new SelectList(db.AspNetUsers, "Id", "Email", rosheta.userid);
            return View(rosheta);
        }
        
        // GET: Roshetas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rosheta rosheta = db.Roshetas.Find(id);
            if (rosheta == null)
            {
                return HttpNotFound();
            }
            ViewBag.userid = new SelectList(db.AspNetUsers, "Id", "Email", rosheta.userid);
            return View(rosheta);
        }

        // POST: Roshetas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Rosheta_id,userid,Rosheta_pic,City,age,note,Accept_doc,Accept_user")] Rosheta rosheta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rosheta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userid = new SelectList(db.AspNetUsers, "Id", "Email", rosheta.userid);
            return View(rosheta);
        }

        // GET: Roshetas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rosheta rosheta = db.Roshetas.Find(id);
            if (rosheta == null)
            {
                return HttpNotFound();
            }
            return View(rosheta);
        }

        // POST: Roshetas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rosheta rosheta = db.Roshetas.Find(id);
            db.Roshetas.Remove(rosheta);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
