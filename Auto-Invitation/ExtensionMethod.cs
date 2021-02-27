using System;
using System.Collections.Generic;

namespace Auto_Invitation
{
    public static class ExtensionMethod
    {
        
        public static string GetValue(this Dictionary<string, string> dic,  string key, bool isNeedExp = false)
        {
            if (!dic.ContainsKey(key))
            {
                if (isNeedExp)
                    throw new ArgumentException($"Key({key}) is empty!");
                return null;
            }

            return dic[key];
        }
    }
}