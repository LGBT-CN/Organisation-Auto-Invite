using System.Text.Json;

namespace Auto_Invitation
{
    public class Json
    {
        private static JsonSerializerOptions _options = new()
        {
            WriteIndented = false, 
            IgnoreNullValues = true
        };

        public static string ToString(object obj)
        {
            return JsonSerializer.Serialize(obj, _options);
        }
    }
}