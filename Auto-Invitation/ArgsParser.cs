using System.Collections.Generic;

namespace Auto_Invitation
{
    public class ArgsParser
    {
        public static Dictionary<string, string> FromStringArray(string[] args)
        {
            var dic = new Dictionary<string, string>();
            dic.Add("DEFAULT", "");
            string _lastArg = "";
            foreach (var arg in args)
            {
                if (_lastArg != "")
                {
                    if (arg.StartsWith("--"))
                    {
                        if (!dic.ContainsKey(_lastArg))
                            dic.Add(_lastArg, "true");
                        else
                            dic[_lastArg] = dic[_lastArg] + "," + "true";
                        _lastArg = arg.Substring(2);
                    }
                    else
                    {
                        if (!dic.ContainsKey(_lastArg))
                            dic.Add(_lastArg, arg);
                        else
                            dic[_lastArg] = dic[_lastArg] + "," + arg;
                        _lastArg = "";
                    }
                }
                else
                {
                    if (arg.StartsWith("--"))
                    {
                        _lastArg = arg.Substring(2);
                    }
                    else
                    {
                        dic["DEFAULT"] = dic["DEFAULT"] + "," + arg;
                    }
                }
            }

            if (!dic.ContainsKey(_lastArg) && _lastArg != "")
                dic.Add(_lastArg, "true");

            return dic;
        }
    }
}