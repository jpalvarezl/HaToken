namespace LanguageModel;

public enum ModelNames {
    gpt2,
    r50k_base,
    p50k_base,
    p50k_edit,
    cl100k_base,
}

public enum SpecialToken {
    EndOfText,
    FimPrefix,
    FimMiddle,
    FimSuffix,
    EndOfPrompt
}

internal static class Extensions {

    public static string GetText(this SpecialToken tokenKey) => tokenKey switch {
        SpecialToken.EndOfText => "<|endoftext|>",
        SpecialToken.FimPrefix => "<|fim_prefix|>",
        SpecialToken.FimMiddle => "<|fim_middle|>",
        SpecialToken.FimSuffix => "<|fim_suffix|>",
        SpecialToken.EndOfPrompt => "<|endofprompt|>",
        _ => throw new ArgumentException("Special token not supported")
    };
}

public interface ILanguageModel {

    ModelNames Name { get; }

    int? ExplicitNVocab { get; }

    string RegexPattern { get; }

    Dictionary<string, int> MergeableRanks { get; }

    Dictionary<string, int> SpecialTokens { get; }
}
