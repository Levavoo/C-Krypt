using System;

internal class Program
{
    static void Main(string[] args)
    {
        // Prolog: Die Spielwelt und die Warnung am Eingang werden vorgestellt.
        Console.WriteLine("DIE VERLASSENE KRYPTA");
        Console.WriteLine("Sie stehen vor einem verwitterten Steintor.");
        Console.WriteLine("======================");
        Console.WriteLine("Seit hundert Jahren ist das Tor verschlossen.\nNiemand aus dem Dorf wagt sich in seine Nähe.");
        Console.WriteLine("Nebel kriecht über die verwitterten Stufen.\nEin kalter Wind dringt aus den Mauerritzen.");
        Console.WriteLine("Auf dem Tor steht: \"Kehr um!\"");
        Console.WriteLine();

        // Der Zustand des Helden wird in einfachen Variablen gespeichert.
        const int maximaleLebensPunkte = 20;
        int lebensPunkte = maximaleLebensPunkte;
        int gold = 8;
        bool imDungeon = false;

        Console.WriteLine("--- Aktueller Zustand ---");
        Console.WriteLine($"Lebenspunkte: {lebensPunkte}");
        Console.WriteLine($"Gold: {gold}");
        Console.WriteLine($"Im Dungeon: {imDungeon}");
        Console.WriteLine($"Maximale Lebenspunkte: {maximaleLebensPunkte}");
        Console.WriteLine();

        // Eine möglicherweise leere Namenseingabe wird fachlich abgesichert.
        Console.Write("Name des Helden: ");
        string name = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(name))
        {
            name = "Namenlos";
        }
        else
        {
            name = name.Trim();
        }

        Console.WriteLine($"{name} steht mit {lebensPunkte} Lebenspunkten und {gold} Gold vor der verlassenen Krypta.");
        Console.WriteLine($"Der Name hat {name.Length} Zeichen.");

        // Der Cast vor der Division verhindert eine ganzzahlige Division.
        double lebensAnteil = (double)lebensPunkte / maximaleLebensPunkte;
        int lebensProzent = (int)(lebensAnteil * 100);
        Console.WriteLine($"Verbleibende LP: {lebensProzent} %");
        Console.WriteLine();

        // Die Konsoleneingabe ist zunächst Text und wird danach in int umgewandelt.
        Console.WriteLine("Wähle eine Klasse:");
        Console.WriteLine("1 = Krieger");
        Console.WriteLine("2 = Schurke");
        Console.WriteLine("3 = Magier");
        Console.Write("Klasse: ");

        string klassenEingabe = Console.ReadLine() ?? "";
        int klassenWahl = int.Parse(klassenEingabe);
        Console.WriteLine($"Gewählte Zahl: {klassenWahl}");

        bool istKrieger = klassenWahl == 1;
        bool istSchurke = klassenWahl == 2;
        bool istMagier = klassenWahl == 3;
        bool bekannteKlassenWahl = istKrieger || istSchurke || istMagier;
        bool unbekannteKlassenWahl = !bekannteKlassenWahl;

        string heldenKlasse = unbekannteKlassenWahl ? "Abenteurer" : "";

        if (istKrieger)
        {
            heldenKlasse = "Krieger";
        }
        else if (istSchurke)
        {
            heldenKlasse = "Schurke";
        }
        else if (istMagier)
        {
            heldenKlasse = "Magier";
        }
        Console.WriteLine($"{name} betritt den Pfad als {heldenKlasse}.");
        Console.WriteLine();

        // Vier W6 bestimmen die Stärke; nur einer der niedrigsten Würfe entfällt.
        int wurf1 = Random.Shared.Next(1, 7);
        int wurf2 = Random.Shared.Next(1, 7);
        int wurf3 = Random.Shared.Next(1, 7);
        int wurf4 = Random.Shared.Next(1, 7);

        int niedrigsterWurf = Math.Min(Math.Min(wurf1, wurf2), Math.Min(wurf3, wurf4));
        int staerke = wurf1 + wurf2 + wurf3 + wurf4 - niedrigsterWurf;

        Console.WriteLine($"Würfe: {wurf1}, {wurf2}, {wurf3}, {wurf4}");
        Console.WriteLine($"Gestrichen: {niedrigsterWurf}");
        Console.WriteLine($"Stärke: {staerke}");
        Console.WriteLine();

