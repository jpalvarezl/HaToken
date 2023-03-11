using LanguageModel;
using Services;
using Encoders;

internal class Program {

    public static async Task Main(string[] args) {
        using var fileManager = new FileManager();
        using var encoderFactory = new EncoderFactory(fileManager);

        Encoder model = await encoderFactory.Create(Utils.EncodingFor("gpt-3.5-turbo"));
    }
}
