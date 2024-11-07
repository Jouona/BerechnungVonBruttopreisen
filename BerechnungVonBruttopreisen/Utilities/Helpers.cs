namespace BerechnungVonBruttopreisen.Utilities;

public class Helpers {
    public static int IntEingabeLesen(bool allowNegative = false, IEnumerable<int> enabledValues = null,
        IEnumerable<int> disabledValues = null) {
        int ausgangszahl = 0;
        try {
            ausgangszahl = Convert.ToInt32(Console.ReadLine());
            if (!allowNegative && ausgangszahl < 0) {
                throw new Exception("Cant be negative");
            }

            if (enabledValues != null && !enabledValues.Contains(ausgangszahl)) {
                throw new Exception(
                    $"Needs to be an enabled value. Enabled values: {string.Join(",", enabledValues)}");
            }

            if (disabledValues != null && disabledValues.Contains(ausgangszahl)) {
                throw new Exception(
                    $"Wert darf kein ausgeschlossener Wert sein. Disabled values: {string.Join(",", disabledValues)}");
            }
        }
        catch (Exception e) {
            Console.WriteLine(e.Message);
            return IntEingabeLesen(allowNegative, enabledValues);
        }

        return ausgangszahl;
    }

    public static float FloatEingabeLesen(bool allowNegative = false, IEnumerable<float> enabledValues = null,
        IEnumerable<float> disabledValues = null) {
        float ausgangszahl = 0f;
        try {
            ausgangszahl = float.Parse(Console.ReadLine());
            if (!allowNegative && ausgangszahl < 0f) {
                throw new Exception("Cant be negative");
            }

            if (enabledValues != null && !enabledValues.Contains(ausgangszahl)) {
                throw new Exception($"Needs to be an enabled value. Enabled values: {string.Join(",", enabledValues)}");
            }

            if (disabledValues != null && disabledValues.Contains(ausgangszahl)) {
                throw new Exception(
                    $"Wert darf kein ausgeschlossener Wert sein. Disables values: {string.Join(",", disabledValues)}");
            }
        }
        catch (Exception e) {
            Console.WriteLine(e.Message);
            return FloatEingabeLesen(allowNegative, enabledValues);
        }

        return ausgangszahl;
    }

    public static bool Abfrage() {
        Console.WriteLine("y/n");
        char yOrN = Convert.ToChar(Console.ReadLine());
        if (yOrN == 'y') {
            return true;
        }
        else if (yOrN == 'n') {
            return false;
        }
        else {
            Console.WriteLine("Korrekte Eingabe erforderlich");
            return Abfrage();
        }
    }
}