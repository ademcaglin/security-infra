using MediatR;
using Microsoft.Extensions.Logging;
using SecurityInfra.Common.Cqrs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SecurityInfra.Configuration.Web.Application.Behaviors
{
    public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TResponse: CommandResponse
    {
        private readonly ILogger<TransactionBehaviour<TRequest, TResponse>> _logger;

        public TransactionBehaviour(ILogger<TransactionBehaviour<TRequest, TResponse>> logger)
        {
            _logger = logger ?? throw new ArgumentException(nameof(ILogger));
        }

        public async Task<TResponse> Handle(TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var response = default(TResponse);

            try
            {

                _logger.LogInformation($"Begin transaction {typeof(TRequest).Name}");

                //await _dbContext.BeginTransactionAsync();

                response = await next();

                //await _dbContext.CommitTransactionAsync();
                var cmdResponse = (CommandResponse)response;
                _logger.LogInformation(cmdResponse.IntegrationEvents.Count.ToString());

                _logger.LogInformation($"Committed transaction {typeof(TRequest).Name}");

                //await _orderingIntegrationEventService.PublishEventsThroughEventBusAsync();

                return response;
            }
            catch (Exception)
            {
                _logger.LogInformation($"Rollback transaction executed {typeof(TRequest).Name}");

                // Rollback
                throw;
            }
        }
    }
}
