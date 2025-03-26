using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.MediatR.Commands.MessageSamples;

public class CreateMessageSampleCommandHandler(IBaseRepository<MessageSample> messageSampleRepository,
    IMapper mapper) 
    : IRequestHandler<CreateMessageSampleCommand, int>
{
    public async Task<int> Handle(CreateMessageSampleCommand request, CancellationToken cancellationToken)
    {
        var messageSample = mapper.Map<MessageSample>(request);

        await messageSampleRepository.CreateAsync(messageSample);
        await messageSampleRepository.SaveTransationAsync();
        
        return messageSample.Id;
    }
}