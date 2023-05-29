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
    /// Логика взаимодействия для DeleteParameterWindow.xaml
    /// </summary>
    public partial class DeleteParameterWindow : Window {
        public DeleteParameterWindow() {
            InitializeComponent();
            InitParameterComboBox();
        }
        
        private void InitParameterComboBox() {
            ID_parameterComboBox.Items.Clear();
            DataBaseWorker.GetParameters().ForEach(material => ID_parameterComboBox.Items.Add(material));
        }

        private void ID_parameterComboBox_OnDropDownClosed(object? sender, EventArgs e) {
            var index = ID_parameterComboBox.SelectedIndex;
            if (index == -1) {
                return;
            }
            var p = DataBaseWorker.GetParameterById(Convert.ToInt32(ID_parameterComboBox.Text));
            DeleteNameTextBox.Text = p!.Name;
            DeleteSymbolTextBox.Text = p.Symbol;
            DeleteUnitTextBox.Text = p.Unit;
        }

        private void ButtonDelete_OnClick(object sender, RoutedEventArgs e) {
            var id = Convert.ToInt32(ID_parameterComboBox.Text);
            DataBaseWorker.DeleteParameter(id);
            InitParameterComboBox();
            SetDefaultValues();
            MessageBox.Show("Параметр удален!");
            
        }

        private void SetDefaultValues() {
            DeleteNameTextBox.Text = "";
            DeleteSymbolTextBox.Text = "";
            DeleteUnitTextBox.Text = "";
        }
    }
}
