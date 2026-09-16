using System.ComponentModel.DataAnnotations;

namespace Toxic.EId.Sdk.SampleApp.Models;

public class LogoutRequest
{
    [Required]
    public string? SessionId { get; init; }
}
