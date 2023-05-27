using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Diagnostics;

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
            InitialChannels();
            InitMaterialInfoPanel();
            InitCoefficientInfoPanel();
        }

        private void InitMaterialComboBox() {
            DataBaseWorker.GetMaterials().ForEach(material => MaterialComboBox.Items.Add(material));
            MaterialComboBox.SelectedIndex = 0;
        }

        private void InitialChannels() {
            _calc.Channel = GetChannelFromBd();
        }

        private Channel GetChannelFromBd() {
            return new (0.20, 0.009, 4.5);
        }

        private void InitMaterialInfoPanel() {
            
            var materialsValues = DataBaseWorker.GetMaterialsInfoForLabel(MaterialComboBox.Text);
            if (materialsValues.Count != 3) {
                MessageBox.Show("Ошибка! Данные либо мало, либо много! Будут записаны данные по умолчанию");
                InitDefaultMaterialInfoPanel();
            } else {
                InitDataBaseMaterialInfo(materialsValues);
            }
        }
        private void InitCoefficientInfoPanel() {
            
            var coefficientsValues = DataBaseWorker.GetCoefficientsInfoForLabel(MaterialComboBox.Text);
            if (coefficientsValues.Count != 5) {
                MessageBox.Show("Ошибка! Коэффициенты!");
                InitDefaultCoefficientsInfoPanel();
            } else {
                InitDataBaseCoefficientsInfo(coefficientsValues);
            }
        }

        private void InitDataBaseMaterialInfo(IReadOnlyList<DataBaseWorker.MaterialInfo> materialsValues) {
            var firstMaterial = materialsValues[0];
            var secondMaterial = materialsValues[1];
            var thirdMaterial = materialsValues[2];

            DensityLabel.Text = firstMaterial.MaterialType;
            SpecificHeatCapacityLabel.Text = secondMaterial.MaterialType;
            MeltingPointLabel.Text = thirdMaterial.MaterialType;

            P.Text = firstMaterial.Value.ToString(CultureInfo.CurrentCulture);
            c.Text = secondMaterial.Value.ToString(CultureInfo.CurrentCulture);
            T0.Text = thirdMaterial.Value.ToString(CultureInfo.CurrentCulture);

            _calc.Material = new Material(firstMaterial.Value,
                secondMaterial.Value,
                thirdMaterial.Value);

            DensityMeasureLabel.Content = firstMaterial.Unit;
            SpecificHeatCapacityUnitLabel.Content = secondMaterial.Unit;
            MeltingPointUnitLabel.Content = thirdMaterial.Unit;
        }
        private void InitDataBaseCoefficientsInfo(IReadOnlyList<DataBaseWorker.MaterialInfo> materialsValues) {
            var firstCoefficient = materialsValues[0];
            var secondCoefficient = materialsValues[1];
            var thirdCoefficient = materialsValues[2];
            var fourthCoefficient = materialsValues[3];
            var fifthCoefficient = materialsValues[4];

            C1Label.Text = firstCoefficient.MaterialType;
            C2Label.Text = secondCoefficient.MaterialType;
            C3Label.Text = thirdCoefficient.MaterialType;
            C4Label.Text = fourthCoefficient.MaterialType;
            C5Label.Text = fifthCoefficient.MaterialType;

            mu0.Text = firstCoefficient.Value.ToString(CultureInfo.CurrentCulture);
            Ea.Text = secondCoefficient.Value.ToString(CultureInfo.CurrentCulture);
            Tr.Text = thirdCoefficient.Value.ToString(CultureInfo.CurrentCulture);
            n.Text = fourthCoefficient.Value.ToString(CultureInfo.CurrentCulture);
            alphaU.Text = fifthCoefficient.Value.ToString(CultureInfo.CurrentCulture);

            U1Label.Content = firstCoefficient.Unit;
            U2Label.Content = secondCoefficient.Unit;
            U3Label.Content = thirdCoefficient.Unit;
            U4Label.Content = fourthCoefficient.Unit;
            U5Label.Content = fifthCoefficient.Unit;
            
            //
            // _calc.Material = new Material(firstMaterial.Value,
            //     secondMaterial.Value,
            //     thirdMaterial.Value);
        }

        private void InitDefaultCoefficientsInfoPanel() {

            C1Label.Text = "Коэффициент консистенции материала при температуре приведения";
            C2Label.Text = "Энергия активации вязкого течения материала";
            C3Label.Text = "Температура приведения";
            C4Label.Text = "Индекс течения материала";
            C5Label.Text = "Коэффициент теплоотдачи от крышки канала к материалу";

            mu0.Text = "";
            Ea.Text = "";
            Tr.Text = "";
            n.Text = "";
            alphaU.Text = "";
            
            U1Label.Content = "Па*с^n";
            U2Label.Content = "Дж/моль";
            U3Label.Content = "°С";
            U4Label.Content = "";
            U5Label.Content = "Вт/(м^2*°С)";
        }

        private void InitDefaultMaterialInfoPanel() {
            DensityLabel.Text = "Плотность";
            SpecificHeatCapacityLabel.Text = "Удельная теплоеёмкость";
            MeltingPointLabel.Text = "Температура плавления";
            
            P.Text = "";
            c.Text = "";
            T0.Text = "";

            DensityMeasureLabel.Content = "кг/м^3";
            SpecificHeatCapacityUnitLabel.Content = "Дж/(кг*°C)";
            MeltingPointUnitLabel.Content = "°C";
        }


        private void Window_Loaded(object sender, RoutedEventArgs e) {
            step.Text = _calc.Step.ToString(CultureInfo.CurrentCulture);
            W.Text = _calc.Channel.W.ToString(CultureInfo.CurrentCulture);
            H.Text = _calc.Channel.H.ToString(CultureInfo.CurrentCulture);
            L.Text = _calc.Channel.L.ToString(CultureInfo.CurrentCulture);

            Vu.Text = _calc.Vu.ToString(CultureInfo.CurrentCulture);
            Tu.Text = _calc.Tu.ToString(CultureInfo.CurrentCulture);
            mu0.Text = _calc.Mu0.ToString(CultureInfo.CurrentCulture);
            Ea.Text = _calc.Ea.ToString(CultureInfo.CurrentCulture);
            Tr.Text = _calc.Tr.ToString(CultureInfo.CurrentCulture);
            n.Text = _calc.N.ToString(CultureInfo.CurrentCulture);
            alphaU.Text = _calc.AlphaU.ToString(CultureInfo.CurrentCulture);
        }


        private void MenuItemTable_Click(object sender, RoutedEventArgs e) {
            if (! CalculateLists()) return;
            var table = new Table(_zCoord, _temperature, _viscosity);
            table.Show();
            //eff.Content = _calc.Efficiency().ToString(CultureInfo.CurrentCulture);
            //T.Content = Math.Round(_temperature![^1], 2).ToString(CultureInfo.CurrentCulture);
            //visc.Content = Math.Round(_viscosity![^1], 2).ToString(CultureInfo.CurrentCulture);

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
            if (!IsGood(_calc.PMin, _calc.PMax, P))
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

            var material = new Material(Convert.ToDouble(P.Text), Convert.ToDouble(c.Text), Convert.ToDouble(T0.Text));
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
            var stopwatch = new Stopwatch();
            var currentProcess = Process.GetCurrentProcess();
            stopwatch.Start();
            if (!CalculateLists()){
                stopwatch.Stop();
                return; 
            }
            var memoryUsed = currentProcess.WorkingSet64 / (1024 * 1024);
            stopwatch.Stop();
            var timeSpan = stopwatch.Elapsed;
            var efficiency = _calc.Efficiency().ToString(CultureInfo.CurrentCulture);
            var tem = Math.Round(_temperature![^1], 2).ToString(CultureInfo.CurrentCulture);
            var vis = Math.Round(_viscosity![^1], 2).ToString(CultureInfo.CurrentCulture);
            var windowPlot = new WindowPlot(_zCoord, _temperature, _viscosity, efficiency, tem, vis, timeSpan, memoryUsed);
            windowPlot.Show();
        }

        private void SaveExcel_Click(object sender, RoutedEventArgs e) {
            var sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == false) {
                return;
            }
            // TODO: убрать материал из эксельворкера отстальвить только calc
            var fileName = sfd.FileName;
            fileName = fileName.EndsWith(".xlsx") ? fileName : fileName + ".xlsx";
            ExcelWorker excelWorker = new(_zCoord, _temperature, _viscosity, _q, fileName, _currentMaterial, _calc);
            excelWorker.SaveToExel();
        }

        private void MaterialComboBox_DropDownClosed(object? sender, EventArgs e) {
           // InitialMaterials();
           InitMaterialInfoPanel();
           InitCoefficientInfoPanel();
        }

        private void ChangeUser_Click(object sender, RoutedEventArgs e) {
            new LoginWindow().Show();
            Close();
        }
    }
}
