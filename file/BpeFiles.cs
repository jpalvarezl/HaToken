namespace Services;

public readonly struct BpeFiles {
    public BpeFiles(string VocabFile, string? MergeFile)
    {
        this.VocabFile = VocabFile;
        this.MergeFile = MergeFile;
    }

    public string VocabFile { get; init; }

    public string? MergeFile { get; init; }

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

        // Not sure but it seems that the tiktoken repo named the files in the opposite they should
        // At least according to what can be found in this repo:
        // https://huggingface.co/gpt2/tree/main
        return new BpeFiles(mergeFile.Name, vocabFile.Name);
    }
}
