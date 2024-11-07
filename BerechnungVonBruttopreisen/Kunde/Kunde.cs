using BerechnungVonBruttopreisen.Utilities;

namespace BerechnungVonBruttopreisen;

public class Kunde : IValidator {
    public string Name { get; private set; }
    public bool Treuebonus => this.GetätigteEinkäufe.Count > 10 || treuebonusManual;

    bool treuebonusManual = false;
    List<Einkauf> GetätigteEinkäufe = new();

    public Kunde(string name, bool autoRegister = true) {
        Name = name;
        if (autoRegister) {
            KundenSpeicher.Instance.RegisterKunde(this, KundenSpeicher.Instance.GetUnusedID());
        }

        Validate();
    }

    public void AllowTreueBonus() {
        treuebonusManual = true;
    }

    public bool Validate() {
        switch (this.Name.Length) {
            case < 2:
                Console.WriteLine($"Name {Name} is too short");
                Console.WriteLine("Enter new name");
                Name = Console.ReadLine();
                return Validate();
            case > 20:
                Console.WriteLine($"Name {Name} is too long");
                Console.WriteLine("Enter new name");
                Name = Console.ReadLine();
                return Validate();
                return false;
            default:
                return true;
        }
    }

    public class EinkaufHandler : SingletonBase<EinkaufHandler> {
        public void RegisterEinkauf(Einkauf einkauf) {
            Kunde? käuferKunde = KundenSpeicher.Instance.GetKunde(einkauf.Kundennummer);
            if (käuferKunde == null) {
                Console.WriteLine("Kunde nicht registriert.");
                return;
            }


            käuferKunde.GetätigteEinkäufe.Add(einkauf);
            Console.WriteLine(
                $"Der Einkauf von Kunde {KundenSpeicher.Instance.GetKunde(einkauf.Kundennummer)?.Name} mit der Kundennummer " +
                $"von {einkauf.Kundennummer} kostete {einkauf.Rechnungsbetrag}");
        }
    }
}