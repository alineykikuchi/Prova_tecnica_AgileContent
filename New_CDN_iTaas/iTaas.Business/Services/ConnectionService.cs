using System;
using System.IO;
using System.Net;

namespace iTaas.Business.Services
{
    public class ConnectionService
    {
        HttpWebRequest requisicaoWeb;
        string sourceURL;

        public ConnectionService(string sourceURL)
        {
            this.sourceURL = sourceURL;
            requisicaoWeb = WebRequest.CreateHttp(sourceURL);
        }

        public object RequestFile()
        {
            try
            {
                var resposta = requisicaoWeb.GetResponse();
                var streamDados = resposta.GetResponseStream();
                StreamReader reader = new StreamReader(streamDados);

                object objResponse = reader.ReadToEnd();
                Console.WriteLine(objResponse.ToString());
                return objResponse;
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao realizar a requisição do link: "+ sourceURL + "\n" + e.Message);
            }
        }
    }
}
