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
    /// Логика взаимодействия для WinEditHotel.xaml
    /// </summary>
    public partial class WinEditHotel : Window
    {
        private DE_KarnaukhA4338Entities _Context;
        private Hotel _hotel;
        private WinHotels _window;
        public WinEditHotel(DE_KarnaukhA4338Entities Context, object o, Windows.WinHotels winHotels)
        {
            InitializeComponent();
            _hotel = (o as Button).DataContext as Hotel;
            _Context = Context;
            _window = winHotels;

            cmbCntry.ItemsSource = _Context.Country.ToList();
            txtName.Text = _hotel.Name;
            txtCntOfStrs.Text = Convert.ToString(_hotel.CountOfStars);
            txtDscrp.Text = _hotel.Description;
        }

        private void btnDelHotel_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы точно хотите удалить данный отель?", "Предупреждение!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if(result == MessageBoxResult.Yes)
            {
                _Context.Hotel.Remove(_hotel);
                _Context.SaveChanges();
                _window.RefreshHotels();
                this.Close();
            }
        }

        private void btnUpdHotelInfo_Click(object sender, RoutedEventArgs e)
        {
            _hotel.Name = txtName.Text;
            _hotel.CountOfStars = Convert.ToInt32(txtCntOfStrs.Text);
            _hotel.Description = txtDscrp.Text;

            _hotel.Country = cmbCntry.SelectedItem as Country;
            _window.RefreshHotels();
            _Context.SaveChanges();
            this.Close();
        }
    }
}
