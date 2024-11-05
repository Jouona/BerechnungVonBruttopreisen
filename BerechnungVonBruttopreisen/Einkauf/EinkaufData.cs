namespace BerechnungVonBruttopreisen;

public class EinkaufData {
    public float NettopreisDesArtikels { get; private set; }
    public int AnzahlDesArtikels { get; private set; }
    public int Kundennummer { get; private set; }

    private EinkaufData(float nettopreisDesArtikels, int anzahlDesArtikels, int kundennummer) {
        NettopreisDesArtikels = nettopreisDesArtikels;
        AnzahlDesArtikels = anzahlDesArtikels;
        Kundennummer = kundennummer;
    }

    public static EinkaufDataBuilder Create() {
        return new EinkaufDataBuilder();
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