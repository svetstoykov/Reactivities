using Application.Common.Identity.Models.Base;

namespace Application.Profiles.Models
{
    public class ProfileOutputModel : BaseUserOutputModel
    {
        public string Bio { get; set; }
    }
}
