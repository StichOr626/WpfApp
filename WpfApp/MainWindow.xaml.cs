using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Company> Companies { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Companies = new ObservableCollection<Company>
            {
                new Company(),
                new Company(),
                new Company()
            };
            DataContext = this;
        }

        private void CalculateDiscounts(object sender, RoutedEventArgs e)
        {
            foreach (var company in Companies)
            {
                company.CalculateDiscount();
            }
        }
    }

    public class Company : INotifyPropertyChanged
    {
        private string _name;
        private int _productCount;
        private bool _isBronze, _isSilver, _isGold;
        private double _totalDiscount;

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        public int ProductCount
        {
            get => _productCount;
            set { _productCount = value; OnPropertyChanged(nameof(ProductCount)); }
        }

        public bool IsBronze
        {
            get => _isBronze;
            set { _isBronze = value; OnPropertyChanged(nameof(IsBronze)); }
        }

        public bool IsSilver
        {
            get => _isSilver;
            set { _isSilver = value; OnPropertyChanged(nameof(IsSilver)); }
        }

        public bool IsGold
        {
            get => _isGold;
            set { _isGold = value; OnPropertyChanged(nameof(IsGold)); }
        }

        public double TotalDiscount
        {
            get => _totalDiscount;
            set { _totalDiscount = value; OnPropertyChanged(nameof(TotalDiscount)); }
        }

        public void CalculateDiscount()
        {
            double levelDiscount = IsBronze ? 5 : IsSilver ? 10 : IsGold ? 15 : 0;
            double productDiscount = ProductCount switch
            {
                >= 10000 and < 50000 => 5,
                >= 50000 and < 300000 => 10,
                >= 300000 and < 1000000000 => 15,
                _ => 0
            };

            TotalDiscount = levelDiscount + productDiscount;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
