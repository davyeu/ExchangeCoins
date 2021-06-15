using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace WindowsFormsApp1
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
        }

       

        private void button2_Click(object sender, EventArgs e)
        {
            LogicLayer.Connector c = LogicLayer.Connector.GetInstance;
            string conString = @"Server= DESKTOP-SGST7QL\SQLEXPRESS; Database =Currency; Trusted_Connection = True;";
            String[] rates = new String[2];
            rates= c.connectSqlServer(conString, FromComboBox.Text.ToString(),
                ToComboBox.Text.ToString());
            //
            if (rates[0].Equals("ERROR"))
            {
                MessageBox.Show(rates[1]);//show the kind of error
                return;
            }
            textBoxMinRate1.Text = rates[0];
            textBoxMaxRate1.Text = rates[1];


        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
