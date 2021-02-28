using FluentValidation;
using System;

namespace Application.Activities.Command.UpdateActivity
{
	public class UpdateActivityCommandValidator : AbstractValidator<UpdateActivityCommand>
	{
		public UpdateActivityCommandValidator()
		{
			RuleFor(v => v.Title).MaximumLength(30).WithMessage("Length with in 30 charectures").NotEmpty().WithMessage("Title is required");
		}
	}
}
