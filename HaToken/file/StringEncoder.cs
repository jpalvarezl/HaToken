namespace Services;

internal static class StringEncoder {

    // BPE keys strings are Base64 strings
    public static byte[] BpeKeyDecode(string keyValue) {
        return Convert.FromBase64String(keyValue);
    }
}
