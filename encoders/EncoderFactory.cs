using Services;

namespace Encoders;

public class EncoderFactory: IDisposable {

    private FileManager fileManager { get; init; }

    public EncoderFactory(FileManager fileManager) => this.fileManager = fileManager;

    public async Task<Encoder> Create(EncoderName encoderName) {

        return new Encoder() {
            Name = encoderName,
            ExplicitNVocab = GetExplicitNVocabFor(encoderName),
            RegexPattern = GetRegexPatternFor(encoderName),
            SpecialTokens = GetSpecialTokensFor(encoderName),
            MergeableRanks = await GetMergeableTokensFor(encoderName)
        };
    }

    private int? GetExplicitNVocabFor(EncoderName encoderName) => encoderName switch {
        EncoderName.cl100k_base => null,
        EncoderName.p50k_edit => null,
        EncoderName.p50k_base => 50281,
        EncoderName.r50k_base => 50257,
        EncoderName.gpt2 => 50257,
        _ => throw new ArgumentException("Unsupported model")
    };

    private string GetRegexPatternFor(EncoderName encoderName) => encoderName switch {
        EncoderName.cl100k_base => @"(?i:'s|'t|'re|'ve|'m|'ll|'d)|[^\r\n\p{L}\p{N}]?\p{L}+|\p{N}{1,3}| ?[^\s\p{L}\p{N}]+[\r\n]*|\s*[\r\n]+|\s+(?!\S)|\s+",
        EncoderName.p50k_edit => "",
        EncoderName.p50k_base => "",
        EncoderName.r50k_base => "",
        EncoderName.gpt2 => "",
        _ => throw new ArgumentException("Unsupported model")
    };

    private Dictionary<string, int> GetSpecialTokensFor(EncoderName encoderName) => encoderName switch {
        EncoderName.cl100k_base => new Dictionary<string, int> {
            { SpecialToken.EndOfText.GetText(), 100257 },
            { SpecialToken.FimPrefix.GetText(), 100258 },
            { SpecialToken.FimMiddle.GetText(), 100259 },
            { SpecialToken.FimSuffix.GetText(), 100260 },
            { SpecialToken.EndOfPrompt.GetText(), 100276 },
        },
        EncoderName.p50k_edit => new Dictionary<string, int> {
            { SpecialToken.EndOfText.GetText(), 50256 },
            { SpecialToken.FimPrefix.GetText(), 50281 },
            { SpecialToken.FimMiddle.GetText(), 50282 },
            { SpecialToken.FimSuffix.GetText(), 50283 },
        },
        EncoderName.p50k_base => new Dictionary<string, int> {
            { SpecialToken.EndOfText.GetText(), 50256 }
        },
        EncoderName.r50k_base => new Dictionary<string, int> {
            { SpecialToken.EndOfText.GetText(), 50256 }
        },
        EncoderName.gpt2 => new Dictionary<string, int> {
            { SpecialToken.EndOfText.GetText(), 50256 }
        },
        _ => throw new ArgumentException("Unsupported model")
    };

    private async Task<Dictionary<byte[], int>> GetMergeableTokensFor(EncoderName encoderName) => encoderName switch {
        EncoderName.cl100k_base => await fileManager.loadTokens(
            new Uri("https://openaipublic.blob.core.windows.net/encodings/cl100k_base.tiktoken"),
            $"{encoderName}"
        ),
        EncoderName.p50k_edit => await fileManager.loadTokens(
            new Uri("https://openaipublic.blob.core.windows.net/encodings/p50k_base.tiktoken"),
            $"{encoderName}"
        ),
        EncoderName.p50k_base => await fileManager.loadTokens(
            new Uri("https://openaipublic.blob.core.windows.net/encodings/p50k_base.tiktoken"),
            $"{encoderName}"
        ),
        EncoderName.r50k_base => await fileManager.loadTokens(
            new Uri("https://openaipublic.blob.core.windows.net/encodings/r50k_base.tiktoken"),
            $"{encoderName}"
        ),
        // TODO: missing GPT2
        _ => throw new ArgumentException("Unsupported model")
    };

    public void Dispose() => fileManager.Dispose();
}
