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

namespace DEKarnaukh4338.Windows
{
    /// <summary>
    /// Логика взаимодействия для WinHotels.xaml
    /// </summary>
    public partial class WinHotels : Window
    {
        private Hotel _hotel;
        private int _curPage = 1;
        private int _maxPage = 0;
        public static DE_KarnaukhA4338Entities _Context = new DE_KarnaukhA4338Entities();
        public WinHotels()
        {
            InitializeComponent();
            RefreshHotels();

        }

        public void RefreshHotels()
        {
            dtGrdHotels.ItemsSource = _Context.Hotel.OrderBy(y => y.Name).ToList();
            _maxPage = Convert.ToInt32(Math.Ceiling(_Context.Hotel.ToList().Count * 1.0 / 10));

            var listHotels = _Context.Hotel.ToList().Skip((_curPage - 1) * 10).Take(10).ToList();

            TotalPages.Content = "из " + Convert.ToString(_maxPage);
            TxtCurrentPage.Text = Convert.ToString(_curPage);
            dtGrdHotels.ItemsSource = listHotels;
        }

        private void BtnEdHtlInfo_Click(object sender, RoutedEventArgs e)
        {
            WinEditHotel winEditHotel = new WinEditHotel(_Context, sender, this);
            winEditHotel.Show();
        }

        private void TxtCurrentPage_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TxtCurrentPage.Text != "")
            {
                if (Convert.ToInt32(TxtCurrentPage.Text) >= 0 & Convert.ToInt32(TxtCurrentPage.Text) <= _maxPage)
                {
                    _curPage = Convert.ToInt32(TxtCurrentPage.Text);
                    RefreshHotels();
                }
            }

        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            if (_curPage + 1 > _maxPage)
            {
                MessageBox.Show("Даже не пытайтесь) Страниц там нет!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                _curPage++;
                RefreshHotels();
            }
        }

        private void LastPage_Click(object sender, RoutedEventArgs e)
        {           
            _curPage = _maxPage;
            RefreshHotels();
        }

        private void FirstPage_Click(object sender, RoutedEventArgs e)
        {
            _curPage = 1;
            RefreshHotels();
        }

        private void PreviosPage_Click(object sender, RoutedEventArgs e)
        {
            if (_curPage - 1 < 1)
            {
                MessageBox.Show("Даже не пытайтесь) Страниц там нет!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                _curPage--;
                RefreshHotels();
            }
        }

        private void btnAddHotel_Click(object sender, RoutedEventArgs e)
        {
            Windows.WinAddHotel winAddHotel = new WinAddHotel(_Context, this);
            winAddHotel.Show();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
