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
        BindingSource pasesBindingSource = new BindingSource();
        BindingSource goleadoresBindingSource = new BindingSource();
        List<JugadorEntity> ListaJugadores = new List<JugadorEntity>();
        ConfiguracionBO confBO = new ConfiguracionBO();

        public Form1()
        {
            InitializeComponent();
        }

        private void CargarJugadores()
        {
            try
            {
                ListadoJugadoresDataGridView.AutoGenerateColumns = false;
                pasesDataGridView.AutoGenerateColumns = false;
                MarcadoresDataGridView.AutoGenerateColumns = false;
                ListaJugadores = JugadorBO.GetAllJugadores().OrderByDescending(x=>x.TotalPuntos).ToList();
                CargarMarcadores();
                CargarPases();
                CargarGoleadores();
                jugadoresBindingList = new BindingListView<JugadorEntity>(ListaJugadores);
                jugadoresBindingSource.DataSource = jugadoresBindingList;
                JugadoresDataGridView.DataSource = jugadoresBindingSource;
                ListadoJugadoresDataGridView.DataSource = jugadoresBindingSource;
                
                marcadoresBindingSource.DataMember = "Marcadores";
                marcadoresBindingSource.DataSource = jugadoresBindingSource;                
                MarcadoresDataGridView.DataSource = marcadoresBindingSource;
                
                pasesBindingSource.DataSource = jugadoresBindingSource;
                pasesBindingSource.DataMember = "Pases";
                pasesDataGridView.DataSource = pasesBindingSource;

                goleadoresBindingSource.DataMember = "Goleadores";
                goleadoresBindingSource.DataSource = jugadoresBindingSource;
                GoleadoresDataGridView.DataSource = goleadoresBindingSource;

                EnlazarComponentes();
                HabilitarColumnasMarcadoresDVG();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }   
        }

        private void CargarGoleadores()
        {
            List<GoleadorEntity> lista = GoleadorBO.GetAll();
            ListaJugadores.ForEach(x => x.Goleadores = lista.Where(y => y.IdJugador == x.JugadorId).ToList());
        }

        private void CargarMarcadores()
        {
            List<PartidoEntity> listaMarcadores = PartidosBO.GetAll();
            ListaJugadores.ForEach(x => x.Marcadores = listaMarcadores.Where(y => y.JugadorId == x.JugadorId).ToList());
        }

        private void CargarPases()
        {
            List<PaseEntity> listaPases = PasesBO.GetPasesAll();
            ListaJugadores.ForEach(x => x.Pases = listaPases.Where(y => y.Idjugador == x.JugadorId).ToList());
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

                fasePaseLabel.DataBindings.Add(new Binding("text", pasesBindingSource, "Fase", true));
                idPaseLabel.DataBindings.Add(new Binding("text", pasesBindingSource, "IdPase", true));
                idEquipoPaseTextBox.DataBindings.Add(new Binding("text", pasesBindingSource, "Equipo", true));
                nombreEquipoPaseTextBox.DataBindings.Add(new Binding("text", pasesBindingSource, "EquipoNombre", true));
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
            List<PaseEntity> PasesResultadoE1 = PasesBO.GetPasesPorJugador(0,"GRUPO");
            List<GoleadorEntity> GoleadorResultadoE1 = GoleadorBO.GetPorJugador(0, "GRUPO");
            List<PaseEntity> PasesResultadoE2 = PasesBO.GetPasesPorJugador(0,"FINAL");
            List<GoleadorEntity> GoleadorResultadoE2 = GoleadorBO.GetPorJugador(0, "FINAL");
            if (PartidosResultadosE1.Count + PartidosResultadosE2.Count == 0)
            { 
                MessageBox.Show("No estan los resultados de los partidos","Error",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
            if (ListaJugadores.Count > 0)
            {
                foreach (JugadorEntity jugador in ListaJugadores)
                {
                    if (jugador.JugadorId > 0)
                    {
                        JugadorBO.CalcularPartidos(jugador, PartidosResultadosE1, "E1");
                        PasesBO.CalcularPasesE1(jugador, PasesResultadoE1, "GRUPO");
                        jugador.PuntosEtapa1 += GoleadorBO.CalcularGoleador(jugador, GoleadorResultadoE1, "GRUPO", puntoExtra: true);
                            
                        JugadorBO.CalcularPartidos(jugador, PartidosResultadosE2, "E2");
                        PasesBO.CalcularPasesE2(jugador, PasesResultadoE2,"FINAL");
                        jugador.PuntosEtapa2 +=  GoleadorBO.CalcularGoleador(jugador, GoleadorResultadoE2, "FINAL", puntosGoleadorFinal: true);
                        JugadorBO.SavePuntosJugador(jugador);
                    }
                }
            }
            CargarJugadores();
            MessageBox.Show("Datos Calculados","Listo",MessageBoxButtons.OK,MessageBoxIcon.Information);            
            
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

        private PaseEntity ObtenerPaseActual()
        {
            var current = (PaseEntity)pasesBindingSource.Current;
            return current != null ? current : null;
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

        private void tabControl1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab != ListadoTabPage)
            {
                tabControl1.SelectedTab.Controls.Add(ListadoJugadoresDataGridView);
                tabControl1.SelectedTab.Controls.Add(bloquearMarcadoresButton);
                tabControl1.SelectedTab.Controls.Add(jugadorLabel);
                tabControl1.SelectedTab.Controls.Add(jugadoresLabel);
            }
        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                JugadorEntity jugadorActual = ObtenerJugadorActual();
                if (jugadorActual == null)
                {
                    MessageBox.Show("No se pudo encontrar el jugador selecionado actual", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var pase = ObtenerPaseActual();
                if (pase == null)
                {
                    MessageBox.Show("No se pudo encontrar el Pase a actualizar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DialogResult resp = MessageBox.Show($"Esta seguro que desea actualizar el {pase.IdPase} con equipo {pase.Equipo} - {pase.EquipoNombre}"
                    , "Esta seguro", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resp == DialogResult.Yes)
                {
                    string result =  PasesBO.SaveOneByJugador(pase);
                    if (result != null)
                        MessageBox.Show("Error" + result, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        MessageBox.Show("El marcado han sido actualizado correctamente", "Ok", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            CargarJugadores();
            EstaPermitidoActualizarMarcadores();
        }
    }

    
}
