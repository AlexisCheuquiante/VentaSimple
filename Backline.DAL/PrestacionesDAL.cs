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
    public class PrestacionesDAL
    {
        public static Backline.Entidades.Prestaciones InsertarPrestacion(Entidades.Prestaciones prestaciones)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db = DatabaseFactory.CreateDatabase("baseDatosFarmacias");
            DbCommand dbCommand = db.GetStoredProcCommand("SP_PRES_PRESTACIONES_INS");


            db.AddInParameter(dbCommand, "ID", DbType.Int32, prestaciones.Id);
            db.AddInParameter(dbCommand, "EMP_ID", DbType.Int32, prestaciones.Emp_Id != 0 ? prestaciones.Emp_Id : (object)null);
            db.AddInParameter(dbCommand, "EST_ID", DbType.Int32, prestaciones.Est_Id != 0 ? prestaciones.Est_Id : (object)null);
            db.AddInParameter(dbCommand, "CODIGO", DbType.String, prestaciones.Codigo != null ? prestaciones.Codigo : (object)null);
            db.AddInParameter(dbCommand, "DESCRIPCION", DbType.String, prestaciones.Descripcion != "" ? prestaciones.Descripcion : (object)null);
            db.AddInParameter(dbCommand, "VALOR", DbType.Int32, prestaciones.Valor != 0 ? prestaciones.Valor : (object)null);
            db.AddInParameter(dbCommand, "VALOR_LIBRE", DbType.Byte, prestaciones.Valor_Libre == true ? 1 : 0);
            db.AddInParameter(dbCommand, "ELIMINADO", DbType.Byte, prestaciones.Eliminado == true ? 1 : 0);

            prestaciones.Id = int.Parse(db.ExecuteScalar(dbCommand).ToString());

            return prestaciones;
        }
        public static List<Backline.Entidades.Prestaciones> ObtenerPrestaciones(Entidades.Filtro filtro)
        {
            List<Backline.Entidades.Prestaciones> lista = new List<Backline.Entidades.Prestaciones>();
            Database db = DatabaseFactory.CreateDatabase("baseDatosFarmacias");
            DbCommand dbCommand = db.GetStoredProcCommand("SP_PRES_PRESTACIONES_LEER");

            db.AddInParameter(dbCommand, "ID", DbType.Int32, filtro.Id != 0 ? filtro.Id : (object)null);
            db.AddInParameter(dbCommand, "EMP_ID", DbType.Int32, filtro.EmpId != 0 ? filtro.EmpId : (object)null);
            db.AddInParameter(dbCommand, "EST_ID", DbType.Int32, filtro.EstId != 0 ? filtro.EstId : (object)null);

            IDataReader reader = (IDataReader)db.ExecuteReader(dbCommand);

            try
            {
                int ID = reader.GetOrdinal("ID");
                int EMP_ID = reader.GetOrdinal("EMP_ID");
                int EST_ID = reader.GetOrdinal("EST_ID");
                int NOMBRE_ESTABLECIMIENTO = reader.GetOrdinal("NOMBRE_ESTABLECIMIENTO");
                int CODIGO = reader.GetOrdinal("CODIGO");
                int DESCRIPCION = reader.GetOrdinal("DESCRIPCION");
                int VALOR = reader.GetOrdinal("VALOR");

                while (reader.Read())
                {
                    Backline.Entidades.Prestaciones OBJ = new Backline.Entidades.Prestaciones();
                    //BeginFields
                    OBJ.Id = (int)(!reader.IsDBNull(ID) ? reader.GetValue(ID) : 0);
                    OBJ.Emp_Id = (int)(!reader.IsDBNull(EMP_ID) ? reader.GetValue(EMP_ID) : 0);
                    OBJ.Est_Id = (int)(!reader.IsDBNull(EST_ID) ? reader.GetValue(EST_ID) : 0);
                    OBJ.Nombre_Establecimiento = (String)(!reader.IsDBNull(NOMBRE_ESTABLECIMIENTO) ? reader.GetValue(NOMBRE_ESTABLECIMIENTO) : string.Empty);
                    OBJ.Codigo = (String)(!reader.IsDBNull(CODIGO) ? reader.GetValue(CODIGO) : string.Empty);
                    OBJ.Descripcion = (String)(!reader.IsDBNull(DESCRIPCION) ? reader.GetValue(DESCRIPCION) : string.Empty);
                    OBJ.Valor = (int)(!reader.IsDBNull(VALOR) ? reader.GetValue(VALOR) : 0);
                    //EndFields

                    lista.Add(OBJ);
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
            return lista;
        }
        public static void EliminarPrestacion(int id)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db = DatabaseFactory.CreateDatabase("baseDatosFarmacias");
            DbCommand dbCommand = db.GetStoredProcCommand("SP_PRES_PRESTACIONES_DEL");

            db.AddInParameter(dbCommand, "ID", DbType.Int32, id);


            db.ExecuteNonQuery(dbCommand);

        }
    }
}
