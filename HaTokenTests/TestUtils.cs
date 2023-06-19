using Newtonsoft.Json;

namespace HaTokenTests;

public static class TestUtils
{

    public static IEnumerable<object[]> GetEncoderTestData(string encoderName)
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "test_data", $"{encoderName}.json");
        var json = File.ReadAllText(filePath);
        var testData = JsonConvert.DeserializeObject<TestData>(json);

        foreach (var row in testData.rows)
        {
            yield return new object[] { row };
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
