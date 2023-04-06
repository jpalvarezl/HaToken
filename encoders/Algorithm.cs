using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Encoders;

internal static partial class Extensions {

    public static List<int> Encode(this Encoder encoder, string text) {

        Regex regex = new Regex(encoder.RegexPattern);

        var output = new List<int>();
        foreach (Match match in regex.Matches(text)) {
            Console.WriteLine($"Matched text: {match.ToString()}");
            byte[] matchEncoded = System.Text.Encoding.UTF8.GetBytes(match.ToString());

            foreach(var byteValue in matchEncoded) {
                Console.WriteLine($"Matched Encoded roundtrip: {byteValue}");
            }

            int value;
            if(encoder.MergeableRanks.TryGetValue(matchEncoded, out value)) {
                output.Prepend(value);
            } else {
                output.AddRange(BytePairEncode(matchEncoded, encoder.MergeableRanks));
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

        while(true) {
            if(parts.Count == 1) {
                break;
            }

            var minRank = (int.MaxValue, 0); // rank, index

            for(int i = 0; i < parts.Count; i++) {
                if(parts[i].Item2 < minRank.Item1) {
                    minRank = (parts[i].Item2, i);
                }
            }

            if(minRank.Item1 != int.MaxValue) {
                int i = minRank.Item2;

                parts[i] = (i, GetRank(parts, ranks, piece, i, 1) ?? int.MaxValue);

                if(i > 0) {
                    parts[i - 1] = (i - 1, GetRank(parts, ranks, piece, i - 1, 1) ?? int.MaxValue);
                }

                parts.RemoveAt(i);
            } else {
                break;
            }
        }

        var output = new List<int>();
        for(int i = 0; i < parts.Count; i++) {
            var range = parts[i].Item1..parts[i + 1].Item1;
            output.Add(f.Invoke(range));
        }
        return output;
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
