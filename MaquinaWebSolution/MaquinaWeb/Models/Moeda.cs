using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MaquinaWeb.Models
{
    public class Moeda
    {
        [Range(0.0, float.MaxValue, ErrorMessage ="Inválido")]
        public int Quantidade { get; set; }
        public double Valor { get; set; }
        public string srcImg { get; set; }
    }
}