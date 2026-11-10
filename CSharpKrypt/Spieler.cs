using System;

namespace KryptaSpiel;

public class Spieler
{
    public string Name { get; }
    public string HeldenKlasse { get; }
    public int Staerke { get; }
    public int Geschicklichkeit { get; }
    public int Intelligenz { get; }
    public int MaximaleLebenspunkte { get; }
    public int Lebenspunkte { get; private set; }

    public Spieler(
        string name,
        string heldenKlasse,
        int staerke,
        int geschicklichkeit,
        int intelligenz)
    {
        Name = string.IsNullOrWhiteSpace(name) ? "Namenlos" : name.Trim();
        HeldenKlasse = heldenKlasse;
        Staerke = staerke;
        Geschicklichkeit = geschicklichkeit;
        Intelligenz = intelligenz;
        MaximaleLebenspunkte = 20;
        Lebenspunkte = MaximaleLebenspunkte;
    }

    public void ErhalteSchaden(int schaden)
    {
        if (schaden > 0)
        {
            Lebenspunkte = Math.Max(0, Lebenspunkte - schaden);
        }
    }

    public void Heile(int heilung)
    {
        if (heilung > 0)
        {
            Lebenspunkte = Math.Min(MaximaleLebenspunkte, Lebenspunkte + heilung);
        }
    }

    public bool IstBesiegt()
    {
        return Lebenspunkte == 0;
    }

    public void ZeigeStatus()
    {
        Console.WriteLine();
        Console.WriteLine("--- Heldenstatus ---");
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Klasse: {HeldenKlasse}");
        Console.WriteLine($"Stärke: {Staerke}");
        Console.WriteLine($"Geschicklichkeit: {Geschicklichkeit}");
        Console.WriteLine($"Intelligenz: {Intelligenz}");
        Console.WriteLine($"Angriffsbonus: {AngriffsBonus()}");
        Console.WriteLine($"Rüstung: {Ruestungswert()}");
        Console.WriteLine($"Lebenspunkte: {Lebenspunkte}/{MaximaleLebenspunkte}");
    }

    public int AngriffsBonus()
    {
        if (HeldenKlasse == "Krieger")
        {
            return Staerke / 3;
        }

        if (HeldenKlasse == "Schurke")
        {
            return Geschicklichkeit / 3;
        }

        return Intelligenz / 3;
    }

    public int Ruestungswert()
    {
        return 10 + Geschicklichkeit / 6;
    }

    public int WuerfleSchaden()
    {
        return Wuerfel.Wuerfeln(6);
    }
}
