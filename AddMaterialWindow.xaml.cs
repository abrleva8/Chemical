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

namespace Don_tKnowHowToNameThis
{
    /// <summary>
    /// Логика взаимодействия для AddMaterialWindow.xaml
    /// </summary>
    public partial class AddMaterialWindow : Window {
        private List<string> _parameters = new();
        private static int _rows;
        public AddMaterialWindow() {
            InitializeComponent();
            InitParameters();
        }

        private void InitParameters() {
            _parameters = DataBaseWorker.GetParametersName();
        }

        private void AddRowButton_Click(object sender, RoutedEventArgs e) {
            var x = new RowDefinition {
                MinHeight = 20
            };
            TextEditButtons.RowDefinitions.Add(x);
            var typeComboBox = TypeComboBox();
            var parameterNameComboBox = ParameterNameComboBox();
            var textBox = new TextBox();
            textBox.IsEnabled = true;
            TextEditButtons.Children.Add(parameterNameComboBox);
            TextEditButtons.Children.Add(typeComboBox);
            Grid.SetRow(parameterNameComboBox, TextEditButtons.RowDefinitions.Count - 1);
            Grid.SetRow(textBox, TextEditButtons.RowDefinitions.Count - 1);
            Grid.SetRow(typeComboBox, TextEditButtons.RowDefinitions.Count - 1);
            Grid.SetColumn(parameterNameComboBox, 0);
            Grid.SetColumn(textBox, 1);
            Grid.SetColumn(typeComboBox, 2);
            _rows++;
        }

        private static ComboBox ParameterNameComboBox() {
            var parameterNameComboBox = new ComboBox {
                Name = $"typeComboBox_{_rows}"
            };
            DataBaseWorker.GetParametersName().ForEach(material => parameterNameComboBox.Items.Add(material));
            parameterNameComboBox.SelectedIndex = 0;
            return parameterNameComboBox;
        }

        private static ComboBox TypeComboBox() {
            var typeComboBox = new ComboBox {
                Name = $"typeComboBox_{_rows}"
            };
            typeComboBox.Items.Add("Параметр");
            typeComboBox.Items.Add("Коэффициент");
            typeComboBox.SelectedIndex = 0;
            return typeComboBox;
        }

        private void ButtonSendBase_OnClick(object sender, RoutedEventArgs e) {
            var name = AddNameTextBox.Text;
            if (name == "") {
                MessageBox.Show("Имя не может быть пустым!");
            }

            if (_rows == 0) {
                MessageBox.Show("Добавьте, хотя бы один параметр!");
            }
        }
    }
}
