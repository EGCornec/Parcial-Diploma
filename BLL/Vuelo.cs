using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Vuelo
    {
        public List<Cliente> clientes = new List<Cliente>();
        public Vuelo()
        {

        }
        public Vuelo(string id, string destination, int first_class, int normal_class)
        {
            this.ID = id;
            this.destino = destination;
            this.asientos_first_class = first_class;
            this.asientos_normales = normal_class;
        }
        //private List<Cliente> _clientes;

        //public List<Cliente> clientes
        //{
        //    get { return _clientes; }
        //    set { _clientes = value; }
        //}
        private string _ID;

        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private string _destino;

        public string destino
        {
            get { return _destino; }
            set { _destino = value; }
        }

        private int _asientos_first_class;

        public int asientos_first_class
        {
            get { return _asientos_first_class; }
            set { _asientos_first_class = value; }
        }

        private int _asientos_normales;

        public int asientos_normales
        {
            get { return _asientos_normales; }
            set { _asientos_normales = value; }
        }

        public List<Vuelo> getVuelos()
        {
            List<Vuelo> vuelos = new List<Vuelo>();
            Manejador Mnj = new Manejador();
            DataTable Tabla = new DataTable();
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@nombre_tabla", "Vuelos");
            Tabla = Mnj.ConsultarLista("ConsultarTabla", sqlParameters);
            foreach (DataRow row in Tabla.Rows)
            {
                Vuelo nodo = new Vuelo(row[1].ToString(), row[2].ToString(), (int)row[3], (int)row[4]);
                vuelos.Add(nodo);
            }
            return vuelos;
        }
    }
}
