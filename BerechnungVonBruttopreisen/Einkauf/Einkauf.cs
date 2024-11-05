namespace BerechnungVonBruttopreisen;

public class Einkauf {
    public float NettopreisDesArtikels { get; private set; }
    public int AnzahlDesArtikels { get; private set; }
    public int Kundennummer { get; private set; }

    public float Rechnungsbetrag { get; private set; }

    public Einkauf(EinkaufData data, ICalculateBrutto bruttoCalculator) {
        this.NettopreisDesArtikels = data.NettopreisDesArtikels;
        this.AnzahlDesArtikels = data.AnzahlDesArtikels;
        this.Kundennummer = data.Kundennummer;

        this.Rechnungsbetrag = bruttoCalculator.GetBrutto(data);
    }
}