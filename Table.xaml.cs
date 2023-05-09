using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace Don_tKnowHowToNameThis {
    /// <summary>
    /// Логика взаимодействия для Table.xaml
    /// </summary>
    public partial class Table {
        readonly List<double>? _zCoords;
        readonly List<double>? _temperature;
        readonly List<double>? _viscosity;
        readonly BindingList<MyList> _data = new();
        public Table(List<double>? z, List<double>? T, List<double>? n) {
            InitializeComponent();
            _zCoords = z;
            _temperature = T;
            _viscosity = n;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e) {
            for (var i = 0; i < _temperature!.Count; i++) {
                _data.Add(new() { Z = Math.Round(_zCoords![i], 2), T = Math.Round(_temperature[i], 2), N = Math.Round(_viscosity![i], 2) });
            }
            table.ItemsSource = _data;
        }
    }
}
