namespace Yva.UsersCsvImport.Dto.Yva;

public enum CsvHandlerStatus
{
    Created = 1,
    Validating = 2,
    Applying  = 3,
    Finished = 4,
    ValidationFail = 5
}