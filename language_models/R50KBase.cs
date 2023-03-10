namespace LanguageModel;

using Services;

public sealed class R50KBase
{

    public int? ExplicitNVocab => null;

    public string RegexPattern => "";

    public Dictionary<string, int> SpecialTokens =>
    new Dictionary<string, int> {
        { SpecialToken.EndOfText.GetText(), 50256 }
    };

    public Uri BpeFileLocation =>
    new Uri("https://openaipublic.blob.core.windows.net/encodings/p50k_base.tiktoken");

    public Dictionary<string, int> MergeableRanks {get; private set;}

    // public static async Task<R50KBase> Create(FileManager fileManager) {
    //     var caca = await fileManager.loadTokens(languageModel);
    //     var languageModel = new R50KBase(caca);
    //     languageModel.MergeableRanks = ;
    //     return languageModel;
    // }
}
