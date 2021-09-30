using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NET.Models;
namespace NET.Controllers
{
    public class ProductoCompraController : Controller
    {
        // GET: ProductoCompra
        public ActionResult Index()
        {
            using (var db = new inventario2021Entities1())
            {
                return View(db.producto_compra.ToList());
            }
        }

        public static int Nombrecompra(int idCompra)
        {
            using (var db = new inventario2021Entities1())
            {
                return db.compra.Find(idCompra).id;
            }
        }
        public static string NombreProductos (int idPro)
        {
            using (var db = new inventario2021Entities1())
            {
                return db.producto.Find(idPro).nombre;
            }
        }

        public ActionResult Create()
        {
            return View();
        }


        public ActionResult ListarCompra()
        {
            using (var db = new inventario2021Entities1())
            {
                return PartialView(db.compra.ToList());
            }
        }
        public ActionResult ListarProducto()
        {
            using (var db = new inventario2021Entities1())
            {
                return PartialView(db.producto.ToList());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(producto_compra compra)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var db = new inventario2021Entities1())
                {
                    db.producto_compra.Add(compra);
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
                var findcom = db.producto_compra.Find(id);
                return View(findcom);
            }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new inventario2021Entities1())
                {
                    var findcom = db.producto_compra.Find(id);
                    db.producto_compra.Remove(findcom);
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
                    producto_compra findcom = db.producto_compra.Where(a => a.id == id).FirstOrDefault();
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
        public ActionResult Edit(producto_compra editCompra)
        {
            try
            {
                using (var db = new inventario2021Entities1())
                {
                    producto_compra ente = db.producto_compra.Find(editCompra.id);
                    ente.id_compra = editCompra.id_compra;
                    ente.id_producto = editCompra.id_producto;
                    ente.cantidad = editCompra.cantidad;
                  

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