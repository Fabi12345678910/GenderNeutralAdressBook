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
        List<Artikel> liste_Suchergebnisse = new List<Artikel>();
        int aktuellesSuchErgebnis;
        public int gendernr = 0;
        public struct Postleitzahl
        {
            public int PLZ;
            public string OrtMitZusatz;
            public string Bundesland;

            public Postleitzahl(int PLZ, string OrtMitZusatz, string Bundesland)
            {
                this.PLZ = PLZ;
                this.OrtMitZusatz = OrtMitZusatz;
                this.Bundesland = Bundesland;
            }
        }
        List<Postleitzahl> plz_liste = new List<Postleitzahl>();
    public Form1()
        {
            PLZ_einlesen();
    }
        private void PLZ_einlesen()
        {
            string[] zeilen = File.ReadAllLines("...\\plz_de.csv", Encoding.GetEncoding("iso-8859-1"));
            foreach (string PLZ in zeilen.Skip(1))
            {
                if (PLZ == "")
                {
                    continue;
                }
                String[] data = PLZ.Split(';');
                Postleitzahl a = new Postleitzahl();
                a.PLZ = int.Parse(data[2]);
                a.OrtMitZusatz = data[0] + data[1];
                a.Bundesland = data[4];
                plz_liste.Add(new Postleitzahl(int.Parse(data[2]), data[0] + data[1], data[4]));
            }
            InitializeComponent();
        }
        private Timer timer1 = new Timer();
        public void InitTimer()
        {
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 2000; // in miliseconds
            if (checkBox1.Checked)
            {
                timer1.Start();
            }
            else
            {
                timer1.Stop();
                this.BackgroundImage = new Bitmap(@"...\Pink.jpg");
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int RandomNumber = rnd.Next(1, 12);

            int caseSwitch = RandomNumber;
            switch (caseSwitch)
            {
                case 1:
                    this.BackgroundImage = new Bitmap(@"...\fa\602151_1.jpg");
                    break;
                case 2:
                    this.BackgroundImage = new Bitmap(@"...\fa\awrDG51_700b.jpg");
                    break;
                case 3:
                    this.BackgroundImage = new Bitmap(@"...\fa\f86a786110f444087bb568b10870c86d09bcc361.jpg");
                    break;
                case 4:
                    this.BackgroundImage = new Bitmap(@"...\fa\GandalfPink.jpg");
                    break;
                case 5:
                    this.BackgroundImage = new Bitmap(@"...\fa\hellovader.jpg");
                    break;
                case 6:
                    this.BackgroundImage = new Bitmap(@"...\fa\images.jpg");
                    break;
                case 7:
                    this.BackgroundImage = new Bitmap(@"...\fa\imagewrjwes.jpg");
                    break;
                case 8:
                    this.BackgroundImage = new Bitmap(@"...\fa\im-fabulous.jpg");
                    break;
                case 9:
                    this.BackgroundImage = new Bitmap(@"...\fa\toh74rucu3y11.jpg");
                    break;
                case 10:
                    this.BackgroundImage = new Bitmap(@"...\fa\u3x1c.jpg");
                    break;
           
                default:
                    break;

            }

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
            public String HausNr;
            public String tel;
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
            textBoxanachname.Text = artikel.nachname;
            textBoxaort.Text = artikel.ort;
            textBoxastaus.Text = artikel.Status;
            textBoxaNickname.Text = artikel.nickname;
            textBoxaGender.Text = artikel.gender;
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
            liste_Suchergebnisse.Clear();
            for (int index = 0;index < anzahl;index++)
            {
                //Suche nach Artikel
                //Eingabe über PK
                if (textBoxpk.Text == tabelle[index].id 
                    || (textBoxAlter.Text != "" ? textBoxAlter.Text == Convert.ToString(tabelle[index].Alter ) : false)
                    || (textBoxplz.Text != "" ? textBoxplz.Text == Convert.ToString(tabelle[index].plz) : false)
                    || (textBoxvorname.Text != "" ? textBoxvorname.Text == tabelle[index].vorname : false)
                    || (textBoxdnachname.Text != "" ? textBoxdnachname.Text == tabelle[index].nachname : false)
                    || (textBoxort.Text != "" ? textBoxort.Text == tabelle[index].ort : false)
                    || (textBoxlstatus.Text != "" ? textBoxlstatus.Text == tabelle[index].Status : false)
                    || (textBoxlnickname.Text != "" ? textBoxlnickname.Text == tabelle[index].nickname : false)
                    || (textBoxstraße.Text != "" ? textBoxstraße.Text == tabelle[index].straße : false)
                    || (textBoxHausnr.Text != "" ? textBoxHausnr.Text == tabelle[index].HausNr : false)
                    || (textBoxemail.Text != "" ? textBoxemail.Text == tabelle[index].Email : false)
                    || (textBoxtel.Text != "" ? textBoxtel.Text == tabelle[index].tel : false))
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
                labelerror.Text = "Upsi da ist was schief gelaufen";
            }
        }
        //User speicher logik
        public static void savecsv1(string filepath, string id, int alter, int plz,
            string vorname, string gender, string nachname, string ort, string Status, string nickname, string straße, string Hausnummar, string tel ,string Email)
        {
            //Erstelle  streamWriter
            using (StreamWriter streamWriter = new StreamWriter(filepath, true))
            {
                streamWriter.WriteLine(id + ";" + alter + ";" + plz + ";" + vorname + ";" + gender + ";" + nachname + ";" + ort + ";" + Status + ";" + nickname + ";"+ straße + ";" 
                    + Hausnummar + ";" + tel + ";" + Email);

            }

        }
     
        // Speicher in Array
        public static class Tabelle
        {
            public static Artikel[] getTabel(string Datei)
            {
                List<Artikel> artikel = new List<Artikel>();
                try {
                    String[] zeilen = File.ReadAllLines(Datei, Encoding.GetEncoding("iso-8859-1"));


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
                savecsv1(@"...\db.csv",
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
                    textBoxaemail.Text
                    )

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
                textBoxstraße.Text = "";
                textBoxaHausNr.Text = "";
                textBoxtel.Text = "";

                
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
                using (StreamWriter streamWriter = new StreamWriter(@"...\db.csv", false))
                {

                    streamWriter.WriteLine("id" + ";" + "alter" + ";" + "plz" + ";" + "vorname" + ";" + "gender" + ";" + "nachname" + ";" + "ort" + ";" + "Status" + ";" + "nickname!" + ";" + "straße" + ";"
                        + "Hausnummar" + ";" + "tel" + ";" + "Email");

                }
                for (; index < anzahl; index++)
                {
                    savecsv1(@"...\db.csv", tabelle[index].id, tabelle[index].Alter, tabelle[index].plz, tabelle[index].vorname,
                        tabelle[index].nachname, tabelle[index].ort, tabelle[index].Status, tabelle[index].nickname, tabelle[index].gender, 
                        tabelle[index].straße, tabelle[index].HausNr, tabelle[index].tel , tabelle[index].Email);
                }
            }
        }
        private void button10_Click(object sender, EventArgs e)
        {
            var tabelle = Tabelle.getTabel(@"...\db.csv");
            int anzahl = tabelle.Length;
            liste_Suchergebnisse = new List<Artikel>();
            int index = 0;
            for (; index < anzahl; index++)
            {
                if (labelpk.Text == tabelle[index].id)
                {
                    tabelle[index].Alter = Convert.ToInt32(textBoxaalter.Text);
                    tabelle[index].plz = Convert.ToInt32(textBoxaplz.Text);
                    tabelle[index].vorname = textBoxavorname.Text;
                    tabelle[index].nachname = textBoxanachname.Text;
                    tabelle[index].ort = textBoxaort.Text;
                    tabelle[index].Status = textBoxastaus.Text;
                    tabelle[index].nickname = textBoxaNickname.Text;
                    tabelle[index].gender = textBoxaGender.Text;
                    tabelle[index].straße = textBoxaStraße.Text;
                    tabelle[index].tel = textBoxatel.Text;
                    tabelle[index].Email = textBoxaemail.Text;
                }
            }
            using (StreamWriter streamWriter = new StreamWriter(@"...\db.csv", false))
            {

                streamWriter.WriteLine("id" + ";" + "alter" + ";" + "plz" + ";" + "vorname" + ";" + "gender" + ";" + "nachname" + ";" + "ort" + ";" + "Status" + ";" + "nickname!" + ";" + "straße" + ";"
                    + "Hausnummar" + ";" + "tel" + ";" + "Email");

            }
            for (index = 0; index < anzahl; index++)
            {
                savecsv1(@"...\db.csv", tabelle[index].id, tabelle[index].Alter, tabelle[index].plz, tabelle[index].vorname,
                    tabelle[index].nachname, tabelle[index].ort, tabelle[index].Status, tabelle[index].nickname, tabelle[index].gender,
                    tabelle[index].straße, tabelle[index].HausNr, tabelle[index].tel, tabelle[index].Email);
            }
            MessageBox.Show("Fertig");
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
            textBoxanachname.ReadOnly = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBoxaort.ReadOnly = false;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBoxaNickname.ReadOnly = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBoxastaus.ReadOnly = false;
        }

        private void textBoxplz_TextChanged(object sender, EventArgs e)
        {
            textBoxort.Clear();
            textBoxbund.Clear();
            if (textBoxplz.Text != "") { 
                foreach (Postleitzahl Postleitzahl in plz_liste)
                {
                    if (Postleitzahl.PLZ == int.Parse(textBoxplz.Text))
                    {
                        textBoxort.Text = Postleitzahl.OrtMitZusatz;
                        textBoxbund.Text = Postleitzahl.Bundesland;
                        break;
                    }
                }
            }
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

        private void Button11_Click(object sender, EventArgs e)
        {
            textBoxaStraße.ReadOnly = false;
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            textBoxaHausNr.ReadOnly = false;
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            textBoxatel.ReadOnly = false;       
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            textBoxaemail.ReadOnly = false;
        }

        private void Button15_Click(object sender, EventArgs e)
        {
            if (gendernr > 0)
            {
                textBoxaGender.ReadOnly = false;
            }
            else
            {
                groupBox1.Visible = false;
                this.BackgroundImage = new Bitmap(@"...\gender.jfif");
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"...\Did_you_just_assume_my_gender.wav");
                player.Play();
                MessageBox.Show("Did you just assume my gender????????");
                gendernr++;
            }
    }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            InitTimer();
             

        }
    }
  

    
}
