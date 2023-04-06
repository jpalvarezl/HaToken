// Add using statements
using Microsoft.ML;
using Microsoft.ML.Tokenizers;
using Services;


internal class Program {

    public static async Task Main(string[] args) {
        // Initialize MLContext
        // var mlContext = new MLContext();

        var vocabFileName = "vocab";
        var mergeFileName = "encoder";

        using var fileManager = new FileManager();
        var vocabFile = await fileManager.LoadBpeFile(
            new Uri("https://openaipublic.blob.core.windows.net/gpt-2/encodings/main/vocab.bpe"),
            vocabFileName
        );
        var mergeFile = await fileManager.LoadBpeFile(
            new Uri("https://openaipublic.blob.core.windows.net/gpt-2/encodings/main/encoder.json"),
            mergeFileName
        );

        fileManager.Dispose();
        // Initialize Tokenizer
        var tokenizer = new Tokenizer(new Bpe(vocabFile.Name, mergeFile.Name),RobertaPreTokenizer.Instance);

        // Define input for tokenization
        var input = "the brown fox jumped over the lazy dog!";

        // Encode input
        var tokenizerEncodedResult = tokenizer.Encode(input);

        // Decode results
        tokenizer.Decode(tokenizerEncodedResult.Ids);
    }
}
