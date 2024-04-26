using System.Data;
using Microsoft.Data.SqlClient;
using Models.ACME;

namespace DataAccess.ACME
{
    public class EmpresaDA
    {
        private Conexion _conexion = new Conexion();

        public void Insertar(EmpresaEntidad empresaEntidad)
        {
            SqlConnection sqlconn = _conexion.Conectar();
            SqlCommand sqlcomm = new SqlCommand();

            try
            {
                sqlconn.Open();
                sqlcomm.Connection = sqlconn;
                sqlcomm.CommandType = CommandType.StoredProcedure;
                sqlcomm.CommandText = "InsertarEmpresa";
                sqlcomm.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int)).Direction = ParameterDirection.Output;
                sqlcomm.Parameters.Add(new SqlParameter("@IDTipoEmpresa", empresaEntidad.IDTipoEmpresa));
                sqlcomm.Parameters.Add(new SqlParameter("@Empresa", empresaEntidad.Empresa));
                sqlcomm.Parameters.Add(new SqlParameter("@RUC", empresaEntidad.RUC));
                sqlcomm.Parameters.Add(new SqlParameter("@FechaCreacion", empresaEntidad.FechaCreacion));
                sqlcomm.Parameters.Add(new SqlParameter("@Presupuesto", empresaEntidad.Presupuesto));
                sqlcomm.Parameters.Add(new SqlParameter("@Activo", empresaEntidad.Activo));

