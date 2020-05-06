using System;
using System.Collections.Generic;
using System.IO;
using iTaas.Business;
using iTaas.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestNew_CDN_iTaas
{
    [TestClass]
    public class MinhaCDNBusinessUnitTest
    {
        [TestMethod]
        public void Teste_QuebraDeDadosDoArquivoMinhaCDNEmLista()
        {
            string linkTest = "https://s3.amazonaws.com/uux-itaas-static/minha-cdn-logs/input-01.txt";
            MinhaCDNBusiness minhaCDNBusiness = new MinhaCDNBusiness(linkTest);

            MinhaCDN formato;
            List<MinhaCDN> resultadoEsperado = new List<MinhaCDN>();

            #region dados de teste
            formato = new MinhaCDN();
            formato.CacheSatus= "HIT";
            formato.HttpMethod= "GET";
            formato.Provider = "MINHA CDN";
            formato.ResponseSize= 312;
            formato.StatusCode= 200;
            formato.TimeTaken= 100;
            formato.UriPath= "/robots.txt";
            resultadoEsperado.Add(formato);

            formato = new MinhaCDN();
            formato.CacheSatus= "MISS";
            formato.HttpMethod= "POST";
            formato.Provider= "MINHA CDN";
            formato.ResponseSize= 101;
            formato.StatusCode= 200;
            formato.TimeTaken= 319;
            formato.UriPath= "/myImages";
            resultadoEsperado.Add(formato);

            formato = new MinhaCDN();
            formato.CacheSatus= "MISS";
            formato.HttpMethod= "GET";
            formato.Provider= "MINHA CDN";
            formato.ResponseSize= 199;
            formato.StatusCode= 404;
            formato.TimeTaken= 143;
            formato.UriPath = "/not-found";
            resultadoEsperado.Add(formato);

            formato = new MinhaCDN();
            formato.CacheSatus = "INVALIDATE";
            formato.HttpMethod = "GET";
            formato.Provider = "MINHA CDN";
            formato.ResponseSize = 312;
            formato.StatusCode = 200;
            formato.TimeTaken = 245;
            formato.UriPath = "/robots.txt";
            resultadoEsperado.Add(formato);
            #endregion

            var resultadoObtido = minhaCDNBusiness.ReturnListMinhaCDN();
            CollectionAssert.AllItemsAreNotNull(resultadoObtido);
        }

        [TestMethod]
        public void Teste_VerificandoDadosConvertidosParaAgora()
        {
            AutoMapperConfig.RegisterMapping();
            string linkTest = "https://s3.amazonaws.com/uux-itaas-static/minha-cdn-logs/input-01.txt";
            MinhaCDNBusiness minhaCDNBusiness = new MinhaCDNBusiness(linkTest);

            string startupPath = Environment.CurrentDirectory;
            string fileForTest = startupPath +"\\teste.txt";
            string newFile = startupPath + "\\teste2.txt";

            if(File.Exists(newFile))
                File.Delete(newFile);
            minhaCDNBusiness.CreateFileFormatAgora(newFile);

            StreamReader readerTest = File.OpenText(fileForTest);
            string resultadoEsperado = readerTest.ReadToEnd();
            int indexTest = resultadoEsperado.IndexOf("# Fields:");
            resultadoEsperado = resultadoEsperado.Substring(indexTest);

            StreamReader reader = File.OpenText(newFile);
            string resultadoObtido = reader.ReadToEnd();
            int index = resultadoObtido.IndexOf("# Fields:");
            resultadoObtido = resultadoObtido.Substring(index);

            Assert.AreEqual(resultadoEsperado, resultadoObtido);
        }
    }
}
