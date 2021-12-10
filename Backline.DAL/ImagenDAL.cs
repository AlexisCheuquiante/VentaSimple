using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;

namespace Backline.DAL
{
    public class ImagenDAL
    {
        public static List<Entidades.Imagen> ObtenerImagenes()
        {
            List<Backline.Entidades.Imagen> listaRetorno = new List<Backline.Entidades.Imagen>();
            Database db = DatabaseFactory.CreateDatabase("baseDatosFarmacias");
            DbCommand dbCommand = db.GetStoredProcCommand("SP_IMAGE_IMAGEN_EMPRESA_GET ");

            IDataReader reader = (IDataReader)db.ExecuteReader(dbCommand);

            try
            {
                int ID = reader.GetOrdinal("ID");
                int EMP_ID = reader.GetOrdinal("EMP_ID");
                int RUTA_IMAGEN = reader.GetOrdinal("RUTA_IMAGEN");

                while (reader.Read())
                {
                    Backline.Entidades.Imagen obj = new Backline.Entidades.Imagen();
                    //BeginFields
                    obj.ID = (int)(!reader.IsDBNull(ID) ? reader.GetValue(ID) : 0);
                    obj.EMP_ID = (int)(!reader.IsDBNull(EMP_ID) ? reader.GetValue(EMP_ID) : 0);
                    obj.RUTA_IMAGEN = (String)(!reader.IsDBNull(RUTA_IMAGEN) ? reader.GetValue(RUTA_IMAGEN) : string.Empty);
                    //EndFields

                    listaRetorno.Add(obj);
                }
            }
            catch (Exception ex)
            {
                //GlobalesDAO.InsertErrores(ex);
                throw ex;
            }
            finally
            {
                reader.Close();
            }

            return listaRetorno;

        }
    }
}
