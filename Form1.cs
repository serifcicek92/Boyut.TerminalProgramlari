using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Boyut.TerminalProgramlari
{
    public partial class Form1 : Form
    {
        private FileInfo[] files;
        private int ekranBoy;
        private int ekranGenislik;
        public Form1()
        {
            InitializeComponent();
            ekraniBoyutlandir();
            Masaustundekiler();
            acilistaCalistir();

            string hostName = Dns.GetHostName();
            string myIp = Dns.GetHostByName(hostName).AddressList[0].ToString();
            string[] ipBloklari = myIp.Split('.');


            String uName = Environment.UserName;
            string calcIp = uName.Substring(uName.Length - 2, 2);

            label1.Text = uName + "-";


            if (int.TryParse(calcIp, out int output))
            {
                label1.Text += ipBloklari[0] + "." + ipBloklari[1] + "." + ipBloklari[2] + "." + (Convert.ToInt32(calcIp) + 10);
            }
            else if (int.TryParse(uName.Substring(uName.Length - 1, 1), out int output2))
            {
                string calcIp2 = uName.Substring(uName.Length - 1, 1);
                label1.Text += ipBloklari[0] + "." + ipBloklari[1] + "." + ipBloklari[2] + "." + (Convert.ToInt32(calcIp2) + 10);
            }

            //label1.Text = uName + "-" + calcIp;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void ekraniBoyutlandir()
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
            ekranGenislik = SystemInformation.VirtualScreen.Width;
            ekranBoy = SystemInformation.VirtualScreen.Height;
            this.Size = new Size(ekranGenislik, ekranBoy);
        }

        public void Masaustundekiler()
        {
            int birKenar;
            string dosyaYolu;
            int x = 0;
            int y = 0;
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            DirectoryInfo d = new DirectoryInfo(path);
            this.files = d.GetFiles("*");

            if (files.Count() < 7)
            {
                birKenar = ekranGenislik / 2 - 50;
            }
            else
            {
                birKenar = ekranGenislik / 6 - 50;
            }



            if (ekranBoy < ekranGenislik)
            {
                //birKenar = (birKenar) - 100;
                birKenar = (birKenar) - Math.Abs(birKenar - 120);

                int say = 1;
                foreach (var item in files)
                {

                    var linkBol = item.FullName.Split('\\');
                    string dosya = linkBol[linkBol.Count() - 1];


                    if (trReplace(dosya).ToUpper().Contains("GIRIS") ||
                        trReplace(dosya).ToUpper().Contains("SAYIM") ||
                        trReplace(dosya).ToUpper().Contains("SEVKIYAT") ||
                        trReplace(dosya).ToUpper().Contains("SEPET") ||
                        trReplace(dosya).ToUpper().Contains("REYON") ||
                        trReplace(dosya).ToUpper().Contains("RAF") ||
                        trReplace(dosya).ToUpper().Contains("KONTROL"))
                    {
                        addNewButton(x + 20, y + 20, item.FullName, birKenar);
                        x += birKenar + 20;
                        if (say == 3 || say == 6)
                        {
                            y += birKenar + 20;
                            x = 1;

                        }

                        //Siparis_kontrol - Kısayol 
                        //if (toplamSatir % 3 == 0 && toplamSatir != okunanSatirlar.Count )
                        //{
                        //    y += birKenari+20;
                        //    x = 1;

                        //}
                        say++;
                    }
                }



            }
            else
            {
                //MOBil
                birKenar = (ekranGenislik / 2) - 50;
                int say = 1;
                foreach (var item in files)
                {
                    var linkBol = item.FullName.Split('\\');
                    string dosya = linkBol[linkBol.Count() - 1];



                    if (trReplace(dosya).ToUpper().Contains("GIRIS") ||
                        trReplace(dosya).ToUpper().Contains("SAYIM") ||
                        trReplace(dosya).ToUpper().Contains("SEVKIYAT") ||
                        trReplace(dosya).ToUpper().Contains("SEPET") ||
                        trReplace(dosya).ToUpper().Contains("REYON") ||
                        trReplace(dosya).ToUpper().Contains("SPOT") ||
                        trReplace(dosya).ToUpper().Contains("RAF") ||
                        trReplace(dosya).ToUpper().Contains("KONTROL"))

                    {
                        addNewButton(x + 40, y + 20, item.FullName, birKenar);
                        x += birKenar + 20;
                        if (say % 2 == 0 || say == 4 || say == 6 || say == 2)
                        {
                            y += birKenar + 20;
                            x = 1;
                        }

                        //if (toplamSatir % 3 == 0 && toplamSatir != okunanSatirlar.Count )
                        //{
                        //    y += birKenari+20;
                        //    x = 1;

                        //}
                        say++;
                    }

                }

            }


        }

        public Button addNewButton(int x, int y, string yol, int kenar)
        {
            Button button1 = new Button();
            this.Controls.Add(button1);
            button1.Top = y;
            button1.Left = x;
            button1.Width = kenar;
            button1.Height = kenar;
            button1.Text = yol;
            button1.BackgroundImageLayout = ImageLayout.Stretch;



            var linkBol = yol.Split('\\');
            string dosya = linkBol[linkBol.Count() - 1];
            if (trReplace(dosya).ToUpper().Contains("GIRIS")) { button1.BackColor = Color.Red; button1.Text = ""; button1.BackgroundImage = Properties.Resources.malgiris; }
            if (trReplace(dosya).ToUpper().Contains("SAYIM")) { button1.BackColor = Color.Blue; button1.Text = ""; button1.BackgroundImage = Properties.Resources.sayim; }
            else if (trReplace(dosya).ToUpper().Contains("SEVK")) { button1.BackColor = Color.LightGray; button1.Text = ""; button1.BackgroundImage = Properties.Resources.sevkiyat; }
            else if (trReplace(dosya).ToUpper().Contains("SEPET")) { button1.BackColor = Color.LightBlue; button1.Text = ""; button1.BackgroundImage = Properties.Resources.sepet; button1.ForeColor = Color.Black; }
            else if (trReplace(dosya).ToUpper().Contains("REYON SORGU") || trReplace(dosya).ToUpper().Contains("REYON_SORGU")) { button1.BackColor = Color.LightBlue; button1.Text = ""; button1.BackgroundImage = Properties.Resources.reyon_sorgu; button1.ForeColor = Color.Black; }
            else if (trReplace(dosya).ToUpper().Contains("SPOT") || trReplace(dosya).ToUpper().Contains("SPOT")) { button1.BackColor = Color.LightBlue; button1.Text = ""; button1.BackgroundImage = Properties.Resources.spotreyon; button1.ForeColor = Color.Black; }
            else if (trReplace(dosya).ToUpper().Contains("RAF") || trReplace(dosya).ToUpper().Contains("RAF"))
            {
                if (trReplace(dosya).ToUpper().Contains("ECZANE") && trReplace(dosya).ToUpper().Contains("RAF"))
                {
                    button1.BackColor = Color.LightBlue;
                    button1.Text = "";
                    button1.BackgroundImage = Properties.Resources.RAFECZANE;
                    button1.BackgroundImageLayout = ImageLayout.Stretch;
                    button1.ForeColor = Color.Black;

                }
                else if (trReplace(dosya).ToUpper().Contains("HASTANE") && trReplace(dosya).ToUpper().Contains("RAF"))
                {
                    button1.BackColor = Color.LightBlue;
                    button1.Text = "";
                    button1.BackgroundImage = Properties.Resources.RAFHASTANE;
                    button1.BackgroundImageLayout = ImageLayout.Stretch;
                    button1.ForeColor = Color.Black;

                }
                else
                {
                    button1.BackColor = Color.LightBlue;
                    button1.Text = "";
                    button1.BackgroundImage = Properties.Resources.raf;
                    button1.ForeColor = Color.Black;
                }
            }
            else if (trReplace(dosya).ToUpper().Contains("REYON") || trReplace(dosya).ToUpper().Contains("KONTROL"))
            {
                button1.BackColor = Color.Yellow; button1.Text = ""; button1.ForeColor = Color.Black;
                button1.BackgroundImageLayout = ImageLayout.Stretch;
                if (trReplace(dosya).ToUpper().Contains("ECZANE") && trReplace(dosya).ToUpper().Contains("REYON"))
                {
                    button1.BackgroundImage = Properties.Resources.eczane;
                    button1.BackgroundImageLayout = ImageLayout.Stretch;
                }
                else if (trReplace(dosya).ToUpper().Contains("HASTANE") && trReplace(dosya).ToUpper().Contains("REYON"))
                {
                    button1.BackgroundImage = Properties.Resources.hastane;
                    button1.BackgroundImageLayout = ImageLayout.Stretch;

                }
                else
                {
                    button1.BackgroundImage = Properties.Resources.sipariskontrol;
                    button1.BackgroundImageLayout = ImageLayout.Stretch;

                }

            }
            button1.Font = new Font("Microsoft Sans Serif", 25F, FontStyle.Bold);





            button1.Click += new EventHandler(
                (sender, e) => tiklananButton(sender, e, yol, "bbb")
                );
            return button1;
        }
        void tiklananButton(object sender, EventArgs e, string program, string exe)
        {


            Process.Start(program);
            this.SendToBack();
        }

        void acilistaCalistir()
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                var a = "fafd";
                if (key.GetValue("Boyut.TerminalProgramlari") != null)
                { // Eğer regeditte varsa, checkbox ı işaretle "\"" + Application.ExecutablePath + "\""

                }
                else
                {
                    key.SetValue("Boyut.TerminalProgramlari", "\"" + Application.ExecutablePath + "\"");

                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }


            //RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
            //key.DeleteValue(ProgramAdi);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private String trReplace(string metin)
        {
            metin = metin.ToUpper();
            //metin.Replace("ş", "S");
            //metin.Replace("i", "I");
            //metin.Replace("ı", "I");
            //metin.Replace("ç", "Ç");
            //metin.Replace("ğ", "G");
            //metin.Replace("ö", "O");
            //metin.Replace("ü", "U");
            //metin = metin.ToUpper();
            Encoding srcEncoding = Encoding.UTF8;
            Encoding destEncoding = Encoding.GetEncoding(1252); // Latin alphabet

            metin = destEncoding.GetString(Encoding.Convert(srcEncoding, destEncoding, srcEncoding.GetBytes(metin)));

            string normalizedString = metin.Normalize(NormalizationForm.FormD);
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < normalizedString.Length; i++)
            {
                if (!CharUnicodeInfo.GetUnicodeCategory(normalizedString[i]).Equals(UnicodeCategory.NonSpacingMark))
                {
                    result.Append(normalizedString[i]);
                }
            }
            string metinss = result.ToString().ToUpper();
            return metinss;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            WindowsLogOff();
        }
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool ExitWindowsEx(uint uFlags, uint dwReason);

        public static bool WindowsLogOff()
        {
            //return ExitWindowsEx(0, 0);
            return ExitWindowsEx(0 | 0x00000004, 0);
        }
    }
}
