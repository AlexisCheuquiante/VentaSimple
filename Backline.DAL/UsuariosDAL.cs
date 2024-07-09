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
    public class UsuariosDAL
    {
        public static Backline.Entidades.Usuario InsertarUsuario(Backline.Entidades.Usuario usuario)
        {
            Database db = DatabaseFactory.CreateDatabase("baseDatosFarmacias");
            DbCommand dbCommand = db.GetStoredProcCommand("SP_USR_USUARIO_INS");

            db.AddInParameter(dbCommand, "ID", DbType.Int32, usuario.Id != 0 ? usuario.Id : (object)null);
            db.AddInParameter(dbCommand, "NOMBRE", DbType.String, usuario.Nombre != "" ? usuario.Nombre : (object)null);
            db.AddInParameter(dbCommand, "USUARIO", DbType.String, usuario.NombreUsuario != "" ? usuario.NombreUsuario : (object)null);
            db.AddInParameter(dbCommand, "PASSWORD", DbType.String, usuario.Password != "" ? usuario.Password : (object)null);
            db.AddInParameter(dbCommand, "EMP_ID", DbType.Int32, usuario.Emp_Id != 0 ? usuario.Emp_Id : (object)null);
            db.AddInParameter(dbCommand, "EST_ID", DbType.Int32, usuario.Est_Id != 0 ? usuario.Est_Id : (object)null);
            db.AddInParameter(dbCommand, "ADMINISTRADOR", DbType.Byte, usuario.Administrador == true ? 1 : 0);
            db.AddInParameter(dbCommand, "ELIMINADO", DbType.Byte, usuario.Eliminado == true ? 1 : 0);
            db.AddInParameter(dbCommand, "CLAVE_AUTORIZACION", DbType.String, usuario.Clave_Autorizacion != "" ? usuario.Clave_Autorizacion : (object)null);

            usuario.Id = int.Parse(db.ExecuteScalar(dbCommand).ToString());


            return usuario;

        }
        public static List<Backline.Entidades.Usuario> ObtenerUsuario(Backline.Entidades.Usuario usuarios)
        {
            List<Backline.Entidades.Usuario> listaUsuarios = new List<Backline.Entidades.Usuario>();
            Database db = DatabaseFactory.CreateDatabase("baseDatosFarmacias");
            DbCommand dbCommand = db.GetStoredProcCommand("SP_USR_USUARIO_LEER_V2");

            db.AddInParameter(dbCommand, "ID", DbType.Int32, usuarios.Id != 0 ? usuarios.Id : (object)null);
            db.AddInParameter(dbCommand, "EMP_ID", DbType.Int32, usuarios.Emp_Id != 0 ? usuarios.Emp_Id : (object)null);
            db.AddInParameter(dbCommand, "EST_ID", DbType.Int32, usuarios.Est_Id != 0 ? usuarios.Est_Id : (object)null);
            db.AddInParameter(dbCommand, "USUARIO", DbType.String, usuarios.NombreUsuario != "" ? usuarios.NombreUsuario : (object)null);
            db.AddInParameter(dbCommand, "PASSWORD", DbType.String, usuarios.Password != "" ? usuarios.Password : (object)null);

            IDataReader reader = (IDataReader)db.ExecuteReader(dbCommand);

            try
            {
                int ID = reader.GetOrdinal("ID");
                int NOMBRE = reader.GetOrdinal("NOMBRE");
                int USUARIO = reader.GetOrdinal("USUARIO");
                int PASSWORD = reader.GetOrdinal("PASSWORD");
                int EMP_ID = reader.GetOrdinal("EMP_ID");
                int EMPRESA = reader.GetOrdinal("EMPRESA");
                int ESTABLECIMIENTO = reader.GetOrdinal("ESTABLECIMIENTO");
                int AFECTA_IVA = reader.GetOrdinal("AFECTA_IVA");
                int FACTURADOR = reader.GetOrdinal("FACTURADOR");
                int AMBIENTE = reader.GetOrdinal("AMBIENTE");
                int USUARIO_FE = reader.GetOrdinal("USUARIO_FE");
                int CLAVE_FE = reader.GetOrdinal("CLAVE_FE");
                int RUT_EMPRESA = reader.GetOrdinal("RUT_EMPRESA");
                int ADMINISTRADOR = reader.GetOrdinal("ADMINISTRADOR");
                int EST_ID = reader.GetOrdinal("EST_ID");
                int ALIAS = reader.GetOrdinal("ALIAS");
                int OCUPA_RUT = reader.GetOrdinal("OCUPA_RUT");
                int CLAVE_AUTORIZACION = reader.GetOrdinal("CLAVE_AUTORIZACION");
                int BASIC_AUTH = reader.GetOrdinal("BASIC_AUTH");
                int OCUPA_PRESTACIONES = reader.GetOrdinal("OCUPA_PRESTACIONES");
                int OCUPA_CLAVE_AUTORIZACION = reader.GetOrdinal("OCUPA_CLAVE_AUTORIZACION");
                int PUEDE_EMITIR_NOTA_CREDITO = reader.GetOrdinal("PUEDE_EMITIR_NOTA_CREDITO");
                int GIRO_IMPRESO = reader.GetOrdinal("GIRO_IMPRESO");
                int DIRECCION_SUCURSAL = reader.GetOrdinal("DIRECCION_SUCURSAL");
                int COMUNA = reader.GetOrdinal("COMUNA");
                int CIUDAD = reader.GetOrdinal("CIUDAD");
                int SELECCIONA_TIPO_BOLETA = reader.GetOrdinal("SELECCIONA_TIPO_BOLETA");

                while (reader.Read())
                {
                    Backline.Entidades.Usuario OBJ = new Backline.Entidades.Usuario();
                    //BeginFields
                    OBJ.Id = (int)(!reader.IsDBNull(ID) ? reader.GetValue(ID) : 0);
                    OBJ.Nombre = (String)(!reader.IsDBNull(NOMBRE) ? reader.GetValue(NOMBRE) : string.Empty);
                    OBJ.NombreUsuario = (String)(!reader.IsDBNull(USUARIO) ? reader.GetValue(USUARIO) : string.Empty);
                    OBJ.Password = (String)(!reader.IsDBNull(PASSWORD) ? reader.GetValue(PASSWORD) : string.Empty);
                    OBJ.Emp_Id = (int)(!reader.IsDBNull(EMP_ID) ? reader.GetValue(EMP_ID) : 0);
                    OBJ.NombreEmpresa = (String)(!reader.IsDBNull(EMPRESA) ? reader.GetValue(EMPRESA) : string.Empty);
                    OBJ.NombreEstablecimiento = (String)(!reader.IsDBNull(ESTABLECIMIENTO) ? reader.GetValue(ESTABLECIMIENTO) : string.Empty);
                    OBJ.EsAfecta = (bool)(!reader.IsDBNull(AFECTA_IVA) ? reader.GetValue(AFECTA_IVA) : false);
                    OBJ.Facturador = (String)(!reader.IsDBNull(FACTURADOR) ? reader.GetValue(FACTURADOR) : string.Empty);
                    OBJ.Ambiente = (String)(!reader.IsDBNull(AMBIENTE) ? reader.GetValue(AMBIENTE) : string.Empty);
                    OBJ.Usuario_FE = (String)(!reader.IsDBNull(USUARIO_FE) ? reader.GetValue(USUARIO_FE) : string.Empty);
                    OBJ.Clave_FE = (String)(!reader.IsDBNull(CLAVE_FE) ? reader.GetValue(CLAVE_FE) : string.Empty);
                    OBJ.RutEmpresa = (String)(!reader.IsDBNull(RUT_EMPRESA) ? reader.GetValue(RUT_EMPRESA) : string.Empty);
                    OBJ.Administrador = (bool)(!reader.IsDBNull(ADMINISTRADOR) ? reader.GetValue(ADMINISTRADOR) : false);
                    OBJ.Est_Id = (int)(!reader.IsDBNull(EST_ID) ? reader.GetValue(EST_ID) : 0);
                    OBJ.AliasEmpresa = (String)(!reader.IsDBNull(ALIAS) ? reader.GetValue(ALIAS) : string.Empty);
                    OBJ.OcupaRut = (bool)(!reader.IsDBNull(OCUPA_RUT) ? reader.GetValue(OCUPA_RUT) : false);
                    OBJ.RutEmpresa = OBJ.RutEmpresa.Trim();
                    OBJ.Clave_Autorizacion = (String)(!reader.IsDBNull(CLAVE_AUTORIZACION) ? reader.GetValue(CLAVE_AUTORIZACION) : string.Empty);
                    OBJ.Basic_Auth = (String)(!reader.IsDBNull(BASIC_AUTH) ? reader.GetValue(BASIC_AUTH) : string.Empty);
                    OBJ.Ocupa_Prestaciones = (bool)(!reader.IsDBNull(OCUPA_PRESTACIONES) ? reader.GetValue(OCUPA_PRESTACIONES) : false);
                    OBJ.Ocupa_Clave_Autorizacion = (bool)(!reader.IsDBNull(OCUPA_CLAVE_AUTORIZACION) ? reader.GetValue(OCUPA_CLAVE_AUTORIZACION) : false);
                    OBJ.Puede_Emitir_Nota = (bool)(!reader.IsDBNull(PUEDE_EMITIR_NOTA_CREDITO) ? reader.GetValue(PUEDE_EMITIR_NOTA_CREDITO) : false);
                    OBJ.Giro_Impreso = (String)(!reader.IsDBNull(GIRO_IMPRESO) ? reader.GetValue(GIRO_IMPRESO) : string.Empty);
                    OBJ.Direccion_Sucursal = (String)(!reader.IsDBNull(DIRECCION_SUCURSAL) ? reader.GetValue(DIRECCION_SUCURSAL) : string.Empty);
                    OBJ.Comuna = (String)(!reader.IsDBNull(COMUNA) ? reader.GetValue(COMUNA) : string.Empty);
                    OBJ.Ciudad = (String)(!reader.IsDBNull(CIUDAD) ? reader.GetValue(CIUDAD) : string.Empty);
                    OBJ.Selecciona_Tipo_Boleta = (bool)(!reader.IsDBNull(SELECCIONA_TIPO_BOLETA) ? reader.GetValue(SELECCIONA_TIPO_BOLETA) : false);
                    //EndFields

                    listaUsuarios.Add(OBJ);
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

            return listaUsuarios;

        }
        public static void EliminarUsuario(int id)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db = DatabaseFactory.CreateDatabase("baseDatosFarmacias");
            DbCommand dbCommand = db.GetStoredProcCommand("SP_USR_USUARIO_DEL");

            db.AddInParameter(dbCommand, "ID", DbType.Int32, id);


            db.ExecuteNonQuery(dbCommand);

        }
        public static void CambiarClave(Entidades.Usuario usuario)
        {
            Database db = DatabaseFactory.CreateDatabase("baseDatosFarmacias");
            DbCommand dbCommand = db.GetStoredProcCommand("SP_CAMBIO_CLAVE");

            db.AddInParameter(dbCommand, "ID", DbType.String, usuario.Id);
            db.AddInParameter(dbCommand, "CLAVE", DbType.String, usuario.Password);

            db.ExecuteNonQuery(dbCommand);
        }
        public static List<Backline.Entidades.Usuario> ValidarClaveAutorizacion(Backline.Entidades.Filtro filtro)
        {
            List<Backline.Entidades.Usuario> listaUsuarios = new List<Backline.Entidades.Usuario>();
            Database db = DatabaseFactory.CreateDatabase("baseDatosFarmacias");
            DbCommand dbCommand = db.GetStoredProcCommand("SP_VALIDA_CLAVE_AUTORIZACION");

            db.AddInParameter(dbCommand, "ID", DbType.Int32, filtro.UsrId != 0 ? filtro.UsrId : (object)null);
            db.AddInParameter(dbCommand, "EMP_ID", DbType.Int32, filtro.EmpId != 0 ? filtro.EmpId : (object)null);
            db.AddInParameter(dbCommand, "ADMINISTRADOR", DbType.Byte, filtro.EsAdministrador == true ? 1 : 0);
            db.AddInParameter(dbCommand, "CLAVE_AUTORIZACION", DbType.String, filtro.Clave_Autorizacion != "" ? filtro.Clave_Autorizacion : (object)null);

            IDataReader reader = (IDataReader)db.ExecuteReader(dbCommand);

            try
            {
                int ID = reader.GetOrdinal("ID");
                int NOMBRE = reader.GetOrdinal("NOMBRE");

                while (reader.Read())
                {
                    Backline.Entidades.Usuario OBJ = new Backline.Entidades.Usuario();
                    //BeginFields
                    OBJ.Id = (int)(!reader.IsDBNull(ID) ? reader.GetValue(ID) : 0);
                    OBJ.Nombre = (String)(!reader.IsDBNull(NOMBRE) ? reader.GetValue(NOMBRE) : string.Empty);
                    //EndFields

                    listaUsuarios.Add(OBJ);
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

            return listaUsuarios;

        }
        public static List<Backline.Entidades.Usuario> ObtenerDatos(Backline.Entidades.Filtro filtro)
        {
            List<Backline.Entidades.Usuario> listaUsuarios = new List<Backline.Entidades.Usuario>();
            Database db = DatabaseFactory.CreateDatabase("baseDatosFarmacias");
            DbCommand dbCommand = db.GetStoredProcCommand("SP_DATOS_BOLETA_LEER");

            db.AddInParameter(dbCommand, "ID", DbType.Int32, filtro.Id != 0 ? filtro.Id : (object)null);

            IDataReader reader = (IDataReader)db.ExecuteReader(dbCommand);

            try
            {
                int AFECTA_IVA = reader.GetOrdinal("AFECTA_IVA");
                int FACTURADOR = reader.GetOrdinal("FACTURADOR");

                while (reader.Read())
                {
                    Backline.Entidades.Usuario OBJ = new Backline.Entidades.Usuario();
                    //BeginFields
                    OBJ.EsAfecta = (bool)(!reader.IsDBNull(AFECTA_IVA) ? reader.GetValue(AFECTA_IVA) : false);
                    OBJ.Facturador = (String)(!reader.IsDBNull(FACTURADOR) ? reader.GetValue(FACTURADOR) : string.Empty);
                    //EndFields

                    listaUsuarios.Add(OBJ);
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

            return listaUsuarios;

        }
    }
}
