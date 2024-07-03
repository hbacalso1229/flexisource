using MediatR;

namespace FitnessTracker.Application.Common.Interfaces
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
