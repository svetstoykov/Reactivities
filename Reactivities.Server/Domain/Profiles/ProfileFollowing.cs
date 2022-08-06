using Domain.Common.Base;

namespace Domain.Profiles
{
    public class ProfileFollowing : DomainEntity
    {
        public int? ObserverId { get; set; }
    
        public Profile Observer { get; set; }
    
        public int TargetId { get; set; }
    
        public Profile Target { get; set; }

        public static ProfileFollowing New(Profile observer, Profile target)
            => new ProfileFollowing
            {
                Observer = observer,
                Target = target
            };
    }
}