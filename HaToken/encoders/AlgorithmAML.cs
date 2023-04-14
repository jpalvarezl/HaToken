using Microsoft.ML;
using Microsoft.ML.Tokenizers;
using Services;

namespace Encoders.AML;

public static class AzureMLEncoder {

    public static async Task<IReadOnlyList<int>> Encode(string text, EncoderName encoderName) {

                // Initialize MLContext
        // var mlContext = new MLContext();

        using var fileManager = new FileManager();
        using var bpeFactory = new BpeFactory(fileManager);

        var bpe = await bpeFactory.GetBpe(encoderName);

        // Initialize Tokenizer
        var tokenizer = new Tokenizer(bpe, RobertaPreTokenizer.Instance);

        // Define input for tokenization
        var input = "Hello world";

        // Encode input
        var tokenizerEncodedResult = tokenizer.Encode(input);


        return tokenizerEncodedResult.Ids;
        // foreach(var tokenId in tokenizerEncodedResult.Ids) {
        //     Console.WriteLine(tokenId);
        // }
        // Decode results
        // tokenizer.Decode(tokenizerEncodedResult.Ids);
    }
}
