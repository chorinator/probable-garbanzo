using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace HRAcuity.Presentation.WebApi.Dtos;

public class PaginationDto : IValidatableObject, IParsable<PaginationDto>
{
    public int Page { get; init; }
    public int PageSize { get; init; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Page < 1)
            yield return new ValidationResult("Page must be greater than 0");

        if (PageSize < 1)
            yield return new ValidationResult("Page size must be greater than 0");

        if (Page > 100)
            yield return new ValidationResult("Page size must be less than 100");
    }

    public static PaginationDto Parse(string s, IFormatProvider? provider)
    {
        var split = s.Split(",");
        var page = split[0];
        var pageSize = split[1];
        if (string.IsNullOrWhiteSpace(page) || string.IsNullOrWhiteSpace(pageSize))
            throw new ArgumentException("Page and page size must be provided");

        var pagination = new PaginationDto
        {
            Page = int.Parse(page),
            PageSize = int.Parse(pageSize)
        };

        var validation = pagination.Validate(new ValidationContext(pagination));
        if (validation.Any())
            throw new ArgumentException(validation.ToString());

        return pagination;
    }

    public static bool TryParse(
        [NotNullWhen(true)] string? s,
        IFormatProvider? provider,
        [MaybeNullWhen(false)] out PaginationDto result)
    {
        result = null;
        if (string.IsNullOrWhiteSpace(s))
            return false;

        try
        {
            result = Parse(s, provider);
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }
}