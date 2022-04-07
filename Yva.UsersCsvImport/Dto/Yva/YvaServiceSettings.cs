using System;

namespace Yva.UsersCsvImport.Dto.Yva;

public class YvaServiceSettings
{
    public Uri ApiAddress { get; set; }
    public string TeamsApiToken { get; set; }
    public string AuthorizationHeaderName { get; set; }
    public Uri ImportCsvEndpoint { get; set; }
    public Uri GetImportStatusEndpoint { get; set; }
    public Uri GetImportErrorsEndpoint { get; set; }
    public int ErrorsPerPage { get; set; }
}