using System.Text.RegularExpressions;
using Services;

namespace Encoders;

internal static partial class Extensions {

    public static List<int> Encode(this Encoder encoder, string text) {

        Regex regex = new Regex(encoder.RegexPattern);

        var output = new List<int>();
        foreach (Match match in regex.Matches(text)) {
            byte[] matchedKey = System.Text.Encoding.UTF8.GetBytes(match.ToString());

            int value;
            if(encoder.MergeableRanks.TryGetValue(matchedKey, out value)) {
                output.Append(value);
            } else {
                output.AddRange(BytePairEncode(matchedKey, encoder.MergeableRanks));
            }
        }

        return output;
    }

    private static List<int> BytePairEncode(byte[] piece, Dictionary<byte[], int> ranks) {
        if (piece.Length == 1) {
            return new List<int> { ranks[piece] };
        }
        return BytePairMerge(piece, ranks, (range) => ranks[piece[range]]);
    }

    private static List<int> BytePairMerge(
        byte[] piece,
        Dictionary<byte[], int> ranks,
        Func<Range, int> f) {
        return new List<int>();
    }
}
