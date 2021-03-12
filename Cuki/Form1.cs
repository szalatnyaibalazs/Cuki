﻿using System;
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
            Random(); //Balázs
            MiniMax();//Milán
            DijazottDb();//Balázs
            listaKiiras();//Balázs
            statKiiras();//Ábel
        }
        static List<Sutemeny> sutemenyek = new List<Sutemeny>();
        public void Random()
        {
            Random rnd = new Random();
            int rand = rnd.Next(0,sutemenyek.Count);
            tbAjanlat.Text = sutemenyek[rand].Nev;
        }
        public void MiniMax()
        {
            foreach (var s in sutemenyek.OrderBy(x => x.Ar).Take(1))
            {
                tbOlcsosuti.Text = s.Nev;
                tbOlcsoar.Text = s.Ar +"/"+ s.Egyseg;
            }
            foreach (var s in sutemenyek.OrderByDescending(x => x.Ar).Take(1))
            {
                tbDragasuti.Text = s.Nev;
                tbDragaar.Text = s.Ar + "/" + s.Egyseg;
            }
        }
        public void DijazottDb()
        {
            int db = 0;
            foreach (var s in sutemenyek)
            {
                if (s.Dijazott)
                {
                    db++;
                }
            }
            tbDij.Text = db + " Féle dijnyertes édességből választhat";
        }
        public void listaKiiras()
        {
            StreamWriter ki = new StreamWriter("list.txt");
            Dictionary<string, string> lista = new Dictionary<string, string>();
            foreach (var s in sutemenyek)
            {
                if (!lista.ContainsKey(s.Nev))
                {
                    lista.Add(s.Nev,s.Tipus);
                }
            }
            foreach (var l in lista)
            {
                ki.WriteLine(l.Key+" "+l.Value);
            }
            ki.Close();
        }
        public void statKiiras()
        {
            StreamWriter ki = new StreamWriter("stat.csv");
            Dictionary<string, int> lista = new Dictionary<string, int>();
            foreach (var s in sutemenyek)
            {
                if (!lista.ContainsKey(s.Tipus))
                {
                    lista.Add(s.Tipus,1);
                }
                else
                {
                    lista[s.Tipus]++;
                }
            }
            foreach (var l in lista)
            {
                ki.WriteLine(l.Key + ";" + l.Value);
            }
            ki.Close();
        }
        //Máté
        private void btnArment_Click(object sender, EventArgs e)
        {
            string tipus = tbSutitipus.Text.Trim();
            if (tipus == "".Trim())
            {
                MessageBox.Show("Nem írtál be sütemény nevet!");
            }

            else
            {
                List<Ajanlat> ajanlatok = new List<Ajanlat>();
                foreach (var s in sutemenyek) 
                {
                   
                    if (s.Tipus == tipus)
                    {
                        ajanlatok.Add(new Ajanlat (s.Nev,s.Egyseg,s.Ar));
                    }
                }
                if (ajanlatok.Count == 0)
                {
                    MessageBox.Show("Nincs megfelelő sütink kérünk válasz mást!");
                }
                else
                {
                    StreamWriter iro = new StreamWriter("ajanlat.txt");
                    double atlag = 0;
                    foreach (var a in ajanlatok)
                    {
                        iro.WriteLine(a.nev,a.egyseg,a.ar);
                        atlag += a.ar;
                    }
                    MessageBox.Show($"{ajanlatok.Count} darab sütit írtam az ajanlott.txt-be \n átlag ár {atlag/ajanlatok.Count}ft");
                    iro.Close();
                }
            }
        }
        //Máté
        private void btnFelvetel_Click(object sender, EventArgs e)
        {
            char[] szamok = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            bool van = true;
            if (tbSnev.Text == "" || tbStipus.Text == "" || tbSar.Text == "" || tbSegyseg.Text == "")
            {
                van = false;
            }
            for (int i = 0; i < tbSar.Text.Length; i++)
            {
                if (!szamok.Contains(tbSar.Text[i]))
                {
                    MessageBox.Show("Az új sütemény ára nem szám!");
                    van = false;
                    break;
                }
            }
            if (van)
            {
                StreamWriter iro = new StreamWriter("cuki.txt",true);
                iro.Write($"\n{tbSnev.Text};{tbStipus.Text};{cbsDij.Checked};{tbSar.Text};{tbSegyseg.Text}");
                iro.Close();
                MessageBox.Show("Az állomány bővítése sikeres volt");
            }
            else
            {
                MessageBox.Show("Nem adtál meg minden adatot!");
            }
            
        }
    }
}
