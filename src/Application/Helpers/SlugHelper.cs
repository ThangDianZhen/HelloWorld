using System.Text.RegularExpressions;

namespace Ignite.Application.Helpers;

public static class SlugHelper
{
    public static string ToSlug(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        var lower = value.Trim().ToLowerInvariant();
        var slug = Regex.Replace(lower, @"[^a-z0-9\s-]", string.Empty);
        slug = Regex.Replace(slug, @"[\s-]+", "-").Trim('-');
        return slug;
    }
}
