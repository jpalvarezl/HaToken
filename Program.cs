using Encoders;
using Encoders.AML;

internal class Program {

    public static async Task Main(string[] args) {
        var tokenIds = await AzureMLEncoder.Encode("Hello world", EncoderName.gpt2);
        // var tokenIds = await NonAzureEncoder.Encode("Hello world", Utils.EncodingFor("text-davinci-003"));

        foreach(var tokenId in tokenIds) {
            Console.WriteLine(tokenId);
        }
    }
}
