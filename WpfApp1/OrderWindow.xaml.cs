using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        public OrderWindow()
        {
            InitializeComponent();
        }
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            MainPage main = new MainPage();
            main.Show();
            Close();
        }
        private void Mouse_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                DeckorEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(a => a.Reload());
                DGridServise.ItemsSource = DeckorEntities.GetContext().Order.ToList();
            }
        }
    }
}
