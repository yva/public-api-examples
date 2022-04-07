using System.IO;

namespace Yva.UsersCsvImport.Services;

// Some csv source repository, like file exporter or db connector
public class CsvRepository
{
    // Lets just read file from our drive
    public Stream ExportCsvAsStream(string filePath)
    {
        return File.OpenRead(filePath);
    } 
}