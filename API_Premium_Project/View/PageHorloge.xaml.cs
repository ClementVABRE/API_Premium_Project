using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml.Linq;
using API_Premium_Project.Service;
using Newtonsoft.Json;

namespace API_Premium_Project.View
{
    /// <summary>
    /// Logique d'interaction pour PageHorloge.xaml
    /// </summary>
    public partial class PageHorloge : UserControl
    {
        public PageHorloge()
        {
            InitializeComponent();

        }
            private void CB_Pays_Horloge_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BTN_Retour_Click2(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Window.GetWindow(this).Close();
        }
    }
}
