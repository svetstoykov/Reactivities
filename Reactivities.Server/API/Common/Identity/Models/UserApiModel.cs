using API.Common.Identity.Models.Base;

namespace API.Common.Identity.Models
{
    public class UserApiModel : BaseUserApiModel
    {
        public string Token { get; set; }
    }
}
    