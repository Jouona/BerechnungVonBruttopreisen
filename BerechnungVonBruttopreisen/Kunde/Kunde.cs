namespace BerechnungVonBruttopreisen;

public class Kunde {
    internal List<Einkauf> GetätigteEinkäufe { get; private set; } = new();
    public string Name { get; private set; }
    public bool Treuebonus => this.GetätigteEinkäufe.Count > 10 || treuebonusManual;

    bool treuebonusManual = false;


    public Kunde(string name, bool autoRegister = true) {
        Name = name;
        if (autoRegister) {
            KundenSpeicher.Instance.RegisterKunde(this, KundenSpeicher.Instance.GetUnusedID());
        }
    }

    public void AllowTreueBonus() {
        treuebonusManual = true;
    }
}

public class EinkaufHandler : SingletonBase<EinkaufHandler> {
    public void RegisterEinkauf(Einkauf einkauf) {
        if (KundenSpeicher.Instance.GetKunde(einkauf.Kundennummer) == null) {
            Console.WriteLine("Kunde nicht registriert.");
            return;
        }

        KundenSpeicher.Instance.GetKunde(einkauf.Kundennummer)?.GetätigteEinkäufe.Add(einkauf);
        Console.WriteLine(
            $"Der Einkauf von Kunde {KundenSpeicher.Instance.GetKunde(einkauf.Kundennummer)?.Name} mit der Kundennummer " +
            $"von {einkauf.Kundennummer} kostete {einkauf.Rechnungsbetrag}");
    }
}