using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NET.Models
{
    public class Compa
    {
        public int id { get; set; }
        public System.DateTime fecha { get; set; }
        public int total { get; set; }
        public String idUser { get; set; }
        public String idCliente { get; set; }

    }
}