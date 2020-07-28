using MaquinaWeb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;

namespace MaquinaWeb.Controllers
{
    public class MaquinaController : Controller
    {
        public ActionResult Index()
        {
            Troco troco = new Troco();
            MaquinaDeTroco maquina = MaquinaDeTroco.getMaquinaDeTroco();
            troco.Moedas = maquina.Moedas;
            return View(troco);
        }

        public ActionResult Abastece()
        {
            Troco troco = new Troco();
            MaquinaDeTroco maquina = MaquinaDeTroco.getMaquinaDeTroco();
            troco.Moedas = maquina.Moedas;
            return View(troco);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Adicionar(Troco troco)
        {
            MaquinaDeTroco maquina = MaquinaDeTroco.getMaquinaDeTroco();
            string sucesso = "S";
            string mensagem = "Máquina Abastecida!";
            JsonResult retorno;
            SerializeObject(maquina);
            try
            {
                for (int i = 0; i < maquina.Moedas.Count; i++)
                {
                    maquina.Moedas[i].Quantidade += troco.NovasMoedas[i].Quantidade;
                }
                //maquina tem o valor em centavos
                maquina.atualizaTotal();
                Troco t = new Troco();
                t.Moedas = maquina.Moedas;

                List<string> quantidades = new List<string>();
                foreach (var moeda in maquina.Moedas)
                {
                    quantidades.Add(moeda.Quantidade.ToString());
                }
                retorno = Json(new
                {
                    Sucesso = sucesso,
                    Mensagem = mensagem,
                    Valores = quantidades
                },
                        JsonRequestBehavior.AllowGet);
                return retorno;
            }
            catch(Exception ex)
            {
                maquina = DeSerializeObject<MaquinaDeTroco>();
                sucesso = "N";
                retorno = Json(new
                {
                    Sucesso = sucesso,
                    Mensagem = ex.Message,
                },
                        JsonRequestBehavior.AllowGet);
                return retorno;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Retirar(Troco troco)
        {
            MaquinaDeTroco maquina = MaquinaDeTroco.getMaquinaDeTroco();
            string sucesso = "S";
            string mensagem = "Retirada feita com sucesso!";
            JsonResult retorno;
            SerializeObject(maquina);
            try
            {
                for (int i = 0; i < maquina.Moedas.Count; i++)
                {
                    maquina.Moedas[i].Quantidade -= troco.NovasMoedas[i].Quantidade;
                }
                maquina.atualizaTotal();

                List<string> quantidades = new List<string>();
                foreach (var moeda in maquina.Moedas)
                {
                    quantidades.Add(moeda.Quantidade.ToString());
                }
                retorno = Json(new
                {
                    Sucesso = sucesso,
                    Mensagem = mensagem,
                    Valores = quantidades
                },
                        JsonRequestBehavior.AllowGet);
                return retorno;
            }
            catch (Exception ex)
            {
                maquina = DeSerializeObject<MaquinaDeTroco>();
                sucesso = "N";
                retorno = Json(new
                {
                    Sucesso = sucesso,
                    Mensagem = ex.Message,
                },
                        JsonRequestBehavior.AllowGet);
                return retorno;
            }

        }

        public ActionResult Sangria()
        {
            Troco troco = new Troco();
            MaquinaDeTroco maquina = MaquinaDeTroco.getMaquinaDeTroco();
            troco.Moedas = maquina.Moedas;
            return View(troco);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CalcularTroco(Troco troco)
        {
            string sucesso = "";
            string mensagem = "";
            double valor = troco.Total * 100;
            JsonResult retorno;

            MaquinaDeTroco maquina = MaquinaDeTroco.getMaquinaDeTroco();
            if (maquina.Total < valor) //troco maior do que máquina possui
            {
                mensagem = "Máquina com valor insuficiente";
                sucesso = "N";
            }
            else //Tem o valor na máquina
            {
                Troco resposta = new Troco();
                double restante = valor;
                SerializeObject(maquina);
                for (int i = resposta.NovasMoedas.Count-1; i >= 0; i--)
                {
                    if (maquina.Moedas[i].Quantidade > 0) // tem moedas desse valor na maquina?
                    {
                        if ((restante / resposta.NovasMoedas[i].Valor) >= 1) // preciso dessa moeda?
                        {
                            double moedasNecessarias = Math.Floor(restante / resposta.NovasMoedas[i].Valor);
                            int moedasInt = Convert.ToInt32(moedasNecessarias);
                            if (moedasNecessarias <= maquina.Moedas[i].Quantidade) //tenho a qntd dessa moeda que preciso na maquina?
                            {
                                resposta.NovasMoedas[i].Quantidade = moedasInt;
                                maquina.Moedas[i].Quantidade-=moedasInt;
                                restante = restante % resposta.NovasMoedas[i].Valor;
                            }
                            else //tenho menos do que a quantidade necessária
                            {
                                restante -= maquina.Moedas[i].Quantidade * maquina.Moedas[i].Valor;
                                resposta.NovasMoedas[i].Quantidade = maquina.Moedas[i].Quantidade;
                                maquina.Moedas[i].Quantidade = 0;
                            }
                        }
                    }
                }
                maquina.atualizaTotal();
                if (restante > 0)
                {
                    sucesso = "N";
                    mensagem = "Não há moedas o suficiente para dar o troco exato";
                    maquina = DeSerializeObject<MaquinaDeTroco>();

                }
                else
                {
                    sucesso = "S";
                    mensagem = "Troco liberado com sucesso. Confira as moedas utilizadas.";
                    List<string> quantidades = new List<string>();
                    SerializeObject(maquina);
                    foreach (var moeda in resposta.NovasMoedas)
                    {
                        quantidades.Add(moeda.Quantidade.ToString());
                    }
                    retorno = Json(new
                    {
                        Sucesso = sucesso,
                        Mensagem = mensagem,
                        Valores = quantidades
                    },
                    JsonRequestBehavior.AllowGet);
                    return retorno;
                }
            }
            retorno = Json(new
            {
                Sucesso = sucesso,
                Mensagem = mensagem
            },
                JsonRequestBehavior.AllowGet);
            return retorno;
        }

        public void SerializeObject<T>(T serializableObject)
        {
            string fileName = "\\Persistencia.xml";
            fileName = Server.MapPath("~") + fileName;
            if (serializableObject == null) { return; }

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                XmlSerializer serializer = new XmlSerializer(serializableObject.GetType());
                using (MemoryStream stream = new MemoryStream())
                {
                    serializer.Serialize(stream, serializableObject);
                    stream.Position = 0;
                    xmlDocument.Load(stream);
                    xmlDocument.Save(fileName);
                }
            }
            catch (Exception ex)
            {

            }
        }
        public T DeSerializeObject<T>()
        {
            string fileName = "\\Persistencia.xml";
            fileName = Server.MapPath("~") + fileName;
            if (string.IsNullOrEmpty(fileName)) { return default(T); }
            T objectOut = default(T);
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(fileName);
                string xmlString = xmlDocument.OuterXml;

                using (StringReader read = new StringReader(xmlString))
                {
                    Type outType = typeof(T);

                    XmlSerializer serializer = new XmlSerializer(outType);
                    using (XmlReader reader = new XmlTextReader(read))
                    {
                        objectOut = (T)serializer.Deserialize(reader);
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return objectOut;
        }
    }
}