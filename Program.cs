using LanguageModel;
using Services;
using Encoders;

internal class Program {

    public static async Task Main(string[] args) {
        using var fileManager = new FileManager();
        using var encoderFactory = new EncoderFactory(fileManager);

        Encoder model = await encoderFactory.Create(Utils.EncodingFor("gpt-3.5-turbo"));


        // Result we expect is [9906, 1917]
        // Using model cl100k_base
        var encoded = model.Encode("Hello world");

        foreach (int token in encoded) {
            Console.WriteLine($"{token}");
        }
    }
}
