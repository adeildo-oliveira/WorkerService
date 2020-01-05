using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BaseIntegrationCli360
{
    public class BaseFinal : IBaseFinal
    {
        private readonly IConfigurationConnection _configurationConnection;

        public BaseFinal(IConfigurationConnection configurationConnection) => _configurationConnection = configurationConnection;

        public async Task InserirTodosOsSalariosAsync(IEnumerable<Salario> salarios)
        {
            var teste = salarios.ToArray();
            using (var conn = new SqlConnection(_configurationConnection.Base2))
            {
                await conn.ExecuteAsync(@"INSERT INTO Salario (Id, Pagamento, Adiantamento, Status)
                                            VALUES (@Id, @Pagamento, @Adiantamento, @Status)"
                , salarios
                , commandType: CommandType.Text);
            }
        }
    }

    public interface IBaseFinal
    {
        Task InserirTodosOsSalariosAsync(IEnumerable<Salario> salarios);
    }
}
