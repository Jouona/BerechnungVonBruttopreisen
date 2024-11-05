namespace BerechnungVonBruttopreisen;

public class Helpers {
    public static int IntEingabeLesen(bool allowNegative = false, IEnumerable<int> enabledValues = null) {
        int ausgangszahl = 0;
        try {
            ausgangszahl = Convert.ToInt32(Console.ReadLine());
            if (!allowNegative && ausgangszahl < 0) {
                throw new Exception("Cant be negative");
            }

            if (enabledValues != null && !enabledValues.Contains(ausgangszahl)) {
                throw new Exception("Needs to be an enabled value");
            }
        }
        catch (Exception e) {
            Console.WriteLine(e.Message);
            return IntEingabeLesen(allowNegative, enabledValues);
        }

        return ausgangszahl;
    }

    public static float FloatEingabeLesen(bool allowNegative = false) {
        float ausgangszahl = 0;
        try {
            ausgangszahl = Convert.ToSingle(Console.ReadLine());
            if (!allowNegative && ausgangszahl < 0) {
                throw new Exception("Kann nicht negativ sein");
            }
        }
        catch (Exception e) {
            Console.WriteLine(e.Message);
            return FloatEingabeLesen(allowNegative);
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