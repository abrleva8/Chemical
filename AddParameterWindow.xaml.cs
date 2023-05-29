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
    /// Логика взаимодействия для AddParameterWindow.xaml
    /// </summary>
    public partial class AddParameterWindow : Window {
        public AddParameterWindow() {
            InitializeComponent();
        }

        private void ButtonAdd_OnClick(object sender, RoutedEventArgs e) {
            var name = AddNameTextBox.Text;
            var symbol = AddSymbolTextBox.Text;
            var unit = AddUnitTextBox.Text;
            
            if (CheckNewParameter(name, symbol, unit)) {
                return;
            }

            var newParameter = new DataBaseWorker.ParameterInfo(name, symbol, unit);
            DataBaseWorker.AddParameter(newParameter);
            MessageBox.Show("Параметр добавлен!");
        }

        public static bool CheckNewParameter(string name, string symbol, string unit) {
            if (name == "" || symbol == "") {
                MessageBox.Show("Заполните, пожалуйста, поля названия и символ!");
                return true;
            }

            if (symbol.Length > 6) {
                MessageBox.Show("Поле символ не может содержать больше 6 символов!");
                return true;
            }

            if (unit.Length > 20) {
                MessageBox.Show("Поле символ не может содержать больше 20 символов!");
                return true;
            }

            return false;
        }
    }
}
