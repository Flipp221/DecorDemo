using Microsoft.Win32;
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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp1.DataBase;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для AddTovars.xaml
    /// </summary>
    public partial class AddTovars : Window
    {
        OpenFileDialog ofdImage1 = new OpenFileDialog();
        private Tovars _tovars = new Tovars();
        public AddTovars(Tovars selected)
        {
            InitializeComponent();
            if (selected != null)
                _tovars = selected;

            DataContext = _tovars;
            ProizvCB.ItemsSource = DeckorEntities.GetContext().Proizvoditel.ToList();
            PostavshCB.ItemsSource = DeckorEntities.GetContext().Postavshik.ToList();
            TypeTCB.ItemsSource = DeckorEntities.GetContext().TypeT.ToList();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(NameTB.Text))
                errors.AppendLine("укажите Наименование");
            if (string.IsNullOrWhiteSpace(UnitOnesTB.Text))
                errors.AppendLine("укажите unit");
            if (string.IsNullOrWhiteSpace(PriceTB.Text))
                errors.AppendLine("укажите цену");
            if (string.IsNullOrWhiteSpace(DiscountTB.Text))
                errors.AppendLine("укажите Возможную скидку");
            if (_tovars.Proizvoditel == null)
                errors.AppendLine("Укажите производителя");
            if (_tovars.Postavshik == null)
                errors.AppendLine("Укажите поставщика");
            if (_tovars.TypeT == null)
                errors.AppendLine("Укажите тип товара");
            if (string.IsNullOrWhiteSpace(DiscTB.Text))
                errors.AppendLine("укажите Скидку");
            if (string.IsNullOrWhiteSpace(CountTB.Text))
                errors.AppendLine("укажите кол-во на складе");
            if (string.IsNullOrWhiteSpace(DescriptionTB.Text))
                errors.AppendLine("укажите описание");



            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_tovars.IdT == 0)
            {
                _tovars.Image = File.ReadAllBytes(ofdImage1.FileName);
                DeckorEntities.GetContext().Tovars.Add(_tovars);
            }
            try
            {
                DeckorEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                MainPage mainPage = new MainPage();
                mainPage.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofdImage = new OpenFileDialog();
            ofdImage.Filter = "Image files|*.bmp;*.jpg;*.png|All files|*.*";
            ofdImage.FilterIndex = 1;
            if (ofdImage.ShowDialog() == true)
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(ofdImage.FileName);
                image.EndInit();
                ofdImage1 = ofdImage;
                img.Source = image;
            }
        }
    }
}
