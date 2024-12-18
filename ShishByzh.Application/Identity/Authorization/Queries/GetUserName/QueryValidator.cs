namespace ShishByzh.Application.Identity.Authorization.Queries.GetUserName
{
    public class QueryValidator : AbstractValidator<Query>
    {
        public QueryValidator()
        {
            RuleFor(request => request.UserId).NotEmpty().NotEqual(Guid.Empty)
                .WithMessage("UserId должен быть не пустой.");
        }
    }
}
