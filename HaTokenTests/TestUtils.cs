using System.Reflection;
using Newtonsoft.Json;

namespace HaTokenTests;

public static class TestUtils
{

    public static IEnumerable<object[]> GetEncoderTestData(string encoderName)
    {
        // Why this name is like this?
        var testData = LoadJson($"HaTokenTests.test_data.{encoderName}.json");

        foreach (var row in testData)
        {
            yield return new object[] { row };
        }
    }

    private static List<TestDataRow> LoadJson(string resourceName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        using (var stream = assembly.GetManifestResourceStream(resourceName))
        using (var reader = new StreamReader(stream))
        using (var jsonReader = new JsonTextReader(reader))
        {
            var serializer = new JsonSerializer();
            return serializer.Deserialize<List<TestDataRow>>(jsonReader);
        }
    }

    public class TestData
    {
        public List<TestDataRow> rows { get; set; }
    }

    public class TestDataRow
    {
        public string text { get; set; }
        public List<int> tokens { get; set; }
    }
}
