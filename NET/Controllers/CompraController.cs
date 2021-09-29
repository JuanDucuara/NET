using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NET.Models;

namespace NET.Controllers
{
    public class CompraController : Controller
    {
        [Authorize]
        // GET: Compra
        public ActionResult Index()
        {
            try
            {
                var db = new inventario2021Entities1();
                var query = from tabCliente in db.cliente
                            join tabUser in db.usuario on tabCliente.id equals tabUser.id
                            select new Compa
                            {
                                idUser = tabUser.nombre,
                                idCliente = tabCliente.nombre
                            };
                return View(query);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();
            }
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(compra compra)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                using (var db = new inventario2021Entities1())
                {
                    db.compra.Add(compra);
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
                var findCo = db.compra.Find(id);
                return View(findCo);
            }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new inventario2021Entities1())
                {
                    var findCo = db.compra.Find(id);
                    db.compra.Remove(findCo);
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
                    compra findCo = db.compra.Where(a => a.id == id).FirstOrDefault();
                    return View(findCo);
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
        public ActionResult Edit(compra editCo)
        {
            try
            {
                using (var db = new inventario2021Entities1())
                {
                    compra compra = db.compra.Find(editCo.id);
                    compra.fecha = editCo.fecha;
                    compra.total = editCo.total;
                    compra.id_usuario = editCo.id_usuario;
                    compra.id_cliente = editCo.id_cliente;

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