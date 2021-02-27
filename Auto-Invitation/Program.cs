using System;

namespace Auto_Invitation
{
    class Program
    {
        static void Main(string[] args)
        {
            var arg = ArgsParser.FromStringArray(args);
            
            Initialize.Do(arg.GetValue("apikey", true));
            
            var c = new GitHub.InviteToOrgModel();
            c.Email = arg.GetValue("mail");
            c.Role  = arg.GetValue("role");
            
            Console.WriteLine(Json.ToString(c));
            Console.ReadKey();
        }
    }
}