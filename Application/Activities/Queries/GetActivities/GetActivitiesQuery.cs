using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Activities.Queries.GetActivities
{
	public class GetActivitiesQuery : IRequest<List<Activity>>
	{
	}
	public class GetActivitiesQueryHandler : IRequestHandler<GetActivitiesQuery, List<Activity>>
	{
		private readonly IApplicationDbContext _context;

		public GetActivitiesQueryHandler(IApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<List<Activity>> Handle(GetActivitiesQuery request, CancellationToken cancellationToken)
		{
			return await _context.Activities.ToListAsync();
		}
	}

}
