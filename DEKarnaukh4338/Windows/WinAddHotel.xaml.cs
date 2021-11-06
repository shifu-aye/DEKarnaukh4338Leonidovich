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
    /// Логика взаимодействия для WinAddHotel.xaml
    /// </summary>
    public partial class WinAddHotel : Window
    {
        private DE_KarnaukhA4338Entities _Context;
        private WinHotels _Window;
        public WinAddHotel(DE_KarnaukhA4338Entities dE_KarnaukhA4338Entities, WinHotels winHotels)
        {
            InitializeComponent();
            this._Context = dE_KarnaukhA4338Entities;
            this._Window = winHotels;
            cmbCntry.ItemsSource = _Context.Country.ToList();
        }

        private void AddHotel_Click(object sender, RoutedEventArgs e)
        {
            _Context.Hotel.Add(new Hotel()
            {
                Name = txtName.Text,
                CountOfStars = Convert.ToInt32(txtCntOfStrs.Text),
                Description = txtDscrp.Text,
                Country = cmbCntry.SelectedItem as Country
            });
            _Context.SaveChanges();
            _Window.RefreshHotels();
            this.Close();
        }
    }
}
