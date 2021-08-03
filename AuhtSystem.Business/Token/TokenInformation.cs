using System;

namespace AuhtSystem.Business.Token
{
    public class TokenInformation
    {
        public TokenInformation(string token, DateTime expiresIn)
        {
            Token = token;
            ExpiresIn = expiresIn;
        }
        public string Token { get; private set; }
        public DateTime ExpiresIn { get; private set; }
    }
}
