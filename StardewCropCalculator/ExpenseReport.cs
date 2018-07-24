using System;
using System.ComponentModel;

// EventHandler
// ObservableCollection

// INotifyPropertyChanged, PropertyChangedEventArgs

/// <summary>
/// UI stuff taken from the Microsoft WPF demo, ExpenseIt: 
/// https://github.com/Microsoft/WPF-Samples/tree/master/Sample%20Applications/ExpenseIt
/// </summary>
namespace StardewCropCalculator
{
    public class ExpenseReport : INotifyPropertyChanged
    {
        private string _alias;
        private string _costCenter;
        private string _employeeNumber;
        private int _totalExpenses;

        public ExpenseReport()
        {
            LineItems = new LineItemCollection();
            LineItems.LineItemCostChanged += OnLineItemCostChanged;
        }

        public string Alias
        {
            get { return _alias; }
            set
            {
                _alias = value;
                OnPropertyChanged("Alias");
            }
        }

        public string CostCenter
        {
            get { return _costCenter; }
            set
            {
                _costCenter = value;
                OnPropertyChanged("CostCenter");
            }
        }

        public string EmployeeNumber
        {
            get { return _employeeNumber; }
            set
            {
                _employeeNumber = value;
                OnPropertyChanged("EmployeeNumber");
            }
        }

        public int TotalExpenses
        {
            // calculated property, no setter
            get
            {
                RecalculateTotalExpense();
                return _totalExpenses;
            }
        }

        public LineItemCollection LineItems { get; }
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnLineItemCostChanged(object sender, EventArgs e)
        {
            OnPropertyChanged("TotalExpenses");
        }

        private void RecalculateTotalExpense()
        {
            _totalExpenses = 0;
            foreach (var item in LineItems)
                _totalExpenses += item.Cost;
        }

        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}