using BerechnungVonBruttopreisen.Utilities;

namespace BerechnungVonBruttopreisen;

public class KundenSpeicher : SingletonBase<KundenSpeicher> {
    Dictionary<int, Kunde> kundenMap = new();

    public Kunde? GetKunde(int id) {
        if (kundenMap.ContainsKey(id)) return kundenMap[id];

        Console.WriteLine("Für diese ID ist kein Kundeneintrag gespeichert");
        return null;
    }

    public void RegisterKunde(Kunde kunde, int id) {
        if (kundenMap.ContainsValue(kunde)) {
            Console.WriteLine($"Kunde schon registriert unter anderer Kundennummer");
        }

        if (kundenMap.ContainsKey(id)) {
            Console.WriteLine($"This {id} is already used by a different customer");
            int unusedId = GetUnusedID();
            Console.WriteLine($"Defaulting to unused ID: {unusedId}");
            kundenMap.Add(unusedId, kunde);
            return;
        }

        kundenMap.Add(id, kunde);
    }

    public void AllowTreueBonusByID(int id) {
        if (!kundenMap.ContainsKey(id)) return;

        kundenMap[id].AllowTreueBonus();
    }

    public int GetUnusedID() {
        for (int i = 0; i < kundenMap.Count; i++) {
            if (kundenMap.ContainsKey(i)) {
                i++;
            }
            else {
                return i;
            }
        }

        throw new KeyNotFoundException();
    }

    public int[] GetKundenIds() {
        return kundenMap.Keys.ToArray();
    }
}