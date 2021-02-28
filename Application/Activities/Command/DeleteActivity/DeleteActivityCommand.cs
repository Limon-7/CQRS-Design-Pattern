using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Activities.Command.DeleteActivity
{
	public class DeleteActivityCommand : IRequest<Response<Activity>>
	{
		public Guid Id { get; set; }
	}
	public class DeleteActivityCommandHandler : IRequestHandler<DeleteActivityCommand, Response<Activity>>
	{
		private readonly IApplicationDbContext _context;

		public DeleteActivityCommandHandler(IApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<Response<Activity>> Handle(DeleteActivityCommand request, CancellationToken cancellationToken)
		{
			var activity = await _context.Activities.FindAsync(request.Id);
			if (activity == null)
				throw new Exception($"Entity Not found with given {request.Id}");
			_context.Activities.Remove(activity);
			var success = await _context.SaveChangesAsync(cancellationToken);
			if (success > 0)
				return await Task.FromResult(Response.Ok<Activity>(default, "Deleted Successfully"));
			return await Task.FromResult(Response.Fail<Activity>("Something went wrong"));
		}
	}
}
