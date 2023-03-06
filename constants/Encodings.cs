namespace Encoding;

public class Utils {
    public static string EncodingFor(string model) => model switch {
        // chat
        string x when x.StartsWith("gpt-3.5-turbo-")
            || x == "gpt-3.5-turbo" => "cl100k_base",

        // text
        "text-davinci-003" => "p50k_base",
        "text-davinci-002" => "p50k_base",
        "text-davinci-001" => "r50k_base",
        "text-curie-001" => "r50k_base",
        "text-babbage-001" => "r50k_base",
        "text-ada-001" => "r50k_base",
        "davinci" => "r50k_base",
        "curie" => "r50k_base",
        "babbage" => "r50k_base",
        "ada" => "r50k_base",

        // code
        "code-davinci-002" => "p50k_base",
        "code-davinci-001" => "p50k_base",
        "code-cushman-002" => "p50k_base",
        "code-cushman-001" => "p50k_base",
        "davinci-codex" => "p50k_base",
        "cushman-codex" => "p50k_base",

        // edit
        "text-davinci-edit-001" => "p50k_edit",
        "code-davinci-edit-001" => "p50k_edit",

        // embeddings
        "text-embedding-ada-002" => "cl100k_base",

        // old embeddings
        "text-similarity-davinci-001" => "r50k_base",
        "text-similarity-curie-001" => "r50k_base",
        "text-similarity-babbage-001" => "r50k_base",
        "text-similarity-ada-001" => "r50k_base",
        "text-search-davinci-doc-001" => "r50k_base",
        "text-search-curie-doc-001" => "r50k_base",
        "text-search-babbage-doc-001" => "r50k_base",
        "text-search-ada-doc-001" => "r50k_base",
        "code-search-babbage-code-001" => "r50k_base",
        "code-search-ada-code-001" => "r50k_base",

        // open source
        "gpt2" => "gpt2",

        _ => throw new ArgumentException("Unsupported model")
    };
}
