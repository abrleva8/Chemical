namespace Don_tKnowHowToNameThis; 

using System.IO;
using System.Windows;


/// <summary>
///     Логика взаимодействия для LoginWindow.xaml
/// </summary>
public partial class LoginWindow {
    public LoginWindow() {
        InitializeComponent();
    }

    private void StartUserWindow(object sender, RoutedEventArgs e) {
        var currentLogin = LoginTextBox.Text;
        var currentPassword = PasswordTextBox.Password;
        var user = "";
        try {
            user = Check(currentPassword, currentLogin);
        } catch (LoginException loginException) {
            MessageBox.Show(loginException.ToString());
        }

        switch (user) {
            case "admin":
                new AdminWindow().Show();
                break;
            case "user":
                new MainWindow().Show();
                Close();
                break;
        }
        
    }

    private static string Check(string currentPassword, string currentLogin) {
        const string textFile = "C:\\Users\\abrle\\RiderProjects\\Chemical\\passwords.txt";
        var lines = File.ReadAllLines(textFile);

        foreach (var line in lines[1..]) {
            var data = line.Split(',');
            var login = data[0];
            var password = data[1];

            if (login != currentLogin) continue;
            if (password == currentPassword) {
                return login;
            }

            throw new BadPasswordException(login);
        }

        throw new NotSuchUserException(currentLogin);
    }
}