                sqlcomm.ExecuteNonQuery();
                empresaEntidad.IDEmpresa = (int)sqlcomm.Parameters[sqlcomm.Parameters.IndexOf("@IDEmpresa")].Value;
                sqlconn.Close();
            }
            catch(Exception ex)
            {   
                throw new Exception("EmpresaDA.Insertar: " + ex.Message);
            }
            finally
            {
                sqlconn.Dispose();
            }
        }

        public void Modificar(EmpresaEntidad empresaEntidad)
        {
            SqlConnection sqlconn = _conexion.Conectar();
            SqlCommand sqlcomm = new SqlCommand();

            try
            {
                sqlconn.Open();
                sqlcomm.Connection = sqlconn;
                sqlcomm.CommandType = CommandType.StoredProcedure;
                sqlcomm.CommandText = "ModificarEmpresa";
                sqlcomm.Parameters.Add(new SqlParameter("@IDEmpresa", empresaEntidad.IDEmpresa));
                sqlcomm.Parameters.Add(new SqlParameter("@IDTipoEmpresa", empresaEntidad.IDTipoEmpresa));
                sqlcomm.Parameters.Add(new SqlParameter("@Empresa", empresaEntidad.Empresa));
                sqlcomm.Parameters.Add(new SqlParameter("@RUC", empresaEntidad.RUC));
                sqlcomm.Parameters.Add(new SqlParameter("@FechaCreacion", empresaEntidad.FechaCreacion));
                sqlcomm.Parameters.Add(new SqlParameter("@Presupuesto", empresaEntidad.Presupuesto));
                sqlcomm.Parameters.Add(new SqlParameter("@Activo", empresaEntidad.Activo));

                if (sqlcomm.ExecuteNonQuery() != 1)
                {
                    throw new Exception("EmpresaDA.Modificar: Problema al actualizar.");
                }
                sqlconn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("EmpresaDA.Modificar: " + ex.Message);
            }
            finally
            {
                sqlconn.Dispose();
            }
        }
        public void Borrar(EmpresaEntidad empresaEntidad)
        {
            SqlConnection sqlconn = _conexion.Conectar();
            SqlCommand sqlcomm = new SqlCommand();

            try
            {
                sqlconn.Open();
                sqlcomm.Connection = sqlconn;
                sqlcomm.CommandType = CommandType.StoredProcedure;
                sqlcomm.CommandText = "BorrarEmpresa";
                sqlcomm.Parameters.Add(new SqlParameter("@IDEmpresa", empresaEntidad.IDEmpresa));

                sqlcomm.ExecuteNonQuery();
                sqlconn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("EmpresaDA.Borrar: " + ex.Message);
            }
            finally
            {
                sqlconn.Dispose();
            }
        }

        public List<EmpresaEntidad> Listar()
        {
            SqlConnection sqlconn = _conexion.Conectar();
            SqlDataReader sqlDataRead;
            SqlCommand sqlcomm = new SqlCommand();

            List<EmpresaEntidad>? listaEmpresas = new List<EmpresaEntidad>();
            EmpresaEntidad? empresaEntidad;

            try
            {
                sqlconn.Open();
                sqlcomm.Connection = sqlconn;
                sqlcomm.CommandType = CommandType.StoredProcedure;
                sqlcomm.CommandText = "ListarEmpresa";

                sqlDataRead = sqlcomm.ExecuteReader();
                while (sqlDataRead.Read())
                {
                    empresaEntidad = new();
                    empresaEntidad.IDEmpresa = (int)sqlDataRead["IDEmpresa"];
                    empresaEntidad.IDTipoEmpresa = (int)sqlDataRead["IDTipoEmpresa"];
                    empresaEntidad.Empresa = sqlDataRead["Empresa"].ToString() ?? string.Empty;
                    empresaEntidad.Direccion = sqlDataRead["Direccion"].ToString() ?? string.Empty;
                    empresaEntidad.RUC = sqlDataRead["RUC"].ToString() ?? string.Empty;
                    if (sqlDataRead["FechaCreacion"] != DBNull.Value)
                    {
                        empresaEntidad.FechaCreacion = (DateTime)sqlDataRead["FechaCreacion"];
                    }
                    if (sqlDataRead["Presupuesto"] != DBNull.Value)
                    {
                        empresaEntidad.Presupuesto = (decimal)sqlDataRead["Presupuesto"];
                    }
                    empresaEntidad.Activo = (bool)sqlDataRead["Activo"];
                    listaEmpresas.Add(empresaEntidad);
                }

                sqlconn.Close();
                return listaEmpresas;
            }
            catch (Exception ex)
            {
                throw new Exception("EmpresaDA.ListarEmpresa: " + ex.Message);
            }
            finally
            {
                sqlconn.Dispose();
            }
        }

        public EmpresaEntidad BuscarID(int IDEmpresa)
        {
            SqlConnection sqlconn = _conexion.Conectar();
            SqlDataReader sqlDataRead;
            SqlCommand sqlcomm = new SqlCommand();


            EmpresaEntidad? empresaEntidad = null;

            try
            {
                sqlconn.Open();
                sqlcomm.Connection = sqlconn;
                sqlcomm.CommandType = CommandType.StoredProcedure;
                sqlcomm.CommandText = "BuscarEmpresa";

                sqlcomm.Parameters.Add(new SqlParameter("@IDEmpresa", IDEmpresa));

                sqlDataRead = sqlcomm.ExecuteReader();
                while (sqlDataRead.Read())
                {
                    empresaEntidad = new();
                    empresaEntidad.IDEmpresa = (int)sqlDataRead["IDEmpresa"];
                    empresaEntidad.IDTipoEmpresa = (int)sqlDataRead["IDTipoEmpresa"];
                    empresaEntidad.Empresa = sqlDataRead["Empresa"].ToString() ?? string.Empty;
                    empresaEntidad.Direccion = sqlDataRead["Direccion"].ToString() ?? string.Empty;
                    empresaEntidad.RUC = sqlDataRead["RUC"].ToString() ?? string.Empty;
                    if (sqlDataRead["FechaCreacion"] != DBNull.Value)
                    {
                        empresaEntidad.FechaCreacion = (DateTime)sqlDataRead["FechaCreacion"];
                    }
                    if (sqlDataRead["Presupuesto"] != DBNull.Value)
                    {
                        empresaEntidad.Presupuesto = (decimal)sqlDataRead["Presupuesto"];
                    }
                    empresaEntidad.Activo = (bool)sqlDataRead["Activo"];

                }

                sqlconn.Close();
                if (empresaEntidad == null)
                {
                    throw new Exception("EmpresaDA.BuscarID: El ID de Empresa, no existe");
                }

                return empresaEntidad;
            }
            catch (Exception ex)
            {
                throw new Exception("EmpresaDA.BuscarID: " + ex.Message);
            }
            finally
            {
                sqlconn.Dispose();
            }
        }
    }
}
