using System.Net;
using System.IO;
using System;

public class FileManager {

    private HttpClient _httpClient { get;init; }

    public FileManager() {
        _httpClient = new HttpClient();
    }

    public async Task fetchFile(Uri uri) {
        var response = await _httpClient.GetAsync(uri);
        using (FileStream fs = File.Create("")) {
            await response.Content.CopyToAsync(fs);
        }
    }
}
