using Carter;
using Carter.OpenApi;
using RiderTestFailures.Configuration;

namespace RiderTestFailures.Modules;

public class EligibilityRulesetModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/eligibility/rulesets", this.CreateRuleset)
            .WithName(nameof(this.CreateRuleset))
            .Accepts<EligibilityRulesetConfiguration>("application/json")
            .Produces(200)
            .Produces(422)
            .Produces(400)
            .IncludeInOpenApi();
    }
    
    private async Task<IResult> CreateRuleset(HttpContext context, EligibilityRulesetRequest request, CancellationToken cancellationToken)
    {
        return Results.NoContent();
    }
}

public class EligibilityRulesetRequest
{
    public string? Description { get; set; } = null!;
    public List<EligibilityRulesetConfiguration.Rule> Rules { get; set; } = null!;

    public EligibilityRulesetRequest() {}
    public EligibilityRulesetRequest(EligibilityRulesetConfiguration ruleset)
    {
        this.Description = ruleset.Description;
        this.Rules = ruleset.Rules;
    }
}