namespace ShishByzh.Application.Users.Commands.DeleteUser;

public class CommandValidator : AbstractValidator<Command>
{
    public CommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("Не указан идентификатор пользователя");
        RuleFor(x => x.UserId).NotEqual(Guid.Empty)
            .WithMessage("Не указано действительное значение идентификатора пользователя");
    }
}