using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Conexion
    {
        private static Conexion instance;
        private static object mLock = new Object();



        private Conexion()
        {
        }

        ~Conexion()
        {

        }

        public SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-4STBNOH;Initial Catalog=ATERRIZAR;Integrated Security=True");



        public void ConectarBase()
        {
            try
            {
                if (connection.State == ConnectionState.Open) return;
                connection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        public void closeConnection()
        {
            try
            {
                if (connection.State == ConnectionState.Closed) return;

                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static Conexion PedirConexion()
        {
            lock (mLock)
            {
                if (instance == null) instance = new Conexion();
                return instance;
            }

        }
    }
}
