namespace Yva.UsersCsvImport.Dto.Yva;

public enum CsvHandlerErrorCode
{
    InvalidRows = 1,
    InvalidFileType = 2,
    InvalidEncoding = 3,
    InvalidDelimiter = 4,
    UnknownError = 9
}