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
    public class BitacoraDAL
    {
        public static Entidades.Bitacora InsertarBitacora(Entidades.Bitacora bitacora)
        {
            Database db = DatabaseFactory.CreateDatabase("baseDatosFarmacias");
            DbCommand dbCommand = db.GetStoredProcCommand("SP_BITA_BITACORA_INS");

            db.AddInParameter(dbCommand, "ID", DbType.Int32, bitacora.Id != 0 ? bitacora.Id : (object)null);
            db.AddInParameter(dbCommand, "EMP_ID", DbType.Int32, bitacora.Emp_Id != 0 ? bitacora.Emp_Id : (object)null);
            db.AddInParameter(dbCommand, "EST_ID", DbType.Int32, bitacora.Est_Id != 0 ? bitacora.Est_Id : (object)null);
            db.AddInParameter(dbCommand, "FECHA", DbType.DateTime, bitacora.Fecha != DateTime.MinValue ? bitacora.Fecha : (object)null);
            db.AddInParameter(dbCommand, "MOD_ID", DbType.Int32, bitacora.Mod_Id != 0 ? bitacora.Mod_Id : (object)null);
            db.AddInParameter(dbCommand, "OBSERVACION", DbType.String, bitacora.Observacion != null ? bitacora.Observacion : (object)null);
            db.AddInParameter(dbCommand, "USR_ID", DbType.Int32, bitacora.Usr_Id != 0 ? bitacora.Usr_Id : (object)null);
            db.AddInParameter(dbCommand, "ELIMINADO", DbType.Byte, bitacora.Eliminado == true ? 1 : 0);

            bitacora.Id = int.Parse(db.ExecuteScalar(dbCommand).ToString());

            return bitacora;
        }
        public static List<Backline.Entidades.Bitacora> ObtenerBitacora(Backline.Entidades.Filtro filtro)
        {
            List<Backline.Entidades.Bitacora> lista = new List<Backline.Entidades.Bitacora>();
            Database db = DatabaseFactory.CreateDatabase("baseDatosFarmacias");
            DbCommand dbCommand = db.GetStoredProcCommand("SP_BITA_BITACORA_LEER");

            db.AddInParameter(dbCommand, "EMP_ID", DbType.Int32, filtro.EmpId != 0 ? filtro.EmpId : (object)null);
            db.AddInParameter(dbCommand, "EST_ID", DbType.Int32, filtro.EstId != 0 ? filtro.EstId : (object)null);
            db.AddInParameter(dbCommand, "MOD_ID", DbType.Int32, filtro.Mod_Id != 0 ? filtro.Mod_Id : (object)null);
            db.AddInParameter(dbCommand, "USR_ID", DbType.Int32, filtro.UsrId != 0 ? filtro.UsrId : (object)null);
            db.AddInParameter(dbCommand, "FECHA_DESDE", DbType.DateTime, filtro.Desde != DateTime.MinValue ? filtro.Desde : (object)null);
            db.AddInParameter(dbCommand, "FECHA_HASTA", DbType.DateTime, filtro.Hasta != DateTime.MinValue ? filtro.Hasta : (object)null);

            IDataReader reader = (IDataReader)db.ExecuteReader(dbCommand);

            try
            {
                int ID = reader.GetOrdinal("ID");
                int EMP_ID = reader.GetOrdinal("EMP_ID");
                int EST_ID = reader.GetOrdinal("EST_ID");
                int ESTABLECIMIENTO = reader.GetOrdinal("ESTABLECIMIENTO");
                int FECHA = reader.GetOrdinal("FECHA");
                int MOD_ID = reader.GetOrdinal("MOD_ID");
                int MODULO = reader.GetOrdinal("MODULO");
                int OBSERVACION = reader.GetOrdinal("OBSERVACION");
                int USR_ID = reader.GetOrdinal("USR_ID");
                int USUARIO = reader.GetOrdinal("USUARIO");

                while (reader.Read())
                {
                    Backline.Entidades.Bitacora OBJ = new Backline.Entidades.Bitacora();
                    //BeginFields
                    OBJ.Id = (int)(!reader.IsDBNull(ID) ? reader.GetValue(ID) : 0);
                    OBJ.Emp_Id = (int)(!reader.IsDBNull(EMP_ID) ? reader.GetValue(EMP_ID) : 0);
                    OBJ.Est_Id = (int)(!reader.IsDBNull(EST_ID) ? reader.GetValue(EST_ID) : 0);
                    OBJ.Establecimiento = (String)(!reader.IsDBNull(ESTABLECIMIENTO) ? reader.GetValue(ESTABLECIMIENTO) : string.Empty);
                    OBJ.Fecha = (DateTime)(!reader.IsDBNull(FECHA) ? reader.GetValue(FECHA) : DateTime.MinValue);
                    OBJ.Mod_Id = (int)(!reader.IsDBNull(MOD_ID) ? reader.GetValue(MOD_ID) : 0);
                    OBJ.Modulo = (String)(!reader.IsDBNull(MODULO) ? reader.GetValue(MODULO) : string.Empty);
                    OBJ.Observacion = (String)(!reader.IsDBNull(OBSERVACION) ? reader.GetValue(OBSERVACION) : string.Empty);
                    OBJ.Usr_Id = (int)(!reader.IsDBNull(USR_ID) ? reader.GetValue(USR_ID) : 0);
                    OBJ.Usuario = (String)(!reader.IsDBNull(USUARIO) ? reader.GetValue(USUARIO) : string.Empty);
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
    }
}
