using LanguageModel;
using Services;
using Encoding;

internal class Program {

    public static async Task Main(string[] args) {
        using var fileManager = new FileManager();
        using var languageModelFactory = new LanguageModelFactory(fileManager);

        var model = await languageModelFactory.Create(Utils.EncodingFor("gpt-3.5-turbo"));
    }
}
