using System.Threading;
using System.Threading.Tasks;
using Application.Activities.Interfaces.DataServices;
using Application.Activities.Models.Output;
using Application.Profiles.Interfaces.DataServices;
using AutoMapper;
using Domain.Activities;
using MediatR;
using Reactivities.Common.Result.Models;

namespace Application.Activities.Commands;

public class AddComment
{
    public class Command : IRequest<Result<CommentOutputModel>>
    {
        public Command(string content, int activityId, string username)
        {
            this.Content = content;
            this.ActivityId = activityId;
            this.Username = username;
        }
            
        public string Content { get; }
        public int ActivityId { get; }
        public string Username { get; }
    }
        
    public class Handler : IRequestHandler<Command, Result<CommentOutputModel>>
    {
        private readonly IProfilesDataService _profilesDataService;
        private readonly IActivitiesDataService _activitiesDataService;
        private readonly IMapper _mapper;

        public Handler(IProfilesDataService profilesDataService, IActivitiesDataService activitiesDataService, IMapper mapper)
        {
            this._profilesDataService = profilesDataService;
            this._activitiesDataService = activitiesDataService;
            this._mapper = mapper;
        }

        public async Task<Result<CommentOutputModel>> Handle(Command request, CancellationToken cancellationToken)
        {
            var activity = await this._activitiesDataService
                .GetByIdAsync(request.ActivityId);

            var profile = await this._profilesDataService
                .GetByUsernameAsync(request.Username);

            var comment = Comment.New(request.Content, profile);
                
            activity.AddComment(comment);

            await this._activitiesDataService.SaveChangesAsync(cancellationToken);

            return Result<CommentOutputModel>.Success(
                this._mapper.Map<CommentOutputModel>(comment));
        }
    }
}