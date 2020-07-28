using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace MaquinaWeb.Models
{
    [Serializable]
    public class MaquinaDeTroco
    {
        private static MaquinaDeTroco maquinaDeTroco { get; set; }
        private MaquinaDeTroco(){}
        public static MaquinaDeTroco getMaquinaDeTroco()
        {
            if (maquinaDeTroco == null)
            {
                maquinaDeTroco = new MaquinaDeTroco();
                string src = "~/Content/Image/";
                maquinaDeTroco.Moedas = new List<Moeda>();
                Moeda cent1 = new Moeda();
                cent1.Valor = 1;
                cent1.srcImg = src + "1cent.png";
                maquinaDeTroco.Moedas.Add(cent1);
                Moeda cent5 = new Moeda();
                cent5.Valor = 5;
                cent5.srcImg = src + "5cent.jpg";
                maquinaDeTroco.Moedas.Add(cent5);
                Moeda cent10 = new Moeda();
                cent10.Valor = 10;
                cent10.srcImg = src + "10cent.png";
                maquinaDeTroco.Moedas.Add(cent10);
                Moeda cent25 = new Moeda();
                cent25.Valor = 25;
                cent25.srcImg = src + "25cent.jpg";
                maquinaDeTroco.Moedas.Add(cent25);
                Moeda cent50 = new Moeda();
                cent50.Valor = 50;
                cent50.srcImg = src + "50cent.png";
                maquinaDeTroco.Moedas.Add(cent50);
                Moeda real1 = new Moeda();
                real1.Valor = 100;
                real1.srcImg = src + "1real.jpg";
                maquinaDeTroco.Moedas.Add(real1);
                maquinaDeTroco.Total = 0;
                foreach(var moeda in maquinaDeTroco.Moedas)
                {
                    moeda.Quantidade = 0;
                }
                maquinaDeTroco.atualizaTotal();

            }
            return maquinaDeTroco;
        }
        public List<Moeda> Moedas { get; set; }

        public double Total { get; set; }

        public void atualizaTotal()
        {
            Total = 0;
            foreach(var moeda in Moedas)
            {
                Total += (moeda.Valor) * moeda.Quantidade;
            }
        }
    }
}