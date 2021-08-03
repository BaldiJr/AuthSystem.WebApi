using System;

namespace AuhtSystem.Business.Token
{
    public class AuthInformation
    {
        public string User { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Token { get; set; }
        public DateTime TokenExpiresIn { get; set; }

    }
}
