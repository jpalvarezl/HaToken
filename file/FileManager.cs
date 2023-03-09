public class FileManager {

    private HttpClient httpClient { get; init; }

    public FileManager() {
        httpClient = new HttpClient();
    }

    public async Task LoadBpeFile(string fileName) {
    }

    private async Task FetchFile(Uri uri, string fileName) {
        var response = await httpClient.GetAsync(uri);
        using (FileStream fs = File.Create(fileName)) {
            await response.Content.CopyToAsync(fs);
        }
    }
}


