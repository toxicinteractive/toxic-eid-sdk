using System.ComponentModel.DataAnnotations;

namespace Toxic.EId.Sdk;

public class EIdOptions
{
    [Required]
    public string? ApiKey { get; set; }
    
    [Required]
    public string? ServiceKey { get; set; }
    
    public bool IsTest { get; set; }
}
