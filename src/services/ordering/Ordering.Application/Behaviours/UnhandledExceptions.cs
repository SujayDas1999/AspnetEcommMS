using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Behaviours
{
    public class UnhandledExceptions<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
			try
			{
				return await next();
			}
			catch (Exception ex)
			{

				var requestName = typeof(TRequest).Name;
				Console.WriteLine($"--> {requestName} exception came with message {ex.Message}");
				throw;
			}
        }
    }
}
