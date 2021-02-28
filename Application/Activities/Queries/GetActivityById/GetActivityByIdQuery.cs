using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Activities.Queries.GetActivityById
{
    public class GetActivityByIdQuery:IRequest<Activity>
    {
		public Guid ActivityId { get; set; }
	}
	public class GetActivityByIdQueryHandler : IRequestHandler<GetActivityByIdQuery, Activity>
	{
		private readonly IApplicationDbContext _context;

		public GetActivityByIdQueryHandler(IApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<Activity> Handle(GetActivityByIdQuery request, CancellationToken cancellationToken)
		{
			var activity = await _context.Activities.FindAsync(request.ActivityId);
			return activity;
		}
	}
}
