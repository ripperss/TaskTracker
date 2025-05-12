using FluentValidation;

namespace TaskTracker.Application.Auth.Commands.Register;

public class UserRegisterCommnadValidation : AbstractValidator<UserRegisterCommand>
{
    public UserRegisterCommnadValidation()
    {
        RuleFor(command => command.FirstName).Length(3, 20);
        RuleFor(command => command.LastName).Length(3, 20);
    }
}
