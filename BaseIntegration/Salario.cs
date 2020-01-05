using System;

namespace BaseIntegrationCli360
{
    public class Salario
    {
        public Salario(decimal pagamento, decimal adiantamento, bool status)
        {
            Pagamento = pagamento;
            Adiantamento = adiantamento;
            Status = status;
        }

        public Salario(Guid id, decimal pagamento, decimal adiantamento, bool status)
        {
            Id = id;
            Pagamento = pagamento;
            Adiantamento = adiantamento;
            Status = status;
        }

        public Guid Id { get; private set; } = Guid.NewGuid();
        public decimal Pagamento { get; private set; }
        public decimal Adiantamento { get; private set; }
        public bool Status { get; private set; } = true;
    }
}
