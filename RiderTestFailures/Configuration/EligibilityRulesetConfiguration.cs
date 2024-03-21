namespace RiderTestFailures.Configuration;

public class EligibilityRulesetConfiguration
{
    public long Id { get; set; }
    public string? Description { get; set; }
    public List<Rule> Rules { get; set; } = null!;

    public class Rule {
        public string Id { get; set; } = null!;
        public Dictionary<string, string> Parameters { get; set; } = new() { };
    }
}