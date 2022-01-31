using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.IO;
using System.Drawing.Imaging;
//using ZXing;
//using ZXing.QrCode.Internal;
//using ZXing.Rendering;


namespace Backline.DTE
{
    public class Transaction
    {
        public APIResult GenerarDTE(ModelDteNotaCredito modelo, string mailUsuario, string passUsuario, string path, string ambiente)
        {
            var api = new API(mailUsuario, passUsuario);
            var result = new APIResult();
            var guid = Guid.NewGuid().ToString();
            //DESKTOP - D1K9R0A
            if (Environment.MachineName == "PC_EDURIO" || Environment.MachineName == "DESKTOP-D1K9R0A")
            {
                ambiente = "pro";
            }

            try
            {
                var json = new JavaScriptSerializer().Serialize(modelo);
                path = @"C:\Documentos Backline\BOLETAS\";
                api.SetSavePDF(path + "\\" + guid);
                //api.SetSaveXML(path + "\\" + guid);

                result = api.SendDTE(json, ambiente);
                result.Path = path;
                result.FileGuid = guid;
               
                //GenerateBarCodeZXing("eduardoriosescanillatestpdf471");
            }
            catch(Exception ex)
            {
                result.ok = false;
                result.Message = ex.Message + " | " + ex.StackTrace;
            }

            return result;
        }

        public APIResult GenerarDTE(ModelDte modelo, string mailUsuario, string passUsuario, string path, string ambiente)
        {
            var api = new API(mailUsuario, passUsuario);            
            var result = new APIResult();
            var guid = Guid.NewGuid().ToString();

            
            //if (Environment.MachineName == "PC_EDURIO" || Environment.MachineName == "DESKTOP-D1K9R0A")
            //{
            //    ambiente = "cer";
            //}

            try
            {
                var json = new JavaScriptSerializer().Serialize(modelo);
                path = @"C:\Documentos Backline\BOLETAS\";
                api.SetSavePDF(path + "\\" + guid);
                //api.SetSaveXML(path + "\\" + guid);

                result = api.SendDTE(json, ambiente);
                result.Path = path;
                result.FileGuid = guid;

                //GenerateBarCodeZXing("eduardoriosescanillatestpdf471");
            }
            catch (Exception ex)
            {
                result.ok = false;
                result.Message = ex.Message + " | " + ex.StackTrace;
            }

            return result;
        }

        //private byte[] GenerateBarCodeZXing(string data)
        //{

        //    var writer = new BarcodeWriter
        //    {
        //        Format = BarcodeFormat.PDF_417,
        //        Options = new ZXing.Common.EncodingOptions { Width = 200, Height = 50 } //optional
        //    };
        //    var imgBitmap = writer.Write(data);
        //    using (var stream = new MemoryStream())
        //    {
        //        imgBitmap.Save(stream, ImageFormat.Png);
        //        imgBitmap.Save(@"C:\Backline\ARICA\test.png");
        //        return stream.ToArray();
        //    }
        //}
    }
}
