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
            SqlConnection cn = new SqlConnection("Server=.\\SQLEXPRESS;Database=BD_ENUA;User ID=sa;Password=sql;Trusted_Connection=False;MultipleActiveResultSets=True");//BD para desarrollo Click Dermo
            //SqlConnection cn = new SqlConnection("Server=186.64.122.205;Database=Dermosalud;User ID=sa;Password=sqlclick;Trusted_Connection=False;MultipleActiveResultSets=True");
            //SqlConnection cn = new SqlConnection("Server=192.168.1.96;Database=Dermosalud;User ID=sa;Password=C3s4r4r4n4;Trusted_Connection=False;MultipleActiveResultSets=True");//estaba aqui
            //SqlConnection cn = new SqlConnection("Server=.\\SQLEXPRESS;Database=Dermosalud;User ID=sa;Password=sql;Trusted_Connection=False;MultipleActiveResultSets=True");
            return cn;
        }
    }
}
