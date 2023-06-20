using Services;

namespace Encoders.HF;

public static class HFAlgorithm
{

    public static async Task<List<int>> Encode(string text, EncoderName encoderName)
    {
        using var fileManager = new FileManager();
        using var encoderFactory = new EncoderFactory(fileManager);

        Encoder model = await encoderFactory.Create(encoderName);
        var encoded = model.Encode(text);

        return encoded;
    }

    private static List<int> Encode(this Encoder encoder, string text)
    {
        return new List<int>();
    }
}

