// Add using statements
using Microsoft.ML;
using Microsoft.ML.Tokenizers;
using Services;
using Encoders;

internal class Program {

    public static async Task Main(string[] args) {
        // Initialize MLContext
        // var mlContext = new MLContext();

        using var fileManager = new FileManager();
        using var bpeFactory = new BpeFactory(fileManager);

        var bpe = await bpeFactory.GetBpe(EncoderName.gpt2);

        // Initialize Tokenizer
        var tokenizer = new Tokenizer(bpe, RobertaPreTokenizer.Instance);

        // Define input for tokenization
        var input = "Hello world";

        // Encode input
        var tokenizerEncodedResult = tokenizer.Encode(input);


        foreach(var tokenId in tokenizerEncodedResult.Ids) {
            Console.WriteLine(tokenId);
        }
        // Decode results
        tokenizer.Decode(tokenizerEncodedResult.Ids);
    }
}
