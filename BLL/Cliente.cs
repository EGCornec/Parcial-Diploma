using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Cliente
    {
        public Cliente()
        {

        }
        public Cliente(string id, string name, string correo)
        {
            this.ID = id;
            this.nombre_completo = name;
            this.mail = correo;
        }
        private string _ID;

        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private string _nombre_completo;

        public string nombre_completo
        {
            get { return _nombre_completo; }
            set { _nombre_completo = value; }
        }

        private string _mail;

        public string mail
        {
            get { return _mail; }
            set { _mail = value; }
        }

    }
}
