using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entities;
using Bussines;

namespace Mundial
{
    public partial class Form1 : Form
    {
        BindingSource JugadoresBindingSource = new BindingSource();
        List<JugadorEntity> ListaJugadores = new List<JugadorEntity>();

        public Form1()
        {
            InitializeComponent();
            CargarJugadores();
        }

        private void CargarJugadores()
        {
            ListaJugadores = JugadorBO.GetAllJugadores();
            JugadoresBindingSource.DataSource = ListaJugadores;
            JugadoresDataGridView.DataSource = JugadoresBindingSource;
        
        }

        private void rectangleShape1_Click(object sender, EventArgs e)
        {

        }
    }
}
