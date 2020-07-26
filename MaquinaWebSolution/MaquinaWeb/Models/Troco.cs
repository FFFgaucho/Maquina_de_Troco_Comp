using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaquinaWeb.Models
{
    public class Troco
    {
        public List<Moeda> Moedas { get; set; }

        public float Total { get; set; }
    }
}