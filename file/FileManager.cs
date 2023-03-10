using LanguageModel;

namespace Services;

public class FileManager {

    private HttpClient httpClient { get; init; }

    public FileManager() {
        httpClient = new HttpClient();
    }

    public async Task<Dictionary<string, int>> loadTokens(ILanguageModel languageModel) {
        var fileName = $"{languageModel.Name}.bpe";
        var filePath = BuildFileName(fileName);

        var output = new Dictionary<string, int>();
        foreach(string line in File.ReadLines(filePath)) {
            var result = line.Split(" ");
            output.Add(result[0], int.Parse(result[1]));
        }
        return output;
    }
    public async Task<FileStream> LoadBpeFile(ILanguageModel languageModel) {
        var fileName = $"{languageModel.Name}.bpe";
        var filePath = BuildFileName(fileName);
        var fileExists = File.Exists(filePath);
        return !fileExists ?
            await FetchFile(languageModel.BpeFileLocation, fileName) :
            File.Open(filePath, FileMode.Open);
    }

    private async Task<FileStream> FetchFile(Uri uri, string fileName) {
        var response = await httpClient.GetAsync(uri);
        using (FileStream fileStream = GetFileHandle(fileName)) {
            await response.Content.CopyToAsync(fileStream);
            return fileStream;
        }
    }

    private FileStream GetFileHandle(string fileName) {
        System.IO.Directory.CreateDirectory(REPORT_ROOT_FOLDER);
        return File.Create(BuildFileName(fileName));
    }

    private string BuildFileName(string fileName) =>
        $"{REPORT_ROOT_FOLDER}/{fileName}";

    private static string REPORT_ROOT_FOLDER = ".bpe_files";
}


