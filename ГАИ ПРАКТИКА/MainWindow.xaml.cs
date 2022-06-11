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

namespace ГАИ_ПРАКТИКА
{
    public partial class MainWindow : Window
    {   
        DB_gaiEntities context;
        public MainWindow()
        {
            InitializeComponent();
            context = new DB_gaiEntities();//соединение с БД гаишника
        }
        static int count = 3; 

        private void EnterClick(object sender, RoutedEventArgs e)
        {
           try//типо как Else либо весь код слетел либо соединение с БД при выводе ошибки try{...}catch {тогда}
            {
                {
                    
                    int tabNum = Convert.ToInt32(login.Text);
                    string pass = password.Text;
                   
                    Inspector gai = context.Inspector.ToList().Find(x => x.tabNum == tabNum);
                    if (gai == null)
                    {
                        MessageBox.Show("Некорректный ввод", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        if (gai.password.Equals(pass))
                        {
                            MessageBox.Show("Успешная авторизация!", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);
                            StartWindow window = new StartWindow();
                            window.Show();
                            this.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            MessageBox.Show("Некорректный ввод!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Information);
                            count--;
                        }
                        if (count == 0)
                        {
                            password.IsEnabled = false;
                            login.IsEnabled = false;
                            enterButton.IsEnabled = false;
                        }
                    }

                }
            }
                 catch (FormatException)
                 {
                MessageBox.Show("Некорректный ввод!");
                 }
                 catch
                 {
                MessageBox.Show("Системная ошибка", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Information);
                 }
        }
    }
}
