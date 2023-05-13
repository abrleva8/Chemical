using System;
using K4os.Compression.LZ4.Streams.Frames;

namespace Don_tKnowHowToNameThis; 

public class LoginException : Exception {}

public class NotSuchUserException : LoginException {
    private readonly string _login;

    public NotSuchUserException(string login) {
        _login = login;
    }

    public override string ToString() {
        return $"Пользователь {_login} не найден";
    }
}

public class BadPasswordException : LoginException {
    private readonly string _login;

    public BadPasswordException(string login) {
        _login = login;
    }

    public override string ToString() {
        return $"Пароль для {_login} неверный";
    }
} 