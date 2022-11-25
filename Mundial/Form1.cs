using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Equin.ApplicationFramework;
using Entities;
using Bussines;

namespace Mundial
{
    public partial class Form1 : Form
    {
        BindingSource jugadoresBindingSource = new BindingSource();
        BindingListView<JugadorEntity> jugadoresBindingList;
        BindingSource marcadoresBindingSource = new BindingSource();
        List<JugadorEntity> ListaJugadores = new List<JugadorEntity>();
        ConfiguracionBO confBO = new ConfiguracionBO();

        public Form1()
        {
            InitializeComponent();
            CargarJugadores();
            EstaPermitidoActualizarMarcadores();
        }

        private void CargarJugadores()
        {
            try
            {
                ListadoJugadoresDataGridView.AutoGenerateColumns = false;
                MarcadoresDataGridView.AutoGenerateColumns = false;
                ListaJugadores = JugadorBO.GetAllJugadores().OrderByDescending(x=>x.TotalPuntos).ToList();
                CargarMarcadores();
                jugadoresBindingList = new BindingListView<JugadorEntity>(ListaJugadores);
                jugadoresBindingSource.DataSource = jugadoresBindingList;
                JugadoresDataGridView.DataSource = jugadoresBindingSource;
                marcadoresBindingSource.DataMember = "Marcadores";
                marcadoresBindingSource.DataSource = jugadoresBindingSource;                
                MarcadoresDataGridView.DataSource = marcadoresBindingSource;
                ListadoJugadoresDataGridView.DataSource = jugadoresBindingSource;
                EnlazarComponentes();
                HabilitarColumnasMarcadoresDVG();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }   
        }

        private void CargarMarcadores()
        {
            List<PartidoEntity> listaMarcadores = PartidosBO.GetAll();
            ListaJugadores.ForEach(x => x.Marcadores = listaMarcadores.Where(y => y.JugadorId == x.JugadorId).ToList());
        }

        private void EstaPermitidoActualizarMarcadores() 
        {
            if (confBO.EstaBloqueadoModidicarMarcadores())
            {
                bloquearMarcadoresButton.Text = "Desbloquear Actualizacion Marcadores";
                bloquearMarcadoresButton.BackColor = Color.DarkCyan;
            }
            else 
            {
                bloquearMarcadoresButton.Text = "Bloquear Actualizacion Marcadores";
                bloquearMarcadoresButton.BackColor = Color.Crimson;
            }
            
        }

        private void EnlazarComponentes()
        {
            if (marcador1TextBox.DataBindings.Count == 0)
            {
                marcador1TextBox.DataBindings.Add(new Binding("text", marcadoresBindingSource, "MarcadorE1", true));
                marcador2TextBox.DataBindings.Add(new Binding("text", marcadoresBindingSource, "MarcadorE2", true));
                equipo1Label.DataBindings.Add(new Binding("text", marcadoresBindingSource, "NombreE1", true));
                equipo2Label.DataBindings.Add(new Binding("text", marcadoresBindingSource, "NombreE2", true));
                faseLabel.DataBindings.Add(new Binding("text", marcadoresBindingSource, "Fase", true));
                jugadorLabel.DataBindings.Add(new Binding("text", jugadoresBindingSource, "Nombre", true));
            }
        }

        private void HabilitarColumnasMarcadoresDVG()
        { 
            foreach (DataGridViewColumn col in MarcadoresDataGridView.Columns)
		        col.ReadOnly = true;

            MarcadoresDataGridView.Columns["marcadorE1Column"].ReadOnly = false;
            MarcadoresDataGridView.Columns["marcadorE2Column"].ReadOnly = false;
            MarcadoresDataGridView.Columns["marcadorE1Column"].DefaultCellStyle.BackColor = Color.LightGreen;
            MarcadoresDataGridView.Columns["marcadorE2Column"].DefaultCellStyle.BackColor = Color.LightGreen;
        }

