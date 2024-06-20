using System.Data.SqlClient;

namespace PetAdoptionSystem.Domain.Interfaces
{
    public interface IDatabaseExecutorService
    {
        Task<T?> ExecuteScalarAsync<T>(string query, SqlParameter[]? parameters = null);
        Task<int> ExecuteNonQueryAsync(string query, SqlParameter[]? parameters = null);
        Task<List<T>> ExecuteQueryAsync<T>(string query, SqlParameter[]? parameters = null) where T : new();
    }
}