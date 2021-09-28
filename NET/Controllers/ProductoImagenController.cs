using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using NET.Models;

namespace NET.Controllers
{
    public class ProductoImagenController : Controller
    {
        // GET: ProductoImagen
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CargarImagen()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CargarImagen(int producto, HttpPostedFileBase imagen)
        {
            try
            {
                //Guardar ruta
                string filePath = string.Empty;
                string nameFile = "";

                //Si el archivo llega
                if (imagen != null)
                {
                    string path = Server.MapPath("~/Uploads/Imagenes/");

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    nameFile = Path.GetFileName(imagen.FileName);

                    filePath = path + Path.GetFileName(imagen.FileName);

                    string extension = Path.GetExtension(imagen.FileName);

                    imagen.SaveAs(filePath);
                }

                using (var db = new inventario2021Entities1())
                {
                    var imagenProducto = new producto_imagen();
                    imagenProducto.id_producto = producto;
                    imagenProducto.imagen = "/Uploads/Imagenes/" + nameFile;
                    db.producto_imagen.Add(imagenProducto);
                    db.SaveChanges();

                }

                return View();

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();
            }

        }
    }
} 