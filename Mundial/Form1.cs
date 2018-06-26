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
            DataTable dt = ListaJugadores.ToDataTable();
            JugadoresBindingSource.DataSource = dt;
            JugadoresDataGridView.DataSource = JugadoresBindingSource;
        
        }

        private void rectangleShape1_Click(object sender, EventArgs e)
        {

        }

        private void panelC_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CalcularPuntosButton_Click(object sender, EventArgs e)
        {
            CalcularPuntosButton.Cursor = Cursors.WaitCursor;
            List<PartidoEntity> PartidosResultados = PartidosBO.GetMarcadoresPorJugador(0);
            List<PaseEntity> PasesResultado = PasesBO.GetPasesPorJugador(0);
            if (PartidosResultados.Count > 0)
            {
                ListaJugadores = JugadorBO.GetAllJugadores();
                if (ListaJugadores.Count > 0)
                {
                    foreach (JugadorEntity jugador in ListaJugadores)
                    {
                        if (jugador.JugadorId > 0)
                        {
                            JugadorBO.CalcularPartidosE1(jugador, PartidosResultados);
                            //jugador.PuntosEtapa1 = 0;
                            JugadorBO.CalcularPasesE1(jugador, PasesResultado);
                            JugadorBO.SavePuntosJugador(jugador);
                        }
                    }
                }
                CargarJugadores();
                MessageBox.Show("Datos Calculados","Listo",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            { 
                MessageBox.Show("No estan los resultados de los partidos","Error",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
            CalcularPuntosButton.Cursor = Cursors.Default;
        }
    }
}
