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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.DataBase;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static DeckorEntities db = new DeckorEntities();
        public static Users User;
        public static Roles roles;
        public MainWindow()
        {
            InitializeComponent();
            
        }
        private void btn2_Click(object sender, RoutedEventArgs e)
        {
                foreach (var users in MainWindow.db.Users)
                {
                        if (users.Login == tbLogin.Text.Trim())
                        {
                            if (users.Password == tbPassword.Password.Trim() && users.IDRoll == 1)
                            {
                                MessageBox.Show($"Привет Администратор - {users.FIO}");
                                MainWindow.User = users;
                            }
                            if (users.Password == tbPassword.Password.Trim() && users.IDRoll == 2)
                            {
                                MessageBox.Show($"Привет Клиент - {users.FIO}");
                                MainWindow.User = users;
                            }
                            if (users.Password == tbPassword.Password.Trim() && users.IDRoll == 3)
                            {
                                MessageBox.Show($"Привет Менеджер - {users.FIO}");
                                MainWindow.User = users;
                            }
                            if (users.Password == tbPassword.Password.Trim() && users.IDRoll == 4)
                            {
                                MessageBox.Show($"Привет Сотрудник - {users.FIO}");
                                MainWindow.User = users;
                            }
                            if (users.Login == tbLogin.Text.Trim() && users.Password == tbPassword.Password.Trim() && SerTB.Text == textBox1.Text)
                            {
                                MessageBox.Show("Успешно");
                                MainPage mainPage = new MainPage();
                                mainPage.Show();
                                Close();
                            }
                            else
                            {
                                MessageBox.Show("Попробуйте ещё раз");
                            }
                        }
                }
            if (MainWindow.User == null)
            {
                MessageBox.Show("Введите коректные данные");
                textBox1.Visibility = Visibility.Visible;
                SerTB.Visibility = Visibility.Visible;
                String allowchar = " ";
                allowchar = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
                allowchar += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,y,z";
                allowchar += "1,2,3,4,5,6,7,8,9,0";
                char[] a = { ',' };
                String[] ar = allowchar.Split(a);
                String pwd = "";
                string temp = " ";
                Random r = new Random();
                for (int i = 0; i < 6; i++)
                {
                    temp = ar[(r.Next(0, ar.Length))];
                    pwd += temp;
                }
            textBox1.Text = pwd;
            }
        }

        private void btnAuth_Click(object sender, RoutedEventArgs e)
        {
            MainPage mainPage = new MainPage();
            mainPage.Show();
            Close();
        }
    }
}
