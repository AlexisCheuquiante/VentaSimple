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
        public static List<Backline.Entidades.Usuario> ObtenerUsuario(Backline.Entidades.Usuario usuarios)
        {
            List<Backline.Entidades.Usuario> listaUsuarios = new List<Backline.Entidades.Usuario>();
            Database db = DatabaseFactory.CreateDatabase("baseDatosFarmacias");
            DbCommand dbCommand = db.GetStoredProcCommand("SP_USR_USUARIO_LEER");

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
                    OBJ.RutEmpresa = OBJ.RutEmpresa.Trim();
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

        public static void CambiarClave(Entidades.Usuario usuario)
        {
            Database db = DatabaseFactory.CreateDatabase("baseDatosFarmacias");
            DbCommand dbCommand = db.GetStoredProcCommand("SP_CAMBIO_CLAVE");

            db.AddInParameter(dbCommand, "ID", DbType.String, usuario.Id);
            db.AddInParameter(dbCommand, "CLAVE", DbType.String, usuario.Password);

            db.ExecuteNonQuery(dbCommand);
        }
    }
}
