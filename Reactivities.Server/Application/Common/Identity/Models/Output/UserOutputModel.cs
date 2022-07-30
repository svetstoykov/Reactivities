using Application.Common.Identity.Models.Base;

namespace Application.Common.Identity.Models.Output
{
    public class UserOutputModel : BaseUserOutputModel
    {
        public string Token { get; set; }
    }
}
