using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaquinaWeb.Models
{
    public class MaquinaDeTroco
    {
        private static MaquinaDeTroco maquinaDeTroco { get; set; }
        private MaquinaDeTroco(){}
        public static MaquinaDeTroco getMaquinaDeTroco()
        {
            if (maquinaDeTroco == null)
            {
                maquinaDeTroco = new MaquinaDeTroco();
                maquinaDeTroco.Moedas = new List<Moeda>();
                Moeda cent1 = new Moeda();
                cent1.Valor = 1;
                maquinaDeTroco.Moedas.Add(cent1);
                Moeda cent5 = new Moeda();
                cent5.Valor = 5;
                maquinaDeTroco.Moedas.Add(cent5);
                Moeda cent10 = new Moeda();
                cent10.Valor = 10;
                maquinaDeTroco.Moedas.Add(cent10);
                Moeda cent25 = new Moeda();
                cent25.Valor = 25;
                maquinaDeTroco.Moedas.Add(cent25);
                Moeda cent50 = new Moeda();
                cent50.Valor = 50;
                maquinaDeTroco.Moedas.Add(cent50);
                Moeda real1 = new Moeda();
                real1.Valor = 100;
                maquinaDeTroco.Moedas.Add(real1);
                foreach(var moeda in maquinaDeTroco.Moedas)
                {
                    moeda.Quantidade = 0;
                }

            }
            return maquinaDeTroco;
        }
        public List<Moeda> Moedas { get; set; }
    }
}