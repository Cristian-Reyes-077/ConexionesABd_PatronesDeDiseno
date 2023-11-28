
using ConexionesABd_PatronesDeDiseno.Factory;
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
        IMSConexionFactory dbFactoryMS;

        bool enMS = false;

        int conactivas = 0;
        int llamadassr = 0;

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
            llamadassr++;
            CambiarTextoL();

            string connectionMsString = $"Server={textBox1.Text};Database={textBox2.Text};User ID={textBox3.Text};Password={textBox4.Text};";

            if (comboBox2.SelectedIndex == 0) //--  tipo de diseño Factory --// 
            {

                if (dbFactoryMS == null || !enMS)
                {
                    enMS = true;
                    ConexionesBD conect = new ConexionesBD(); //--Instancia a la clase donde implementa la interfaz osea la clase fabrica--//
                    dbFactoryMS = conect.getStrConexion("MS", connectionMsString); //-- creo una instancia de la interface de conexiones de MySQL mandandole como parametro la cadena de conexion --//
                }

                if (dbFactoryMS.ConexionEstaAbierta())
                {
                    dbFactoryMS.Disconnect();conactivas--; CambiarTextoL();
                }
                else
                {
                    MessageBox.Show($"Conectado a Mysql por FACTORY\n\nBase de datos: {textBox2.Text} con el usuario: {textBox3.Text}", "Info BD");
                    dbFactoryMS.Connect(); conactivas++; CambiarTextoL();
                }

            }
            else
            {
                // Utilizando el proxy para acceder al servicio real
                IDatabaseServiceProxyMS proxy = new DatabaseServiceProxyMS(connectionMsString);
                proxy.ExecuteConect();
                conactivas++; CambiarTextoL();
                MessageBox.Show($"Conectado a MySql por PROXY\n\nBase de datos: {textBox2.Text} con el usuario: {textBox3.Text}", "Info BD");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            llamadassr++;
            CambiarTextoL(); 
            
            string connectionSqlString = $"Data Source={textBox5.Text};Initial Catalog={textBox6.Text};User ID={textBox7.Text};Password={textBox8.Text};"; //--Creamos cadena de conexion--//

            if (comboBox1.SelectedIndex == 0) //--  tipo de diseño Factory --//
            {
                if (dbFactoryMS == null || enMS)
                {
                    enMS = false;
                    ConexionesBD conect = new ConexionesBD(); //--Instancia a la clase donde implementa la interfaz osea la clase fabrica--//
                    dbFactoryMS = conect.getStrConexion("S", connectionSqlString); //-- creo una instancia de la interface de conexiones de MySQL mandandole como parametro la cadena de conexion --//
                }

                if (dbFactoryMS.ConexionEstaAbierta())
                {
                    dbFactoryMS.Disconnect(); conactivas--; CambiarTextoL();
                }
                else
                {
                    MessageBox.Show($"Conectado a SQL Server por FACTORY\n\nBase de datos: {textBox6.Text} con el usuario: {textBox7.Text}", "Info BD");
                    dbFactoryMS.Connect(); conactivas++; CambiarTextoL();
                }

            }
            else
            {
                // Utilizando el proxy para acceder al servicio real
                IDatabaseServiceProxySQ proxy = new DatabaseServiceProxySQ(connectionSqlString);
                proxy.ExecuteConect();
                conactivas++; CambiarTextoL();
                MessageBox.Show($"Conectado a Sql server por PROXY\n\nBase de datos: {textBox6.Text} con el usuario: {textBox7.Text}", "Info BD");
            }
        }

        private void CambiarTextoL()
        {
            label11.Text = $"Conexiones activas: {conactivas}";
            label12.Text = $"Llamadas / Peticiones: {llamadassr}";
        }

        private void ConsumirPEstructural()
        {
            // Utilizar DataProvider directamente
            IDataProvider dataProvider = new DataProvider();
            MessageBox.Show(dataProvider.GetData(), "Interfaz principal");

            // Utilizar DataProvider como IDataConsumer a través del adaptador
            IDataConsumer dataConsumer = new DataProviderAdapter(dataProvider);
            dataConsumer.ConsumeData();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ConsumirPEstructural();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ConsumirPComportamiento();
        }
        private void ConsumirPComportamiento()
        {
            // Crear un botón de Windows Forms (Invoker)
            InvokerButton button = new InvokerButton();

            // Crear un comando con una acción específica
            ICommand command = new ConcreteCommand(() => MessageBox.Show("Clic desde el codigo."));

            // Asociar el comando al botón
            button.SetCommand(command);

            // Simular un clic en el botón
            button.Click();
        }
    }
}
