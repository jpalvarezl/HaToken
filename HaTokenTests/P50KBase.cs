
using Encoders;
using Xunit;
using static HaTokenTests.TestUtils;

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

    [Theory]
    [MemberData(nameof(GetP50KBaseTestData))]
    public async Task TestEncoder(TestDataRow row)
    {
        List<int> actual = await NonAzureEncoder.Encode(row.text, Utils.EncodingFor("text-davinci-003"));
        List<int> expected = row.tokens;

        Assert.True(expected.SequenceEqual(actual));
    }

    public static IEnumerable<object[]> GetP50KBaseTestData()
    {
        return GetEncoderTestData("p50k_base");
    }
}
