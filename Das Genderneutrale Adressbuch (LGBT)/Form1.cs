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

namespace Das_Genderneutrale_Adressbuch__LGBT_
{
    public partial class Form1 : Form
    {
        List<Artikel> liste_Suchergebnisse;
        public struct Postleitzahl

        {
            public int PLZ;
            public string OrtMitZusatz;
            public string Bundesland;
        }
        List<Postleitzahl> plz_liste = new List<Postleitzahl>();
        public Form1()
        {
            string[] zeilen = File.ReadAllLines("...\\plz_de.csv", Encoding.GetEncoding("iso-8859-1"));
            foreach(string PLZ in zeilen.Skip(1))
            {
                String[] data = PLZ.Split(';');
                if (PLZ == "")
                {
                    continue;
                }
                Postleitzahl a = new Postleitzahl();
                a.PLZ = int.Parse(data[2]);
                a.OrtMitZusatz = data[0] + data[1];
                a.Bundesland = data[4];
                plz_liste.Add(a);
            }
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
            
            public Artikel(string id, int Alter, int plz, string vorname, string gender, string nachname, string ort, string Status, string nickname)
            {
                this.id = id;
                this.Alter = Alter;
                this.plz = plz;
                this.vorname = vorname;
                this.gender = gender;
                this.nachname = nachname;
                this.ort = ort;
                this.Status = Status;
                this.nickname = nickname;
            }
        }
        public void zeigeArtikel(Artikel artikel)
        {
            labelpk.Text = artikel.id;
            textBoxaalter.Text = Convert.ToString(artikel.Alter);
            textBoxaplz.Text = Convert.ToString(artikel.plz);
            textBoxavorname.Text = artikel.vorname;
            textBoxd2a.Text = artikel.nachname;
            textBoxaort.Text = artikel.ort;
            textBoxastaus.Text = artikel.Status;
            textBoxNickname.Text = artikel.nickname;
            labelGender.Text = artikel.gender;
            groupBox1.Visible = true;
        }
        private void buttonEingabe_Click(object sender, EventArgs e)
        {
            var tabelle = Tabelle.getTabel(@"...\db.csv");
            int index = 0;
            int anzahl = tabelle.Length;
            string temp = textBoxpk.Text;
            int tempint = 0;
            liste_Suchergebnisse = new List<Artikel>();
            for (; index < anzahl;)
            {
                //Suche nach Artikel
                //Eingabe über PK
                
                if (temp == tabelle[index].id) //Ergebnis passt.
                {
                    liste_Suchergebnisse.Add(new Artikel(
                        tabelle[index].id,
                        tabelle[index].Alter,
                        tabelle[index].plz,
                        tabelle[index].vorname,
                        tabelle[index].gender,
                        tabelle[index].nachname,
                        tabelle[index].ort,
                        tabelle[index].Status,
                        tabelle[index].nickname));
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
                    textBoxaalter.Text = Convert.ToString(tabelle[tempint].Alter);
                    textBoxaplz.Text = Convert.ToString(tabelle[tempint].plz);
                    textBoxavorname.Text = Convert.ToString(tabelle[tempint].vorname);
                    textBoxd2a.Text = Convert.ToString(tabelle[tempint].nachname);
                    textBoxaort.Text = Convert.ToString(tabelle[tempint].ort);
                    textBoxastaus.Text = Convert.ToString(tabelle[tempint].Status);
                    textBoxNickname.Text = tabelle[tempint].nickname;
                    labelGender.Text = tabelle[tempint].gender;
                    groupBox1.Visible = true;
                }

                index++;
            }
            if (liste_Suchergebnisse.Count > 0)
            {
                zeigeArtikel(liste_Suchergebnisse[0]);
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
            public static Artikel[] getTabel(string Datei)
            {
                List<Artikel> artikel = new List<Artikel>();
                try {
                    String[] zeilen = File.ReadAllLines(Datei);


                    foreach (String zeile in zeilen.Skip(1))
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
                }
                catch (Exception)
                {
                    MessageBox.Show("Nix gefunde oder Uups, da ist was schief gegangen:\n versuchen sie zb Excel zu sclisen"); // Fehler anzeigen
                                                                                                             
                }

                return artikel.ToArray();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try {
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
                    comboBoxgender.Text);

                textBoxpk.Text = "";
                textBoxAlter.Text = "";
                textBoxplz.Text = "";
                textBoxvorname.Text = "";
                textBoxdnachname.Text = "";
                textBoxort.Text = "";
                textBoxlstatus.Text = "";
                textBoxlnickname.Text = "";
                comboBoxgender.Text = "";
            } catch 
            {
                MessageBox.Show("Feheler Bittel alle Felder Aufühlen");
            }
            }
    
        private void button2_Click(object sender, EventArgs e)
        {
            //Erstelle  streamWriter
           
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                Pfad = openFileDialog1.FileName;
            labelerror.Text = Pfad;
            if (Pfad != "")
            {
                var tabelle = Tabelle.getTabel(Pfad);
                int index = 0;
                using (StreamWriter streamWriter = new StreamWriter(@"...\db.csv"))
                {
                    streamWriter.WriteLine("");
                }
                int anzahl = tabelle.Length;
                for (; index < anzahl; index++)
                {
                    savecsv(@"...\db.csv", tabelle[index].id, tabelle[index].Alter, tabelle[index].plz, tabelle[index].vorname,
                        tabelle[index].nachname, tabelle[index].ort, tabelle[index].Status, tabelle[index].nickname, tabelle[index].gender);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBoxaalter.ReadOnly = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBoxaplz.ReadOnly = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBoxavorname.ReadOnly = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBoxd2a.ReadOnly = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBoxaort.ReadOnly = false;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBoxNickname.ReadOnly = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBoxastaus.ReadOnly = false;
        }

        private void textBoxplz_TextChanged(object sender, EventArgs e)
        {
            foreach (Postleitzahl tmp in plz_liste)
            {
                if (textBoxplz.Text != "")
                {
                    if (tmp.PLZ == int.Parse(textBoxplz.Text))
                    {
                        textBoxort.Text = tmp.OrtMitZusatz;
                        textBoxbund.Text = tmp.Bundesland;
                    }
                }
            }
        }
        private void button10_Click(object sender, EventArgs e)
        {
            
        }
    }
}
