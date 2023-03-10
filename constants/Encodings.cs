using LanguageModel;

namespace Encoding;

internal static class Utils {
    public static ModelName EncodingFor(string model) => model switch {
        // chat
        string x when x.StartsWith("gpt-3.5-turbo-")
            || x == "gpt-3.5-turbo" => ModelName.cl100k_base,

        // text
        "text-davinci-003" => ModelName.p50k_base,
        "text-davinci-002" => ModelName.p50k_base,
        "text-davinci-001" => ModelName.r50k_base,
        "text-curie-001" => ModelName.r50k_base,
        "text-babbage-001" => ModelName.r50k_base,
        "text-ada-001" => ModelName.r50k_base,
        "davinci" => ModelName.r50k_base,
        "curie" => ModelName.r50k_base,
        "babbage" => ModelName.r50k_base,
        "ada" => ModelName.r50k_base,

        // code
        "code-davinci-002" => ModelName.p50k_base,
        "code-davinci-001" => ModelName.p50k_base,
        "code-cushman-002" => ModelName.p50k_base,
        "code-cushman-001" => ModelName.p50k_base,
        "davinci-codex" => ModelName.p50k_base,
        "cushman-codex" => ModelName.p50k_base,

        // edit
        "text-davinci-edit-001" => ModelName.p50k_edit,
        "code-davinci-edit-001" => ModelName.p50k_edit,

        // embeddings
        "text-embedding-ada-002" => ModelName.cl100k_base,

        // old embeddings
        "text-similarity-davinci-001" => ModelName.r50k_base,
        "text-similarity-curie-001" => ModelName.r50k_base,
        "text-similarity-babbage-001" => ModelName.r50k_base,
        "text-similarity-ada-001" => ModelName.r50k_base,
        "text-search-davinci-doc-001" => ModelName.r50k_base,
        "text-search-curie-doc-001" => ModelName.r50k_base,
        "text-search-babbage-doc-001" => ModelName.r50k_base,
        "text-search-ada-doc-001" => ModelName.r50k_base,
        "code-search-babbage-code-001" => ModelName.r50k_base,
        "code-search-ada-code-001" => ModelName.r50k_base,

        // open source
        "gpt2" => ModelName.gpt2,

        _ => throw new ArgumentException("Unsupported model")
    };
}
