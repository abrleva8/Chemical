using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Don_tKnowHowToNameThis {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow {
        private List<double>? _zCoord;
        private List<double>? _temperature;
        private List<double>? _viscosity;
        private List<double>? _q;
        public MainWindow() {
            InitializeComponent();
            Combo.SelectedIndex = 0;
        }

        private Calc _calc = new();
        private void Window_Loaded(object sender, RoutedEventArgs e) {
            step.Text = _calc.Step.ToString(CultureInfo.CurrentCulture);
            W.Text = _calc.W.ToString(CultureInfo.CurrentCulture);
            H.Text = _calc.H.ToString(CultureInfo.CurrentCulture);
            L.Text = _calc.L.ToString(CultureInfo.CurrentCulture);
            p.Text = _calc.P.ToString(CultureInfo.CurrentCulture);
            c.Text = _calc.C.ToString(CultureInfo.CurrentCulture);
            T0.Text = _calc.T0.ToString(CultureInfo.CurrentCulture);
            Vu.Text = _calc.Vu.ToString(CultureInfo.CurrentCulture);
            Tu.Text = _calc.Tu.ToString(CultureInfo.CurrentCulture);
            mu0.Text = _calc.Mu0.ToString(CultureInfo.CurrentCulture);
            Ea.Text = _calc.Ea.ToString(CultureInfo.CurrentCulture);
            Tr.Text = _calc.Tr.ToString(CultureInfo.CurrentCulture);
            n.Text = _calc.N.ToString(CultureInfo.CurrentCulture);
            alphaU.Text = _calc.AlphaU.ToString(CultureInfo.CurrentCulture);
        }


        private void MenuItem_Click(object sender, RoutedEventArgs e) {
            if (!CalculateLists()) return;
            var table = new Table(_zCoord, _temperature, _viscosity);
            table.Show();
            eff.Content = _calc.Efficiency().ToString(CultureInfo.CurrentCulture);
            T.Content = Math.Round(_temperature![^1], 2).ToString(CultureInfo.CurrentCulture);
            visc.Content = Math.Round(_viscosity![^1], 2).ToString(CultureInfo.CurrentCulture);
            menuItemPlot.IsEnabled = true;
            excelSaveItem.IsEnabled = true;
        }

        private static bool IsGood(double min, double max, TextBox text) {
            double d;
            try {
                d = double.Parse(text.Text);
            } catch {
                text.Text = "0";
                text.Foreground = new SolidColorBrush(Colors.Red);
                return false;
            }
            
            if (min <= d && d <= max) {
                text.Foreground = new SolidColorBrush(Colors.Black);
                return true;
            }
            text.Foreground = new SolidColorBrush(Colors.Red);
            return false;
        }

        private bool CalculateLists() {
            var isGoodData = IsGood(_calc.WMin, _calc.WMax, W);
            if (!IsGood(_calc.HMin, _calc.HMax, H))
                isGoodData = false;
            if (!IsGood(_calc.LMin, _calc.LMax, L))
                isGoodData = false;
            if (!IsGood(_calc.StepMin, _calc.StepMax, step))
                isGoodData = false;
            if (!IsGood(_calc.PMin, _calc.PMax, p))
                isGoodData = false;
            if (!IsGood(_calc.CMin, _calc.CMax, c))
                isGoodData = false;
            if (!IsGood(_calc.T0Min, _calc.T0Max, T0))
                isGoodData = false;
            if (!IsGood(_calc.VuMin, _calc.VuMax, Vu))
                isGoodData = false;
            if (!IsGood(_calc.TuMin, _calc.TuMax, Tu))
                isGoodData = false;
            if (!IsGood(_calc.Mu0Min, _calc.Mu0Max, mu0))
                isGoodData = false;
            if (!IsGood(_calc.EaMin, _calc.EaMax, Ea))
                isGoodData = false;
            if (!IsGood(_calc.TrMin, _calc.TrMax, Tr))
                isGoodData = false;
            if (!IsGood(_calc.NMin, _calc.NMax, n))
                isGoodData = false;
            if (!IsGood(_calc.AlphaUMin, _calc.AlphaUMax, alphaU))
                isGoodData = false;
            if (!isGoodData) return false;
            _calc = new Calc(Convert.ToDouble(W.Text), Convert.ToDouble(H.Text), Convert.ToDouble(L.Text), Convert.ToDouble(step.Text), Convert.ToDouble(p.Text), Convert.ToDouble(c.Text),
                Convert.ToDouble(T0.Text), Convert.ToDouble(Vu.Text), Convert.ToDouble(Tu.Text), Convert.ToDouble(mu0.Text), Convert.ToDouble(Ea.Text), Convert.ToDouble(Tr.Text),
                Convert.ToDouble(n.Text), Convert.ToDouble(alphaU.Text));
            _zCoord = new List<double>();
            _temperature = new List<double>();
            _viscosity = new List<double>();
            _q = new List<double>();

            _calc.MaterialShearStrainRate();
            _calc.SpecificHeatFluxes();
            _calc.VolumeFlowRateOfMaterialFlowInTheChannel();

            for (double z = 0; z <= _calc.L; z += _calc.Step) {
                _zCoord.Add(z);
                var temperature = _calc.Temperature(z);
                _temperature.Add(temperature);
                var viscosity = Math.Round(_calc.Viscosity(temperature), 2);
                _viscosity.Add(viscosity);
                _q.Add(_calc.Efficiency());
            }
            return true;
        }

        private void CheckInputChange(object sender, TextChangedEventArgs e) {
            var a = (TextBox)e.Source;
            //a.Foreground = Brushes.Red;
            if (double.TryParse(a.Text, out _)) {
                a.Foreground = Brushes.Black;
                tableValueButton.IsEnabled = true;
            } else {
                a.Foreground = Brushes.Red;
                //tableValueButton.IsEnabled = false;
            }
        }

        private void menuItemPlot_Click(object sender, RoutedEventArgs e) {
            var windowPlot = new WindowPlot(_zCoord, _temperature, _viscosity);
            windowPlot.Show();
        }

        private void SaveExcel_Click(object sender, RoutedEventArgs e) {
            var sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == false) {
                return;
            }

            var fileName = sfd.FileName;
            var excelWorker = fileName.Contains(".xlsx") ? new ExcelWorker(_zCoord, _temperature, _viscosity, _q,  fileName)
                : new ExcelWorker(_zCoord, _temperature, _viscosity, _q, fileName + ".xlsx");
            excelWorker.SaveToExel();
        }
    }
}
