using System;

namespace Application.Activities.Models.Output
{
    public class CommentOutputModel
    {
        public int Id { get; set; }
    
        public DateTime CreatedAt { get; set; }
    
        public string Content { get; set; }
    
        public string Username { get; set; }
    
        public string DisplayName { get; set; }
    
        public string ProfilePictureUrl { get; set; }
    }
}