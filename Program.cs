using LanguageModel;
using Services;

internal class Program {

    public static async Task Main(string[] args) {

        var languageModel = new R50KBase();
        var fileManager = new FileManager();

        await fileManager.LoadBpeFile(languageModel);
        Console.WriteLine("Got the file!");
    }
}
