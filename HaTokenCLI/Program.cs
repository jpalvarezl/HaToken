using Encoders;

// "text-davinci-003" is the encoder for GTP3
// var tokenIds = await AzureMLEncoder.Encode("Hello world", Utils.EncodingFor("text-davinci-003"));
var tokenIds = await NonAzureEncoder.Encode("Hello world", Utils.EncodingFor("gpt-3.5-turbo-"));

Console.WriteLine(string.Join(", ", tokenIds));
