using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace WindowsForms
{
    public partial class Usermanage : Form
    {
        public Usermanage()
        {
            InitializeComponent();
        }

        private void Usermanage_Load(object sender, EventArgs e)
        {
            Conexion.Conectar();
            MessageBox.Show("Conexion exitosa");
            dataGridView1.DataSource = llenar_grid();
        }
        public DataTable llenar_grid()
        {
            Conexion.Conectar();
            DataTable dt = new DataTable();
            string consulta = "SELECT * FROM Usuarios";
            SqlCommand cmd = new SqlCommand(consulta, Conexion.Conectar());

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);
            return dt;
        }

        private void bttAgregar_Click(object sender, EventArgs e)
        {
            Conexion.Conectar();
            string insertar = "INSERT INTO Usuarios(usuario, contrasena) values (@Usuario,@Contrasena)";
            SqlCommand cmd1 = new SqlCommand(insertar, Conexion.Conectar());
            cmd1.Parameters.AddWithValue("@Usuario", txtUsuario.Text);
            cmd1.Parameters.AddWithValue("@Contrasena", txtcontrasena.Text);

            cmd1.ExecuteNonQuery();
            MessageBox.Show("Los datos fueron ingresados de manera exitosa");
            dataGridView1.DataSource = llenar_grid();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txtUsuario.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                txtcontrasena.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            }
            catch
            {

            }
        }

        private void bttModificar_Click(object sender, EventArgs e)
        {
            Conexion.Conectar();
            string actualizar = "UPDATE Usuarios SET usuario=@usuario, contrasena=@contrasena WHERE id_usuario=@id";
            SqlCommand cmd2 = new SqlCommand(actualizar, Conexion.Conectar());

            cmd2.Parameters.AddWithValue("@id", txtID.Text);
            cmd2.Parameters.AddWithValue("@Usuario", txtUsuario.Text);
            cmd2.Parameters.AddWithValue("@contrasena", txtcontrasena.Text);

            cmd2.ExecuteNonQuery();
            MessageBox.Show("Los datos fueron actualizados correctamente");
            dataGridView1.DataSource = llenar_grid();
        }

        private void bttEliminar_Click(object sender, EventArgs e)
        {
            Conexion.Conectar();
            string eliminar = "DELETE FROM Usuarios WHERE id_usuario = @id";
            SqlCommand cmd3 = new SqlCommand(eliminar, Conexion.Conectar());

            cmd3.Parameters.AddWithValue("@id", txtID.Text);

            cmd3.ExecuteNonQuery();
            MessageBox.Show("El usuario ha sido eliminado exitosamente.");

            dataGridView1.DataSource= llenar_grid();
        }

        private void bttLimpiar_Click(object sender, EventArgs e)
        {
            txtUsuario.Clear();
            txtcontrasena.Clear();
            txtID.Clear();
        }

        private void bttVolver_Click(object sender, EventArgs e)
        {
            this.Hide();

            Login login = new Login();

            login.Show();
        }
    }
}
