namespace ServiceDefaults.Extensions;

using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;

public static class ConfigurationSectionExtensions
{
    public static TSection? GetSection<TSection>(this IApplicationBuilder app, string? sectionName = null, bool notNull = true, bool validate = true) where TSection : class, new()
    {
        var config = app.ApplicationServices.GetRequiredService<IConfiguration>();
        return config.GetSection<TSection>(sectionName, notNull, validate);
    }

    public static TSection? GetSection<TSection>(this IConfiguration configuration, string? sectionName = null, bool notNull = true, bool validate = true) where TSection : class, new()
    {
        sectionName ??= typeof(TSection).Name;

        var section = configuration.GetSection(sectionName);
        if (!section.Exists())
        {
            throw new ArgumentException($"The section '{sectionName}' is missing in the configuration!");
        }

        var sectionInstance = section.Get<TSection>();
        if (notNull && sectionInstance == null)
        {
            throw new ArgumentException($"Failed to load section '{sectionName}' from the configuration!");
        }

        if (validate && sectionInstance != null)
        {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(sectionInstance);

            if (!Validator.TryValidateObject(sectionInstance, context, validationResults, true))
            {
                throw new ArgumentException($"Invalid configuration: {string.Join(", ", validationResults.Select(r => r.ErrorMessage))}");
            }
        }

        return sectionInstance;
    }
}
