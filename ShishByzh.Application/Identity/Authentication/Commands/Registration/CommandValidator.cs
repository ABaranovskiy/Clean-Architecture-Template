namespace ShishByzh.Application.Identity.Authentication.Commands.Registration;

public class CommandValidator : AbstractValidator<Command>
{
    public CommandValidator()
    {
        RuleFor(x => x.Fio).NotEmpty().WithMessage("ФИО обязательно для заполнения");
        RuleFor(x => x.UserName).NotEmpty().WithMessage("Логин обязателен для заполнения");
        RuleFor(x => x.UserName).MinimumLength(4).WithMessage("Логин должен содержать минимум 4 символа");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Пароль обязателен для заполнения");
        RuleFor(x => x.Password).MinimumLength(4).WithMessage("Пароль должен содержать минимум 4 символа");
    }
}