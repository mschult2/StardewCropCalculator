// // Copyright (c) Microsoft. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.ComponentModel;

namespace StardewCropCalculator
{
    public class LineItem : INotifyPropertyChanged
    {
        private string _name = "(Crop Name)";
        private int _timeToMature = 1000;
        private int _timeBetweenHarvests = 1000;
        private int _cost = 0;
        private int _sell = 0;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public int TimeToMature
        {
            get { return _timeToMature; }
            set
            {
                _timeToMature = value;
                OnPropertyChanged("TimeToMature");
            }
        }

        public int TimeBetweenHarvests
        {
            get { return _timeBetweenHarvests; }
            set
            {
                _timeBetweenHarvests = value;
                OnPropertyChanged("TimeBetweenHarvests");
            }
        }

        public int Cost
        {
            get { return _cost; }
            set
            {
                _cost = value;
                OnPropertyChanged("Cost");
            }
        }

        public int Sell
        {
            get { return _sell; }
            set
            {
                _sell = value;
                OnPropertyChanged("Sell");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}