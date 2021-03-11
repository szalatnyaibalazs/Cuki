using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Cuki
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            StreamReader be = new StreamReader("cuki.txt");
            while (!be.EndOfStream)
            {
                string[] a = be.ReadLine().Split(';');
                sutemenyek.Add(new Sutemeny(a[0],a[1],bool.Parse(a[2]),int.Parse(a[3]),a[4]));
            }
            be.Close();
        }
        static List<Sutemeny> sutemenyek = new List<Sutemeny>();
        private void btnArment_Click(object sender, EventArgs e)
        {

        }
    }
}
