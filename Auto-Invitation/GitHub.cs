using System.Net.Mail;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Auto_Invitation
{
    public class GitHub
    {
        public static async Task<string> Get(string querry)
        {
            return await Http.GetStrAsync("https://api.github.com" + querry);
        }

        public static async Task<HttpResponseModel<string>> Post(string querry, string data)
        {
            return await Http.PostStrAsync("https://api.github.com" + querry, data);
        }

        public static async Task<HttpResponseModel<string>> InviteToOrg(string org, InviteToOrgModel option)
        {
            return await Post(
                $"/orgs/{org}/invitations",
                Json.ToString(option)
            );
        }

        public static async Task<HttpResponseModel<string>> InviteToOrg(string org, MailAddress mail, string role = "direct_member")
        {
            return await InviteToOrg(org, new InviteToOrgModel()
            {
                Email = mail.Address,
                Role = role
            });
        }

        public class InviteToOrgModel
        {
            [JsonPropertyName(("email"))] public string Email { get; set; }

            /// <summary>
            /// Only accept {billing_manager, direct_member, admin}
            /// </summary>
            [JsonPropertyName(("role"))]
            public string Role { get; set; }
        }
    }
}