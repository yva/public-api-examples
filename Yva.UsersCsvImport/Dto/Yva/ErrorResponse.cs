using System.Collections.Generic;

namespace Yva.UsersCsvImport.Dto.Yva;

public class ErrorResponse
{
    public int Status { get; set; }
    public string Title { get; set; }
    public Error[] Errors { get; set; }
    public Dictionary<string, object> Extensions { get; set; } = new();
}