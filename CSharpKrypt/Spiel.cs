using System;

namespace KryptaSpiel;

public class Spiel
{
    public void Starten()
    {
        ZeigeTitel();

        Spieler spieler = ErstelleSpieler();
        Gegner gegner = new Gegner("Goblin");

        spieler.ZeigeStatus();

        Console.WriteLine();
        Console.WriteLine($"Ein {gegner.Name} bewacht die erste Kammer.");
        Console.WriteLine(
            $"Gegner: {gegner.Lebenspunkte} LP, " +
            $"Rüstung {gegner.Ruestungswert}, " +
            $"Angriffsbonus {gegner.AngriffsBonus}");

        Kaempfe(spieler, gegner);

        Console.WriteLine();
        Console.WriteLine(spieler.IstBesiegt()
            ? $"Niederlage: {spieler.Name} unterliegt in der Krypta."
            : $"Sieg: {spieler.Name} besiegt {gegner.Name}.");
    }

    private Spieler ErstelleSpieler()
    {
        Console.Write("Name des Helden: ");
        string name = Console.ReadLine() ?? "";

        Console.WriteLine();
        Console.WriteLine("Stärke wird ausgewürfelt:");
        int staerke = Wuerfel.AttributAuswuerfeln();

        Console.WriteLine("Geschicklichkeit wird ausgewürfelt:");
        int geschicklichkeit = Wuerfel.AttributAuswuerfeln();

        Console.WriteLine("Intelligenz wird ausgewürfelt:");
        int intelligenz = Wuerfel.AttributAuswuerfeln();

        Console.WriteLine("Wähle eine Klasse:");
        Console.WriteLine("1 = Krieger (+1 Stärke)");
        Console.WriteLine("2 = Schurke (+1 Geschicklichkeit)");
        Console.WriteLine("3 = Magier (+1 Intelligenz)");

        int klassenWahl = LiesZahl(1, 3);
        string heldenKlasse;

        switch (klassenWahl)
        {
            case 1:
                heldenKlasse = "Krieger";
                staerke += 1;
                break;
            case 2:
                heldenKlasse = "Schurke";
                geschicklichkeit += 1;
                break;
            case 3:
                heldenKlasse = "Magier";
                intelligenz += 1;
                break;
            default:
                heldenKlasse = "Abenteurer";
                break;
        }

        return new Spieler(
            name,
            heldenKlasse,
            staerke,
            geschicklichkeit,
            intelligenz);
    }

    private void Kaempfe(Spieler spieler, Gegner gegner)
    {
        int runde = 1;

        while (!spieler.IstBesiegt() && !gegner.IstBesiegt())
        {
            Console.WriteLine();
            Console.WriteLine($"--- Runde {runde} ---");

            int spielerW20 = Wuerfel.W20();
            Console.WriteLine(
                $"Spielerangriff: W20 {spielerW20} + " +
                $"Bonus {spieler.AngriffsBonus()} gegen " +
                $"Rüstung {gegner.Ruestungswert}");

            if (Trifft(
                    spielerW20,
                    spieler.AngriffsBonus(),
                    gegner.Ruestungswert))
            {
                int schaden = spieler.WuerfleSchaden();
                gegner.ErhalteSchaden(schaden);
                Console.WriteLine($"Treffer: {schaden} Schaden.");
            }
            else
            {
                Console.WriteLine("Der Spieler verfehlt.");
            }

            if (gegner.IstBesiegt())
            {
                Console.WriteLine($"{gegner.Name} wurde besiegt.");
                break;
            }

            int gegnerW20 = Wuerfel.W20();
            Console.WriteLine(
                $"Gegenangriff: W20 {gegnerW20} + " +
                $"Bonus {gegner.AngriffsBonus} gegen " +
                $"Rüstung {spieler.Ruestungswert()}");

            if (Trifft(
                    gegnerW20,
                    gegner.AngriffsBonus,
                    spieler.Ruestungswert()))
            {
                int schaden = gegner.WuerfleSchaden();
                spieler.ErhalteSchaden(schaden);
                Console.WriteLine($"Gegentreffer: {schaden} Schaden.");
            }
            else
            {
                Console.WriteLine($"{gegner.Name} verfehlt.");
            }

            Console.WriteLine($"Held: {spieler.Lebenspunkte} LP");
            Console.WriteLine($"Gegner: {gegner.Lebenspunkte} LP");
            runde++;
        }
    }

    private bool Trifft(int w20, int bonus, int ruestung)
    {
        if (w20 == 1)
        {
            return false;
        }

        if (w20 == 20)
        {
            return true;
        }

        return w20 + bonus >= ruestung;
    }

    private int LiesZahl(int minimum, int maximum)
    {
        while (true)
        {
            Console.Write($"Eingabe ({minimum} bis {maximum}): ");
            string eingabe = Console.ReadLine() ?? "";

            if (!int.TryParse(eingabe, out int zahl))
            {
                Console.WriteLine("Bitte gib eine ganze Zahl ein.");
                continue;
            }

            if (zahl < minimum || zahl > maximum)
            {
                Console.WriteLine(
                    $"Die Zahl muss zwischen {minimum} und {maximum} liegen.");
                continue;
            }

            return zahl;
        }
    }

    private void ZeigeTitel()
    {
        Console.WriteLine("DIE VERLASSENE KRYPTA");
        Console.WriteLine("======================");
        Console.WriteLine("Das Siegel der Objekte öffnet die erste Kammer.");
        Console.WriteLine();
    }
}
