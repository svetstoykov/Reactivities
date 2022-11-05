using Application.Messages.Commands;
using AutoMapper;
using Reactivities.Common.Messages.Models.Request;

namespace Application.Messages.Mappings;

public class MessagesMappingProfile : Profile
{
    public MessagesMappingProfile()
    {
        this.CreateMap<SendMessage.Command, SendMessageRequestModel>();
    }
}