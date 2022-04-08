using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Activities.Command.CreateActivity
{
    public class CreateActivityCommand:IRequest<Response<Activity>>
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string City { get; set; }
        public string Venue { get; set; }
        public bool IsCancelled { get; set; }
    }
	public class CreateActivityCommandHandler : IRequestHandler<CreateActivityCommand, Response<Activity>>
	{
		private readonly IApplicationDbContext _context;

		public CreateActivityCommandHandler(IApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<Response<Activity>> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
		{
			var activity = new Activity
			{
				Title = request.Title,
				Date = DateTime.UtcNow,
				Description = request.Description,
				Category = request.Category,
				City = request.City,
				Venue = request.Venue,
				IsCancelled = request.IsCancelled
			};

			_context.Activities.Add(activity);
			var result=await _context.SaveChangesAsync(cancellationToken);
			if (result>0)
			{
				return await Task.FromResult(Response.Ok<Activity>(activity, "created succesfully"));
			}
				return await Task.FromResult(Response.Fail<Activity>( "Server Error"));
		}
	}
}
