namespace Yva.UsersCsvImport.Dto.Yva;

/// <summary>
/// Contains information about validation error while importing users via csv file.
/// </summary>
public class CsvImportError
{
    /// <summary>
    /// <inheritdoc cref="UsersFileErrorCode"/>
    /// </summary>
    public UsersFileErrorCode Code { get; set; }
        
    /// <summary>
    /// [Optional] Value which raised validation error.
    /// </summary>
    public string ProblemValue { get; set; }

    /// <summary>
    /// Row number which contains problem.
    /// </summary>
    public int RowNumber { get; set; }
}