using Encoders;

namespace LanguageModel;

internal static class Utils {
    public static EncoderName EncodingFor(string modelName) => modelName switch {
        // chat
        string x when x.StartsWith("gpt-3.5-turbo-")
            || x == "gpt-3.5-turbo" => EncoderName.cl100k_base,

        // text
        "text-davinci-003" => EncoderName.p50k_base,
        "text-davinci-002" => EncoderName.p50k_base,
        "text-davinci-001" => EncoderName.r50k_base,
        "text-curie-001" => EncoderName.r50k_base,
        "text-babbage-001" => EncoderName.r50k_base,
        "text-ada-001" => EncoderName.r50k_base,
        "davinci" => EncoderName.r50k_base,
        "curie" => EncoderName.r50k_base,
        "babbage" => EncoderName.r50k_base,
        "ada" => EncoderName.r50k_base,

        // code
        "code-davinci-002" => EncoderName.p50k_base,
        "code-davinci-001" => EncoderName.p50k_base,
        "code-cushman-002" => EncoderName.p50k_base,
        "code-cushman-001" => EncoderName.p50k_base,
        "davinci-codex" => EncoderName.p50k_base,
        "cushman-codex" => EncoderName.p50k_base,

        // edit
        "text-davinci-edit-001" => EncoderName.p50k_edit,
        "code-davinci-edit-001" => EncoderName.p50k_edit,

        // embeddings
        "text-embedding-ada-002" => EncoderName.cl100k_base,

        // old embeddings
        "text-similarity-davinci-001" => EncoderName.r50k_base,
        "text-similarity-curie-001" => EncoderName.r50k_base,
        "text-similarity-babbage-001" => EncoderName.r50k_base,
        "text-similarity-ada-001" => EncoderName.r50k_base,
        "text-search-davinci-doc-001" => EncoderName.r50k_base,
        "text-search-curie-doc-001" => EncoderName.r50k_base,
        "text-search-babbage-doc-001" => EncoderName.r50k_base,
        "text-search-ada-doc-001" => EncoderName.r50k_base,
        "code-search-babbage-code-001" => EncoderName.r50k_base,
        "code-search-ada-code-001" => EncoderName.r50k_base,

        // open source
        "gpt2" => EncoderName.gpt2,

        _ => throw new ArgumentException("Unsupported model")
    };
}
