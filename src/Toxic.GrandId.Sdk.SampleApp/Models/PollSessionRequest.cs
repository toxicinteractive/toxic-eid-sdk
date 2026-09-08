using System.ComponentModel.DataAnnotations;

namespace Toxic.GrandId.Sdk.SampleApp.Models;

public class PollSessionRequest
{
    [Required]
    public string? SessionId { get; init; }
}
