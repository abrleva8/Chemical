using System.Collections.Generic;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace Don_tKnowHowToNameThis; 

public abstract class DataBaseWorker {

    private static readonly string ConnectionString = ConfigurationManager.AppSettings["connectionString"]!;
    
    public record ParameterInfo(
        string Name,
        string Symbol,
        string Unit
    );
    
    public record MaterialInfoNames(
        string MaterialName,
        string ParameterName,
        double Value,
        string Type
    );
    
    public record ParameterInfoWithId(
        int Id,
        string Name,
        string Symbol,
        string Unit
    );

    private static MySqlConnection JoinBase() {
        var connection = new MySqlConnection();
        connection.ConnectionString = ConnectionString;
        connection.Open();
        return connection;
    }
    
    public static void AddParameter(ParameterInfo parameterInfo) {

        var query = "INSERT parameter(name, symbol, unit) " +
                    $"VALUES ('{parameterInfo.Name}', '{parameterInfo.Symbol}', '{parameterInfo.Unit}');";
        using var connection = JoinBase();
        var command = new MySqlCommand();
        command.Connection = connection;
        command.CommandText = query;
        command.ExecuteReader();
        connection.Close();
    }
    
    public static void UpdateParameter(int id, ParameterInfo parameterInfo) {

        var query = "UPDATE parameter " +
                    $"SET name = '{parameterInfo.Name}', symbol = '{parameterInfo.Symbol}', unit = '{parameterInfo.Unit}' " +
                    $"WHERE ID_parameter = {id}";
        using var connection = JoinBase();
        var command = new MySqlCommand();
        command.Connection = connection;
        command.CommandText = query;
        command.ExecuteReader();
        connection.Close();
    }
    
    public static List<int> GetParametersId() {

        const string query = "SELECT ID_parameter FROM parameter;";
        using var connection = JoinBase();
        var command = new MySqlCommand();
        command.Connection = connection;
        command.CommandText = query;
        var reader = command.ExecuteReader();
        var result = new List<int>();
        while (reader.Read()) {
            result.Add(reader.GetInt32(0));
        }
        connection.Close();
        
        return result;
    }
    
    public static ParameterInfo? GetParameterById(int id) {
        var query = "SELECT name, symbol, unit " +
                    "FROM parameter " +
                    $"WHERE ID_parameter = {id}";
        using var connection = JoinBase();
        var command = new MySqlCommand();
        command.Connection = connection;
        command.CommandText = query;
        var reader = command.ExecuteReader();
        ParameterInfo? result = null;
        while (reader.Read()) {
            result = new (reader.GetString(0),reader.GetString(1), reader.GetString(2));
        }
        connection.Close();
        
        return result;
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
    
    public static List<string> GetParametersName() {

        const string query = "SELECT name FROM parameter";
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
    
    public static List<ParameterInfoWithId> GetParameterTable() {

        const string query = "SELECT ID_parameter AS \"ИД\", " +
                             "name AS \"Название\", " +
                             "symbol AS \"Условное_обозначение\", " +
                             "unit AS \"Единица_измерения\" " +
                             "FROM parameter;";
        using var connection = JoinBase();
        var command = new MySqlCommand();
        command.Connection = connection;
        command.CommandText = query;
        var reader = command.ExecuteReader();
        var result = new List<ParameterInfoWithId>();
        while (reader.Read()) {
            result.Add(new (reader.GetInt32(0), reader.GetString(1),
                                reader.GetString(2), reader.GetString(3)));
        }
        connection.Close();
        
        return result;
    }
    
    public static List<MaterialInfoNames> GetMaterialTable() {

        const string query = "SELECT m.type, p.name, value, parameter_material_attr.type " +
                             "FROM parameter_material_attr " +
                             "JOIN material m on parameter_material_attr.ID_material = m.ID_material " +
                             "JOIN parameter p on parameter_material_attr.ID_parameter = p.ID_parameter;";
        using var connection = JoinBase();
        var command = new MySqlCommand();
        command.Connection = connection;
        command.CommandText = query;
        var reader = command.ExecuteReader();
        var result = new List<MaterialInfoNames>();
        while (reader.Read()) {
            result.Add(new (reader.GetString(0), reader.GetString(1),
                reader.GetDouble(2), reader.GetString(3)));
        }
        connection.Close();
        
        return result;
    }
    
    public static void DeleteMaterial(string materialType) {
        var query = $"DELETE FROM material WHERE type = \"{materialType}\";";
        using var connection = JoinBase();
        var command = new MySqlCommand();
        command.Connection = connection;
        command.CommandText = query;
        command.ExecuteReader();
        connection.Close();
    }
    
    public static void DeleteParameter(int id) {
        var query = $"DELETE FROM parameter WHERE ID_parameter = {id};";
        using var connection = JoinBase();
        var command = new MySqlCommand();
        command.Connection = connection;
        command.CommandText = query;
        command.ExecuteReader();
        connection.Close();
    }
    
    public static void DeleteMaterialInfo(string materialType) {

        var query = "DELETE " +
                    "FROM parameter_material_attr " +
                    "WHERE ID_material = (" +
                    "SELECT ID_material " +
                    "FROM material " +
                    $"WHERE type = \"{materialType}\");";
        using var connection = JoinBase();
        var command = new MySqlCommand();
        command.Connection = connection;
        command.CommandText = query;
        command.ExecuteReader();
        connection.Close();
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
        
        var query = "SELECT name, value, unit " +
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
            var isNull = reader.IsDBNull(2);
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