using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для DeleteUserWindow.xaml
    /// </summary>
    public partial class DeleteUserWindow : Window {

        private Dictionary<string, string> logins = new Dictionary<string, string>();
        const string textFile = "C:\\Users\\abrle\\RiderProjects\\Chemical\\passwords.txt";

        public DeleteUserWindow() {
            InitializeComponent();
            InitLoginComboBox();
        }

        private void InitLoginComboBox() {
            var lines = File.ReadAllLines(textFile);
            LoginToDeleteTextBox.Items.Clear();
            GetLogins(lines);
            foreach (var log in logins) {
                LoginToDeleteTextBox.Items.Add(log.Key);
            }
        }

        private void GetLogins(string[] lines) {
            logins = new Dictionary<string, string>();
            foreach (var line in lines[1..]) {
                var data = line.Split(',');
                var login = data[0];
                var password = data[1];
                if (login == "admin") {
                    continue;
                }
                logins[login] = password;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            var user = LoginToDeleteTextBox.Text;
            logins.Remove(user);
            using (StreamWriter w = new StreamWriter(textFile)) {
                w.WriteLine("login,password");
                w.WriteLine("admin,admin");

                foreach (var log in logins) {
                    w.WriteLine($"{log.Key},{log.Value}");
                }
            }
            InitLoginComboBox();
            MessageBox.Show($"Пользователь {user} удален!");
        }

        private void LoginToDeleteTextBox_DropDownClosed(object sender, EventArgs e) {

        }
    }
}
