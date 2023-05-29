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
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;

namespace Don_tKnowHowToNameThis {
    /// <summary>
    /// Логика взаимодействия для ChangePasswordWindow.xaml
    /// </summary>
    public partial class ChangePasswordWindow : Window {

        private Dictionary<string, string> logins = new Dictionary<string, string>();
        const string textFile = "C:\\Users\\abrle\\RiderProjects\\Chemical\\passwords.txt";

        public ChangePasswordWindow() {
            InitializeComponent();
            InitLoginComboBox();
        }

        public void InitLoginComboBox() {
            
            var lines = File.ReadAllLines(textFile);
            LoginToChangeTextBox.Items.Clear();
            GetLogins(lines);
            foreach (var log in logins) {
                LoginToChangeTextBox.Items.Add(log.Key);
            }
        }

        private void GetLogins(string[] lines) {
           
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

        private void ChangeButton_Click(object sender, RoutedEventArgs e) {
            var newPassword = PasswordToChangeTextBox.Text;
            if (newPassword == "") {
                MessageBox.Show("Пароль не может быть пустым!");
                return;
            }

            if (newPassword.Contains(",")) {
                MessageBox.Show("Пароль не может содкржать запятую!");
                return;
            }

            logins[LoginToChangeTextBox.Text] = newPassword;

            using (StreamWriter w = new StreamWriter(textFile)) {
                w.WriteLine("login,password");
                w.WriteLine("admin,admin");

                foreach (var log in logins) {
                    w.WriteLine($"{log.Key},{log.Value}");
                }
            }

            MessageBox.Show("Пароль изменен!");
        }

        private void LoginToChangeTextBoxChanged(object sender, SelectionChangedEventArgs e) {
          //  PasswordToChangeTextBox.Text = logins[LoginToChangeTextBox.Text];
        }

        private void LoginToChangeTextBoxClosed(object sender, EventArgs e) {
            var t = LoginToChangeTextBox.SelectedIndex;
            if (t != -1) {
                PasswordToChangeTextBox.Text = logins[LoginToChangeTextBox.Text];
            }
        }
    }
}
