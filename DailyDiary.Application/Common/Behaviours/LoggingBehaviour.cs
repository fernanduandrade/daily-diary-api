using MediatR.Pipeline;
using Serilog;

namespace DailyDiary.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest, TOut> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        
        // TODO adicionar no claims o usuário e criar um userManager;
        //var userId = _currentUserService.UserId ?? string.Empty;


        Log.Information("CleanArchitecture Request: {Name} {@Request}",
            requestName, request);

        return Task.CompletedTask;
    }
}