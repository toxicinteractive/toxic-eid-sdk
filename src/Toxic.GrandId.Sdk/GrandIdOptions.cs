using System.ComponentModel.DataAnnotations;

namespace Toxic.GrandId.Sdk;

public class GrandIdOptions
{
    [Required]
    public string? ApiKey { get; set; }
    
    [Required]
    public string? ServiceKey { get; set; }
    
    public bool IsTest { get; set; }
}
