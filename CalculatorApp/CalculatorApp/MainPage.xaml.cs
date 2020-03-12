using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CalculatorApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }


        private void tikla(Object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Text == "x²")
            {
                ekran.Text += "²";
            }
            else
            {
                ekran.Text += btn.Text;
            }

        }

        private void sonuc(Object sender, EventArgs e)
        {
            try
            {
                //Yazılımcının imzası :)
                if (ekran.Text == "01-01-1995")
                {
                    ekran.Text = "Murat";
                    ekran2.Text = "Bayraktar";
                }
                else
                {
                    ekran2.Text = parantezici(ekran.Text);
                }
            }
            catch (Exception)
            {

                ekran2.Text = "Hata Oluştu";
            }


        }

        private void temizle(Object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            if (btn.Text == "AC")
            {
                ekran.Text = "";
                ekran2.Text = "";
            }
            else
            {
                try
                {
                    ekran.Text = ekran.Text.Remove(ekran.Text.Length - 1, 1);
                    ekran2.Text = "";
                }
                catch (Exception)
                {
                }               
            }            
        }

        private string parantezici(string text)
        {
            while (text.Contains('(') && text.Contains(')'))
            {
                int acikIndex = 0;
                int kapaliIndex = 0;

                for (int i = 0; i < text.Length; i++)
                {
                    if (text[i] == '(')
                    {
                        acikIndex = i;
                    }
                    if (text[i] == ')')
                    {
                        kapaliIndex = i;

                        //Parantez içini bulur hesaplar ve paranteziyle beraber siler yerine parantez içinin sonucunu yazar.
                        text = text.Remove(acikIndex, kapaliIndex - acikIndex + 1).Insert(acikIndex, parantezCoz(acikIndex, kapaliIndex, text));

                        break;
                    }

                }
            }
            return hesapla(text);

        }

        private string parantezCoz(int acikIndex, int kapaliIndex, string text)
        {
            if (text.Contains(")²"))
            {
                if (double.Parse(hesapla(text.Substring(acikIndex + 1, kapaliIndex - acikIndex - 1))) < 0)
                {
                    double sonuc = double.Parse(hesapla(text.Substring(acikIndex + 1, kapaliIndex - acikIndex - 1))) * -1;
                    string parantezSonucu = sonuc + "";
                    return parantezSonucu;
                }
                else
                {
                    string parantezSonucu = hesapla(text.Substring(acikIndex + 1, kapaliIndex - acikIndex - 1));
                    return parantezSonucu;
                }
            }
            else
            {
                //Parantez içinin sonucunu hesaplar
                string parantezSonucu = hesapla(text.Substring(acikIndex + 1, kapaliIndex - acikIndex - 1));
                return parantezSonucu;
            }
        }

        public string hesapla(string text)
        {
            double sonuc = topla(text);
            return sonuc.ToString();
        }

        public double topla(string text)
        {
            string[] sayilar = text.Split('+');

            double butun;

            for (int i = 0; i < sayilar.Length; i++)
            {
                if (sayilar[i] == "")
                {
                    sayilar[i] = "0";

                }
                if (sayilar[0] == "")
                {
                    butun = 0;
                }
            }

            butun = cikar(sayilar[0]);

            for (int i = 1; i < sayilar.Length; i++)
            {
                butun = butun + cikar(sayilar[i]);
            }
            return butun;
        }

        private double cikar(string text)
        {
            string[] sayilar = text.Split('-');

            double butun;

            for (int i = 0; i < sayilar.Length; i++)
            {
                if (sayilar[i] == "")
                {
                    sayilar[i] = "0";

                }
                if (sayilar[0] == "")
                {
                    butun = 0;
                }
            }

            butun = carp(sayilar[0]);

            for (int i = 1; i < sayilar.Length; i++)
            {
                butun = butun - carp(sayilar[i]);
            }
            return butun;
        }

        private double carp(string text)
        {
            string[] sayilar = text.Split('*');

            double butun = bol(sayilar[0]);
            for (int i = 1; i < sayilar.Length; i++)
            {
                butun = butun * bol(sayilar[i]);
            }
            return butun;
        }

        private double bol(string text)
        {
            string[] sayilar = text.Split('÷');

            double butun = kareAl(sayilar[0]);
            for (int i = 1; i < sayilar.Length; i++)
            {
                butun = butun / kareAl(sayilar[i]);
            }
            return butun;
        }

        private double kareAl(string text)
        {
            string[] sayilar = text.Split('²');

            double butun = kokal(sayilar[0]);

            if (sayilar.Length > 1)
            {
                for (int i = 0; i < sayilar.Length - 1; i++)
                {
                    butun = Math.Pow(kokal(sayilar[i]), 2);

                }
            }
            return butun;
        }

        private double kokal(string text)
        {
            string[] sayilar = text.Split('√');

            double butun;

            if (sayilar[0] != "")
            {
                butun = double.Parse(sayilar[0]);
            }
            else
            {
                butun = 0;
            }
            

            if (sayilar.Length > 1)
            {
                for (int i = 1; i < sayilar.Length; i++)
                {
                    try
                    {
                        butun = Math.Sqrt(topla(sayilar[i]));
                    }
                    catch (Exception)
                    {
                        ekran2.Text = "Hata Oluştu.";
                    }
                }
            }
            return butun;
        }
    }
}
