namespace API.Common.Identity.Models.Base
{
    public abstract class BaseUserApiModel : BaseApiModel
    {
        public string DisplayName { get; set; }

        public string Username { get; set; }

        public string Image { get; set; }
    }
}