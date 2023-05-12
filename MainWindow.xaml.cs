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
        private string _currentMaterial = null!;
        private Calc _calc = new();
        public MainWindow() {
            InitializeComponent();
            InitMaterialComboBox();
            InitialMaterials();
            InitialChannels();
        }

        private void InitMaterialComboBox() {
            DataBaseWorker.GetMaterials().ForEach(material => MaterialComboBox.Items.Add(material));
            MaterialComboBox.SelectedIndex = 0;
        }

        // TODO: добавить сюда результат работы с бд
        private void InitialMaterials() {
            _calc.Material = GetMaterialsFromBd();
        }
        
        private void InitialChannels() {
            _calc.Channel = GetChannelFromBd();
        }

        private Channel GetChannelFromBd() {
            return new (0.20, 0.009, 4.5);
        }

        private Material GetMaterialsFromBd() {
            var materialsValues = DataBaseWorker.GetMaterialsValues(MaterialComboBox.Text);
            double ro = Double.Parse(materialsValues["Плотность"]);
            double c = Double.Parse(materialsValues["Удельная теплоеёмкость"]);
            double t = Double.Parse(materialsValues["Температура плавления"]);
            Material m = new Material(ro, c, t);
            return m;
        }

       
        private void Window_Loaded(object sender, RoutedEventArgs e) {
            step.Text = _calc.Step.ToString(CultureInfo.CurrentCulture);
            W.Text = _calc.Channel.W.ToString(CultureInfo.CurrentCulture);
            H.Text = _calc.Channel.H.ToString(CultureInfo.CurrentCulture);
            L.Text = _calc.Channel.L.ToString(CultureInfo.CurrentCulture);
            
            // TODO: получить из БД
            SetStartMaterialField();
            //
            
            Vu.Text = _calc.Vu.ToString(CultureInfo.CurrentCulture);
            Tu.Text = _calc.Tu.ToString(CultureInfo.CurrentCulture);
            mu0.Text = _calc.Mu0.ToString(CultureInfo.CurrentCulture);
            Ea.Text = _calc.Ea.ToString(CultureInfo.CurrentCulture);
            Tr.Text = _calc.Tr.ToString(CultureInfo.CurrentCulture);
            n.Text = _calc.N.ToString(CultureInfo.CurrentCulture);
            alphaU.Text = _calc.AlphaU.ToString(CultureInfo.CurrentCulture);
        }

        private void SetStartMaterialField() {
            p.Text = _calc.Material.P.ToString(CultureInfo.CurrentCulture);
            c.Text = _calc.Material.C.ToString(CultureInfo.CurrentCulture);
            T0.Text = _calc.Material.T0.ToString(CultureInfo.CurrentCulture);
        }


        private void MenuItemTable_Click(object sender, RoutedEventArgs e) {
            if (!CalculateLists()) return;
            var table = new Table(_zCoord, _temperature, _viscosity);
            table.Show();
            eff.Content = _calc.Efficiency().ToString(CultureInfo.CurrentCulture);
            T.Content = Math.Round(_temperature![^1], 2).ToString(CultureInfo.CurrentCulture);
            visc.Content = Math.Round(_viscosity![^1], 2).ToString(CultureInfo.CurrentCulture);

            _currentMaterial = MaterialComboBox.Text!;
            
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

            if (MaterialComboBox.Text == "") {
                isGoodData = false;
            }
            
            if (!isGoodData) return false;

            var material = new Material(Convert.ToDouble(p.Text), Convert.ToDouble(c.Text), Convert.ToDouble(T0.Text));
            var channel = new Channel(Convert.ToDouble(W.Text), Convert.ToDouble(H.Text), Convert.ToDouble(L.Text));
            
            _calc = new(channel, Convert.ToDouble(step.Text), material, Convert.ToDouble(Vu.Text), Convert.ToDouble(Tu.Text), Convert.ToDouble(mu0.Text), Convert.ToDouble(Ea.Text), Convert.ToDouble(Tr.Text),
                Convert.ToDouble(n.Text), Convert.ToDouble(alphaU.Text));
            _zCoord = new();
            _temperature = new();
            _viscosity = new();
            _q = new();

            _calc.MaterialShearStrainRate();
            _calc.SpecificHeatFluxes();
            _calc.VolumeFlowRateOfMaterialFlowInTheChannel();

            for (double z = 0; z <= _calc.Channel.L; z += _calc.Step) {
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
            // TODO: убрать материал из эксельворкера отстальвить только calc
            var fileName = sfd.FileName;
            fileName = fileName.Contains(".xlsx") ? fileName : fileName + ".xlsx";
            ExcelWorker excelWorker = new(_zCoord, _temperature, _viscosity, _q, fileName, _currentMaterial, _calc);
            excelWorker.SaveToExel();
        }

        private void MaterialComboBox_DropDownClosed(object? sender, EventArgs e) {
            InitialMaterials();
            SetStartMaterialField();
        }
    }
}
