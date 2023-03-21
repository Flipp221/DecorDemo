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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Window
    {
        public MainPage()
        {
            InitializeComponent();
            Names();
            Roles();
            RefreshComboBox();
            UpdateTovar();
            RefreshFilter();
        }
        private void Mouse_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                DeckorEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(a => a.Reload());
                TovarsList.ItemsSource = DeckorEntities.GetContext().Tovars.ToList();
            }
        }
        private void Red_Click(object sender, RoutedEventArgs e)
        {
            AddTovars page = new AddTovars((sender as Button).DataContext as Tovars);
            page.Show();
            this.Close();
        }
        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var ClientsForRemoving = TovarsList.SelectedItems.Cast<Tovars>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить сдедующие{ClientsForRemoving.Count()} элементов?", "Внимение",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    DeckorEntities.GetContext().Tovars.RemoveRange(ClientsForRemoving);
                    DeckorEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");

                    TovarsList.ItemsSource = DeckorEntities.GetContext().Tovars.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            AddTovars add = new AddTovars(null);
            add.Show();
            this.Close();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainPage = new MainWindow();
            mainPage.Show();
            this.Close();
        }
        private void Names()
        {
            tbFIO.Text = MainWindow.User.FIO;
        }
        private void Roles()
        {
            if (MainWindow.User.IDRoll == 1)
            {
                AddBtn.Visibility = Visibility.Visible;
                DeleteBtn.Visibility = Visibility.Visible;
            }
            if (MainWindow.User.IDRoll == 2)
            {
                AddBtn.Visibility = Visibility.Hidden;
                DeleteBtn.Visibility = Visibility.Hidden;
            }
            if (MainWindow.User.IDRoll == 3)
            {
                AddBtn.Visibility = Visibility.Hidden;
                DeleteBtn.Visibility = Visibility.Hidden;
            }
            if (MainWindow.User.IDRoll == 4)
            {
                AddBtn.Visibility = Visibility.Hidden;
                DeleteBtn.Visibility = Visibility.Hidden;
            }

        }
        private void RefreshComboBox()
        {
            SortCB.Items.Add("По наименованию");
            SortCB.Items.Add("По Скидке");
        }
        private void RefreshFilter()
        {
            foreach (var item in MainWindow.db.TypeT)
                FilterCB.Items.Add(item.Name);
        }
        private void Poisk_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateTovar();
        }
        private void UpdateTovar()
        {
            var currentKeyboard = DeckorEntities.GetContext().Tovars.ToList();

            currentKeyboard = currentKeyboard.Where(p => p.Name.ToLower().Contains(Poisk.Text.ToLower())).ToList();

            TovarsList.ItemsSource = currentKeyboard.OrderBy(p => p.Name).ToList();
        }
        private void SortCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SortCB.SelectedIndex == 0)
            {
                TovarsList.ItemsSource = DeckorEntities.GetContext().Tovars.OrderBy(z => z.Name).ToList();
            }
            if (SortCB.SelectedIndex == 1)
            {
                TovarsList.ItemsSource = DeckorEntities.GetContext().Tovars.OrderBy(z => z.PossibleDiscount).ToList();
            }
        }
        List<Tovars> tovars = MainWindow.db.Tovars.ToList();
        private void FilterCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combobox = (ComboBox)sender;
            string item = Convert.ToString(combobox.SelectedItem);
            if (item == "Фильтрация")
            {
                TovarsList.ItemsSource = tovars;
                return;
            }
            tovars = MainWindow.db.Tovars.Where(z => z.TypeT.Name == item).ToList();
            TovarsList.ItemsSource = tovars;
        }
    }
}
