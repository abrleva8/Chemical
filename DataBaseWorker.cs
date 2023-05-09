using System.Collections.Generic;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace Don_tKnowHowToNameThis; 

public class DataBaseWorker {

    private static readonly string ConnectionString = ConfigurationManager.AppSettings["connectionString"]!;

    private static MySqlConnection JoinBase() {
        var connection = new MySqlConnection();
        connection.ConnectionString = ConnectionString;
        connection.Open();
        return connection;
    }

    public static List<string> GetMaterials() {

        const string query = "select Type from material";
        using var connection = JoinBase();
        var command = new MySqlCommand();
        command.Connection = connection;
        command.CommandText = query;
        var reader = command.ExecuteReader();
        var result = new List<string>();
        while (reader.Read()) {
            result.Add(reader.GetString(0));
        }
        connection.Close();
        
        return result;
    }
}