using Domain.Common.Base;

namespace Domain.Profiles
{
    public class ProfileFollowing : DomainEntity
    {
        private ProfileFollowing() {}

        private ProfileFollowing(Profile observer, Profile target)
        {
            Observer = observer;
            Target = target;
        }

        public int ObserverId { get; private set; }
    
        public Profile Observer { get; private set; }
    
        public int TargetId { get; private set; }
    
        public Profile Target { get; private set; }

        public static ProfileFollowing New(Profile observer, Profile target) 
            => new(observer, target);
    }
}