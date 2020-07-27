using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaquinaWeb.Models
{
    public class Troco
    {
        public Troco()
        {
            NovasMoedas = new List<Moeda>();
            string src = "~/Content/Image/";
            Moeda cent1 = new Moeda();
            cent1.Valor = 1;
            cent1.srcImg = src + "1cent.png";
            NovasMoedas.Add(cent1);
            Moeda cent5 = new Moeda();
            cent5.Valor = 5;
            cent5.srcImg = src + "5cent.jpg";
            NovasMoedas.Add(cent5);
            Moeda cent10 = new Moeda();
            cent10.Valor = 10;
            cent10.srcImg = src + "10cent.png";
            NovasMoedas.Add(cent10);
            Moeda cent25 = new Moeda();
            cent25.Valor = 25;
            cent25.srcImg = src + "25cent.jpg";
            NovasMoedas.Add(cent25);
            Moeda cent50 = new Moeda();
            cent50.Valor = 50;
            cent50.srcImg = src + "50cent.png";
            NovasMoedas.Add(cent50);
            Moeda real1 = new Moeda();
            real1.Valor = 100;
            real1.srcImg = src + "1real.jpg";
            NovasMoedas.Add(real1);
            foreach (var moeda in NovasMoedas)
            {
                moeda.Quantidade = 0;
            }
        }
        public List<Moeda> Moedas { get; set; }

        public List<Moeda> NovasMoedas { get; set; }

        public double Total { get; set; }

        public void atualizaSrcImgs() {
            string src = "~/Content/Image/";
            NovasMoedas[0].srcImg = src + "1cent.png";

            NovasMoedas[1].srcImg = src + "5cent.jpg";

            NovasMoedas[2].srcImg = src + "10cent.png";

            NovasMoedas[3].srcImg = src + "25cent.jpg";

            NovasMoedas[4].srcImg = src + "50cent.png";

            NovasMoedas[5].srcImg = src + "1real.jpg";
        }
    }
}