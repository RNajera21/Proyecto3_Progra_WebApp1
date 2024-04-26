using Microsoft.Data.SqlClient;

namespace DataAccess.ACME
{
    public class Conexion
    {
        private readonly string? _cadenaConexion;

        public Conexion()
        {
            string? cadenaConexion;

            cadenaConexion = Environment.GetEnvironmentVariable("SQLServerXE");
            _cadenaConexion = cadenaConexion;
        }
        public SqlConnection Conectar()
        {
            SqlConnection sqlconn;
            sqlconn = new SqlConnection(_cadenaConexion);
            return sqlconn;
        }
    }
}
