﻿namespace BerechnungVonBruttopreisen;

public class BaseBruttoCalculator : ICalculateBrutto {
    public float GetBrutto(EinkaufData data) {
        float summe = data.AnzahlDesArtikels * data.NettopreisDesArtikels * (1f + data.MehrwertSteuerSatz / 100);
        if (summe >= 25f) {
            summe *= 0.95f;
        }
        else if (summe >= 500f) {
            summe *= 0.9f;
        }

        if (KundenSpeicher.Instance.GetKunde(data.Kundennummer).Treuebonus) {
            summe *= 0.97f;
        }

        return MathF.Round(summe, 2);
    }
}