using BerechnungVonBruttopreisen.Utilities;

namespace BerechnungVonBruttopreisen;

public class Einkauf : IValidator {
    public float NettopreisDesArtikels { get; private set; }
    public int AnzahlDesArtikels { get; private set; }
    public int Kundennummer { get; private set; }

    public float Rechnungsbetrag { get; private set; }

    public Einkauf(EinkaufData data, ICalculateBrutto bruttoCalculator) {
        data.Validate();
        this.NettopreisDesArtikels = data.NettopreisDesArtikels;
        this.AnzahlDesArtikels = data.AnzahlDesArtikels;
        this.Kundennummer = data.Kundennummer;

        this.Rechnungsbetrag = bruttoCalculator.GetBrutto(data);

        Validate();
    }

    public bool Validate() {
        // EinkaufData values are validated in constructor
        if (Rechnungsbetrag < 0) {
            Console.WriteLine("Rechnungsbetrag cannot be negative");
            return false;
        }

        return true;
    }
}