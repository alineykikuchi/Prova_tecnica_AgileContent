using iTaas.Business;
using iTaas.Models;
using iTaas.Models.Extension;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace New_CDN_iTaas
{
    class Program
    {
        static void Main(string[] args)
        {
            const int SOURCE_URL = 0, TARGET_PATH = 1;
            AutoMapperConfig.RegisterMapping();
            MinhaCDNBusiness minhaCDNBusiness;
            string path = ConfigurationManager.AppSettings["outputPath"];
            List<string> sourceURLtargetPath = null;

            Console.Write("\nconvert ");
            var stringCall = Console.ReadLine();

            try
            {
                ValidationStringCall(ref sourceURLtargetPath, ref stringCall);
                minhaCDNBusiness = new MinhaCDNBusiness(sourceURLtargetPath.ElementAt(SOURCE_URL).Trim());

                string outputFileName = path + sourceURLtargetPath.ElementAt(TARGET_PATH).Trim();

                if (File.Exists(outputFileName))
                {
                    Console.Write("\nThere is a file with the same name in this location."
                                 + "\nDo you want to replace it? y(yes)/n(no)/c(cancel): ");

                    EnumProcess processeDeleteFile = GetResponse();

                    if (processeDeleteFile == EnumProcess.cancel)
                        return;

                    else if (processeDeleteFile == EnumProcess.yes)
                        File.Delete(outputFileName);

                    else if (processeDeleteFile == EnumProcess.no)
                        outputFileName = NewFileName(path, sourceURLtargetPath.ElementAt(1));
                }
                
                minhaCDNBusiness.CreateFileFormatAgora(outputFileName);
                Console.WriteLine("Successfully converted file!\nPress enter...");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                Console.Write("\nPress enter...");
                Console.Read();
            }
            
        }

        public static void ValidationStringCall(ref List<string> sourceURLtargetPath, ref string stringCall)
        {
            do
            {
                sourceURLtargetPath = stringCall.Split(new string[] { "./output/" }, StringSplitOptions.None).ToList();
                if (sourceURLtargetPath.Count == 1)
                {
                    Console.WriteLine("Invalid call. Please enter a default call");
                    stringCall = Console.ReadLine();
                }

            } while (sourceURLtargetPath.Count <= 1);
                       
        }
        public static EnumProcess GetResponse()
        {
            string response;
            response = Console.ReadLine().Trim().ToUpper();

            while (response != "Y" && response != "N" && response != "C")
            {
                Console.Write(@"Invalid character!
                                Please enter the following characters: Y(yes), N(no) or C(cancel) ");
                response = Console.ReadLine();
            }

            var listEnumProcess = EnumDataExtension.GetDataValues<EnumProcess>(typeof(EnumProcess));
            return listEnumProcess.FirstOrDefault(x => x.GetDatavalue().Equals(response.Trim()));
        }
        public static string NewFileName(string path, string fileName)
        {
            string newFile;
            string extension = Path.GetExtension(fileName);
            
            int count = 1;

            do
            {
                newFile = path + fileName + "(" + count.ToString() + ")" + extension;
                count++;

            } while (File.Exists(newFile));

            return newFile;
        }
    }
}
