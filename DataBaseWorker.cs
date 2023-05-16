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

    public record MaterialInfo(
        string materialType,
        double value,
        string unit
    );
    public static List<MaterialInfo> GetMaterialsInfo() {
        

        const string query = "SELECT Тип, Величина as \"Плотность\", Единица_измерения " +
                             "FROM материал " +
                             "JOIN параметры_свойств_материала псм on материал.ID_материала = псм.ID_материала " +
                             "JOIN параметры п on псм.ID_Параметра = п.ID_Параметра " +
                             "WHERE Название_параметра = \"Плотность\"";
        using var connection = JoinBase();
        var command = new MySqlCommand();
        command.Connection = connection;
        command.CommandText = query;
        var reader = command.ExecuteReader();
        var result = new List<MaterialInfo>();
        while (reader.Read()) {
            result.Add(new (reader.GetString(0),reader.GetDouble(1), reader.GetString(2)));
        }
        connection.Close();
        
        return result;
    }
    
    public static Dictionary<string, string> GetMaterialsValues(string selectedMaterial) {

        const string query = "select `Название_параметра`, `Величина`" +
                             "from `материал`" +
                             "join `параметры_свойств_материала` t on t.`ID_материала` = `материал`.ID_материала " +
                             "and t.Тип_параметра = \"Свойство материала\" " +
                             "and `материал`.Тип = @material " +
                             "join `параметры` p on p.ID_Параметра = t.ID_Параметра";
        
        using var connection = JoinBase();
        var command = new MySqlCommand();
        command.Connection = connection;
        command.Parameters.AddWithValue("@material", selectedMaterial);
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