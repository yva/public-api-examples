using System.Collections.Generic;
using System.Threading.Tasks;
using Yva.UsersCsvImport.Dto;
using Yva.UsersCsvImport.Dto.Yva;

namespace Yva.UsersCsvImport.Services;

public class CsvImportService
{
    //Configuration
    private static YvaServiceSettings _yvaServiceSettings;
    private static CsvRepositorySettings _csvRepositorySettings;
    
    //Services and repositories
    private static YvaServiceAdapter _yvaServiceAdapter;
    private static CsvRepository _csvRepository;

    
    public CsvImportService(
        YvaServiceAdapter yvaServiceAdapter,
        YvaServiceSettings yvaServiceSettings,
        CsvRepositorySettings csvRepositorySettings,
        CsvRepository csvRepository)
    {
        _yvaServiceAdapter = yvaServiceAdapter;
        _yvaServiceSettings = yvaServiceSettings;
        _csvRepositorySettings = csvRepositorySettings;
        _csvRepository = csvRepository;
    }
    
    public async Task<bool> ImportCsvAsync()
    {
        // Exporting csv from some internal source
        var csvStream = _csvRepository.ExportCsvAsStream(_csvRepositorySettings.CsvFilePath);
        // Streaming file to yva teams api.
        // In case of success it will start importing users into Yva.
        return await _yvaServiceAdapter.TryUploadCsvToYvaAsync(csvStream);
    }

    // Waiting for import either to complete or fail on validation
    // intermediate statuses is not interesting for us
    public async Task<CsvProgressStatus> WaitForImportToEndAsync()
    {
        var progressStatus = await _yvaServiceAdapter.GetImportStatusAsync();
        while (progressStatus?.Status is not (CsvHandlerStatus.Finished or CsvHandlerStatus.ValidationFail))
        {
            await Task.Delay(5000);
            progressStatus = await _yvaServiceAdapter.GetImportStatusAsync();
        }

        return progressStatus;
    }

    // Paging through errors of csv import
    public async Task<List<CsvImportError>> GetImportErrorsAsync()
    {
        var offset = 0;
        var errors = new List<CsvImportError>();
        var errorsReturnedFromYva = 0;
        
        do
        {
            var errorsFromYva = await _yvaServiceAdapter.GetImportErrorsAsync(_yvaServiceSettings.ErrorsPerPage, offset);
            errors.AddRange(errorsFromYva);
            offset += _yvaServiceSettings.ErrorsPerPage;
            errorsReturnedFromYva = errorsFromYva.Length;
        } while (errorsReturnedFromYva == _yvaServiceSettings.ErrorsPerPage);

        return errors;
    }
}