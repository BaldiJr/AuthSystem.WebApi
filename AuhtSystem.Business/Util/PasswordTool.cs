using System;
using System.Text;
using System.Text.RegularExpressions;

namespace AuhtSystem.Business.Util
{
    public static class PasswordTool
    {       
        private const int _minLength = 15;

        public static bool ValidatePassword(string password)
        {
            if (password != null)
            {
                Regex validPass = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[@#_\-!])(?:([0-9a-zA-Z\w~@#$%^&*+=`´|'{}:;!.?\()""\-])(?!\1)){15,}$");
                return validPass.IsMatch(password);
            }
            return false;

        }
        public static string GeneratePassword()
        {
            var generatePassword = new byte[_minLength];

            for (var i = 0; i < _minLength; i++)
            {
                
                if (!i.Equals(0))
                {
                    var currentValue = GetRandomByte(i);
                    var previousValue = generatePassword[i - 1];

                    while (previousValue == currentValue)
                    {
                        currentValue = GetRandomByte(i);
                    }

                    generatePassword[i] = currentValue;
                }
                else
                {
                    generatePassword[i] = GetRandomByte(i);
                }
            }

            return Encoding.Default.GetString(generatePassword);
        }
        private static byte GetRandomByte(int bytePosition)
        {
            byte[] requiredChars = Encoding.ASCII.GetBytes(@"@#_-!");
            byte[] lowCaseLetters = Encoding.ASCII.GetBytes(@"abcdefghijklmnopqrstuvwxyz");
            byte[] capitalLetters = Encoding.ASCII.GetBytes(@"ABCCDEFGHIJKLMNOPQRSTUVWXYZ");
            byte[] specialChars = Encoding.ASCII.GetBytes(@"~@#$%^&*+=|{}:;!.?()-_");
            
            var rule = bytePosition % 4;

            switch (rule)
            {
                case 0:
                    return requiredChars[new Random().Next(0, requiredChars.Length)];
                case 1:
                    return lowCaseLetters[new Random().Next(0, lowCaseLetters.Length)];
                case 2:
                    return capitalLetters[new Random().Next(0, capitalLetters.Length)];
                case 3:
                    return specialChars[new Random().Next(0, specialChars.Length)];
                default:
                    return 0;
            }

        }
    }
}
