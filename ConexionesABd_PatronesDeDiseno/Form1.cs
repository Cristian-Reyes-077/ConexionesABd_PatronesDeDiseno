using ConexionesABd_PatronesDeDiseno.Clase_factory;
using ConexionesABd_PatronesDeDiseno.Proxy;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConexionesABd_PatronesDeDiseno
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;

            textBox1.Text = "localhost";
            textBox2.Text = "descargamasiva";
            textBox3.Text = "root";
            textBox4.Text = "2118012";

            textBox5.Text = "(localdb)\\LocalDbPrueba";
            textBox6.Text = "master";
            textBox7.Text = "sa";
            textBox8.Text = "2118012";
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            string connectionMsString = $"Server={textBox1.Text};Database={textBox2.Text};User ID={textBox3.Text};Password={textBox4.Text};";
            
            if (comboBox2.SelectedIndex == 0) //-- Solo valido que tipo de diseño eligio --// 
            {

                
                IDbMsFactory dbFactoryMS = new MySqlConexionFactory(connectionMsString); //-- creo una instancia de la interface de conexiones de MySQL mandandole como parametro la cadena de conexion --//

                using (var connection = dbFactoryMS.CreateConnection()) //-- hago uso de la instancia antes hecha de la interface de IDbMsFactory , indicandole que creare una nueva conexion ds MySQL--//
                {
                    try
                    {
                        connection.Open(); //-- abro la conexion mandando llamar el metodo de Open--//

                        MessageBox.Show("Conectado a MySql por FACTORY", "#PPCDSALVC");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($" {ex.Message}", "#PPCDSALVC");
                    }
                }

            }
            else
            {
                // Utilizando el proxy para acceder al servicio real
                IDatabaseServiceProxyMS proxy = new DatabaseServiceProxyMS(connectionMsString);
                proxy.ExecuteConect();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connectionSqlString = $"Data Source={textBox5.Text};Initial Catalog={textBox6.Text};User ID={textBox7.Text};Password={textBox8.Text};"; //--Creamos cadena de conexion--//
            
            if (comboBox1.SelectedIndex == 0) //-- Solo valido que tipo de diseño eligio --// 
            {
                IDbMsFactory dbFactorySql = new SqlConexionFactory(connectionSqlString); //-- creo una instancia de la interface de conexiones de SQL mandandole como parametro la cadena de conexion --//

                using (var connection = dbFactorySql.CreateConnection()) //-- hago uso de la instancia antes hecha de la interface de IDbMsFactory, indicandole que creare una nueva conexion d SQL--//
                {
                    try
                    {
                        connection.Open(); //-- abro la conexion mandando llamar el metodo de Open--//

                        MessageBox.Show("Conectado a SQL server por FACTORY.", "#PPCDSALVC");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"{ex.Message}", "#PPCDSALVC");
                    }
                }

            }
            else
            {
                // Utilizando el proxy para acceder al servicio real
                IDatabaseServiceProxySQ proxy = new DatabaseServiceProxySQ(connectionSqlString);
                proxy.ExecuteConect();
            }
        }
    }
}
