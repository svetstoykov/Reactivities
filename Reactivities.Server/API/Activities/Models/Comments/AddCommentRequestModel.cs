using API.Common;

namespace API.Activities.Models.Comments;

public class AddCommentRequestModel : BaseApiModel
{
    public string Content { get; set; }
        
    public int ActivityId { get; set; }
    
    public string Username { get; set; }
}