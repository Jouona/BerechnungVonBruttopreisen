namespace BerechnungVonBruttopreisen;

class Program {
    static void Main(string[] args) {
        ICalculateBrutto calculator = new BaseBruttoCalculator();

        Kunde meier = new Kunde("Meier", false);
        KundenSpeicher.Instance.RegisterKunde(meier, 4711);
        Kunde eichner = new Kunde("Eichner", false);
        KundenSpeicher.Instance.RegisterKunde(eichner, 4712);
        Kunde utechner = new Kunde("Uchner", false);
        KundenSpeicher.Instance.RegisterKunde(utechner, 4713);
        Kunde jona = new Kunde("Jona");

        KundenSpeicher.Instance.AllowTreueBonusByID(4711);
        KundenSpeicher.Instance.AllowTreueBonusByID(4712);
        KundenSpeicher.Instance.AllowTreueBonusByID(4713);

        EinkaufData einkaufData = EinkaufData.Create().WithAllManually().Build();

        EinkaufHandler.Instance.RegisterEinkauf(new Einkauf(einkaufData, calculator));
    }
}

