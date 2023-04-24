
using Encoders;

namespace HaTokenTests;

[TestClass]
public class P50KBase
{
    [TestMethod]
    public async Task HelloWorld()
    {
        List<int> actual= await NonAzureEncoder.Encode("Hello world", Utils.EncodingFor("text-davinci-003"));
        List<int> expected = new List<int>(){ 15496, 995};

        Assert.IsTrue(expected.SequenceEqual(actual));
    }
}
