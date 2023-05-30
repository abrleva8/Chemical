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
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window {

        public List<DataBaseWorker.ParameterInfoWithId> ParameterList = new();
        public List<DataBaseWorker.MaterialInfoNames> MaterialList = new();
        public AdminWindow() {
           InitializeComponent();
        }

        private void GetMaterialsFromBd(string parameter) {
            // var materialsInfo = DataBaseWorker.GetMaterialsInfo(parameter);
            //
            // MaterialsDataGrid.Columns.Clear();
            // MaterialsDataGrid.Items.Clear();
            // MaterialsDataGrid.ItemsSource = null;
            //
            // var column = new DataGridTextColumn {
            //     Header = "Тип материала",
            //     Binding = new Binding("materialType")
            // };
            //
            // MaterialsDataGrid.Columns.Add(column);
            //
            // column = new()  {
            //     Header = parameter,
            //     Binding = new Binding("value")
            // };
            // MaterialsDataGrid.Columns.Add(column);
            //
            // column = new()  {
            //     Header = "Единица измерения",
            //     Binding = new Binding("unit")
            // };
            // MaterialsDataGrid.Columns.Add(column);
            //
            // foreach (var material in materialsInfo) {
            //         MaterialsDataGrid.Items.Add(new DataBaseWorker.MaterialInfo(material.MaterialType, material.Value, material.Unit));
            // }
        }
        
        private void ParameterComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
            // var currentItem = ParameterComboBox.SelectedItem as TextBlock;
            // var current = currentItem.Text;
            // if (current != "") {
            //     GetMaterialsFromBd(current);
            // }
        }

        private void ParameterComboBox_OnDropDownClosed(object? sender, EventArgs e) {
           // GetMaterialsFromBd(ParameterComboBox.Text);
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e) {
            var addUserWindow = new AddUserWindow();
            addUserWindow.ShowDialog();
        }

        private void ChangePasswordButton_Click(object sender, RoutedEventArgs e) {
            var changePasswordWindow = new ChangePasswordWindow();
            changePasswordWindow.ShowDialog();
        }

        private void DeleteUserButton_Click(object sender, RoutedEventArgs e) {
            var deleteUserWindow = new DeleteUserWindow();
            deleteUserWindow.ShowDialog();
        }

        private void ChangeUser_Click(object sender, RoutedEventArgs e) {
            new LoginWindow().Show();
            Close();
        }

        private void AddMaterialButton_Click(object sender, RoutedEventArgs e) {
            new AddMaterialWindow().ShowDialog();
        }

        private void ChangeMaterialButton_Click(object sender, RoutedEventArgs e) {

        }

        private void DeleteMaterialButton_Click(object sender, RoutedEventArgs e) {
            new DeleteMaterialWindow().ShowDialog();
        }

        private void AddParameterButton_OnClick(object sender, RoutedEventArgs e) {
            new AddParameterWindow().ShowDialog();
        }

        private void ChangeParameterButton_OnClick(object sender, RoutedEventArgs e) {
            new ChangeParameterWindow().ShowDialog();
        }

        private void DeleteParameterButton_OnClick(object sender, RoutedEventArgs e) {
            new DeleteParameterWindow().ShowDialog();
        }

        private void Tab_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (ParameterTab.IsSelected) {
                InitParameterDataGrid();
            }

            if (MaterialTab.IsSelected) {
                InitMaterialDataGrid();
            }
        }

        private void InitMaterialDataGrid() {
            MaterialList = DataBaseWorker.GetMaterialTable();
            MaterialDataGrid.ItemsSource = MaterialList;
        }

        private void InitParameterDataGrid() {
            ParameterList = DataBaseWorker.GetParameterTable();
            ParameterDataGrid.ItemsSource = ParameterList;
        }

        private void RefreshParameterDataGridButton_OnClick(object sender, RoutedEventArgs e) {
            InitParameterDataGrid();
        }

        private void RefreshTableButton_OnClick(object sender, RoutedEventArgs e) {
            InitMaterialDataGrid();
        }
    }
}
