namespace AuhtSystem.Business.Token
{
    public class TokenConfiguration
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int TokenExpiresMinutes { get; set; }
    }
}
