using LanguageModel;
using Services;

internal class Program {

    public static async Task Main(string[] args) {

        var languageModel = new R50KBase();
        var fileManager = new FileManager();

        var tokens = await fileManager.loadTokens(languageModel);

        foreach(var item in tokens) {
            Console.WriteLine($"{item.Key} {item.Value}");
        }
    }
}
