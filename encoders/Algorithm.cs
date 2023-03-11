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
    // fn _encode_ordinary_native(&self, text: &str) -> Vec<usize> {
    //     // This is the core of the encoding logic; the other functions in here
    //     // just make things complicated :-)
    //     let regex = self._get_tl_regex();
    //     let mut ret = vec![];
    //     for mat in regex.find_iter(text) {
    //         let piece = mat.unwrap().as_str().as_bytes();
    //         if let Some(token) = self.encoder.get(piece) {
    //             ret.push(*token);
    //             continue;
    //         }
    //         ret.extend(&byte_pair_encode(piece, &self.encoder));
    //     }
    //     ret
    // }


    // pub fn byte_pair_encode(piece: &[u8], ranks: &HashMap<Vec<u8>, usize>) -> Vec<usize> {
    //     if piece.len() == 1 {
    //         return vec![ranks[piece]];
    //     }
    //     _byte_pair_merge(piece, ranks, |p| ranks[&piece[p.start..p.end]])
    // }
    }
}
