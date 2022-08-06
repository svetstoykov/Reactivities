
using System;
using API.Common;

namespace API.Activities.Models.Comments
{
    public class CommentApiModel : BaseApiModel
    {
        public int Id { get; set; }
        
        public string Content { get; set; }
        
        public DateTime CreatedAt { get; set; }
    
        public string Username { get; set; }
    
        public string DisplayName { get; set; }
    
        public string ProfilePictureUrl { get; set; }
    }
}