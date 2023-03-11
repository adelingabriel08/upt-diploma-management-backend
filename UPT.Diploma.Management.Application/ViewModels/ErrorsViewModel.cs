using System.Text.Json.Serialization;

namespace UPT.Diploma.Management.Application.ViewModels;

public class ErrorsViewModel
{
    [JsonPropertyName("errors")]
    public List<string> Errors { get; set; }
}