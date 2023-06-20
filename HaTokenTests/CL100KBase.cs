using Encoders;
using Xunit;
using static HaTokenTests.TestUtils;

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

    [Theory]
    [MemberData(nameof(GetCL100KBaseTestData))]
    public async Task TestEncoder(TestDataRow row)
    {
        List<int> actual = await NonAzureEncoder.Encode(row.text, Utils.EncodingFor("gpt-3.5-turbo-"));
        List<int> expected = row.tokens;

        Assert.True(expected.SequenceEqual(actual));
    }

    public static IEnumerable<object[]> GetCL100KBaseTestData()
    {
        return GetEncoderTestData("cl100k_base", new RunTop(1));
    }
}
