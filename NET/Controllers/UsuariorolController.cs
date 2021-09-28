using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NET.Models;

namespace NET.Controllers
{
    public class UsuariorolController : Controller
    {
        [Authorize]
        // GET: Usuariorol
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UsRol()
        {
            try
            {
                var db = new inventario2021Entities1();
                var query = from tabRol in db.roles
                            join tabUsuario in db.usuario on tabRol.id equals tabUsuario.id
                            select new UR 
                            {
                                idUsuario = tabUsuario.nombre,
                                idRol = tabRol.descripcion
                            };
                return View(query);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();
            }
        }
    }
}