using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
	public interface IApplicationDbContext
	{
		DbSet<Value> Values { get; set; }
		DbSet<Activity> Activities { get; set; }
		Task<int> SaveChangesAsync(CancellationToken cancellationToken);
	}
}
