using System.Reflection;
using Newtonsoft.Json;

namespace HaTokenTests;

public static class TestUtils
{

    public static IEnumerable<object[]> GetEncoderTestData(
        string encoderName, TestCount? testCount = null)
    {
        // Why this name is like this?
        var testData = LoadJson($"HaTokenTests.test_data.{encoderName}.json");

        var actualTestCount = testCount ?? new RunAll();

        var runnableTests = actualTestCount switch
        {
            RunAll _ => testData,
            RunTop top => testData.Take(top.GetCount),
            _ => throw new NotImplementedException()
        };

        foreach (var row in runnableTests)
        {
            yield return new object[] { row };
        }
    }

    private static List<TestDataRow> LoadJson(string resourceName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        using (var stream = assembly.GetManifestResourceStream(resourceName))
        using (var reader = new StreamReader(stream!))
        using (var jsonReader = new JsonTextReader(reader))
        {
            var serializer = new JsonSerializer();
            return serializer.Deserialize<List<TestDataRow>>(jsonReader)!;
        }
    }

    public class TestDataRow
    {
        public string text { get; set; }
        public List<int> tokens { get; set; }
    }

    public abstract class TestCount { }

    public sealed class RunTop : TestCount
    {
        private int _count { get; }

        public int GetCount => _count;

        public RunTop(int count)
        {
            _count = count;
        }
    }

    public sealed class RunAll : TestCount { }
}
