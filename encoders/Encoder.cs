namespace Encoders;

public enum EncoderName {
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

internal static partial class Extensions {

    public static string GetText(this SpecialToken tokenKey) => tokenKey switch {
        SpecialToken.EndOfText => "<|endoftext|>",
        SpecialToken.FimPrefix => "<|fim_prefix|>",
        SpecialToken.FimMiddle => "<|fim_middle|>",
        SpecialToken.FimSuffix => "<|fim_suffix|>",
        SpecialToken.EndOfPrompt => "<|endofprompt|>",
        _ => throw new ArgumentException("Special token not supported")
    };
}

public struct Encoder {

    public EncoderName Name { get; init; }

    public int? ExplicitNVocab { get; init; }

    public string RegexPattern { get; init; }

    public Dictionary<string, int> MergeableRanks { get; init; }

    public Dictionary<string, int> SpecialTokens { get; init; }
}
