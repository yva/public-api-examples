using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Yva.UsersCsvImport.Dto.Yva;
using Yva.UsersCsvImport.Extensions;

namespace Yva.UsersCsvImport.Services;

public class YvaServiceAdapter
{
    private readonly YvaServiceSettings _yvaSettings;
    private readonly HttpClient _yvaHttpClient;
    private readonly JsonSerializer _serializer = new ();
    
    public YvaServiceAdapter(YvaServiceSettings yvaSettings)
    {
        _yvaSettings = yvaSettings;
        _yvaHttpClient = new HttpClient
        {
            BaseAddress = _yvaSettings.ApiAddress,
            DefaultRequestHeaders = { { _yvaSettings.AuthorizationHeaderName, _yvaSettings.TeamsApiToken } }
        };
    }

    public async Task<bool> TryUploadCsvToYvaAsync(Stream csvBytesStream)
    {
        using var httpRequest = new HttpRequestMessage
        {
            Content = new StreamContent(csvBytesStream),
            Method = HttpMethod.Post,
            RequestUri = _yvaSettings.ImportCsvEndpoint
        };

        using var yvaResponse = await _yvaHttpClient.SendAsync(httpRequest);
        if (yvaResponse.IsSuccessStatusCode)
            return true;
        
        Console.WriteLine("Error during the upload request.");
        var badResponse = _serializer.Deserialize<ErrorResponse>(await yvaResponse.Content.ReadAsStreamAsync());
        ListYvaErrorsToConsole(badResponse);
        
        return false;
    }

    public async Task<CsvProgressStatus> GetImportStatusAsync()
    {
        using var yvaResponse = await _yvaHttpClient.GetAsync(_yvaSettings.GetImportStatusEndpoint);
        if (yvaResponse.IsSuccessStatusCode)
            return _serializer.Deserialize<CsvProgressStatus>(await yvaResponse.Content.ReadAsStreamAsync());

        if (yvaResponse.StatusCode == HttpStatusCode.NoContent)
        {
            Console.WriteLine("No import process has been initialized. First you should call [POST] teams/users/import" +
                              "with csv body.");
            return default;
        }

        Console.WriteLine("Unexpected Yva api response.");
        return default;
    }

    public async Task<CsvImportError[]> GetImportErrorsAsync(int take, int skip)
    {
        using var yvaResponse = await _yvaHttpClient
            .GetAsync($"{_yvaSettings.GetImportErrorsEndpoint}?skip={skip}&take={take}");
        if (yvaResponse.IsSuccessStatusCode)
            return _serializer.Deserialize<CsvImportError[]>(await yvaResponse.Content.ReadAsStreamAsync());

        if (yvaResponse.StatusCode == HttpStatusCode.NoContent)
        {
            Console.WriteLine("No errors were found. Either no import was initialized or no errors were found in csv.");
            return Array.Empty<CsvImportError>();
        }

        if (!yvaResponse.IsSuccessStatusCode)
        {
            Console.WriteLine("Error during get import errors request.");
            var badResponse = _serializer.Deserialize<ErrorResponse>(await yvaResponse.Content.ReadAsStreamAsync());
            ListYvaErrorsToConsole(badResponse);

            return default;
        }

        Console.WriteLine("Failed to get errors. Unexpected response.");
        return default;
    }

    private static void ListYvaErrorsToConsole(ErrorResponse badResponse)
    {
        if (badResponse == null) 
            return;
        
        Console.WriteLine($"Yva responded with status code: {badResponse.Status} - {badResponse.Title}");
        foreach (var error in badResponse.Errors)
            Console.WriteLine($"ERROR CODE: {error.Code}. {error.Parameter} {error.Message}");
    }
}