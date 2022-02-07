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
    public class ErrorDAL
    {
        public static Backline.Entidades.Error InsertarError(Backline.Entidades.Error error)
        {
            Database db = DatabaseFactory.CreateDatabase("baseDatosFarmacias");
            DbCommand dbCommand = db.GetStoredProcCommand("SP_ERR_ERRORES_INS");

            db.AddInParameter(dbCommand, "ID", DbType.Int32, error.Id != 0 ? error.Id : (object)null);
            db.AddInParameter(dbCommand, "EMP_ID", DbType.Int32, error.EmpId != 0 ? error.EmpId : (object)null);
            db.AddInParameter(dbCommand, "EST_ID", DbType.Int32, error.EstId != 0 ? error.EstId : (object)null);
            db.AddInParameter(dbCommand, "USR_ID", DbType.Int32, error.UsrId != 0 ? error.UsrId : (object)null);
            db.AddInParameter(dbCommand, "FECHA", DbType.DateTime, error.Fecha != DateTime.MinValue ? error.Fecha : (object)null);
            db.AddInParameter(dbCommand, "MODULO", DbType.String, error.Modulo != "" ? error.Modulo.ToUpper() : (object)null);
            db.AddInParameter(dbCommand, "DETALLE_ERROR", DbType.String, error.DetalleError != "" ? error.DetalleError.ToUpper() : (object)null);

            error.Id = int.Parse(db.ExecuteScalar(dbCommand).ToString());

            return error;

        }
    }
}
