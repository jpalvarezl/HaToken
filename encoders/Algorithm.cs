using System.Text.RegularExpressions;

namespace Encoders;

internal static partial class Extensions {

    public static List<int> Encode(this Encoder encoder, string text) {

        Regex regex = new Regex(encoder.RegexPattern);

        var output = new List<int>();
        foreach (Match match in regex.Matches(text)) {
            string matchString = match.ToString();
            var a = encoder.MergeableRanks[matchString];
            output.Append(a);
        }

        return output;
    }et
    // }

    private static List<int> BytePairEncode(string piece, Dictionary<string, int> ranks) {
        if (piece.Length == 1) {
            return new List<int> {ranks[piece] };
        }
        return BytePairMerge(piece, ranks, (range) => ranks[piece.Substring(range.Start.Value, range.End.Value)]);
    }

    private static List<int> BytePairMerge(
        string piece,
        Dictionary<string, int> ranks,
        Func<Range, int> f) {
        // TODO
    }
}
