using System;

namespace KryptaSpiel;

public class Gegner
{
    public string Name { get; }
    public int Lebenspunkte { get; private set; }
    public int Ruestungswert { get; }
    public int AngriffsBonus { get; }
    public int SchadensWuerfel { get; }

    public Gegner(
        string name,
        int lebensPunkte,
        int ruestungswert,
        int angriffsBonus,
        int schadensWuerfel)
    {
        Name = string.IsNullOrWhiteSpace(name)
            ? "Unbekannter Gegner"
            : name.Trim();
        Lebenspunkte = Math.Max(1, lebensPunkte);
        Ruestungswert = ruestungswert;
        AngriffsBonus = angriffsBonus;
        SchadensWuerfel = Math.Max(1, schadensWuerfel);
    }

    public Gegner(string name)
        : this(name, 10, 11, 2, 4)
    {
    }

    public void ErhalteSchaden(int schaden)
    {
        if (schaden <= 0)
        {
            return;
        }

        Lebenspunkte -= schaden;
        BegrenzeLebenspunkte();
    }

    public bool IstBesiegt()
    {
        return Lebenspunkte == 0;
    }

    public int WuerfleSchaden()
    {
        return Wuerfel.Wuerfeln(SchadensWuerfel);
    }

    private void BegrenzeLebenspunkte()
    {
        Lebenspunkte = Math.Max(0, Lebenspunkte);
    }
}
