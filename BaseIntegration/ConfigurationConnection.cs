namespace BaseIntegrationCli360
{
    public class ConfigurationConnection : IConfigurationConnection
    {
        public string Base1 { get; set; }
        public string Base2 { get; set; }
    }

    public interface IConfigurationConnection
    {
        string Base1 { get; set; }
        string Base2 { get; set; }
    }
}
