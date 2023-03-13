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

        var parts = new List<(int, int)>(Enumerable.Range(0, piece.Length - 1).Select(index => (index, int.MaxValue)));

        foreach (var index in Enumerable.Range(0, parts.Count -2)) {
            var rank = GetRank(parts, ranks, piece, index, 0);
            if(rank != null) {
                // this fells like it could be a dictionary
                parts[index] = (index, rank.Value);
            } else {
                continue;
            }
        }

        // while(true) {
        //     if(parts.Count == 1) {
        //         break;
        //     }


        // }

        return new List<int>();
    }

    private static int? GetRank(List<(int, int)> parts, Dictionary<byte[], int> ranks, byte[] piece, int startIndex, int skip) {
        if (startIndex + skip + 2 < parts.Count) {
            int rankValue;
            var rankKey = piece[parts[startIndex].Item1 .. parts[startIndex + skip + 2].Item1];

            if(ranks.TryGetValue(rankKey, out rankValue)) {
                return rankValue;
            }
        }
        return null;
    }
}
