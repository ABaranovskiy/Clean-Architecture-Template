namespace ShishByzh.Application.Identity.Authorization.Queries.IsInRole
{
    public class QueryValidator : AbstractValidator<Query>
    {
        public QueryValidator()
        {
            RuleFor(request => request.UserId).NotEmpty().NotEqual(Guid.Empty)
                .WithMessage("UserId должен быть пустым.");
            RuleFor(request => request.Role).NotEmpty()
                .WithMessage("Role не должен быть пустым.");
        }
    }
}
