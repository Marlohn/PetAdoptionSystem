using System.Data.SqlClient;
using System.Reflection;
using PetAdoptionSystem.Domain.Interfaces;

public class DatabaseExecutorService : IDatabaseExecutorService
{
    private readonly string _connectionString;

    public DatabaseExecutorService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<T?> ExecuteScalarAsync<T>(string query, SqlParameter[]? parameters = null)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        using SqlCommand command = new SqlCommand(query, connection);
        if (parameters != null)
        {
            command.Parameters.AddRange(parameters);
        }

        return (T?)await command.ExecuteScalarAsync();
    }

    public async Task<int> ExecuteNonQueryAsync(string query, SqlParameter[]? parameters = null)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        using SqlCommand command = new SqlCommand(query, connection);
        if (parameters != null)
        {
            command.Parameters.AddRange(parameters);
        }

        return await command.ExecuteNonQueryAsync();
    }

    public async Task<List<T>> ExecuteQueryAsync<T>(string query, SqlParameter[]? parameters = null) where T : new()
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        using SqlCommand command = new SqlCommand(query, connection);
        if (parameters != null)
        {
            command.Parameters.AddRange(parameters);
        }

        using SqlDataReader reader = await command.ExecuteReaderAsync();
        List<T> results = new List<T>();

        while (await reader.ReadAsync())
        {
            T item = new T();
            foreach (var prop in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!reader.IsDBNull(reader.GetOrdinal(prop.Name)))
                {
                    prop.SetValue(item, reader[prop.Name]);
                }
            }

            results.Add(item);
        }

        return results;
    }
}