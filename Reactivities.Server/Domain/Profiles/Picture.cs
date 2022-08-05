using Domain.Common.Base;

namespace Domain.Profiles
{
    public class Picture : DomainEntity
    {
        public string PublicId { get; set; }

        public string Url { get; set; }
    }
}
