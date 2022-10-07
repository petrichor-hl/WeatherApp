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
using WeatherApp.Model;

namespace WeatherApp.ViewModel.Controls
{
    /// <summary>
    /// Interaction logic for CityControl.xaml
    /// </summary>
    public partial class CityControl : UserControl
    {
        public City City
        {
            get { return (City)GetValue(CityProperty); }
            set { SetValue(CityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CityProperty =
            DependencyProperty.Register("City", typeof(City), typeof(CityControl), new PropertyMetadata(null, SetText));

        private static void SetText(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CityControl control = d as CityControl;
            if (control != null)
            {
                City newValueCity = e.NewValue as City;

                control.cityNameTextBlock.Text = newValueCity.LocalizedName;
                control.countryTextBlock.Text = newValueCity.Country.LocalizedName;
            }
        }

        public CityControl()
        {
            InitializeComponent();
        }


    }
}
