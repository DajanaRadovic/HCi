using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using RichTextBox = System.Windows.Controls.RichTextBox;

namespace CMS
{
    /// <summary>
    /// Interaction logic for FormaWindow.xaml
    /// </summary>
    public partial class FormaWindow : Window
    {
        bool changed = false;
        int wordNumber;
        public FormaWindow()
        {
            
            InitializeComponent();
            textBoxId.Text = "Unesite ID";
            textBoxId.Foreground = Brushes.LightSlateGray;

            comboBoxMetal.ItemsSource = Liste.metali;
            comboBoxBoja.ItemsSource = Liste.boje;
            comboBoxOblikKamena.ItemsSource = Liste.oblici;
            ComboBoxCena.ItemsSource = Liste.cene;

            cmbFontFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f=>f.Source);
            cmbFontSize.ItemsSource = new List<double>() { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
            WordNumber.Text = "0";
            DataContext = this;
            rtbEditor.Focus();

            
        }

        private void textBoxId_GotFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxId.Text.Trim().Equals("Unesite ID"))
            {
                textBoxId.Text = "";
                textBoxId.Foreground = Brushes.Black;
            }

        }

        private void textBoxId_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxId.Text.Trim().Equals(string.Empty))
            {
                textBoxId.Text = "Unesite ID";
                textBoxId.Foreground = Brushes.LightSlateGray;
            }
        }

        private bool validate() {
            bool result = true;
            if (textBoxId.Text.Trim().Equals("") || textBoxId.Text.Trim().Equals("Unesite ID"))
            {
                result = false;
                labelaIdGreska.Content = "Polje ne smije da bude prazno!";
                textBoxId.BorderBrush = Brushes.Red;
            } else if(Convert.ToInt32(textBoxId.Text) < 0){
                labelaIdGreska.Content = "Ne smije da bude negativna vrijednost!";
                
            }
            else {
                labelaIdGreska.Content = "";
                textBoxId.BorderBrush = Brushes.Gray;
            }

            if (comboBoxMetal.SelectedItem == null)
            {
                result = false;
                labelaMetalGreska.Content = "Mora biti odabrana opcija!";
                comboBoxMetal.BorderThickness = new Thickness(1);
                comboBoxMetal.BorderBrush = Brushes.Red;
            }
            else {
                labelaMetalGreska.Content = "";
                comboBoxMetal.BorderBrush = Brushes.Gray;
            }

            if (comboBoxBoja.SelectedItem == null)
            {
                result = false;
                labelaBojaGreska.Content = "Mora biti odabrana opcija!";
                comboBoxBoja.BorderThickness = new Thickness(1);
                comboBoxBoja.BorderBrush = Brushes.Red;
            }
            else
            {
                labelaBojaGreska.Content = "";
                comboBoxBoja.BorderBrush = Brushes.Gray;
            }
            /*
            if (DatePicker.SelectedDate == null)
            {
                result = false;
                labelaDateGreska.Content = "Mora biti odabran datum!";
                DatePicker.BorderThickness = new Thickness(1);
                DatePicker.BorderBrush = Brushes.Red;
            }
            else {
                labelaDateGreska.Content = "";
                DatePicker.BorderBrush = Brushes.Gray;
            }*/

            if (comboBoxOblikKamena.SelectedItem == null)
            {
                result = false;
                labelaOblikKamenaGreska.Content = "Mora biti odabrana opcija!";
                comboBoxOblikKamena.BorderThickness = new Thickness(1);
                comboBoxOblikKamena.BorderBrush = Brushes.Red;
            }
            else
            {
                labelaOblikKamenaGreska.Content = "";
                comboBoxOblikKamena.BorderBrush = Brushes.Gray;
            }

            if (ComboBoxCena.SelectedItem == null)
            {
                result = false;
                labelaCenaGreska.Content = "Mora biti odabrana opcija!";
                ComboBoxCena.BorderThickness = new Thickness(1);
                ComboBoxCena.BorderBrush = Brushes.Red;
            }
            else {
                labelaCenaGreska.Content = "";
                ComboBoxCena.BorderBrush = Brushes.Gray;
               
            }
            foreach (Prsten p in TabelarniPrikaz.Prstenovi)
            {
                if (textBoxId.Text.Trim().ToUpper() == p.Id.ToString().ToUpper())
                {
                    result = false;
                    textBoxId.BorderBrush = Brushes.Red;
                    System.Windows.MessageBox.Show("Ovaj prsten već postoji!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            return result;
        }

        private void DugmeZaSlanje_Click(object sender, RoutedEventArgs e)
        {
            
            if (validate())
            {
                
                    TabelarniPrikaz.Prstenovi.Add(new Prsten(Convert.ToInt32(textBoxId.Text), comboBoxMetal.SelectedValue.ToString(), comboBoxBoja.SelectedValue.ToString(), DateTime.Now, comboBoxOblikKamena.SelectedValue.ToString(), ComboBoxCena.SelectedValue.ToString(), image.Source.ToString(), textBoxBrend.Text, true));
               
                this.Close();
            }
           /* else {
                MessageBox.Show("Polja nisu dobro popunjena!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
            }*/
        }

        private void buttonSlika_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" + "JPEG (.jpg;.jpeg)|*.jpg;*.jpeg|" + "Portable Network Graphic (.png)|.png";

            if (dlg.ShowDialog() == true)
            {
                image.Source = new BitmapImage(new Uri(dlg.FileName));
            }
        }

        private void btnBold_Checked(object sender, RoutedEventArgs e)
        {
            changed = true;
            if (btnBold.IsChecked == true)
            {
                btnBold.Background.Opacity = 1;
            }
            else {
                btnBold.Background.Opacity = 0.5;
            }
        }

        private void rtbEditor_SelectionChanged(object sender, RoutedEventArgs e)
        {
            object temp = rtbEditor.Selection.GetPropertyValue(Inline.FontWeightProperty);
            btnBold.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontWeights.Bold));
            if (btnBold.IsChecked == true)
            {
                btnBold.Background.Opacity = 1;
            }
            else {
                btnBold.Background.Opacity = 0.5;
            }
            temp = rtbEditor.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            btnItalic.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontStyles.Italic));
            if (btnItalic.IsChecked == true)
            {
                btnItalic.Background.Opacity = 1;
            }
            else {
                btnItalic.Background.Opacity = 0.5;
            }
            temp = rtbEditor.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            btnUnderline.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(TextDecorations.Underline));
            if (btnUnderline.IsChecked == true)
            {
                btnUnderline.Background.Opacity = 1;
            }
            else {
                btnUnderline.Background.Opacity = 0.5;
            }
            temp = rtbEditor.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            cmbFontFamily.SelectedItem = temp;
            temp = rtbEditor.Selection.GetPropertyValue(Inline.FontSizeProperty);
            try
            {
                Int32.Parse(temp.ToString());
                cmbFontSize.Text = temp.ToString();
            }
            catch (Exception exc) { 
            }

            var boja = rtbEditor.Selection.GetPropertyValue(Inline.ForegroundProperty);
            try
            {
                btnColor.Background = (Brush)boja;
            }
            catch (Exception exc)
            {

            }
            changed = true;
            wordNumber = 0;
            string text = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd).Text;
            var txt = text.Trim();
            int wordCounter = 0;
            int index = 0;

            while (index < txt.Length) {
                while (index < txt.Length && !char.IsWhiteSpace(txt[index]))
                    index++;
                wordCounter++;
                while (index < txt.Length && char.IsWhiteSpace(txt[index])) //preskoci razmak do sledece rijeci
                    index++;
            }
            wordNumber = wordCounter;
            WordNumber.Text = wordCounter.ToString();
        }

        private void btnItalic_Checked(object sender, RoutedEventArgs e)
        {
            changed = true;
            if (btnItalic.IsChecked == true)
            {
                btnItalic.Background.Opacity = 1;
            }
            else
            {
                btnItalic.Background.Opacity = 0.5;
            }
        }

        private void btnUnderline_Checked(object sender, RoutedEventArgs e)
        {
            changed = true;
            if (btnUnderline.IsChecked == true)
            {
                btnUnderline.Background.Opacity = 1;
            }
            else {
                btnUnderline.Background.Opacity = 0.5;
            }
        }

        

        private void cmbFontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbFontFamily.SelectedItem != null) {
                rtbEditor.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, cmbFontFamily.SelectedItem);
            }
            rtbEditor.Focus();
        }

        private void cmbFontSize_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Int32.Parse(cmbFontSize.Text.Trim());
                if (Int32.Parse(cmbFontSize.Text.Trim()) < 1000) {
                    rtbEditor.Selection.ApplyPropertyValue(Inline.FontSizeProperty, cmbFontSize.Text);
                }
                rtbEditor.Focus();
            }
            catch (Exception exc) {
                System.Windows.MessageBox.Show("Nije validno!", "CMS", MessageBoxButton.OK, MessageBoxImage.Warning);
                rtbEditor.Focus();
                object temp = rtbEditor.Selection.GetPropertyValue(Inline.FontSizeProperty);
                try
                {
                    Int32.Parse(temp.ToString());
                    cmbFontSize.Text = temp.ToString();
                }
                catch (Exception ex)
                {

                }



            }

        }
        /*
        private void cmbFontSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbFontFamily.SelectedItem != null) {
                rtbEditor.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, cmbFontFamily.SelectedItem);

            }
            rtbEditor.Focus();
        }*/

        private void btnColor_Click(object sender, RoutedEventArgs e)
        {
            changed = true;
            fontcolor(rtbEditor);
        }

        private void fontcolor(RichTextBox r) {
            var colorDialog = new System.Windows.Forms.ColorDialog();
            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                var wpfcolor = Color.FromArgb(colorDialog.Color.A, colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B);
                btnColor.Background = new SolidColorBrush(wpfcolor);

                TextRange range = new TextRange(r.Selection.Start, r.Selection.End);
                range.ApplyPropertyValue(FlowDocument.ForegroundProperty, new SolidColorBrush(wpfcolor));
                rtbEditor.Selection.ApplyPropertyValue(Inline.ForegroundProperty, new SolidColorBrush(wpfcolor));
                rtbEditor.Focus();

            }
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (wordNumber == 0 && changed == false)
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
                if (dlg.ShowDialog() == true)
                {
                    FileStream fileStream = new FileStream(dlg.FileName, FileMode.Open);
                    TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
                    range.Load(fileStream, System.Windows.DataFormats.Rtf);
                    fileStream.Close();
                    changed = false;
                }
            }
            else
            {
                var result = System.Windows.MessageBox.Show("Do you want to save?", "Mini Text Editor", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                    dlg.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
                    if (dlg.ShowDialog() == true)
                    {
                        FileStream fileStream = new FileStream(dlg.FileName, FileMode.Create);
                        TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
                        range.Save(fileStream, System.Windows.DataFormats.Rtf);
                        fileStream.Close();
                    }

                    OpenFileDialog dlg1 = new OpenFileDialog();
                    dlg1.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
                    if (dlg1.ShowDialog() == true)
                    {
                        FileStream fileStream = new FileStream(dlg1.FileName, FileMode.Open);
                        TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
                        range.Load(fileStream, System.Windows.DataFormats.Rtf);
                        fileStream.Close();
                        WordNumber.Text = "0";
                        wordNumber = 0;
                        changed = false;
                    }
                }
                else if (result == MessageBoxResult.No)
                {
                    OpenFileDialog dlg1 = new OpenFileDialog();
                    dlg1.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
                    if (dlg1.ShowDialog() == true)
                    {
                        FileStream fileStream = new FileStream(dlg1.FileName, FileMode.Open);
                        TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
                        range.Load(fileStream, System.Windows.DataFormats.Rtf);
                        fileStream.Close();
                        WordNumber.Text = "0";
                        wordNumber = 0;
                        changed = false;
                    }
                }
            }
        }
        private void Save_Executed(object sender, ExecutedRoutedEventArgs e) {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
            if (dlg.ShowDialog() == true) { 
                FileStream FileStream = new FileStream(dlg.FileName, FileMode.Create);
                TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
                range.Save(FileStream, System.Windows.DataFormats.Rtf);
                FileStream.Close();
                changed = false;
            }
        }
    }
}
