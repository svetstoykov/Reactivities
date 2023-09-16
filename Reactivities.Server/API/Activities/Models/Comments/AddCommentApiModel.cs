using API.Common.Models;

namespace API.Activities.Models.Comments;

public class AddCommentApiModel : BaseApiModel
{
    public string Content { get; set; }
        
    public int ActivityId { get; set; }
    
    public string Username { get; set; }
}