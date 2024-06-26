﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CMS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public static class LogIn {
        private static readonly string admin = "admin";
        private static readonly string password = "admin";
        private static readonly string korisnik = "Dajana";
        private static readonly string sifra = "Radovic";
        private static  string tipKorisnika;
       

        public static string Admin => admin;

        public static string Password => password;

        public static string Korisnik => korisnik;

        public static string Sifra => sifra;

        public static string TipKorisnika { get => tipKorisnika; set => tipKorisnika =value ;}

       
    }
    public partial class MainWindow : Window
    {
       
        public MainWindow()
        {
  
            InitializeComponent();

           TextBoxUserName.Text = "Unesite korisnicko ime";
            TextBoxUserName.Foreground = Brushes.LightSlateGray;

            BoxPassword.Password = "**********";
            BoxPassword.Foreground = Brushes.LightSlateGray;
        }

        private void TextBoxUserName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TextBoxUserName.Text.Trim().Equals("Unesite korisnicko ime")) {
                TextBoxUserName.Text = "";
                TextBoxUserName.Foreground = Brushes.Black;
            }

        }

        private void TextBoxUserName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBoxUserName.Text.Trim().Equals(string.Empty)) {
                TextBoxUserName.Text = "Unesite korisnicko ime";
                TextBoxUserName.Foreground = Brushes.LightSlateGray;
            }

        }

        private void BoxPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            if (BoxPassword.Password.Trim().Equals("**********"))
            {
                BoxPassword.Password = "";
                BoxPassword.Foreground = Brushes.Black;
            }

        }

        private void BoxPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (BoxPassword.Password.Trim().Equals(string.Empty))
            {
                BoxPassword.Password = "**********";
                BoxPassword.Foreground = Brushes.LightSlateGray;
            }

        }

        private void ButtonLogIn_Click(object sender, RoutedEventArgs e)
        {
          
            if (validate())
            {
                if (TextBoxUserName.Text == LogIn.Admin && BoxPassword.Password == LogIn.Password)
                {
                    LogIn.TipKorisnika = LogIn.Admin;
                    TabelarniPrikaz tabelarniPrikaz = new TabelarniPrikaz();
                    tabelarniPrikaz.ShowDialog();
                    TextBoxUserName.Text = "";
                    BoxPassword.Password = "";
                }
                else if (TextBoxUserName.Text == LogIn.Korisnik && BoxPassword.Password == LogIn.Sifra)
                {
                    LogIn.TipKorisnika = LogIn.Korisnik;
                    TabelarniPrikaz tabelarniPrikaz = new TabelarniPrikaz();
                    tabelarniPrikaz.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Pogresno korisnicko ime ili lozinka. Pokusajte ponovo");
                }
               
            }
           

        }
        private void ButtonIzlaz_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private bool validate() {
            bool result = true;
            if (TextBoxUserName.Text.Trim().Equals("") || TextBoxUserName.Text.Trim().Equals("Unesite korisnicko ime"))
            {
                result = false;
                labelaUserGreska.Content = "Popunite polje!";
                labelaUserGreska.Foreground = Brushes.Red;
                TextBoxUserName.BorderBrush = Brushes.Red;

            }
            else {
                labelaUserGreska.Content = "";
                TextBoxUserName.BorderBrush = Brushes.Gray;
            }

            if (BoxPassword.Password.Trim().Equals("") || BoxPassword.Password.Trim().Equals("**********"))
            {
                result = false;
                labelaPasswordGreska.Content = "Popunite polje!";
                labelaPasswordGreska.Foreground = Brushes.Red;
                BoxPassword.BorderBrush = Brushes.Red;
            }
            else {
                labelaPasswordGreska.Content = "";
                BoxPassword.BorderBrush = Brushes.Gray;
            }
            return result;
        }
       

       
        
    }
}
