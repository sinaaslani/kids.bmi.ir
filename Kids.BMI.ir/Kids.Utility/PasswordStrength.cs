using System;
using System.Text.RegularExpressions;

namespace Kids.Utility
{
    public enum PasswordStrengthStatus
    {
        Weak = 0,
        Mediocre = 1,
        OK = 2,
        Great = 3,
    }

    public static class PasswordStrength
    {
        public static bool IsPasswordOk(string Password, int PasswordLevel)
        {
            double EffectiveBitSize;
            CheckPasswordStatus(Password, out EffectiveBitSize);
            return EffectiveBitSize >= PasswordLevel;
        }

        public static PasswordStrengthStatus CheckPasswordStatus(string Password)
        {
            double EffectiveBitSize;
            return CheckPasswordStatus(Password, out EffectiveBitSize);
        }

        public static PasswordStrengthStatus CheckPasswordStatus(string Password, out double EffectiveBitSize)
        {
            PasswordStrengthStatus PasswordStatus;
            int charSet = GetCharSetUsed(Password);
            int passSize = Password.Length;

            EffectiveBitSize = Math.Log(Math.Pow(charSet, passSize))/Math.Log(2);
            EffectiveBitSize = Math.Round(EffectiveBitSize, 0);

            if (EffectiveBitSize <= 32)
            {
                PasswordStatus = PasswordStrengthStatus.Weak;
            }
            else if (EffectiveBitSize <= 64)
            {
                PasswordStatus = PasswordStrengthStatus.Mediocre;
            }
            else if (EffectiveBitSize <= 128)
            {
                PasswordStatus = PasswordStrengthStatus.OK;
            }
            else if (EffectiveBitSize > 128)
            {
                PasswordStatus = PasswordStrengthStatus.Great;
            }
            else
            {
                throw new InvalidOperationException();
            }


            return PasswordStatus;
        }

        private static int GetCharSetUsed(string Password)
        {
            int ret = 0;

            if (ContainsNumbers(Password))
            {
                ret += 10;
            }

            if (ContainsLowerCaseChars(Password))
            {
                ret += 26;
            }

            if (ContainsUpperCaseChars(Password))
            {
                ret += 26;
            }

            if (ContainsPunctuation(Password))
            {
                ret += 31;
            }

            return ret;
        }

        private static bool ContainsNumbers(string InputString)
        {
            Regex pattern = new Regex(@"[\d]");
            return pattern.IsMatch(InputString);
        }

        private static bool ContainsLowerCaseChars(string InputString)
        {
            Regex pattern = new Regex("[a-z]");
            return pattern.IsMatch(InputString);
        }

        private static bool ContainsUpperCaseChars(string InputString)
        {
            Regex pattern = new Regex("[A-Z]");
            return pattern.IsMatch(InputString);
        }

        private static bool ContainsPunctuation(string InputString)
        {
            Regex pattern = new Regex(@"[\W|_]");
            return pattern.IsMatch(InputString);
        }
    }
}