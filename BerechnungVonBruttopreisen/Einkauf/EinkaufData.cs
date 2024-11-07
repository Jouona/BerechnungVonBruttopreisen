using BerechnungVonBruttopreisen.Utilities;

namespace BerechnungVonBruttopreisen;

public class EinkaufData : IValidator {
    public float NettopreisDesArtikels { get; private set; }
    public int AnzahlDesArtikels { get; private set; }
    public int Kundennummer { get; private set; }
    public float MehrwertSteuerSatz { get; private set; }

    private EinkaufData(float nettopreisDesArtikels, int anzahlDesArtikels, int kundennummer,
        float mehrwertSteuerSatz) {
        NettopreisDesArtikels = nettopreisDesArtikels;
        AnzahlDesArtikels = anzahlDesArtikels;
        Kundennummer = kundennummer;
        MehrwertSteuerSatz = mehrwertSteuerSatz;
    }

    public static EinkaufDataBuilder Create() {
        return new EinkaufDataBuilder();
    }

    public bool Validate() {
        if (NettopreisDesArtikels < 0) {
            Console.WriteLine(
                $"Negative values {NettopreisDesArtikels} not allowed as Nettopreis des Artikels. Enter different number");
            NettopreisDesArtikels = Helpers.FloatEingabeLesen(false);
            return false;
        }

        if (AnzahlDesArtikels < 0) {
            Console.WriteLine(
                $"Negative values {AnzahlDesArtikels} not allowed as Anzahl des Artikels. Enter different number");
            AnzahlDesArtikels = Helpers.IntEingabeLesen(false);
            return false;
        }

        if (Kundennummer < 0) {
            Console.WriteLine($"Negative values {Kundennummer} not allowed as Kundennummer. Enter different number");
            Kundennummer = Helpers.IntEingabeLesen(false, [], KundenSpeicher.Instance.GetKundenIds());
            return false;
        }

        if (!KundenSpeicher.Instance.GetKundenIds().Contains(Kundennummer)) {
            Console.WriteLine($"Kundenummer {Kundennummer} is not yet registered. Enter different number");
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
        float mehrwertSteuerSatz;

        public EinkaufDataBuilder WithAllReadLine() {
            Console.WriteLine("Was ist der Nettopreis deines Artikels?");
            nettopreisDesArtikels = Helpers.FloatEingabeLesen();
            Console.WriteLine("Was ist die Anzahl deines Artikels?");
            anzahlDesArtikels = Helpers.IntEingabeLesen();

            Console.WriteLine("Was ist der Mehrwertsteuersatz für diesen Artikel?");
            mehrwertSteuerSatz = Helpers.FloatEingabeLesen(false, [0f, 7f, 19f]);

            Console.WriteLine("Was ist deine Kundennummer?");

            do {
                kundennummer = Helpers.IntEingabeLesen(false, KundenSpeicher.Instance.GetKundenIds());
            } while (!KundenSpeicher.Instance.GetKundenIds().Contains(kundennummer));

            return this;
        }

        public EinkaufDataBuilder WithAllParameters(float nettopreisDesArtikels, int anzahlDesArtikels,
            int kundennummer,
            float mehrwertSteuerSatz) {
            this.nettopreisDesArtikels = nettopreisDesArtikels;
            this.anzahlDesArtikels = anzahlDesArtikels;
            this.kundennummer = kundennummer;
            this.mehrwertSteuerSatz = mehrwertSteuerSatz;
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
            return new EinkaufData(nettopreisDesArtikels, anzahlDesArtikels, kundennummer, mehrwertSteuerSatz);
        }
    }
}