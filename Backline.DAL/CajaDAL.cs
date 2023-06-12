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
    public class CajaDAL
    {
        public static Entidades.Caja InsertarCierreCaja(Entidades.Caja caja)
        {
            Database db = DatabaseFactory.CreateDatabase("baseDatosFarmacias");
            DbCommand dbCommand = db.GetStoredProcCommand("SP_CIE_CIERRE_CAJA_INS");

            db.AddInParameter(dbCommand, "ID", DbType.Int32, caja.Id != 0 ? caja.Id : (object)null);
            db.AddInParameter(dbCommand, "EMP_ID", DbType.Int32, caja.Emp_Id != 0 ? caja.Emp_Id : (object)null);
            db.AddInParameter(dbCommand, "EST_ID", DbType.Int32, caja.Est_Id != 0 ? caja.Est_Id : (object)null);
            db.AddInParameter(dbCommand, "USR_CAJERO_ID", DbType.Int32, caja.Id_Cajero != 0 ? caja.Id_Cajero : (object)null);
            db.AddInParameter(dbCommand, "FECHA_CIERRE", DbType.DateTime, caja.Fecha_Cierre != DateTime.MinValue ? caja.Fecha_Cierre : (object)null);
            db.AddInParameter(dbCommand, "FECHA_INT", DbType.Int32, caja.Fecha_Int != 0 ? caja.Fecha_Int : (object)null);
            db.AddInParameter(dbCommand, "TOTAL", DbType.Int32, caja.Total != 0 ? caja.Total : (object)null);
            db.AddInParameter(dbCommand, "OBSERVACION", DbType.String, caja.Observacion != null ? caja.Observacion : (object)null);
            db.AddInParameter(dbCommand, "USR_CIERRA_ID", DbType.Int32, caja.Id_Usr_Cierra_Caja != 0 ? caja.Id_Usr_Cierra_Caja : (object)null);
            db.AddInParameter(dbCommand, "ELIMINADO", DbType.Byte, caja.Eliminado == true ? 1 : 0);

            caja.Id = int.Parse(db.ExecuteScalar(dbCommand).ToString());


            return caja;

        }
        public static List<Entidades.Caja> ObtenerCuadreCaja(Entidades.Filtro filtro)
        {
            List<Entidades.Caja> lista = new List<Entidades.Caja>();
            Microsoft.Practices.EnterpriseLibrary.Data.Database db = DatabaseFactory.CreateDatabase("baseDatosFarmacias");
            DbCommand dbCommand = db.GetStoredProcCommand("SP_CUADRE_CAJA");

            db.AddInParameter(dbCommand, "FECHA_INGRESO", DbType.DateTime, filtro.Fecha_Ingreso != DateTime.MinValue ? filtro.Fecha_Ingreso : (object)null);
            db.AddInParameter(dbCommand, "USR_ID", DbType.Int32, filtro.UsrId != 0 ? filtro.UsrId : (object)null);

            IDataReader reader = (IDataReader)db.ExecuteReader(dbCommand);

            try
            {
                int TIPO_PAGO = reader.GetOrdinal("TIPO_PAGO");
                int SUMA = reader.GetOrdinal("SUMA");
                int TIDO_ID = reader.GetOrdinal("TIDO_ID");

                while (reader.Read())
                {
                    Entidades.Caja obj = new Entidades.Caja();
                    //BeginFields
                    obj.Total = (Int32)(!reader.IsDBNull(SUMA) ? reader.GetValue(SUMA) : 0);
                    obj.Tipo_Pago = (String)(!reader.IsDBNull(TIPO_PAGO) ? reader.GetValue(TIPO_PAGO) : string.Empty);
                    obj.Tido_Id = (int)(!reader.IsDBNull(TIDO_ID) ? reader.GetValue(TIDO_ID) : 0);
                    //EndFields

                    lista.Add(obj);
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

            //Restar notas de crédito
            //List<Entidades.Caja.CuadreCaja> listaNC = GetAll_NotasCredito(filtro);
            //if (listaNC != null && listaNC.Count > 0)
            //{
            //    Entidades.Caja.CuadreCaja itemNota = new Entidades.Caja.CuadreCaja();
            //    itemNota.Total = listaNC.Sum(d => d.Total);
            //    itemNota.MetodoPago = "Notas de Crédito";

            //    lista.Insert(lista.Count, itemNota);
            //}


            //if (listaNC.Count > 0)
            //{
            //    foreach (var a in listaNC)
            //    {
            //        foreach (var v in lista)
            //        {
            //            if (a.MetodoPago == v.MetodoPago)
            //            {
            //                v.Total = v.Total - (a.Total);
            //                break;
            //            }
            //        }
            //    }
            //}


            return lista;
        }
        public static List<Entidades.Caja> ObtenerCierresCajas(Entidades.Filtro filtro)
        {
            List<Entidades.Caja> lista = new List<Entidades.Caja>();
            Microsoft.Practices.EnterpriseLibrary.Data.Database db = DatabaseFactory.CreateDatabase("baseDatosFarmacias");
            DbCommand dbCommand = db.GetStoredProcCommand("SP_CIE_CIERRE_CAJA_LEER");

            db.AddInParameter(dbCommand, "ID", DbType.Int32, filtro.Id != 0 ? filtro.Id : (object)null);
            db.AddInParameter(dbCommand, "EMP_ID", DbType.Int32, filtro.EmpId != 0 ? filtro.EmpId : (object)null);
            db.AddInParameter(dbCommand, "EST_ID", DbType.Int32, filtro.EstId != 0 ? filtro.EstId : (object)null);
            db.AddInParameter(dbCommand, "USR_CAJERO_ID", DbType.Int32, filtro.UsrId_Caja != 0 ? filtro.UsrId_Caja : (object)null);
            db.AddInParameter(dbCommand, "FECHA_DESDE", DbType.DateTime, filtro.Desde != DateTime.MinValue ? filtro.Desde : (object)null);
            db.AddInParameter(dbCommand, "FECHA_HASTA", DbType.DateTime, filtro.Hasta != DateTime.MinValue ? filtro.Hasta : (object)null);

            IDataReader reader = (IDataReader)db.ExecuteReader(dbCommand);

            try
            {
                int ID = reader.GetOrdinal("ID");
                int EMP_ID = reader.GetOrdinal("EMP_ID");
                int EST_ID = reader.GetOrdinal("EST_ID");
                int ESTABLECIMIENTO = reader.GetOrdinal("ESTABLECIMIENTO");
                int USR_CAJERO_ID = reader.GetOrdinal("USR_CAJERO_ID");
                int CAJERO = reader.GetOrdinal("CAJERO");
                int FECHA_CIERRE = reader.GetOrdinal("FECHA_CIERRE");
                int TOTAL = reader.GetOrdinal("TOTAL");
                int USR_CIERRA_ID = reader.GetOrdinal("USR_CIERRA_ID");
                int OBSERVACION = reader.GetOrdinal("OBSERVACION");
                int USR_CERRADOR = reader.GetOrdinal("USR_CERRADOR");

                while (reader.Read())
                {
                    Entidades.Caja obj = new Entidades.Caja();
                    //BeginFields
                    obj.Id = (int)(!reader.IsDBNull(ID) ? reader.GetValue(ID) : 0);
                    obj.Emp_Id = (int)(!reader.IsDBNull(EMP_ID) ? reader.GetValue(EMP_ID) : 0);
                    obj.Est_Id = (int)(!reader.IsDBNull(EST_ID) ? reader.GetValue(EST_ID) : 0);
                    obj.Establecimiento = (String)(!reader.IsDBNull(ESTABLECIMIENTO) ? reader.GetValue(ESTABLECIMIENTO) : string.Empty);
                    obj.Id_Cajero = (int)(!reader.IsDBNull(USR_CAJERO_ID) ? reader.GetValue(USR_CAJERO_ID) : 0);
                    obj.Cajero = (String)(!reader.IsDBNull(CAJERO) ? reader.GetValue(CAJERO) : string.Empty);
                    obj.Fecha_Cierre = (DateTime)(!reader.IsDBNull(FECHA_CIERRE) ? reader.GetValue(FECHA_CIERRE) : DateTime.MinValue);
                    obj.Total = (int)(!reader.IsDBNull(TOTAL) ? reader.GetValue(TOTAL) : 0);
                    obj.Observacion = (String)(!reader.IsDBNull(OBSERVACION) ? reader.GetValue(OBSERVACION) : string.Empty);
                    obj.Id_Usr_Cierra_Caja = (int)(!reader.IsDBNull(USR_CIERRA_ID) ? reader.GetValue(USR_CIERRA_ID) : 0);
                    obj.Usr_Cierra_Caja_Str = (String)(!reader.IsDBNull(USR_CERRADOR) ? reader.GetValue(USR_CERRADOR) : string.Empty);
                    //EndFields

                    lista.Add(obj);
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
