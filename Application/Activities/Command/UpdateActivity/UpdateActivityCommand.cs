using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Activities.Command.UpdateActivity
{
	public class UpdateActivityCommand : IRequest<Response<Activity>>
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public DateTime Date { get; set; }
		public string Description { get; set; }
		public string Category { get; set; }
		public string City { get; set; }
		public string Venue { get; set; }
		public bool IsCancelled { get; set; }
	}
	public class UpdateActivityCommandHandler : IRequestHandler<UpdateActivityCommand, Response<Activity>>
	{
		private readonly IApplicationDbContext _context;

		public UpdateActivityCommandHandler(IApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<Response<Activity>> Handle(UpdateActivityCommand request, CancellationToken cancellationToken)
		{
			var activity = await _context.Activities.FindAsync(request.Id);
			if (activity == null)
				throw new Exception();
				//throw new RestException(statusCode: (int)HttpStatusCode.NotFound,"Entity Not found with given");
			activity.Title = request.Title ?? activity.Title;
			activity.Date = DateTime.UtcNow;
			activity.Description = request.Description ?? activity.Description;
			activity.Category = request.Category ?? activity.Category;
			activity.City = request.City ?? activity.City;
			activity.Venue = request.Venue ?? activity.Venue;

			var success = await _context.SaveChangesAsync(cancellationToken);
			if (success > 0)
				return await Task.FromResult(Response.Ok<Activity>(activity, "Update Successfully"));
			return await Task.FromResult(Response.Fail<Activity>("Something went wrong"));
		}
	}
}
