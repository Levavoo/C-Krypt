using System;

namespace KryptaSpiel;

public static class Wuerfel
{
    public static int Wuerfeln(int seiten)
    {
        if (seiten < 1)
        {
            throw new ArgumentOutOfRangeException(
                nameof(seiten),
                "Ein Würfel braucht mindestens eine Seite.");
        }

        return Random.Shared.Next(1, seiten + 1);
    }

    public static int Wuerfeln(int anzahl, int seiten)
    {
        if (anzahl < 1)
        {
            throw new ArgumentOutOfRangeException(
                nameof(anzahl),
                "Es muss mindestens einmal gewürfelt werden.");
        }

        int summe = 0;

        for (int wurfNummer = 1; wurfNummer <= anzahl; wurfNummer++)
        {
            summe += Wuerfeln(seiten);
        }

        return summe;
    }

    public static int W20()
    {
        return Wuerfeln(20);
    }

    public static int AttributAuswuerfeln()
    {
        int summe = 0;
        int niedrigsterWurf = 7;

        for (int wurfNummer = 1; wurfNummer <= 4; wurfNummer++)
        {
            int wurf = Wuerfeln(6);
            summe += wurf;
            niedrigsterWurf = Math.Min(niedrigsterWurf, wurf);

            Console.WriteLine($"Wurf {wurfNummer}: {wurf}");
        }

        int attribut = summe - niedrigsterWurf;
        Console.WriteLine($"Niedrigster Wurf: {niedrigsterWurf}");
        Console.WriteLine($"Attributswert: {attribut}");
        Console.WriteLine();

        return attribut;
    }
}
