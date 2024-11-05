namespace BerechnungVonBruttopreisen;

public class BaseBruttoCalculator : ICalculateBrutto {
    public float GetBrutto(EinkaufData data) {
        float brutto = data.AnzahlDesArtikels * data.NettopreisDesArtikels;
        if (KundenSpeicher.Instance.GetKunde(data.Kundennummer).Treuebonus) {
            brutto -= 10;
        }

        return brutto;
    }
}