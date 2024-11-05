namespace BerechnungVonBruttopreisen;

public class KundenSpeicher : SingletonBase<KundenSpeicher> {
    Dictionary<int, Kunde> kundenMap = new();

    public Kunde? GetKunde(int id) {
        if (!kundenMap.ContainsKey(id)) {
            Console.WriteLine("Für diese ID ist kein Kundeneintrag gespeichert");
            return null;
        }

        return kundenMap[id];
    }

    public void RegisterKunde(Kunde kunde, int id) {
        if (kundenMap.ContainsValue(kunde)) {
            Console.WriteLine($"Kunde schon registriert unter anderer Kundennummer");
        }

        if (kundenMap.ContainsKey(id)) {
            Console.WriteLine($"Kunde {id} is already registered");
            int newID = GetUnusedID();
            Console.WriteLine($"Defaulting to unused ID: {newID}");
            kundenMap.Add(newID, kunde);
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