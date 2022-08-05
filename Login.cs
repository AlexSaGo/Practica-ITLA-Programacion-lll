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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        SqlConnection cn = new SqlConnection("SERVER=DESKTOP-NI2ORBL;DATABASE=UserLogin;integrated security=true");
        public void Loguear(string usuario, string clave)
        {
            try
            {
                Conexion conexion = new Conexion();

                Conexion.Conectar();

                SqlCommand cmd = new SqlCommand("SELECT usuario,contrasena FROM Usuarios WHERE usuario=@Usuario AND contrasena=@clave", cn);
                cmd.Parameters.AddWithValue("@usuario", usuario);
                cmd.Parameters.AddWithValue("@clave", clave);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                if (dt.Rows.Count == 1)
                {
           
                    this.Hide();
                    Usermanage usermanage = new Usermanage();
                    usermanage.Show();
                }
                else
                {
                    MessageBox.Show("Usuario y/o Contraseña incorrectos.");
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                cn.Close();
            }
        }

        private void bttIngresar_Click(object sender, EventArgs e)
        {
            Loguear(this.txtUsuario.Text, this.txtContrasena.Text);
        }
    }
}
