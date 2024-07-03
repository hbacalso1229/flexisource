using MediatR;

namespace FitnessTracker.Application.Common.Interfaces
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}
