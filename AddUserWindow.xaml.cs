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
    /// Логика взаимодействия для AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window {
        public AddUserWindow() {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e) {
            var newLogin = AddLoginTextBox.Text;
            var newPassword = AddPasswordTextBox.Text;
            string textFile = "";
            try {
                textFile = Check(newLogin, newPassword);
            } catch (LoginException loginException) {
                MessageBox.Show(loginException.ToString());
                return;
            }

            using (StreamWriter w = File.AppendText(textFile)) {
                w.WriteLine($"{newLogin},{newPassword}");
            }
            MessageBox.Show($"Пользователь {newLogin} добавлен!");
        }

        private static string Check(string newLogin, string newPassword) {
            if (newPassword == "") {
                throw new EmptyPasswordException();
            }

            if (newPassword.Contains(",")) {
                throw new SemicolonPasswordException();
            }

            const string textFile = "C:\\Users\\abrle\\RiderProjects\\Chemical\\passwords.txt";
            var lines = File.ReadAllLines(textFile);

            foreach (var line in lines[1..]) {
                var data = line.Split(',');
                var login = data[0];
                var password = data[1];

                if (login == newLogin) {
                    throw new SameUserException(login);
                }

            }

            return textFile;
        }
    }
}
