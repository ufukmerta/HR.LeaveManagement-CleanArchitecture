using System.Text.RegularExpressions;

namespace HR.LeaveManagement.API.Routing;

public partial class ToKebabCaseParameterTransformer : IOutboundParameterTransformer
{
    public string? TransformOutbound(object? value)
    {
        if (value is null)
        {
            return null;
        }

        return KebabCaseRegex().Replace(value.ToString()!, "$1-$2").ToLowerInvariant();
    }

    [GeneratedRegex("([a-z])([A-Z])")]
    private static partial Regex KebabCaseRegex();
}