        private void CalcularPuntosButton_Click(object sender, EventArgs e)
        {
            CalcularPuntosButton.Cursor = Cursors.WaitCursor;
            List<PartidoEntity> PartidosResultadosE1 = PartidosBO.GetMarcadoresPorJugador(0,"E1");
            List<PartidoEntity> PartidosResultadosE2 = PartidosBO.GetMarcadoresPorJugador(0,"E2");
            List<PaseEntity> PasesResultadoE1 = PasesBO.GetPasesPorJugador(0,"E1");
            List<PaseEntity> PasesResultadoE2 = PasesBO.GetPasesPorJugador(0,"E2");
            if (PartidosResultadosE1.Count > 0)
            {
                ListaJugadores = JugadorBO.GetAllJugadores();
                if (ListaJugadores.Count > 0)
                {
                    foreach (JugadorEntity jugador in ListaJugadores)
                    {
                        if (jugador.JugadorId > 0)
                        {
                            JugadorBO.CalcularPartidos(jugador, PartidosResultadosE1, "E1");
                            JugadorBO.CalcularPasesE1(jugador, PasesResultadoE1);
                            JugadorBO.CalcularPartidos(jugador, PartidosResultadosE2, "E2");
                            JugadorBO.CalcularPasesE2(jugador, PasesResultadoE2);
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

        private void cargarMarcadoresButton_Click(object sender, EventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {
            jugadoresBindingSource.CancelEdit();
        }

        private void guardarButton_Click(object sender, EventArgs e)
        {
            try
            {
                JugadorEntity jugadorActual = ObtenerJugadorActual();
                if(jugadorActual == null)
                {
                    MessageBox.Show("No se pudo encontrar el jugador selecionado actual","Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }

                PartidoEntity marcador = jugadorActual.Marcadores.Where(x => x.PartidoId == MarcadoresDataGridView.CurrentRow.Cells["partidoIdColumn"].Value).FirstOrDefault();
                if(marcador == null)
                {
                    MessageBox.Show("No se pudo encontrar el marcador a actualizar","Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }

                DialogResult resp = MessageBox.Show("Esta seguro que desea actualizar el marcador de  " + marcador.NombreE1 + " VS " + marcador.NombreE2 + " de " + jugadorActual.Nombre, "Esta seguro", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resp == DialogResult.Yes)
                {
                    string result = PartidosBO.SaveOneByJugador(marcador);
                    if (result != null)
                        MessageBox.Show("Error" + result, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        MessageBox.Show("El marcado han sido actualizado correctamente", "Ok", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message,"Error",  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guardarTodosButton_Click(object sender, EventArgs e)
        {
            try
            {
                JugadorEntity jugadorActual = ObtenerJugadorActual();
                if(jugadorActual == null)
                {
                    MessageBox.Show("No se pudo encontrar el jugador selecionado actual","Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }
                
                DialogResult resp =  MessageBox.Show("Esta seguro que desea actualizar todos los marcadores de " + jugadorActual.Nombre,"Esta seguro", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if(resp == DialogResult.Yes)
                {
                    string result = PartidosBO.SaveAllByJugador(jugadorActual);
                    if (result != null)
                        MessageBox.Show("Error " + result, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        MessageBox.Show("Los marcadoes han sido actualizado correctamente", "Ok", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private JugadorEntity ObtenerJugadorActual()
        {
            var current = (ObjectView<JugadorEntity>)jugadoresBindingSource.Current;
            return current != null ? (JugadorEntity)current : null;           
        }

        private void bloquearMarcadoresButton_Click(object sender, EventArgs e)
        {
            if (bloquearMarcadoresButton.BackColor == Color.Crimson) // bloque la actualizacion es decir la porpiedad de configuracion pasa a ser true 
            {
                confBO.BloquearModificarMarcadores(true);
            }
            else 
            {
                confBO.BloquearModificarMarcadores(false);
            }
            EstaPermitidoActualizarMarcadores();

        }
    }

    
}
