using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NET.Models;

namespace NET.Controllers
{
    public class ProveedorController : Controller
    {
        [Authorize]
        // GET: Proveedor
        public ActionResult Index()
        {
            using (var db = new inventario2021Entities1())
            {
                return View(db.proveedor.ToList());
            }

        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(proveedor proveedor)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                using (var db = new inventario2021Entities1())
                {
                    db.proveedor.Add(proveedor);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
            }


        }
        public ActionResult Details(int id)
        {
            using (var db = new inventario2021Entities1())
            {
                var findPro = db.proveedor.Find(id);
                return View(findPro);
            }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new inventario2021Entities1())
                {
                    var findPro = db.proveedor.Find(id);
                    db.proveedor.Remove(findPro);
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
            }
        }
        public ActionResult Edit(int id)
        {
            try
            {
                using (var db = new inventario2021Entities1())
                {
                    proveedor findPro = db.proveedor.Where(a => a.id == id).FirstOrDefault();
                    return View(findPro);
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(proveedor editPro)
        {
            try
            {
                using (var db = new inventario2021Entities1())
                {
                    proveedor prov = db.proveedor.Find(editPro.id);
                    prov.nombre = editPro.nombre;
                    prov.direccion = editPro.direccion;
                    prov.telefono = editPro.telefono;
                    prov.nombre_contacto = editPro.nombre_contacto;
                  
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
            }

        }
    }
}