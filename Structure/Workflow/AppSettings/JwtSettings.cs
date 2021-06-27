namespace Workflow.AppSettings
{
    //  jwt token authentication read from appsettings.json from section JwtSettings
    //  configured in startup in service 
    public partial class JwtSettings
    {
        public string SecretKey { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
    }
}