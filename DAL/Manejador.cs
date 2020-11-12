using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Manejador
    {
        SqlTransaction transaction;
        public DataTable ConsultarLista(String SP, SqlParameter[] sqlparameters)
        {
            Conexion conex = Conexion.PedirConexion();
            conex.ConectarBase();

            DataTable tabla = new DataTable();

            SqlCommand command = new SqlCommand();
            command.Connection = conex.connection;
            command.CommandText = SP; //Nombre del SP
            SqlDataAdapter adaptador = new SqlDataAdapter(command);
            adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (sqlparameters != null)
            {
                adaptador.SelectCommand.Parameters.AddRange(sqlparameters);
            }
            try
            {
                adaptador.Fill(tabla); //Llena datatable
                conex.closeConnection();
                return tabla;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                conex.closeConnection();
                throw;

            }
        }
    }
}
