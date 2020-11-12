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
    public class Hotel
    {
        public Hotel()
        {

        }
        public List<Cliente> clientes = new List<Cliente>();
        //private List<Cliente> _clientes;

        //public List<Cliente> clientes
        //{
        //    get { return _clientes; }
        //    set { _clientes = value; }
        //}

        public Hotel(string id, string location, int rooms, string nombre)
        {
            this.ID = id;
            this.ubicacion = location;
            this.habitaciones = rooms;
            this.name = nombre;
        }

        private string _ID;

        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private string _ubicacion;

        public string ubicacion
        {
            get { return _ubicacion; }
            set { _ubicacion = value; }
        }

        private int _habitaciones;

        public int habitaciones
        {
            get { return _habitaciones; }
            set { _habitaciones = value; }
        }

        private string _name;

        public string name
        {
            get { return _name; }
            set { _name = value; }
        }


        public List<Hotel> getHoteles()
        {
            List<Hotel> hoteles = new List<Hotel>();
            Manejador Mnj = new Manejador();
            DataTable Tabla = new DataTable();
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@nombre_tabla", "Hoteles");
            Tabla = Mnj.ConsultarLista("ConsultarTabla", sqlParameters);
            foreach (DataRow row in Tabla.Rows)
            {
                Hotel nodo = new Hotel(row[1].ToString(),row[2].ToString(),(int)row[4],row[3].ToString());

                hoteles.Add(nodo);
            }
            return hoteles;
        }
    }
}
