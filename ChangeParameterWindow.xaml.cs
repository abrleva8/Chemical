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

namespace Don_tKnowHowToNameThis {
    /// <summary>
    /// Логика взаимодействия для ChangeParameterWindow.xaml
    /// </summary>
    public partial class ChangeParameterWindow : Window {
        public ChangeParameterWindow() {
            InitializeComponent();
            InitParameterComboBox();
        }

        private void InitParameterComboBox() {
            ID_parameterComboBox.Items.Clear();
            DataBaseWorker.GetParameters().ForEach(material => ID_parameterComboBox.Items.Add(material));
        }

        private void ButtonRefresh_OnClick(object sender, RoutedEventArgs e) {
            var name = ChangeNameTextBox.Text;
            var symbol = ChangeSymbolTextBox.Text;
            var unit = ChangeUnitTextBox.Text;
            var id = Convert.ToInt32(ID_parameterComboBox.Text);
            
            if (AddParameterWindow.CheckNewParameter(name, symbol, unit)) {
                return;
            }
            
            var newParameter = new DataBaseWorker.ParameterInfo(name, symbol, unit);
            DataBaseWorker.UpdateParameter(id, newParameter);
            MessageBox.Show("Параметр обновлен!");
        }

        private void ID_parameterComboBox_OnDropDownClosed(object? sender, EventArgs e) {
            var index = ID_parameterComboBox.SelectedIndex;
            if (index == -1) {
                return;
            }
            var p = DataBaseWorker.GetParameterById(Convert.ToInt32(ID_parameterComboBox.Text));
            ChangeNameTextBox.Text = p!.Name;
            ChangeSymbolTextBox.Text = p.Symbol;
            ChangeUnitTextBox.Text = p.Unit;
        }
    }
}
