using System;
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

        const string query = "select `Тип` from `материал`";
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
    
    public static Dictionary<string, string> GetMaterialsValues() {

        const string query = "select `Название_параметра`, `Величина`" +
                     
                             "from `материал`" +
                             "join `параметры_свойств_материала` t on t.`ID_материала` = `материал`.ID_материала " +
                             "and t.Тип_параметра = \"Свойство материала\" " +
                             "join `параметры` p on p.ID_Параметра = t.ID_Параметра";
        using var connection = JoinBase();
        var command = new MySqlCommand();
        command.Connection = connection;
        command.CommandText = query;
        var reader = command.ExecuteReader();
        var result = new Dictionary<string, string>();
        while (reader.Read()) {
            result.Add(reader.GetString(0), reader.GetString(1));
        }
        connection.Close();
        
        return result;
    }
}