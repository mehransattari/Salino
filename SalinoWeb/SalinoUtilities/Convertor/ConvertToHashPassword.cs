using System;
using System.Collections.Generic;
using System.Linq;

namespace SalinoMvc5.SalinoUtilities
{
    public static class ConvertToHashPassword
    {
        public static string  ToHashPassword(this string value)
        {
            string pashash = FormsAuthentication.HashPasswordForStoringInConfigFile(value ,"MD5");
            return pashash;
        }
    }
}