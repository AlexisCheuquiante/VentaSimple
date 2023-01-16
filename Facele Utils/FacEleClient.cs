using FacEleUtils.DoceleOLService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;


namespace FacEleUtils
{
    public class FacEleClient
    {
        DoceleOLClient client = new DoceleOLClient();
        public FacEleClient()
        {
        }
        public int generaDTE(ref string rutEmisor, ref int tipoDTE, FacEleUtils.DoceleOLService.generaDTEFormato formato, string txt, string xml, string uuid, out string descripcionOperacion, out long folioDTE)
        {
            using (OperationContextScope scope = new OperationContextScope(this.client.InnerChannel))
            {
                OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = new HttpRequestMessageProperty()
                {
                    Headers = {
                        { "facele.user", "0cc713a13" },
                        { "facele.pass", "mWUX8WQOfuexV9CLWaNFSA=="}
                    }
                };
                return client.generaDTE(ref rutEmisor, ref tipoDTE, formato, txt, xml, uuid, out descripcionOperacion, out folioDTE);
            }
        }       
        public int obtienePDF(string rutEmisor, int tipoDTE, long folioDTE, out string descripcionOperacion, out string XML, out byte[] PDF, out string URL)
        {
            using (OperationContextScope scope = new OperationContextScope(this.client.InnerChannel))
            {
                OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = new HttpRequestMessageProperty()
                {
                    Headers = {
                        { "facele.user", "0cc713a13" },
                        { "facele.pass", "mWUX8WQOfuexV9CLWaNFSA=="}
                    }
                };
                return client.obtieneDTE(rutEmisor, tipoDTE, folioDTE, obtieneDTEFormato.PDF_TERMICO, false, 1, out descripcionOperacion, out XML, out PDF, out URL);
            }
            

        }
        public int obtieneURLPDF(string rutEmisor, int tipoDTE, long folioDTE, out string descripcionOperacion, out string XML, out byte[] PDF, out string URL)
        {
            using (OperationContextScope scope = new OperationContextScope(this.client.InnerChannel))
            {
                OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = new HttpRequestMessageProperty()
                {
                    Headers = {
                        { "facele.user", "0cc713a13" },
                        { "facele.pass", "mWUX8WQOfuexV9CLWaNFSA=="}
                    }
                };
                return client.obtieneDTE(rutEmisor, tipoDTE, folioDTE, obtieneDTEFormato.URL_PDF, false, 1, out descripcionOperacion, out XML, out PDF, out URL);
            }
            

        }
        public void grabaPDF(Byte[] PDF, string fileLocation) {
            string decodedString = Encoding.UTF8.GetString(PDF);

            byte[] binaryData;
            binaryData = Convert.FromBase64String(decodedString);
            File.WriteAllBytes(fileLocation, binaryData);
        }
    }
}
