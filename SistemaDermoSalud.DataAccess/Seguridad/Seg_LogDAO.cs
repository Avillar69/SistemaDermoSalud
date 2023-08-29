using SistemaDermoSalud.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.DataAccess
{
    public class Seg_LogDAO
    {
        public void UpdateInsert(SqlDataAdapter da, SqlConnection cn, int idEmpresa, int idUsuario, string Modulo, string Tabla, int idTabla, string Transaccion, string Origen = "SISTEMA", string Descripcion = "")
        {
            cn = new Conexion().conectar();
            if (cn.State == ConnectionState.Closed) { cn.Open(); }
            Seg_LogDTO oSeg_Log = new Seg_LogDTO();
            oSeg_Log.idEmpresa = idEmpresa;
            oSeg_Log.idUsuario = idUsuario;
            oSeg_Log.Modulo = Modulo;
            oSeg_Log.Tabla = Tabla;
            oSeg_Log.idTabla = idTabla;
            oSeg_Log.Transaccion = Transaccion;
            oSeg_Log.Origen = Origen;
            oSeg_Log.Descripcion = Descripcion;

            da = new SqlDataAdapter("SP_Seg_Log_UpdateInsert", cn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@idLog", oSeg_Log.idLog);
            da.SelectCommand.Parameters.AddWithValue("@idEmpresa", oSeg_Log.idEmpresa);
            da.SelectCommand.Parameters.AddWithValue("@idUsuario", oSeg_Log.idUsuario);
            da.SelectCommand.Parameters.AddWithValue("@Modulo", oSeg_Log.Modulo);
            da.SelectCommand.Parameters.AddWithValue("@Tabla", oSeg_Log.Tabla);
            da.SelectCommand.Parameters.AddWithValue("@idTabla", oSeg_Log.idTabla);
            da.SelectCommand.Parameters.AddWithValue("@Transaccion", oSeg_Log.Transaccion);
            da.SelectCommand.Parameters.AddWithValue("@Origen", oSeg_Log.Origen);
            da.SelectCommand.Parameters.AddWithValue("@Descripcion", oSeg_Log.Descripcion);
            int rpta = da.SelectCommand.ExecuteNonQuery();
        }

    }
}
