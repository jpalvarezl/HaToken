using LanguageModel;

namespace Services;

public class FileManager {

    private HttpClient httpClient { get; init; }

    public FileManager() {
        httpClient = new HttpClient();
    }

    public async Task LoadBpeFile(ILanguageModel languageModel) {
        var fileName = $"{languageModel.Name}.bpe";
        var filePath = BuildFileName(fileName);
        var fileExists = File.Exists(filePath);
        if (!fileExists) {
            await FetchFile(languageModel.BpeFileLocation, fileName);
        }
    }

    private async Task FetchFile(Uri uri, string fileName) {
        var response = await httpClient.GetAsync(uri);
        using (FileStream fs = GetFileHandle(fileName)) {
            await response.Content.CopyToAsync(fs);
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


