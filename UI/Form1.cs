using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using Newtonsoft.Json;

namespace UI
{
    public partial class Form1 : Form
    {
        Hotel hotel;
        Vuelo vuelo;
        List<Hotel> hoteles = new List<Hotel>();
        List<Vuelo> vuelos = new List<Vuelo>();
        private static string path_vuelos = @"C:\temp\testJson\Suscripciones_Vuelos.json";
        private static string path_hoteles = @"C:\temp\testJson\Suscripciones_Hoteles.json";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmb_productos.Items.Add("Vuelos");
            cmb_productos.Items.Add("Hoteles");
            if(File.Exists(path_vuelos))
            { }
            else
            {
                string vuelos_serie = JsonConvert.SerializeObject(vuelos);
                File.WriteAllText(path_vuelos, vuelos_serie);
            }
            if (File.Exists(path_hoteles))
            { }
            else
            {
                string hoteles_serie = JsonConvert.SerializeObject(hoteles);
                File.WriteAllText(path_hoteles, hoteles_serie);
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void cmb_productos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmb_productos.SelectedIndex == 0)
            {
                vuelo = new Vuelo();
                vuelos = vuelo.getVuelos();
                dgv_productos.DataSource = null;
                dgv_productos.DataSource = vuelos;
            }
            else if(cmb_productos.SelectedIndex == 1)
            {
                hotel = new Hotel();
                hoteles = hotel.getHoteles();
                dgv_productos.DataSource = null;
                dgv_productos.DataSource = hoteles;
            }
        }

        private void btn_suscripcion_Click(object sender, EventArgs e)
        {
            Cliente cliente = crearCliente(txt_nombre.Text, txt_correo.Text);
            serializar_suscripciones(cmb_productos.SelectedIndex, cliente);
            txt_nombre.Text = ""; 
            txt_correo.Text = "";
        }

        private Cliente crearCliente(string name, string mail)
        {
            Cliente cliente = new Cliente(name + mail, name, mail);
            return cliente;
        }

        private void serializar_suscripciones(int selectedIndex, Cliente cliente)
        {
            string deserializado = deserializar_productos(selectedIndex);
            if (selectedIndex == 0)
            {
                List<Vuelo> vuelos_desserializar = JsonConvert.DeserializeObject<List<Vuelo>>(deserializado);
                foreach (DataGridViewRow item in this.dgv_productos.SelectedRows) //Recorro los items seleccionados del DGV
                {
                    Vuelo selected = new Vuelo(item.Cells[0].Value.ToString(), item.Cells[1].Value.ToString(), (int)item.Cells[2].Value, (int)item.Cells[3].Value); //Parseo el objeto
                    
                    if (vuelos_desserializar.Find(x => x.ID.Equals(selected.ID)) != null) //Reviso si mi objeto existe en la lista serializada.
                    {

                        if (vuelos_desserializar.Find(x => x.ID.Equals(selected.ID)).clientes.Find(x => x.mail.Equals(cliente.mail)) != null) //Reviso si ya estaba suscripto.
                        {
                            MessageBox.Show("Usted ya estaba suscripto/a a este producto.");
                        }
                        else
                        {
                            vuelos_desserializar.Find(x => x.ID.Equals(selected.ID)).clientes.Add(cliente); //Se suscribe correctamente.
                            MessageBox.Show("Felicidades, usted ya está suscripto/a a este producto.","Suscripción");
                        }
                    }
                    else
                    {
                        if(selected.clientes.Find(x => x.ID.Equals(cliente.mail)) != null)
                        {
                            MessageBox.Show("Usted ya estaba suscripto/a a este producto.","Error");
                        }
                        else
                        {
                            selected.clientes.Add(cliente);
                            MessageBox.Show("Felicidades, usted ya está suscripto/a a este producto.", "Suscripción");
                        }
                        vuelos_desserializar.Add(selected);
                    }
                }
                string test2 = JsonConvert.SerializeObject(vuelos_desserializar);
                File.WriteAllText(path_vuelos, test2);
            }
            else if(selectedIndex == 1)
            {
                List<Hotel> hoteles_desserializar = JsonConvert.DeserializeObject<List<Hotel>>(deserializado);
                foreach (DataGridViewRow item in this.dgv_productos.SelectedRows) //Recorro los items seleccionados del DGV
                {
                    Hotel selected = new Hotel(item.Cells[0].Value.ToString(), item.Cells[1].Value.ToString(), (int)item.Cells[2].Value); //Parseo el objeto

                    if (hoteles_desserializar.Find(x => x.ID.Equals(selected.ID)) != null) //Reviso si mi objeto existe en la lista serializada.
                    {

                        if (hoteles_desserializar.Find(x => x.ID.Equals(selected.ID)).clientes.Find(x => x.mail.Equals(cliente.mail)) != null) //Reviso si ya estaba suscripto.
                        {
                            MessageBox.Show("Usted ya estaba suscripto/a a este producto.");
                        }
                        else
                        {
                            hoteles_desserializar.Find(x => x.ID.Equals(selected.ID)).clientes.Add(cliente); //Se suscribe correctamente.
                            MessageBox.Show("Felicidades, usted ya está suscripto/a a este producto.", "Suscripción");
                        }
                    }
                    else
                    {
                        if (selected.clientes.Find(x => x.ID.Equals(cliente.mail)) != null)
                        {
                            MessageBox.Show("Usted ya estaba suscripto/a a este producto.", "Error");
                        }
                        else
                        {
                            selected.clientes.Add(cliente);
                            MessageBox.Show("Felicidades, usted ya está suscripto/a a este producto.", "Suscripción");
                        }
                        hoteles_desserializar.Add(selected);
                    }
                }
                string test2 = JsonConvert.SerializeObject(hoteles_desserializar);
                File.WriteAllText(path_hoteles, test2);
            }
        }

        private string deserializar_productos(int selectedIndex)
        {
            string deserializado;
            if(selectedIndex == 0)
            {
                using (var reader = new StreamReader(path_vuelos))
                {
                    deserializado = reader.ReadToEnd();
                }
            }
            else
            {
                using (var reader = new StreamReader(path_hoteles))
                {
                    deserializado = reader.ReadToEnd();
                }
            }
            return deserializado;
        }

        private void txt_nombre_TextChanged(object sender, EventArgs e)
        {
            check_controles_suscripcion();
        }

        private void check_controles_suscripcion()
        {
            if (txt_correo.Text != "" && dgv_productos.SelectedRows.Count != 0 && txt_nombre.Text != "")
            {
                btn_suscripcion.Enabled = true;
            }
            else
            {
                btn_suscripcion.Enabled = false;
            }
        }

        private void txt_correo_TextChanged(object sender, EventArgs e)
        {
            check_controles_suscripcion();
        }

        private void dgv_productos_SelectionChanged(object sender, EventArgs e)
        {
            check_controles_suscripcion();
        }
    }
}
