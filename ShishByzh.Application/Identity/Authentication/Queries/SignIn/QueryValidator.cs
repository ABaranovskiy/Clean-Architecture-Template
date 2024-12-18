namespace ShishByzh.Application.Identity.Authentication.Queries.SignIn
{
    public class QueryValidator : AbstractValidator<Query>
    {
        public QueryValidator()
        {
            RuleFor(request => request.UserName).NotEmpty().MinimumLength(4)
                .WithMessage("Имя пользователя должно быть не менее 4 символов длиной.");
            RuleFor(request => request.Password).NotEmpty().MinimumLength(4)
                .WithMessage("Пароль должен быть не менее 4 символов длиной.");
        }
    }
}
