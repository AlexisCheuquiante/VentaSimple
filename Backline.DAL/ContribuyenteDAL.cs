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
    public class ContribuyenteDAL
    {
        public static Backline.Entidades.Contribuyente InsertarContribuyente(Backline.Entidades.Contribuyente contribuyente)
        {
            Database db = DatabaseFactory.CreateDatabase("baseDatosFarmacias");
            DbCommand dbCommand = db.GetStoredProcCommand("SP_CONT_CONTRIBUYENTE_INS");

            db.AddInParameter(dbCommand, "RAZON_SOCIAL", DbType.String, contribuyente.Razon_Social != "" ? contribuyente.Razon_Social.ToUpper() : (object)null);
            db.AddInParameter(dbCommand, "RUT", DbType.String, contribuyente.Rut != "" ? contribuyente.Rut.ToUpper() : (object)null);
            db.AddInParameter(dbCommand, "RUT_CODE", DbType.Int32, contribuyente.Rut_Code != 0 ? contribuyente.Rut_Code : (object)null);
            db.AddInParameter(dbCommand, "ELIMINADO", DbType.Byte, contribuyente.Eliminado == true ? 1 : 0);

            contribuyente.Id = int.Parse(db.ExecuteScalar(dbCommand).ToString());


            return contribuyente;

        }
        public static List<Backline.Entidades.Contribuyente> ObtenerContribuyente(Backline.Entidades.Filtro filtro)
        {
            List<Backline.Entidades.Contribuyente> listaContribuyentes = new List<Backline.Entidades.Contribuyente>();
            Database db = DatabaseFactory.CreateDatabase("baseDatosFarmacias");
            DbCommand dbCommand = db.GetStoredProcCommand("SP_CONT_CONTRIBUYENTE_LEER");

            db.AddInParameter(dbCommand, "ID", DbType.Int32, filtro.ContId != 0 ? filtro.ContId : (object)null);
            db.AddInParameter(dbCommand, "RUT_CODE", DbType.Int32, filtro.RutCode != 0? filtro.RutCode : (object)null);

            IDataReader reader = (IDataReader)db.ExecuteReader(dbCommand);

            try
            {
                int ID = reader.GetOrdinal("ID");
                int RAZON_SOCIAL = reader.GetOrdinal("RAZON_SOCIAL");
                int RUT = reader.GetOrdinal("RUT");
               


                while (reader.Read())
                {
                    Backline.Entidades.Contribuyente OBJ = new Backline.Entidades.Contribuyente();
                    //BeginFields
                    OBJ.Id = (int)(!reader.IsDBNull(ID) ? reader.GetValue(ID) : 0);
                    OBJ.Razon_Social = (String)(!reader.IsDBNull(RAZON_SOCIAL) ? reader.GetValue(RAZON_SOCIAL) : string.Empty);
                    OBJ.Rut = (String)(!reader.IsDBNull(RUT) ? reader.GetValue(RUT) : string.Empty);

                    //EndFields

                    listaContribuyentes.Add(OBJ);
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

            return listaContribuyentes;

        }
    }
}
