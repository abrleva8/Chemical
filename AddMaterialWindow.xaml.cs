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
    public partial class AddMaterialWindow : Window
    {
        public AddMaterialWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            var x = new RowDefinition();
            x.MinHeight = 20;
            TextEditButtons.RowDefinitions.Add(x);
            var textEdit = new ComboBox();
            ComboBox uc = new ComboBox();
            uc.Items.Add("Параметр");
            uc.Items.Add("Коэффициент");
            uc.SelectedIndex = 0;
            TextEditButtons.Children.Add(textEdit);
            TextEditButtons.Children.Add(uc);
            Grid.SetRow(textEdit, TextEditButtons.RowDefinitions.Count - 1);
            Grid.SetRow(uc, TextEditButtons.RowDefinitions.Count - 1);
            Grid.SetColumn(textEdit, 0);
            Grid.SetColumn(uc, 1);
        }
    }
}
