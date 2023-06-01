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
    /// Логика взаимодействия для ChangeMaterialWindow.xaml
    /// </summary>
    public partial class ChangeMaterialWindow : Window {
        public ChangeMaterialWindow() {
            InitializeComponent();
            ID_materialComboBox.Items.Clear();
            DataBaseWorker.GetMaterials().ForEach(material => ID_materialComboBox.Items.Add(material));
        }

        private int _rows;
        List<int> idex = new List<int>();
        
        public record Xrecord(
            int IdMaterial,
            int? IdParameter,
            double Value,
            string Type
        );
        private void ID_materialComboBox_OnDropDownClosed(object? sender, EventArgs e) {
            
            TextEditButtons.Children.Clear();
            TextEditButtons.RowDefinitions.Clear();

            var mater =  DataBaseWorker.GetParameterByNameMaterial(ID_materialComboBox.Text);
            _rows = mater.Count;
            
            for (var i = 0; i < mater.Count; i++) {
                idex = new List<int>();
                var x = new RowDefinition {
                    MinHeight = 20,
                    Name = $"rowUI_{i}"
                };

                TextEditButtons.RowDefinitions.Add(x);
                var typeComboBox = TypeComboBox(mater[i].Type, i);
                var parameterNameComboBox = ParameterNameComboBox(mater[i].Name, i);
                var textBox = new TextBox {
                    Name = $"textBox_{i}",
                    Text = mater[i].Value.ToString()
                };
                var deleteButton = new Button {
                    Name = $"deleteButton_{i}",
                    Content = "X"
                };

                deleteButton.Click += DeleteBtn_Click;

                TextEditButtons.Children.Add(parameterNameComboBox);
                TextEditButtons.Children.Add(typeComboBox);
                TextEditButtons.Children.Add(textBox);
                TextEditButtons.Children.Add(deleteButton);
                Grid.SetRow(parameterNameComboBox, TextEditButtons.RowDefinitions.Count - 1);
                Grid.SetRow(textBox, TextEditButtons.RowDefinitions.Count - 1);
                Grid.SetRow(typeComboBox, TextEditButtons.RowDefinitions.Count - 1);
                Grid.SetRow(deleteButton, TextEditButtons.RowDefinitions.Count - 1);
                Grid.SetColumn(parameterNameComboBox, 0);
                Grid.SetColumn(textBox, 1);
                Grid.SetColumn(typeComboBox, 2);
                Grid.SetColumn(deleteButton, 3);
            }
        }
        private static ComboBox ParameterNameComboBox(string name, int i) {
            var parameterNameComboBox = new ComboBox {
                Name = $"parameterNameComboBox_{i}"
            };
            DataBaseWorker.GetParametersName().ForEach(material => parameterNameComboBox.Items.Add(material));
            foreach (var item in parameterNameComboBox.Items)
            {
                if (item.ToString() == name)
                {
                    parameterNameComboBox.SelectedItem = item;
                    break;
                }
            }
            return parameterNameComboBox;
        }
        private static ComboBox TypeComboBox(string name, int i) {
            var typeComboBox = new ComboBox {
                Name = $"typeComboBox_{i}"
            };
            typeComboBox.Items.Add("Свойство материала");
            typeComboBox.Items.Add("Коэффициент");
            foreach (var item in typeComboBox.Items)
            {
                if (item.ToString() == name)
                {
                    typeComboBox.SelectedItem = item;
                    break;
                }
            }
            return typeComboBox;
        }
        
        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int index = Convert.ToInt32(button.Name.Split("_")[^1]);
            var rowsChildren = TextEditButtons.RowDefinitions;
            foreach (var row in rowsChildren) {
                if (row.Name == $"rowUI_{index}") {
                    row.MaxHeight = 0;
                    row.MinHeight = 0;
                    idex.Add(index);
                    // rowsChildren.RemoveAt(Convert.ToInt32(row.Name.Split("_")[^1]));
                    // break;
                }
            }
        }

        private ComboBox ParameterNameComboBox() {
            var parameterNameComboBox = new ComboBox {
                Name = $"parameterNameComboBox_{_rows}"
            };
            DataBaseWorker.GetParametersName().ForEach(material => parameterNameComboBox.Items.Add(material));
            parameterNameComboBox.SelectedIndex = 0;
            return parameterNameComboBox;
        }

        private ComboBox TypeComboBox() {
            var typeComboBox = new ComboBox {
                Name = $"typeComboBox_{_rows}"
            };
            typeComboBox.Items.Add("Свойство материала");
            typeComboBox.Items.Add("Коэффициент");
            typeComboBox.SelectedIndex = 0;
            return typeComboBox;
        }
        
        private void AddButton_OnClick(object sender, RoutedEventArgs e) {
            var x = new RowDefinition {
                MinHeight = 20,
                Name = $"rowUI_{_rows}"
            };
            TextEditButtons.RowDefinitions.Add(x);
            var typeComboBox = TypeComboBox();
            var parameterNameComboBox = ParameterNameComboBox();
            var textBox = new TextBox {
                Name = $"textBox_{_rows}"
            };
            var deleteButton = new Button {
                Name = $"deleteButton_{_rows}",
                Content = "X"
            };

            deleteButton.Click += DeleteBtn_Click;
            
            TextEditButtons.Children.Add(parameterNameComboBox);
            TextEditButtons.Children.Add(typeComboBox);
            TextEditButtons.Children.Add(textBox);
            TextEditButtons.Children.Add(deleteButton);
            
            Grid.SetRow(parameterNameComboBox, TextEditButtons.RowDefinitions.Count - 1);
            Grid.SetRow(textBox, TextEditButtons.RowDefinitions.Count - 1);
            Grid.SetRow(typeComboBox, TextEditButtons.RowDefinitions.Count - 1);
            Grid.SetRow(deleteButton, TextEditButtons.RowDefinitions.Count - 1);
            Grid.SetColumn(parameterNameComboBox, 0);
            Grid.SetColumn(textBox, 1);
            Grid.SetColumn(typeComboBox, 2);
            Grid.SetColumn(deleteButton, 3);
            _rows++;
        }
        private static string GetParameter(IEnumerable<ComboBox> comboBoxes, int i) {
            foreach (var x in comboBoxes) {
                if (x.Name == $"parameterNameComboBox_{i}") {
                    return x.Text;
                }
            }

            return "";
        }
        private void UpdataBaseOnClick(object sender, RoutedEventArgs e) {

            var name = ID_materialComboBox.Text;

            var comboBoxes = TextEditButtons.Children.OfType<ComboBox>();
            var textBoxes = TextEditButtons.Children.OfType<TextBox>();
            var materialId = DataBaseWorker.GetMaterialIdByType(name);
            DataBaseWorker.DeleteMaterialInfo(name);
            
            for (var i = 0; i < _rows; i++) {
                if (idex.Contains(i)) {
                    continue;
                }

                var parameter = GetParameter(comboBoxes, i);
                double value;
                try {
                    value = GetValue(textBoxes, i);
                } catch (Exception) {
                    MessageBox.Show("Введите числа!");
                    return;
                }
                string type = GetType(comboBoxes, i);
                int? paramId = DataBaseWorker.GetParameterIdByName(parameter);
                var x = new AddMaterialWindow.Xrecord(materialId, paramId, value, type);
                DataBaseWorker.AddMaterialParameter(x);
                MessageBox.Show("Добавил параметр " + i);
            }
            MessageBox.Show("Ура! Победа!");
        }
        private static double GetValue(IEnumerable<TextBox> comboBoxes, int i) {
            foreach (var x in comboBoxes) {
                if (x.Name == $"textBox_{i}") {
                    return Convert.ToDouble(x.Text);
                }
            }

            return -1;
        }
        
        private static string GetType(IEnumerable<ComboBox> comboBoxes, int i) {
            foreach (var x in comboBoxes) {
                if (x.Name == $"typeComboBox_{i}") {
                    return x.Text;
                }
            }

            return "";
        }
    }
}
