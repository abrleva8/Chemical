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
    /// Логика взаимодействия для DeleteMaterialWindow.xaml
    /// </summary>
    public partial class DeleteMaterialWindow : Window {
        public DeleteMaterialWindow() {
            InitializeComponent();
            InitMaterialComboBox();
        }

        private void InitMaterialComboBox() {
            LoginToDeleteTextBox.Items.Clear();
            DataBaseWorker.GetMaterials().ForEach(material => LoginToDeleteTextBox.Items.Add(material));
            LoginToDeleteTextBox.SelectedIndex = 0;
        }

        private void DeleteMaterialButton_Click(object sender, RoutedEventArgs e) {
            var materialType = LoginToDeleteTextBox.Text;
            DataBaseWorker.DeleteMaterialInfo(materialType);
            DataBaseWorker.DeleteMaterial(materialType);
            InitMaterialComboBox();
            MessageBox.Show("Удаление прошло успешно!");
        }
    }
}
