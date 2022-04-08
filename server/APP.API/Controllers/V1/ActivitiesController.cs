using Application.Activities.Command.CreateActivity;
using Application.Activities.Command.DeleteActivity;
using Application.Activities.Command.UpdateActivity;
using Application.Activities.Queries.GetActivities;
using Application.Activities.Queries.GetActivityById;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APP.API.Controllers.V1
{
	[Route("api/[controller]")]
	[ApiController]
	public class ActivitiesController : ControllerBase
	{
		private readonly IMediator _mediator;

		public ActivitiesController(IMediator mediator)
		{
			_mediator = mediator;
		}
		[HttpGet]
		public async Task<List<Activity>> Get()
		{
			return await _mediator.Send(new GetActivitiesQuery());
		}

		[HttpGet("{id}")]
		public async Task<Activity> Get(Guid id)
		{
			return await _mediator.Send(new GetActivityByIdQuery { ActivityId = id });
		}


		[HttpPost]
		public async Task<Response<Activity>> Post([FromBody] CreateActivityCommand cmd)
		{
			return await _mediator.Send(cmd);

		}

		// PUT api/<ActivitiesController>/5
		[HttpPut("{id}")]
		public async Task<Response<Activity>> Put([FromRoute] Guid id, UpdateActivityCommand cmd)
		{
			cmd.Id = id;
			return await _mediator.Send(cmd);
		}

		[HttpDelete("{id}")]
		public async Task<Response<Activity>> Delete([FromRoute] Guid id, DeleteActivityCommand cmd)
		{
			cmd.Id = id;
			return await _mediator.Send(cmd);
		}
	}
}
