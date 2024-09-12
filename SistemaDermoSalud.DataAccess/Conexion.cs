using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.DataAccess
{
    public class Conexion
    {
        public SqlConnection conectar()
        {
            SqlConnection cn = new SqlConnection("Server=143.137.145.154;Database=BD_ENUA;User ID=sa;Password=gpsL1429x;Trusted_Connection=False;MultipleActiveResultSets=True");//BD para desarrollo Click Dermo
            //SqlConnection cn = new SqlConnection("Server=.\\SQLEXPRESS;Database=BD_HARIS_PROD;User ID=sa;Password=sql;Trusted_Connection=False;MultipleActiveResultSets=True");//BD para desarrollo Click Dermo

            return cn;
        }
    }
}
