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

namespace DEKarnaukh4338
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        private DE_KarnaukhA4338Entities _Context = new DE_KarnaukhA4338Entities();
        public MainWindow()
        {
            InitializeComponent();
            var allTypes = _Context.Type.ToList();
            allTypes.Insert(0, new Type { Name = "Все типы" });
            cmbBox.ItemsSource = allTypes;
            chbox.IsChecked = true;
            cmbBox.SelectedIndex = 0;
            ConnectOdb.db = new DE_KarnaukhA4338Entities();
            LstView.ItemsSource = ConnectOdb.db.Tour.OrderBy(Tour => Tour.Name).ToList();
            
        }
        private void UpdateTours()
        {
            var curTours = _Context.Tour.ToList();
            if (cmbBox.SelectedIndex > 0)
            {
                curTours = curTours.Where(x => x.Type.Contains(cmbBox.SelectedItem as Type)).ToList();
            }
            curTours = curTours.Where(y => y.Name.ToLower().Contains(SearchTxt.Text.ToLower())).ToList();
            if (chbox.IsChecked.Value)
            {
                curTours = curTours.Where(i => i.IsActual).ToList();
            }
            LstView.ItemsSource = curTours.OrderBy(p => p.Name).ToList();
        }
            
        private void HotelBtn_Click(object sender, RoutedEventArgs e)
        {
            Windows.WinHotels winHotels = new Windows.WinHotels();
            winHotels.Show();
            this.Close();
        }

        private void LstView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            LstView.Width = gridik.Width;
            LstView.Height = gridik.Height;
        }

        private void SearchTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateTours();
        }

        private void cmbBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateTours();
        }

        private void chbox_Checked(object sender, RoutedEventArgs e)
        {
            UpdateTours();
        }

        private void chbox_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateTours();
        }
    }
}
