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

        const string query = "SELECT type FROM material";
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
        string MaterialType,
        double Value,
        string Unit
    );
    
    public static List<MaterialInfo> GetMaterialsInfo(string parameter) {
        
        string query = $"SELECT type, value AS `{parameter}`, unit " +
                     "FROM material " +
                     "JOIN parameter_material_attr pma on material.ID_material = pma.ID_material " +
                     "JOIN parameter p ON pma.ID_parameter = p.ID_parameter " +
                     $"WHERE name = \"{parameter}\"";
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
    
    public static List<MaterialInfo> GetMaterialsInfoForLabel(string materialName) {
        
        string query = "SELECT name, value, unit " +
                       "FROM material " +
                       "JOIN parameter_material_attr pma on material.ID_material = pma.ID_material " +
                       "JOIN parameter p on pma.ID_parameter = p.ID_parameter " +
                       $"WHERE material.type = \"{materialName}\" and pma.type = \"Свойство материала\"";
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
    public static List<MaterialInfo> GetCoefficientsInfoForLabel(string materialName) {
        
        string query = "SELECT name, value, unit " +
                       "FROM material " +
                       "JOIN parameter_material_attr pma on material.ID_material = pma.ID_material " +
                       "JOIN parameter p on pma.ID_parameter = p.ID_parameter " +
                       $"WHERE material.type = \"{materialName}\" and pma.type = \"Коэффициент\"";
        using var connection = JoinBase();
        var command = new MySqlCommand();
        command.Connection = connection;
        command.CommandText = query;
        var reader = command.ExecuteReader();
        var result = new List<MaterialInfo>();
        while (reader.Read()) {
            var first = reader.GetString(0);
            var second = reader.GetDouble(1);
            bool isNull = reader.IsDBNull(2);
            var third = isNull ? "" : reader.GetString(2);
            result.Add(new (first, second, third));
        }
        connection.Close();

        return result;
    }

    public static Dictionary<string, string> GetMaterialsValues(string selectedMaterial) {

        const string query = "select name, value " +
                             "from material " +
                             "join parameter_material_attr pma on pma.ID_material = material.ID_material " +
                             "and pma.type = \"Свойство материала\" " +
                             "and material.type = @material " +
                             "join parameter p on p.ID_parameter = pma.ID_parameter";
        
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