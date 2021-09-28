using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NET.Models;
using Rotativa; 

namespace NET.Controllers
{
    public class ProductoController : Controller
    {
        [Authorize]
        // GET: Producto
        public ActionResult Index()
        {
            using (var db = new inventario2021Entities1())
            {
                return View(db.producto.ToList());

            }

        }
        public ActionResult ListaProveedores()
        {
            using (var db = new inventario2021Entities1())
            {
                return PartialView(db.proveedor.ToList());
            }
        }
        public ActionResult Create()
        {
            return View();
        }
        public static string NombreProveedor(int idProveedor)
        {
            using (var db= new inventario2021Entities1())
            {
                return db.proveedor.Find(idProveedor).nombre;
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(producto producto)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                using (var db = new inventario2021Entities1())
                {
                    db.producto.Add(producto);
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
                var findProduc = db.producto.Find(id);
                var imagen = db.producto_imagen.Where(e => e.id_producto == findProduc.id).FirstOrDefault();
           
                ViewBag.imagen = imagen.imagen;
                return View(findProduc);
            }
        }
        public ActionResult Edit(int id)
        {
            try
            {
                using (var db = new inventario2021Entities1())
                {
                    producto findProduc = db.producto.Where(a => a.id == id).FirstOrDefault();
                    return View(findProduc);
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
        public ActionResult Edit(producto editProduc)
        {
            try
            {
                using (var db = new inventario2021Entities1())
                {
                    var oldProduc = db.producto.Find(editProduc.id);
                    oldProduc.nombre = editProduc.nombre;
                    oldProduc.cantidad = editProduc.cantidad;
                    oldProduc.descripcion = editProduc.descripcion;
                    oldProduc.percio_unitario = editProduc.percio_unitario;
                    oldProduc.id_proveedor = editProduc.id_proveedor;

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
        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new inventario2021Entities1())
                {
                    producto producto = db.producto.Find(id);
                    db.producto.Remove(producto);
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
            }
        }
        public ActionResult Reporte()
        {
            try
            {
                var db = new inventario2021Entities1();
                var query = from tabProveedor in db.proveedor
                            join tabProducto in db.producto on tabProveedor.id equals tabProducto.id_proveedor
                            select new Reporte
                            {
                                nombreProveedor = tabProveedor.nombre,
                                telefonoProveedor = tabProveedor.telefono,
                                direccionProveedor = tabProveedor.direccion,
                                nombreProducto = tabProducto.nombre,
                                PrecioProducto = tabProducto.percio_unitario
                            };
                return View(query);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();
            }
        }
        public ActionResult PdfReporte()
        {
            return new ActionAsPdf("Reporte") { FileName = "Reporte.pdf" };
        }
    }
}