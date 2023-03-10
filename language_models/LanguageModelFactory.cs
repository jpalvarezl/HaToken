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
        ModelName.r50k_base => null,
        _ => throw new ArgumentException("Unsupported model")
    };

    private string GetRegexPatternFor(ModelName modelName) => modelName switch {
        ModelName.r50k_base => "",
        _ => throw new ArgumentException("Unsupported model")
    };

    private Dictionary<string, int> GetSpecialTokensFor(ModelName modelName) => modelName switch {
        ModelName.r50k_base => new Dictionary<string, int> {
            { SpecialToken.EndOfText.GetText(), 50256 }
        },
        _ => throw new ArgumentException("Unsupported model")
    };

    private async Task<Dictionary<string, int>> GetMergeableTokensFor(ModelName modelName) => modelName switch {
        ModelName.r50k_base => await fileManager.loadTokens(
            new Uri("https://openaipublic.blob.core.windows.net/encodings/p50k_base.tiktoken"),
            $"{modelName}"
        ),
        _ => throw new ArgumentException("Unsupported model")
    };

    public void Dispose() => fileManager.Dispose();
}
