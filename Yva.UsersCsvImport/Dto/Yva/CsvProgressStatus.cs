namespace Yva.UsersCsvImport.Dto.Yva;

/// <summary>
/// Contains information and status about processing imported users
/// </summary>
public class CsvProgressStatus
{
    /// <summary>
    /// Current processing status
    /// </summary>
    public CsvHandlerStatus Status { get; set; }
        
    /// <summary>
    /// Progress of the current import stage
    /// Used for Validating and Applying statuses
    /// Can have values from 0 to 100
    /// Being zeroed once after Validating stage
    /// </summary>
    public int Progress { get; set; }
        
    /// <summary>
    /// Number of processed rows in file
    /// </summary>
    public long ProcessedRowCount { get; set; }
        
    /// <summary>
    /// [Optional] Explains validation errors
    /// </summary>
    public CsvHandlerErrorCode? ErrorType { get; set; }
}