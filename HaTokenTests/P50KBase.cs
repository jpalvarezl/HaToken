
using Encoders;
using Xunit;

namespace HaTokenTests;

public class P50KBase
{
    [Fact]
    public async Task HelloWorld()
    {
        List<int> actual= await NonAzureEncoder.Encode("Hello world", Utils.EncodingFor("text-davinci-003"));
        List<int> expected = new List<int>(){ 15496, 995};

        Assert.True(expected.SequenceEqual(actual));
    }
}
