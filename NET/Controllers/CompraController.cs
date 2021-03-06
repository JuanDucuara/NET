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
        // GET: Compra
        public ActionResult Index()
        {
            using (var db = new inventario2021Entities1())
            {
                return View(db.compra.ToList());
            }
        }
        //Llaves foraneas del index
        public static string NombreUsuario(int idUsuario)
        {
            using (var db = new inventario2021Entities1())
            {
                return db.usuario.Find(idUsuario).nombre;
            }
        }
        public static string NombreCliente(int idCliente)
        {
            using (var db = new inventario2021Entities1())
            {
                return db.cliente.Find(idCliente).nombre;
            }
        }

        public ActionResult Create()
        {
            return View();
        }


        public ActionResult ListarUsuario()
        {
            using (var db = new inventario2021Entities1())
            {
                return PartialView(db.usuario.ToList());
            }
        }
        public ActionResult ListarCliente()
        {
            using (var db = new inventario2021Entities1())
            {
                return PartialView(db.cliente.ToList());
            }
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
                ModelState.AddModelError("", "error " + ex);
                return View();
            }
        }
        public ActionResult Details(int id)
        {
            using (var db = new inventario2021Entities1())
            {
                var findcom = db.compra.Find(id);
                return View(findcom);
            }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new inventario2021Entities1())
                {
                    var findcom = db.compra.Find(id);
                    db.compra.Remove(findcom);
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
                    compra findcom = db.compra.Where(a => a.id == id).FirstOrDefault();
                    return View(findcom);
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
        public ActionResult Edit(compra editCompra)
        {
            try
            {
                using (var db = new inventario2021Entities1())
                {
                    compra ente = db.compra.Find(editCompra.id);
                    ente.fecha = editCompra.fecha;
                    ente.total = editCompra.total;
                    ente.id_usuario = editCompra.id_usuario;
                    ente.id_cliente = editCompra.id_cliente;

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