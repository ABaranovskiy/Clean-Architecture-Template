namespace ShishByzh.Application.Identity.Authorization.Queries.IsInPolicy
{
    public class QueryValidator : AbstractValidator<Query>
    {
        public QueryValidator()
        {
            RuleFor(request => request.UserId).NotEmpty().NotEqual(Guid.Empty)
                .WithMessage("UserId должен быть пустым.");
            RuleFor(request => request.Policy).NotEmpty()
                .WithMessage("Role не должен быть пустым.");
        }
    }
}