        // Erst eine gültige Zugangswahl darf die W20-Probe auslösen.
        Console.WriteLine("1 = Steintor");
        Console.WriteLine("2 = Mauerspalt");
        Console.WriteLine("3 = Runenpforte");
        Console.Write("Zugang: ");

        int zugang = int.Parse(Console.ReadLine() ?? "");
        bool gueltigerZugang = zugang >= 1 && zugang <= 3;

        if (!gueltigerZugang)
        {
            Console.WriteLine("Dieser Zugang existiert nicht.");
        }
        else
        {
            if (zugang == 1)
            {
                Console.WriteLine("Sie stemmen sich gegen das schwere Steintor.");
            }
            else if (zugang == 2)
            {
                Console.WriteLine("Sie zwängen sich durch den schmalen Mauerspalt.");
            }
            else
            {
                Console.WriteLine("Die Runenpforte antwortet mit einem blauen Leuchten.");
            }

            int zugangsBonus = staerke;
            int w20 = Random.Shared.Next(1, 21);
            int gesamtErgebnis = w20 + zugangsBonus;
            bool erfolg;

            // Natürliche Würfe haben Vorrang vor der allgemeinen Schwierigkeit 24.
            if (w20 == 1)
            {
                erfolg = false;
            }
            else if (w20 == 20)
            {
                erfolg = true;
            }
            else
            {
                erfolg = gesamtErgebnis >= 24;
            }

            Console.WriteLine($"W20: {w20}");
            Console.WriteLine($"Bonus: {zugangsBonus}");
            Console.WriteLine($"Gesamtergebnis: {gesamtErgebnis}");
            Console.WriteLine($"Erfolg: {erfolg}");

            if (erfolg)
            {
                Console.WriteLine("Die Probe gelingt. Der Zugang zur Krypta öffnet sich.");
            }
            else
            {
                lebensPunkte -= 3;
                Console.WriteLine("Die Probe misslingt. Der Held verliert 3 Lebenspunkte.");
            }

            // Jeder gültige Weg führt hinein; die Probe bestimmt nur Erfolg und Schaden.
            imDungeon = true;
        }

        if (imDungeon)
        {
            Console.WriteLine();
            Console.Write("Inschrift: ");
            string inschrift = Console.ReadLine() ?? "";
            string schluesselwort = "Krone";

            if (string.IsNullOrWhiteSpace(inschrift))
            {
                Console.WriteLine("Die leere Inschrift gibt kein Zeichen preis.");
            }
            else
            {
                string bereinigteInschrift = inschrift.Trim();
                bool enthaeltSchluesselwort = bereinigteInschrift.Contains(schluesselwort);

                Console.WriteLine($"Erstes Zeichen: {bereinigteInschrift[0]}");

                if (enthaeltSchluesselwort)
                {
                    Console.WriteLine("Die Rune beginnt zu leuchten.");
                }
                else
                {
                    Console.WriteLine("Die Rune bleibt dunkel.");
                }

                Console.WriteLine($"Die Rune ruft: {bereinigteInschrift.ToUpper()}");
            }

            Console.WriteLine();
            Console.Write("Stärke des Heiltranks: ");
            int heilung = int.Parse(Console.ReadLine() ?? "");

            lebensPunkte = Math.Clamp(
                lebensPunkte + heilung,
                0,
                maximaleLebensPunkte);

            double geheilterAnteil = (double)lebensPunkte / maximaleLebensPunkte;
            int geheilterProzentwert = (int)(geheilterAnteil * 100);

            Console.WriteLine($"Lebenspunkte: {lebensPunkte}/{maximaleLebensPunkte}");
            Console.WriteLine($"Lebensenergie: {geheilterProzentwert} %");
        }

        Console.WriteLine();
        Console.WriteLine("--- Endstand ---");
        Console.WriteLine($"Name: {name}");
        Console.WriteLine($"Klasse: {heldenKlasse}");
        Console.WriteLine($"Stärke: {staerke}");
        Console.WriteLine($"Lebenspunkte: {lebensPunkte}/{maximaleLebensPunkte}");
        Console.WriteLine($"Gold: {gold}");
        Console.WriteLine($"Im Dungeon: {imDungeon}");
    }
}
