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
    public class BoletaDAL
    {
        public static Backline.Entidades.Factura InsertarFactura(Backline.Entidades.Factura factura)
        {
            Database db = DatabaseFactory.CreateDatabase("baseDatosFarmacias");
            DbCommand dbCommand = db.GetStoredProcCommand("SP_BOL_BOLETAS_INS");

            db.AddInParameter(dbCommand, "CONT_ID", DbType.Int32, factura.ContId != 0 ? factura.ContId : (object)null);
            db.AddInParameter(dbCommand, "NUMERO", DbType.Int32, factura.NumeroSII != 0 ? factura.NumeroSII : (object)null);
            db.AddInParameter(dbCommand, "EMP_ID", DbType.Int32, factura.EmpId != 0 ? factura.EmpId : (object)null);
            db.AddInParameter(dbCommand, "FECHA", DbType.DateTime, factura.Fecha != DateTime.MinValue ? factura.Fecha : (object)null);
            db.AddInParameter(dbCommand, "GLOSA", DbType.String, factura.Glosa != "" ? factura.Glosa.ToUpper() : (object)null);
            db.AddInParameter(dbCommand, "TOTAL", DbType.Int32, factura.Total != 0 ? factura.Total : (object)null);
            db.AddInParameter(dbCommand, "USR_ID", DbType.Int32, factura.Usr_Id != 0 ? factura.Usr_Id : (object)null);
            db.AddInParameter(dbCommand, "EST_ID", DbType.Int32, factura.EstId != 0 ? factura.EstId : (object)null);
            db.AddInParameter(dbCommand, "TIPA_ID", DbType.Int32, factura.Tipa_Id != 0 ? factura.Tipa_Id : (object)null);
            db.ExecuteNonQuery(dbCommand);


            return factura;

        }
        public static List<Backline.Entidades.Factura> ObtenerFactura(Backline.Entidades.Filtro filtro)
        {
            List<Backline.Entidades.Factura> listaFacturas = new List<Backline.Entidades.Factura>();
            Database db = DatabaseFactory.CreateDatabase("baseDatosFarmacias");
            DbCommand dbCommand = db.GetStoredProcCommand("SP_BOL_BOLETAS_LEER");

            db.AddInParameter(dbCommand, "ID", DbType.Int32, filtro.BoletaId != 0 ? filtro.BoletaId : (object)null);
            db.AddInParameter(dbCommand, "FECHA_DESDE", DbType.DateTime, filtro.Desde != DateTime.MinValue ? filtro.Desde : (object)null);
            db.AddInParameter(dbCommand, "FECHA_HASTA", DbType.DateTime, filtro.Hasta != DateTime.MinValue ? filtro.Hasta : (object)null);
            db.AddInParameter(dbCommand, "EMP_ID", DbType.Int32, filtro.EmpId);
            db.AddInParameter(dbCommand, "EST_ID", DbType.Int32, filtro.EstId != 0 ? filtro.EstId : (object) null );

            IDataReader reader = (IDataReader)db.ExecuteReader(dbCommand);

            try
            {
                int ID = reader.GetOrdinal("ID");
                int CONT_ID = reader.GetOrdinal("CONT_ID");
                int NUMERO = reader.GetOrdinal("NUMERO");
                int EMP_ID = reader.GetOrdinal("EMP_ID");
                int FECHA = reader.GetOrdinal("FECHA");
                int GLOSA = reader.GetOrdinal("GLOSA");
                int TOTAL = reader.GetOrdinal("TOTAL");
                int USR_ID = reader.GetOrdinal("USR_ID");
                int CONTRIBUYENTE = reader.GetOrdinal("CONTRIBUYENTE");
                int USUARIO = reader.GetOrdinal("USUARIO");
                int RUT = reader.GetOrdinal("RUT");
                int ESTABLECIMIENTO = reader.GetOrdinal("ESTABLECIMIENTO");
                int TIPO_PAGO = reader.GetOrdinal("TIPO_PAGO");
                int TIPA_ID = reader.GetOrdinal("TIPA_ID");
                int TIDO_ID = reader.GetOrdinal("TIDO_ID");
                int TIPO_DOCUMENTO = reader.GetOrdinal("TIPO_DOCUMENTO");

                while (reader.Read())
                {
                    Backline.Entidades.Factura OBJ = new Backline.Entidades.Factura();
                    //BeginFields
                    OBJ.Id = (int)(!reader.IsDBNull(ID) ? reader.GetValue(ID) : 0);
                    OBJ.ContId = (int)(!reader.IsDBNull(CONT_ID) ? reader.GetValue(CONT_ID) : 0);
                    OBJ.NumeroSII = (int)(!reader.IsDBNull(NUMERO) ? reader.GetValue(NUMERO) : 0);
                    OBJ.EmpId = (int)(!reader.IsDBNull(EMP_ID) ? reader.GetValue(EMP_ID) : 0);
                    OBJ.Fecha = (DateTime)(!reader.IsDBNull(FECHA) ? reader.GetValue(FECHA) : DateTime.MinValue);
                    OBJ.Glosa = (String)(!reader.IsDBNull(GLOSA) ? reader.GetValue(GLOSA) : string.Empty);
                    OBJ.Total = (int)(!reader.IsDBNull(TOTAL) ? reader.GetValue(TOTAL) : 0);
                    OBJ.Usr_Id = (int)(!reader.IsDBNull(USR_ID) ? reader.GetValue(USR_ID) : 0);
                    OBJ.Rut = (String)(!reader.IsDBNull(RUT) ? reader.GetValue(RUT) : string.Empty);
                    OBJ.Contribuyente = (String)(!reader.IsDBNull(CONTRIBUYENTE) ? reader.GetValue(CONTRIBUYENTE) : string.Empty);
                    OBJ.Usuario = (String)(!reader.IsDBNull(USUARIO) ? reader.GetValue(USUARIO) : string.Empty);
                    OBJ.Sucursal = (String)(!reader.IsDBNull(ESTABLECIMIENTO) ? reader.GetValue(ESTABLECIMIENTO) : string.Empty);
                    OBJ.TipoPago = (String)(!reader.IsDBNull(TIPO_PAGO) ? reader.GetValue(TIPO_PAGO) : string.Empty);
                    OBJ.Tipa_Id = (int)(!reader.IsDBNull(TIPA_ID) ? reader.GetValue(TIPA_ID) : 0);
                    OBJ.Tido_Id = (int)(!reader.IsDBNull(TIDO_ID) ? reader.GetValue(TIDO_ID) : 0);
                    OBJ.TipoDocumentoStr = (String)(!reader.IsDBNull(TIPO_DOCUMENTO) ? reader.GetValue(TIPO_DOCUMENTO) : string.Empty);

                    //EndFields

                    listaFacturas.Add(OBJ);
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

            return listaFacturas;

        }
        public static List<Backline.Entidades.Factura> ObtenerBoleta(Backline.Entidades.Filtro filtro)
        {
            List<Backline.Entidades.Factura> listaFacturas = new List<Backline.Entidades.Factura>();
            Database db = DatabaseFactory.CreateDatabase("baseDatosFarmacias");
            DbCommand dbCommand = db.GetStoredProcCommand("SP_BOL_BOLETAS_GET");

            db.AddInParameter(dbCommand, "ID", DbType.Int32, filtro.BoletaId != 0 ? filtro.BoletaId : (object)null);

            IDataReader reader = (IDataReader)db.ExecuteReader(dbCommand);

            try
            {
                int ID = reader.GetOrdinal("ID");
                int CONT_ID = reader.GetOrdinal("CONT_ID");
                int NUMERO = reader.GetOrdinal("NUMERO");
                int EMP_ID = reader.GetOrdinal("EMP_ID");
                int FECHA = reader.GetOrdinal("FECHA");
                int GLOSA = reader.GetOrdinal("GLOSA");
                int TOTAL = reader.GetOrdinal("TOTAL");
                int USR_ID = reader.GetOrdinal("USR_ID");
                int CONTRIBUYENTE = reader.GetOrdinal("CONTRIBUYENTE");
                int USUARIO = reader.GetOrdinal("USUARIO");
                int RUT = reader.GetOrdinal("RUT");
                int ESTABLECIMIENTO = reader.GetOrdinal("ESTABLECIMIENTO");
                int TIPO_PAGO = reader.GetOrdinal("TIPO_PAGO");
                int TIPA_ID = reader.GetOrdinal("TIPA_ID");
                int TIPO_DOCUMENTO = reader.GetOrdinal("TIPO_DOCUMENTO");

                while (reader.Read())
                {
                    Backline.Entidades.Factura OBJ = new Backline.Entidades.Factura();
                    //BeginFields
                    OBJ.Id = (int)(!reader.IsDBNull(ID) ? reader.GetValue(ID) : 0);
                    OBJ.ContId = (int)(!reader.IsDBNull(CONT_ID) ? reader.GetValue(CONT_ID) : 0);
                    OBJ.Numero = (int)(!reader.IsDBNull(NUMERO) ? reader.GetValue(NUMERO) : 0);
                    OBJ.EmpId = (int)(!reader.IsDBNull(EMP_ID) ? reader.GetValue(EMP_ID) : 0);
                    OBJ.Fecha = (DateTime)(!reader.IsDBNull(FECHA) ? reader.GetValue(FECHA) : DateTime.MinValue);
                    OBJ.Glosa = (String)(!reader.IsDBNull(GLOSA) ? reader.GetValue(GLOSA) : string.Empty);
                    OBJ.Total = (int)(!reader.IsDBNull(TOTAL) ? reader.GetValue(TOTAL) : 0);
                    OBJ.Usr_Id = (int)(!reader.IsDBNull(USR_ID) ? reader.GetValue(USR_ID) : 0);
                    OBJ.Rut = (String)(!reader.IsDBNull(RUT) ? reader.GetValue(RUT) : string.Empty);
                    OBJ.Contribuyente = (String)(!reader.IsDBNull(CONTRIBUYENTE) ? reader.GetValue(CONTRIBUYENTE) : string.Empty);
                    OBJ.Usuario = (String)(!reader.IsDBNull(USUARIO) ? reader.GetValue(USUARIO) : string.Empty);
                    OBJ.Sucursal = (String)(!reader.IsDBNull(ESTABLECIMIENTO) ? reader.GetValue(ESTABLECIMIENTO) : string.Empty);
                    OBJ.TipoPago = (String)(!reader.IsDBNull(TIPO_PAGO) ? reader.GetValue(TIPO_PAGO) : string.Empty);
                    OBJ.Tipa_Id = (int)(!reader.IsDBNull(TIPA_ID) ? reader.GetValue(TIPA_ID) : 0);
                    OBJ.TipoDocumentoStr = (String)(!reader.IsDBNull(TIPO_DOCUMENTO) ? reader.GetValue(TIPO_DOCUMENTO) : string.Empty);
                    //EndFields

                    listaFacturas.Add(OBJ);
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

            return listaFacturas;

        }
        public static Backline.Entidades.Factura InsertarNumeroDocumento(Backline.Entidades.Factura facturaNumero)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db = DatabaseFactory.CreateDatabase("baseDatosFarmacias");
            DbCommand dbCommand = db.GetStoredProcCommand("SP_BOL_BOLETAS_UPDATE_NSII");

            db.AddInParameter(dbCommand, "ID", DbType.Int32, facturaNumero.Id != 0 ? facturaNumero.Id : (object)null);
            db.AddInParameter(dbCommand, "NUMERO", DbType.Int32, facturaNumero.Numero != 0 ? facturaNumero.Numero : (object)null);

            db.ExecuteNonQuery(dbCommand);

            return facturaNumero;

        }
        public static Backline.Entidades.Factura InsertarFacturaV2(Backline.Entidades.Factura factura)
        {
            Database db = DatabaseFactory.CreateDatabase("baseDatosFarmacias");
            DbCommand dbCommand = db.GetStoredProcCommand("SP_BOL_BOLETAS_INS_V2");

            db.AddInParameter(dbCommand, "ID", DbType.Int32, factura.Id != 0 ? factura.Id : (object)null);
            db.AddInParameter(dbCommand, "CONT_ID", DbType.Int32, factura.ContId != 0 ? factura.ContId : (object)null);
            db.AddInParameter(dbCommand, "TIDO_ID", DbType.Int32, factura.Tido_Id != 0 ? factura.Tido_Id : (object)null);
            db.AddInParameter(dbCommand, "EMP_ID", DbType.Int32, factura.EmpId != 0 ? factura.EmpId : (object)null);
            db.AddInParameter(dbCommand, "FECHA", DbType.DateTime, factura.Fecha != DateTime.MinValue ? factura.Fecha : (object)null);
            db.AddInParameter(dbCommand, "GLOSA", DbType.String, factura.Glosa != "" ? factura.Glosa.ToUpper() : (object)null);
            db.AddInParameter(dbCommand, "TOTAL", DbType.Int32, factura.Total != 0 ? factura.Total : (object)null);
            db.AddInParameter(dbCommand, "USR_ID", DbType.Int32, factura.Usr_Id != 0 ? factura.Usr_Id : (object)null);
            db.AddInParameter(dbCommand, "EST_ID", DbType.Int32, factura.EstId != 0 ? factura.EstId : (object)null);
            db.AddInParameter(dbCommand, "TIPA_ID", DbType.Int32, factura.Tipa_Id != 0 ? factura.Tipa_Id : (object)null);
            db.AddInParameter(dbCommand, "ELIMINADO", DbType.Byte, factura.Eliminado == true ? 1 : 0);

            factura.Id = int.Parse(db.ExecuteScalar(dbCommand).ToString());

            return factura;

        }
    }
}
