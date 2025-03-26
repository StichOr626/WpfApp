using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace WpfApp
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private ObservableCollection<Company> _companies;

        public ObservableCollection<Company> Companies
        {
            get { return _companies; }
            set { _companies = value; OnPropertyChanged("Companies"); }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Companies = new ObservableCollection<Company>
            {
                new Company { Name = "", ProductCount = 0, DiscountLevel = DiscountType.Bronze },
                new Company { Name = "", ProductCount = 0, DiscountLevel = DiscountType.Bronze },
                new Company { Name = "", ProductCount = 0, DiscountLevel = DiscountType.Bronze }
            };
        }

        private void CalculateDiscounts(object sender, RoutedEventArgs e)
        {
            foreach (var company in Companies)
            {
                company.CalculateDiscount();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public enum DiscountType
    {
        Bronze = 5,
        Silver = 10,
        Gold = 15
    }

    public static class DiscountTypeHelper
    {
        public static Array Values => Enum.GetValues(typeof(DiscountType));
    }

    public class Company : INotifyPropertyChanged
    {
        private string _name;
        private int _productCount;
        private DiscountType _discountLevel;
        private double _totalDiscount;

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("Name"); }
        }

        public int ProductCount
        {
            get { return _productCount; }
            set { _productCount = value; OnPropertyChanged("ProductCount"); }
        }

        public DiscountType DiscountLevel
        {
            get { return _discountLevel; }
            set { _discountLevel = value; OnPropertyChanged("DiscountLevel"); }
        }

        public double TotalDiscount
        {
            get { return _totalDiscount; }
            set { _totalDiscount = value; OnPropertyChanged("TotalDiscount"); }
        }

        public void CalculateDiscount()
        {
            double baseDiscount = (double)DiscountLevel;
            if (ProductCount >= 10000 && ProductCount < 50000) baseDiscount += 5;
            else if (ProductCount >= 50000 && ProductCount < 300000) baseDiscount += 10;
            else if (ProductCount >= 300000 && ProductCount < 1000000000) baseDiscount += 15;

            TotalDiscount = baseDiscount;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

