namespace BerechnungVonBruttopreisen;

public static class Extensions {
    public static string ToCustomString<T>(this T[] array) {
        return string.Join(",", array);
    }
}