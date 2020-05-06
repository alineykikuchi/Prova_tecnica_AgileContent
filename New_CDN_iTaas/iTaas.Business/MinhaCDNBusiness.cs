using AutoMapper;
using iTaas.Business.Services;
using iTaas.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace iTaas.Business
{
    public class MinhaCDNBusiness
    {
        ConnectionService connectionService;
        object requestFileMinhaCDN;

        public MinhaCDNBusiness(string sourceURL)
        {
            connectionService = new ConnectionService(sourceURL);
            requestFileMinhaCDN = connectionService.RequestFile();
        }
        
        public List<MinhaCDN> ReturnListMinhaCDN()
        {
            var fileText = requestFileMinhaCDN.ToString();
            var records = fileText.ToString().Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();

            List<MinhaCDN> listaMinhaCDN = new List<MinhaCDN>();

            foreach (var line in records)
            {
                MinhaCDN formato = new MinhaCDN();
                var propriedades = line.Split('|').ToList();
                formato.ResponseSize = Convert.ToInt32(propriedades.ElementAt(0));
                formato.StatusCode = Convert.ToInt32(propriedades.ElementAt(1));
                formato.CacheSatus = propriedades.ElementAt(2);
                formato.HttpMethod = propriedades.ElementAt(3).Split(' ').ElementAt(0).Replace("\"", "");
                formato.UriPath = propriedades.ElementAt(3).Split(' ').ElementAt(1).Replace("\"", "");

                var numberDecimal = Convert.ToDecimal(propriedades.ElementAt(4).Replace(".", ","));
                formato.TimeTaken = Convert.ToInt32(numberDecimal);
                formato.Provider = "MINHA CDN";

                listaMinhaCDN.Add(formato);
            }
            return listaMinhaCDN;
        }

        public List<Agora> ConvertMinhaCDNtoAgora()
        {
            return Mapper.Map<List<Agora>>(ReturnListMinhaCDN());
        }

        public void CreateFileFormatAgora(string fileName)
        {
            var listAgora = ConvertMinhaCDNtoAgora();

            using (StreamWriter sw = File.CreateText(fileName))
            {
                sw.WriteLine("# Version: 1.0");
                sw.WriteLine("# Date: " + DateTime.Now);
                string fields = "# Fields:";

                var result = typeof(Agora).GetProperties()
                                          .SelectMany(p => p.GetCustomAttributes(typeof(FormatAgoraAttribute), false)).ToList();

                foreach (var item in result)
                {
                    fields = fields + " " + ((FormatAgoraAttribute)item).Value;
                }

                sw.WriteLine(fields);

                foreach (var line in listAgora)
                {
                    sw.WriteLine("\"" + line.Provider + "\" "
                                      + line.HttpMethod + " "
                                      + line.StatusCode + " "
                                      + line.UriPath + " "
                                      + line.TimeTaken + " "
                                      + line.ResponseSize + " "
                                      + line.CacheSatus);
                }
                
                
            }
            using (StreamReader reader = File.OpenText(fileName))
            {
                Console.WriteLine(reader.ReadToEnd());
                reader.Close();
            }
        }
    }
}
