using System.Text;
using Microsoft.Extensions.Logging;
using Rhyous.WebApiExtensions.Exceptions;
using Rhyous.WebApiExtensions.Models;

namespace Rhyous.WebApiExtensions.Interfaces;


/// <summary>Validates the startup of the microservice.</summary>
/// <remarks>This follows SOLID principles and allows for adding validators using only DI.</remarks>
public class AllStartupValidators : IAllStartupValidators
{
    private readonly IEnumerable<IStartupValidator> _startupValidators;
    private readonly ILogger<AllStartupValidators> _logger;


    /// <summary>The constructor.</summary>
    /// <param name="startupValidators">An instance of <see cref="IEnumerable{IStarupValidator}"/>.</param>
    /// <param name="logger">An instance of <see cref="ILogger{AllStartupValidators}"/></param>
    public AllStartupValidators(IEnumerable<IStartupValidator> startupValidators,
                                ILogger<AllStartupValidators> logger)
    {
        _startupValidators = startupValidators;
        _logger = logger;
    }

    /// <summary>Validates the startup of the microservice.</summary>
    /// <returns>A completed <see cref="Task"/>.</returns>
    public async Task ValidateAsync()
    {
        if (_startupValidators == null || !_startupValidators.Any())
            return;
        var allResults = new List<StartupValidationResult>();
        foreach (var validator in _startupValidators)
        {
            var results = await validator.ValidateAsync();
            if (results != null && results.Any())
                allResults.AddRange(results);
        }
        var failures = allResults.Where(r => !r.IsValid);
        if (failures.Any())
        {
            var message = BuildMessage(failures.ToList());
            var ex = new StartupValidationException(message);
            _logger.LogError(ex, message);
            throw ex;
        }
    }

    private static string BuildMessage(IList<StartupValidationResult> allFailedResults)
    {
        var sb = new StringBuilder($"Startup Validation Errors: ");
        for (int i = 0; i < allFailedResults.Count; i++)
        {
            sb.Append(Environment.NewLine);
            sb.Append($"{allFailedResults[i].Name}: {allFailedResults[i].Message}");
        }
        return sb.ToString();
    }

}
