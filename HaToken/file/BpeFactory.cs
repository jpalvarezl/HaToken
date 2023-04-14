using Encoders;
using Microsoft.ML.Tokenizers;

namespace Services;

public class BpeFactory: IDisposable {

    private FileManager fileManager { get; init; }

    public BpeFactory(FileManager fileManager) => this.fileManager = fileManager;

    public async Task<Bpe> GetBpe(EncoderName encoderName) {
        var bpeFile = await FetchFiles(encoderName);
        return new Bpe(bpeFile.VocabFile, bpeFile.MergeFile);
    }

    private async Task<BpeFiles> FetchFiles(EncoderName encoderName) {
        var filePaths = encoderName switch {
            EncoderName.cl100k_base => await PathsFromDisk(encoderName, "https://openaipublic.blob.core.windows.net/encodings/cl100k_base.tiktoken"),
            EncoderName.p50k_edit => await PathsFromDisk(encoderName, "https://openaipublic.blob.core.windows.net/encodings/p50k_base.tiktoken"),
            EncoderName.p50k_base => await PathsFromDisk(encoderName, "https://openaipublic.blob.core.windows.net/encodings/p50k_base.tiktoken"),
            EncoderName.r50k_base => await PathsFromDisk(encoderName, "https://openaipublic.blob.core.windows.net/encodings/r50k_base.tiktoken"),
            EncoderName.gpt2 => await PathsFromDisk(
                encoderName,
                "https://openaipublic.blob.core.windows.net/gpt-2/encodings/main/encoder.json",
                "https://openaipublic.blob.core.windows.net/gpt-2/encodings/main/vocab.bpe"),
            _ => throw new ArgumentException("Unsupported model")
        };

        return new BpeFiles(filePaths.Item1, filePaths.Item2);
    }

    private async Task<(string, string?)> PathsFromDisk(
        EncoderName encoderName,
        string uriVocabFile,
        string? uriMergeFile = null
        ) {
        var vocabFileName = $"{encoderName}_vocab.bpe";

        using var vocabFile = await fileManager.LoadBpeFile(
                new Uri(uriVocabFile),
                vocabFileName
            );

        if(uriMergeFile != null) {
            var mergeFileName = $"{encoderName}_encoder.json";

            using var mergeFile = await fileManager.LoadBpeFile(
                new Uri(uriMergeFile),
                mergeFileName
            );

            return (vocabFile.Name, mergeFile.Name);
        }

        return (vocabFile.Name, null);
    }

    public void Dispose() => fileManager.Dispose();
}
