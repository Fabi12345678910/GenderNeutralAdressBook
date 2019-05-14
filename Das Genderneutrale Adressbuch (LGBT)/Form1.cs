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
        List<Artikel> liste_Suchergebnisse;
        int aktuellesSuchErgebnis;
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
            public String straße;
            public String tel;
            public String HausNr;
            public String Email;
            
            public Artikel(string id, int Alter, int plz, string vorname, string gender, string nachname, string ort, string Status, string nickname ,string straße ,string tel, string HausNr,string Email)
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
                this.straße = straße;
                this.tel = tel;
                this.HausNr = HausNr;
                this.Email = Email;
            }
        }
        public void zeigeAktuellenArtikel()
        {
            Artikel artikel = liste_Suchergebnisse[aktuellesSuchErgebnis];
            labelpk.Text = artikel.id;
            textBoxaalter.Text = Convert.ToString(artikel.Alter);
            textBoxaplz.Text = Convert.ToString(artikel.plz);
            textBoxavorname.Text = artikel.vorname;
            textBoxd2a.Text = artikel.nachname;
            textBoxaort.Text = artikel.ort;
            textBoxastaus.Text = artikel.Status;
            textBoxNickname.Text = artikel.nickname;
            labelGender.Text = artikel.gender;
            textBoxaStraße.Text = artikel.straße;
            textBoxaHausNr.Text = artikel.HausNr;
            textBoxatel.Text = artikel.tel;
            textBoxaemail.Text = artikel.Email;
            groupBox1.Visible = true;
        }
        private void buttonEingabe_Click(object sender, EventArgs e)
        {
            var tabelle = Tabelle.getTabel(@"...\db.csv");
            int anzahl = tabelle.Length;
            liste_Suchergebnisse = new List<Artikel>();
            for (int index = 0;index < anzahl;index++)
            {
                //Suche nach Artikel
                //Eingabe über PK
                if (textBoxpk.Text == tabelle[index].id 
                    || textBoxAlter.Text == Convert.ToString(tabelle[index].Alter)
                    || textBoxplz.Text == Convert.ToString(tabelle[index].plz)
                    || textBoxvorname.Text == tabelle[index].vorname
                    || textBoxdnachname.Text == tabelle[index].nachname
                    || textBoxort.Text == tabelle[index].ort
                    || textBoxlstatus.Text == tabelle[index].Status
                    || textBoxlnickname.Text == tabelle[index].nickname
                    || textBoxstraße.Text == tabelle[index].straße
                    || textBoxaHausNr.Text == tabelle[index].HausNr
                    || textBoxaemail.Text == tabelle[index].Email
                    || textBoxtel.Text == tabelle[index].tel)
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
                       tabelle[index].nickname,
                       tabelle[index].straße,
                       tabelle[index].HausNr,
                       tabelle[index].tel,
                       tabelle[index].Email));
                }
            }

            textBoxAnzahlErgebnisse.Text = Convert.ToString(liste_Suchergebnisse.Count);

            if (liste_Suchergebnisse.Count > 0)
            {
                aktuellesSuchErgebnis = 0;
                zeigeAktuellenArtikel();
            }

            updateNavigationButtons();
            // Error meldungen
            if (textBoxpk.Text == "" & textBoxAlter.Text == "")
            {
                labelerror.Text = "Artikel Nicht gefunden wollen sie eine Sonder Bessellung";
            }
        }
        //Artikle speicher logik
        public static void savecsv(string filepath, string id, int alter, int plz,
            string vorname, string gender, string nachname, string ort, string Status, string nickname, string straße, string Hausnummar, string tel ,string Email)
        {
            //Erstelle  streamWriter
            using (StreamWriter streamWriter = new StreamWriter(filepath, true))
            {
                streamWriter.WriteLine(id + ";" + alter + ";" + plz + ";" + vorname + ";" + gender + ";" + nachname + ";" + ort + ";" + Status + ";" + nickname + ";"+ straße + ";" + Hausnummar + ";" + tel + ";" + Email);

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
                        a.straße = data[9];
                        a.HausNr = data[10];
                        a.tel = data[11];
                        a.Email = data[12];
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
                    comboBoxgender.Text, 
                    textBoxstraße.Text,
                    textBoxaHausNr.Text,
                    textBoxtel.Text,
                    textBoxaemail.Text)

                ;

                textBoxpk.Text = "";
                textBoxAlter.Text = "";
                textBoxplz.Text = "";
                textBoxvorname.Text = "";
                textBoxdnachname.Text = "";
                textBoxort.Text = "";
                textBoxlstatus.Text = "";
                textBoxlnickname.Text = "";
                comboBoxgender.Text = "";
                textBoxaemail.Text = "";
                
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
                        tabelle[index].nachname, tabelle[index].ort, tabelle[index].Status, tabelle[index].nickname, tabelle[index].gender, 
                        tabelle[index].straße, tabelle[index].HausNr, tabelle[index].tel , tabelle[index].Email);
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

        private void buttonvorwaerts_Click(object sender, EventArgs e)
        {
            if (aktuellesSuchErgebnis < liste_Suchergebnisse.Count-1)
            {
                aktuellesSuchErgebnis++;
                zeigeAktuellenArtikel();
                updateNavigationButtons();
            }
        }

        private void buttonzurueck_Click(object sender, EventArgs e)
        {
            if (aktuellesSuchErgebnis > 0)
            {
                aktuellesSuchErgebnis--;
                zeigeAktuellenArtikel();
                updateNavigationButtons();
            }
        }
        private void updateNavigationButtons()
        {
            buttonvorwaerts.Enabled = !(aktuellesSuchErgebnis >= liste_Suchergebnisse.Count - 1);
            buttonzurueck.Enabled = (aktuellesSuchErgebnis != 0);
        }
    }
}
