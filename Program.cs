using Encoders;
using Encoders.AML;

internal class Program {

    public static async Task Main(string[] args) {
        // "text-davinci-003" is the encoder for GTP3
        // var tokenIds = await AzureMLEncoder.Encode("Hello world", Utils.EncodingFor("text-davinci-003"));
        var tokenIds = await NonAzureEncoder.Encode("Hello world", Utils.EncodingFor("text-davinci-003"));

        foreach(var tokenId in tokenIds) {
            Console.WriteLine(tokenId);
        }
    }
}
