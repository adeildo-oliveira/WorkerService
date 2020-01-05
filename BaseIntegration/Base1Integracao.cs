using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BaseIntegrationCli360
{
    public class Base1Integracao : IBase1Integracao
    {
        private readonly IConfigurationConnection _configurationConnection;

        public Base1Integracao(IConfigurationConnection configurationConnection) => _configurationConnection = configurationConnection;

        public async Task<IEnumerable<Salario>> ObterTodosOsSalariosAsync()
        {
            using (var conn = new SqlConnection(_configurationConnection.Base1))
            {
                var result = await conn.QueryAsync<Salario>(@"SELECT [Pagamento]
                  ,[Adiantamento]
                  ,[Status]
                FROM [DespesaMensalAPI].[dbo].[Salario] (nolock)", commandType: CommandType.Text);

                return result;
            }
        }
    }

    public interface IBase1Integracao
    {
        Task<IEnumerable<Salario>> ObterTodosOsSalariosAsync();
    }
}
