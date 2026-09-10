using System.ComponentModel.DataAnnotations;

namespace Toxic.EId.Sdk.SampleApp.Models;

public class PollSessionRequest
{
    [Required]
    public string? SessionId { get; init; }
}
