using Encoders;
using Xunit;

namespace HaTokenTests;

public class CL100KBase
{
    [Fact]
    public async Task HelloWorld()
    {
        List<int> actual = await NonAzureEncoder.Encode("Hello world", Utils.EncodingFor("gpt-3.5-turbo-"));
        List<int> expected = new List<int>() { 9906, 1917 };

        Assert.True(expected.SequenceEqual(actual));
    }
}
