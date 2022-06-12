using API.Models.Activities.Base;

namespace API.Models.Activities.Request
{
    public class EditActivityRequestModel : CreateEditActivityBaseRequestModel
    {
        public int Id { get; set; }
    }
}
