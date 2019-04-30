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

namespace Das_Genderneutrale_Adressbuch__LGBT_
{
    public partial class Form1 : Form
    {
      
        public Form1()
        {
            InitializeComponent();
        }
        string Pfad = string.Empty;
        //Artikel bedigunen wird erstellt
        public struct Artikel

        {
            public String id;
            public int Alter;
            public int plz;
            public String vorname;
            public String gender;
            public String nachname;
            public String ort;
            public String Status;
            public String nickname;
            
        }

        private void buttonEingabe_Click(object sender, EventArgs e)
        {
            var tabelle = Tabelle.getTabel(@"...\db.csv");
            int index = 0;
            int anzahl = tabelle.Length;
            string temp = textBoxpk.Text;
            int tempint = 0;

            for (; index < anzahl;)
            {
                //Suche nach Artikel
                //Eingabe über PK
                if (temp == tabelle[index].id)
                {
                    tempint = index;
                    labelpk.Text = tabelle[tempint].id;
                    textBoxda.Text = Convert.ToString(tabelle[tempint].Alter);
                    textBoxdmina.Text = Convert.ToString(tabelle[tempint].plz);
                    textBoxla.Text = Convert.ToString(tabelle[tempint].vorname);
                    textBoxd2a.Text = Convert.ToString(tabelle[tempint].nachname);
                    textBoxea.Text = Convert.ToString(tabelle[tempint].ort);
                    textBoxl1a.Text = Convert.ToString(tabelle[tempint].Status);
                    labelt.Text = tabelle[tempint].gender;
                    groupBox1.Visible = true;

                }
                //Eingabe über Abmasse
                else if (textBoxAlter.Text == Convert.ToString(tabelle[index].Alter)
                    & textBoxplz.Text == Convert.ToString(tabelle[index].plz)
                    & textBoxvorname.Text == Convert.ToString(tabelle[index].vorname)
                    & textBoxdnachname.Text == Convert.ToString(tabelle[index].nachname)
                    & textBoxort.Text == Convert.ToString(tabelle[index].ort)
                    & textBoxlstatus.Text == Convert.ToString(tabelle[index].Status)
                    & textBoxlnickname.Text == Convert.ToString(tabelle[index].nickname))
                {
                    tempint = index;
                    labelpk.Text = tabelle[tempint].id;
                    textBoxda.Text = Convert.ToString(tabelle[tempint].Alter);
                    textBoxdmina.Text = Convert.ToString(tabelle[tempint].plz);
                    textBoxla.Text = Convert.ToString(tabelle[tempint].vorname);
                    textBoxd2a.Text = Convert.ToString(tabelle[tempint].nachname);
                    textBoxea.Text = Convert.ToString(tabelle[tempint].ort);
                    textBoxl1a.Text = Convert.ToString(tabelle[tempint].Status);
                    labelt.Text = tabelle[tempint].gender;
                    groupBox1.Visible = true;
                }

                index++;
            }
            // Error meldungen
            if (textBoxpk.Text == "" & textBoxAlter.Text == "")
            {
                labelerror.Text = "Artikel Nicht gefunden wollen sie eine Sonder Bessellung";
            }
        }
        //Artikle speicher logik
        public static void savecsv(string filepath, string id, int alter, int plz, 
            string vorname, string gender, string nachname, string ort, string Status, string nickname)
        {
            //Erstelle  streamWriter
            using (StreamWriter streamWriter = new StreamWriter(filepath, true))
            {
                streamWriter.WriteLine(id + ";" + alter + ";" + plz + ";" + vorname + ";" + gender + ";" + nachname + ";" + ort + ";" + Status + ";" + nickname);

            }

        }
        // Speicher in Array
        public static class Tabelle
        {
            public static Artikel[] getTabel(String Datei)
            {
                List<Artikel> artikel = new List<Artikel>();
                String[] zeilen = File.ReadAllLines(Datei);
                foreach (String zeile in zeilen)
                {
                    String[] data = zeile.Split(';');
                    Artikel a = new Artikel();
                    a.id = data[0];
                    a.Alter = Convert.ToInt32(data[1]);
                    a.plz = Convert.ToInt32(data[2]);
                    a.vorname = Convert.ToString(data[3]);
                    a.nachname = Convert.ToString(data[4]);
                    a.ort = Convert.ToString(data[5]);
                    a.Status = Convert.ToString(data[6]);
                    a.nickname = Convert.ToString(data[7]);
                    a.gender = data[8];
                    artikel.Add(a);

                }
                return artikel.ToArray();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Aufruf vom Programm save csv
            savecsv(@"...\db.csv",
                textBoxpk.Text,
                 Convert.ToInt32(textBoxAlter.Text),
                 Convert.ToInt32(textBoxplz.Text),
                Convert.ToString(textBoxvorname.Text),
                Convert.ToString(textBoxdnachname.Text),
                Convert.ToString(textBoxort.Text),
                Convert.ToString(textBoxlstatus.Text),
                Convert.ToString(textBoxlnickname.Text),
                textBoxtyp.Text);

            textBoxpk.Text = "";
            textBoxAlter.Text = "";
            textBoxplz.Text = "";
            textBoxvorname.Text = "";
            textBoxdnachname.Text = "";
            textBoxort.Text = "";
            textBoxlstatus.Text = "";
            textBoxlnickname.Text = "";
            textBoxtyp.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Erstelle  streamWriter
           
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                Pfad = openFileDialog1.FileName;
            labelerror.Text = Pfad;
            var tabelle = Tabelle.getTabel(Pfad);
            int index = 0;
            using (StreamWriter streamWriter = new StreamWriter(@"...\db.csv"))
            {
                streamWriter.WriteLine("");
            }
            int anzahl = tabelle.Length;
            for (; index < anzahl; index++)
            {
                savecsv(@"...\db.csv", tabelle[index].id, tabelle[index].Alter, tabelle[index].plz, tabelle[index].vorname, tabelle[index].nachname, tabelle[index].ort, tabelle[index].Status, tabelle[index].nickname, tabelle[index].gender);
            }
        }
    }
}
