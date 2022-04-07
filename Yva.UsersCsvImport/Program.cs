using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Yva.UsersCsvImport.Dto;
using Yva.UsersCsvImport.Dto.Yva;
using Yva.UsersCsvImport.Services;

namespace Yva.UsersCsvImport;

public static class Program
{
    private static CsvImportService _csvImportService;
    
    public static async Task<int> Main(string[] args)
    {
        // Reading configuration and initializing services and repositories
        Initialize();
        
        
        // Uploading csv to yva api for it to start processing users
        var importSuccess = await _csvImportService.ImportCsvAsync();
        if (importSuccess)
            Console.WriteLine("Csv successfully uploaded to yva via integration api!");
        else
        {
            Console.WriteLine("Failed to upload csv to yva.");
            return 1;
        }

        // Wait for import status.
        // It will return ValidationFail/Finished or null in case of error.
        var importStatus = await _csvImportService.WaitForImportToEndAsync();
        switch (importStatus?.Status)
        {
            // Finished status means that there were no errors during file processing
            case CsvHandlerStatus.Finished:
                Console.WriteLine("Csv successfully applied to yva. Import is over. You may go check your users.");
                return 0;
            
            
            // In case of failed validation we can request errors explanation
            // which will return row number, reason and problem value
            case CsvHandlerStatus.ValidationFail:
            {
                Console.WriteLine("Errors were found during processing the file:");
                var errors = await _csvImportService.GetImportErrorsAsync();
                foreach (var error in errors)
                    Console.WriteLine($"Row number: {error.RowNumber}. Error code: {error.Code}. Problem value: {error.ProblemValue ?? "[is empty]"}");
            
                return 1;
            }
            default:
                Console.WriteLine("Failed to check status.");
                return 1;
        }
    }

    private static void Initialize()
    {
        // Reading configuration from appsettings.json file 
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        // Loading configuration from appsettings.json
        var yvaServiceSettings = config
            .GetRequiredSection(nameof(YvaServiceSettings))
            .Get<YvaServiceSettings>();
        var csvRepositorySettings = config.GetRequiredSection(nameof(CsvRepositorySettings))
            .Get<CsvRepositorySettings>();
        
        // Initializing instance of yva service adapter 
        var yvaServiceAdapter = new YvaServiceAdapter(yvaServiceSettings);
        // Initializing instance of csv source repository 
        var csvRepository = new CsvRepository();
        
        // Creating instance of Csv Importing service
        _csvImportService = new CsvImportService(yvaServiceAdapter, yvaServiceSettings, csvRepositorySettings, csvRepository);
    }

}