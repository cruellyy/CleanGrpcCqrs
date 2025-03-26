using Application.Dtos;
using Application.MediatR.Commands.MessageSamples;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<MessageSample, MessageSampleDto>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content));

        CreateMap<MessageSampleDto, MessageSample>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content));

        CreateMap<CreateMessageSampleCommand, MessageSample>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content));
    }
}