using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChatServerConnector.Connect(textBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ChatServerConnector.GetInstance().SendMessageToServer(textBox2.Text);
        }
    }
}
