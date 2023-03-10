using LanguageModel;
using Services;

internal class Program {

    public static async Task Main(string[] args) {
        using var fileManager = new FileManager();
        using var languageModelFactory = new LanguageModelFactory(fileManager);

        var model = await languageModelFactory.Create(ModelName.r50k_base);
        foreach(var item in model.MergeableRanks) {
            Console.WriteLine($"{item.Key} {item.Value}");
        }
    }
}
