namespace LanguageModel;

public sealed class R50KBase : ILanguageModel
{
    ModelNames ILanguageModel.Name => ModelNames.r50k_base;

    int? ILanguageModel.ExplicitNVocab => null;

    string ILanguageModel.RegexPattern => "";

    Dictionary<string, int> ILanguageModel.MergeableRanks =>
    new Dictionary<string, int> {
    };

    Dictionary<string, int> ILanguageModel.SpecialTokens =>
    new Dictionary<string, int> {
        { SpecialToken.EndOfText.GetText(), 50256 }
    };
}
