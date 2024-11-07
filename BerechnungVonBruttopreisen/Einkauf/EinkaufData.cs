using BerechnungVonBruttopreisen.Utilities;

namespace BerechnungVonBruttopreisen;

public class EinkaufData : IValidator {
    public float NettopreisDesArtikels { get; private set; }
    public int AnzahlDesArtikels { get; private set; }
    public int Kundennummer { get; private set; }
    public float MehrwertSteuerSatz { get; private set; }

    private EinkaufData(float nettopreisDesArtikels, int anzahlDesArtikels, int kundennummer) {
        NettopreisDesArtikels = nettopreisDesArtikels;
        AnzahlDesArtikels = anzahlDesArtikels;
        Kundennummer = kundennummer;
    }

    public static EinkaufDataBuilder Create() {
        return new EinkaufDataBuilder();
    }

    public bool Validate() {
        if (NettopreisDesArtikels <= 0) {
            Console.WriteLine($"Negative values {NettopreisDesArtikels} not allowed as Nettopreis des Artikels");
            NettopreisDesArtikels = Helpers.FloatEingabeLesen(false);
            return false;
        }

        if (AnzahlDesArtikels < 0) {
            Console.WriteLine($"Negative values {AnzahlDesArtikels} not allowed as Anzahl des Artikels");
            AnzahlDesArtikels = Helpers.IntEingabeLesen(false);
            return false;
        }

        if (Kundennummer < 0) {
            Console.WriteLine($"Negative values {Kundennummer} not allowed as Kundennummer");
            Kundennummer = Helpers.IntEingabeLesen(false, [], KundenSpeicher.Instance.GetKundenIds());
            return false;
        }

        if (KundenSpeicher.Instance.GetKundenIds().Contains(Kundennummer)) {
            Console.WriteLine($"Kundenummer {Kundennummer} is already registered");
            Kundennummer = Helpers.IntEingabeLesen(false, [], KundenSpeicher.Instance.GetKundenIds());
            return false;
        }

        if (this.MehrwertSteuerSatz is not (0f or 7f or 19f)) {
            Console.WriteLine("MehrwertSteuerSatz is not a valid value. Needs to be 0, 7, or 19");
            MehrwertSteuerSatz = Helpers.FloatEingabeLesen(false, [0f, 7f, 19f]);
            return false;
        }

        return true;
    }

    public class EinkaufDataBuilder {
        float nettopreisDesArtikels;
        int anzahlDesArtikels;
        int kundennummer;

        public EinkaufDataBuilder WithAllManually() {
            Console.WriteLine("Was ist der Nettopreis deines Artikels?");
            nettopreisDesArtikels = Helpers.FloatEingabeLesen();
            Console.Write("Was ist die Anzahl deines Artikels?");
            anzahlDesArtikels = Helpers.IntEingabeLesen();

            Console.WriteLine("Was ist deine Kundennummer?");

            // kann nur mit bereits eingetragenen Kundennummern gestartet werden
            kundennummer = Helpers.IntEingabeLesen(false, KundenSpeicher.Instance.GetKundenIds());
            return this;
        }

        public EinkaufDataBuilder WithNettopreisDesArtikels(float nettopreisDesArtikels) {
            nettopreisDesArtikels = nettopreisDesArtikels;
            return this;
        }

        public EinkaufDataBuilder WithAnzahlDesArtikels(int anzahlDesArtikels) {
            anzahlDesArtikels = anzahlDesArtikels;
            return this;
        }

        public EinkaufDataBuilder WithKundennummer(int kundennummer) {
            kundennummer = kundennummer;
            if (KundenSpeicher.Instance.GetKunde(kundennummer) == null) {
                throw new Exception("Diese kundennummer ist nicht eingetragen");
            }

            return this;
        }

        public EinkaufData Build() {
            return new EinkaufData(nettopreisDesArtikels, anzahlDesArtikels, kundennummer);
        }
    }
}