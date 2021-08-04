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
    public class EstablecimientoDAL
    {
        public static List<Backline.Entidades.Establecimiento> ObtenerEstablecimientos(Entidades.Filtro filtro)
        {
            List<Backline.Entidades.Establecimiento> listaEstablecimientos = new List<Backline.Entidades.Establecimiento>();
            Database db = DatabaseFactory.CreateDatabase("baseDatosFarmacias");
            DbCommand dbCommand = db.GetStoredProcCommand("SP_EST_ESTABLECIMIENTO_LEER");

            db.AddInParameter(dbCommand, "EMP_ID", DbType.Int32, filtro.EmpId != 0 ? filtro.EmpId : (object)null);

            IDataReader reader = (IDataReader)db.ExecuteReader(dbCommand);

            try
            {
                int ID = reader.GetOrdinal("ID");
                int EMP_ID = reader.GetOrdinal("EMP_ID");
                int NOMBRE = reader.GetOrdinal("NOMBRE");
                int AFECTA_IVA = reader.GetOrdinal("AFECTA_IVA");

                while (reader.Read())
                {
                    Backline.Entidades.Establecimiento OBJ = new Backline.Entidades.Establecimiento();
                    //BeginFields
                    OBJ.Id = (int)(!reader.IsDBNull(ID) ? reader.GetValue(ID) : 0);
                    OBJ.EmpId = (int)(!reader.IsDBNull(EMP_ID) ? reader.GetValue(EMP_ID) : 0);
                    OBJ.NombreEstablecimiento = (String)(!reader.IsDBNull(NOMBRE) ? reader.GetValue(NOMBRE) : string.Empty);
                    OBJ.AfectaIva = (bool)(!reader.IsDBNull(AFECTA_IVA) ? reader.GetValue(AFECTA_IVA) : false);
                    //EndFields

                    listaEstablecimientos.Add(OBJ);
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



            return listaEstablecimientos;

        }
    }
}
