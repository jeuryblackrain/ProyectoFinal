using Mantenimiento.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProyectoFinal.Controllers
{
    public class HomeController : Controller
    {

        private DBRestauranteEntities db = new DBRestauranteEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListaArticulos()
        {
            return View(db.Articuloes.ToList().Where(x => x.Estado == true));
        }

        public ActionResult RentarArticulo(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articulo articulo = db.Articuloes.Find(id);
            if (articulo == null)
            {
                return HttpNotFound();
            }
            return View(articulo);
        }

        // POST: Articulos/
        [HttpPost, ActionName("RentarArticulo")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmarRenta(int id)
        {
            Articulo articulo = db.Articuloes.Find(id);
            articulo.Estado = false;
            db.Entry(articulo).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ListaArticulos");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}