using Services;

namespace LanguageModel;

public class LanguageModelFactory: IDisposable {

    private FileManager fileManager { get; init; }

    public LanguageModelFactory(FileManager fileManager) => this.fileManager = fileManager;

    public async Task<LanguageModel> Create(ModelName modelName) {

        return new LanguageModel() {
            Name = modelName,
            ExplicitNVocab = GetExplicitNVocabFor(modelName),
            RegexPattern = GetRegexPatternFor(modelName),
            SpecialTokens = GetSpecialTokensFor(modelName),
            MergeableRanks = await GetMergeableTokensFor(modelName)
        };
    }

    private int? GetExplicitNVocabFor(ModelName modelName) => modelName switch {
        ModelName.cl100k_base => null,
        ModelName.p50k_edit => null,
        ModelName.p50k_base => 50281,
        ModelName.r50k_base => 50257,
        ModelName.gpt2 => 50257,
        _ => throw new ArgumentException("Unsupported model")
    };

    private string GetRegexPatternFor(ModelName modelName) => modelName switch {
        ModelName.cl100k_base => "",
        ModelName.p50k_edit => "",
        ModelName.p50k_base => "",
        ModelName.r50k_base => "",
        ModelName.gpt2 => "",
        _ => throw new ArgumentException("Unsupported model")
    };

    private Dictionary<string, int> GetSpecialTokensFor(ModelName modelName) => modelName switch {
        ModelName.cl100k_base => new Dictionary<string, int> {
            { SpecialToken.EndOfText.GetText(), 100257 },
            { SpecialToken.FimPrefix.GetText(), 100258 },
            { SpecialToken.FimMiddle.GetText(), 100259 },
            { SpecialToken.FimSuffix.GetText(), 100260 },
            { SpecialToken.EndOfPrompt.GetText(), 100276 },
        },
        ModelName.p50k_edit => new Dictionary<string, int> {
            { SpecialToken.EndOfText.GetText(), 50256 },
            { SpecialToken.FimPrefix.GetText(), 50281 },
            { SpecialToken.FimMiddle.GetText(), 50282 },
            { SpecialToken.FimSuffix.GetText(), 50283 },
        },
        ModelName.p50k_base => new Dictionary<string, int> {
            { SpecialToken.EndOfText.GetText(), 50256 }
        },
        ModelName.r50k_base => new Dictionary<string, int> {
            { SpecialToken.EndOfText.GetText(), 50256 }
        },
        ModelName.gpt2 => new Dictionary<string, int> {
            { SpecialToken.EndOfText.GetText(), 50256 }
        },
        _ => throw new ArgumentException("Unsupported model")
    };

    private async Task<Dictionary<string, int>> GetMergeableTokensFor(ModelName modelName) => modelName switch {
        ModelName.cl100k_base => await fileManager.loadTokens(
            new Uri("https://openaipublic.blob.core.windows.net/encodings/cl100k_base.tiktoken"),
            $"{modelName}"
        ),
        ModelName.p50k_edit => await fileManager.loadTokens(
            new Uri("https://openaipublic.blob.core.windows.net/encodings/p50k_base.tiktoken"),
            $"{modelName}"
        ),
        ModelName.p50k_base => await fileManager.loadTokens(
            new Uri("https://openaipublic.blob.core.windows.net/encodings/p50k_base.tiktoken"),
            $"{modelName}"
        ),
        ModelName.r50k_base => await fileManager.loadTokens(
            new Uri("https://openaipublic.blob.core.windows.net/encodings/r50k_base.tiktoken"),
            $"{modelName}"
        ),
        // TODO: missing GPT2
        _ => throw new ArgumentException("Unsupported model")
    };

    public void Dispose() => fileManager.Dispose();
}
