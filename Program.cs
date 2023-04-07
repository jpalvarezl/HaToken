﻿// Add using statements
using Microsoft.ML;
using Microsoft.ML.Tokenizers;
using Services;


internal class Program {

    public static async Task Main(string[] args) {
        // Initialize MLContext
        // var mlContext = new MLContext();

        var bpeFile = await BpeFiles.initFromFileManager();

        // Initialize Tokenizer
        var tokenizer = new Tokenizer(new Bpe(bpeFile.VocabFile, bpeFile.MergeFile), RobertaPreTokenizer.Instance);

        // Define input for tokenization
        var input = "the brown fox jumped over the lazy dog!";

        // Encode input
        var tokenizerEncodedResult = tokenizer.Encode(input);

        // Decode results
        tokenizer.Decode(tokenizerEncodedResult.Ids);
    }

    private readonly struct BpeFiles {
        public BpeFiles(string VocabFile, string MergeFile)
        {
            this.VocabFile = VocabFile;
            this.MergeFile = MergeFile;
        }

        public string VocabFile { get; init; }
        public string MergeFile { get; init; }

        public static async Task<BpeFiles> initFromFileManager() {
            var vocabFileName = "vocab";
            var mergeFileName = "encoder";

            using var fileManager = new FileManager();
            using var vocabFile = await fileManager.LoadBpeFile(
                new Uri("https://openaipublic.blob.core.windows.net/gpt-2/encodings/main/vocab.bpe"),
                vocabFileName
            );
            using var mergeFile = await fileManager.LoadBpeFile(
                new Uri("https://openaipublic.blob.core.windows.net/gpt-2/encodings/main/encoder.json"),
                mergeFileName
            );

            return new BpeFiles(vocabFile.Name, mergeFile.Name);
        }
    }
}
