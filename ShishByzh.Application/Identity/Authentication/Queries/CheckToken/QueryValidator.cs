namespace ShishByzh.Application.Identity.Authentication.Queries.CheckToken
{
    public class QueryValidator : AbstractValidator<Query>
    {
        public QueryValidator()
        {
            RuleFor(request => request.Token).NotEmpty()
                .WithMessage("Токен должен быть не пустой.");
        }
    }
}
