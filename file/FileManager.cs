namespace Services;

public class FileManager : IDisposable {

    private HttpClient httpClient { get; init; }

    public FileManager() {
        httpClient = new HttpClient();
    }

    public async Task<Dictionary<byte[], int>> loadTokens(Uri uri, string languageModelName) {
        var fileStream = await LoadBpeFile(uri, languageModelName);

        using StreamReader streamReader = new StreamReader(fileStream);
        var output = new Dictionary<byte[], int>();
        string? line;
        while ((line = streamReader.ReadLine()) != null) {
            var result = line.Split(" ");
            output.Add(StringEncoder.BpeKeyDecode(result[0]), int.Parse(result[1]));
        }

        return output;
    }
    public async Task<FileStream> LoadBpeFile(Uri uri, string fileName) {
        var filePath = BuildFileName(fileName);
        var fileExists = File.Exists(filePath);
        return fileExists ?
            File.Open(filePath, FileMode.Open):
            await FetchFile(uri, fileName);
    }

    public string getFullPathOfFile(string fileName) {
        return BuildFileName(fileName);
    }

    private async Task<FileStream> FetchFile(Uri uri, string fileName) {
        var response = await httpClient.GetAsync(uri);
        FileStream fileStream = GetFileHandle(fileName);
        await response.Content.CopyToAsync(fileStream);
        fileStream.Seek(0, SeekOrigin.Begin);
        return fileStream;
    }

    private FileStream GetFileHandle(string fileName) {
        System.IO.Directory.CreateDirectory(REPORT_ROOT_FOLDER);
        return File.Create(BuildFileName(fileName));
    }

    private string BuildFileName(string fileName) => $"{REPORT_ROOT_FOLDER}/{fileName}";

    public void Dispose() => this.httpClient.Dispose();

    private static string REPORT_ROOT_FOLDER = ".bpe_files";
}


