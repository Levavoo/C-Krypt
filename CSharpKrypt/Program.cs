using System;

internal class Program
{
    static void Main(string[] args)
    {
        // Main koordiniert den Ablauf; die einzelnen Aufgaben liegen in benannten Methoden.
        ZeigeTitel();
        ZeigeKammer();

        const int maximaleLebensPunkte = 20;
        int lebensPunkte = maximaleLebensPunkte;
        int gold = 8;
        bool imDungeon = false;

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

        Console.WriteLine();
        Console.WriteLine("Die drei Attributsrunen erwachen.");

        int staerke = AttributAuswuerfeln();
        int geschicklichkeit = AttributAuswuerfeln();
        int intelligenz = AttributAuswuerfeln();

        Console.WriteLine();
        Console.WriteLine("--- Grundattribute ---");
        Console.WriteLine($"Stärke: {staerke}");
        Console.WriteLine($"Geschicklichkeit: {geschicklichkeit}");
        Console.WriteLine($"Intelligenz: {intelligenz}");

        Console.WriteLine();
        Console.WriteLine("Wähle eine Klasse:");
        Console.WriteLine("1 = Krieger");
        Console.WriteLine("2 = Schurke");
        Console.WriteLine("3 = Magier");

        int klassenWahl = LiesZahl(1, 3);
        string heldenKlasse;
        int angriffsBonus;

        // Jede Klasse verstärkt genau ihr passendes Attribut.
        switch (klassenWahl)
        {
            case 1:
                heldenKlasse = "Krieger";
                staerke += 2;
                angriffsBonus = BerechneAngriffsBonus(staerke);
                break;

            case 2:
                heldenKlasse = "Schurke";
                geschicklichkeit += 2;
                angriffsBonus = BerechneAngriffsBonus(geschicklichkeit);
                break;

            case 3:
                heldenKlasse = "Magier";
                intelligenz += 2;
                angriffsBonus = BerechneAngriffsBonus(intelligenz);
                break;

            default:
                // LiesZahl verhindert diesen Fall; default hält den switch trotzdem vollständig.
                heldenKlasse = "Abenteurer";
                angriffsBonus = 0;
                break;
        }

        Console.WriteLine();
        Console.WriteLine("--- Klassensiegel ---");
        Console.WriteLine($"Klasse: {heldenKlasse}");
        Console.WriteLine($"Stärke: {staerke}");
        Console.WriteLine($"Geschicklichkeit: {geschicklichkeit}");
        Console.WriteLine($"Intelligenz: {intelligenz}");
        Console.WriteLine($"Angriffsbonus: {angriffsBonus}");

        Console.WriteLine();
        Console.WriteLine("Wähle einen Zugang:");
        Console.WriteLine("1 = Steintor");
        Console.WriteLine("2 = Mauerspalt");
        Console.WriteLine("3 = Runenpforte");

        int zugangsWahl = LiesZahl(1, 3);

        // Der switch-Ausdruck ordnet jedem Weg sein fachlich passendes Attribut zu.
        int zugangsBonus = zugangsWahl switch
        {
            1 => staerke,
            2 => geschicklichkeit,
            3 => intelligenz,
            _ => 0
        };

        switch (zugangsWahl)
        {
            case 1:
                Console.WriteLine("Sie stemmen sich gegen das schwere Steintor.");
                break;
            case 2:
                Console.WriteLine("Sie zwängen sich durch den schmalen Mauerspalt.");
                break;
            case 3:
                Console.WriteLine("Sie entziffern die Zeichen der Runenpforte.");
                break;
        }

        int w20 = Wuerfeln();
        int schwierigkeit = 24;
        int gesamtErgebnis = w20 + zugangsBonus;
        bool zugangsProbeBestanden = IstProbeBestanden(w20, zugangsBonus, schwierigkeit);

        Console.WriteLine($"W20: {w20}");
        Console.WriteLine($"Bonus: {zugangsBonus}");
        Console.WriteLine($"Gesamtergebnis: {gesamtErgebnis}");
        Console.WriteLine($"Erfolg: {zugangsProbeBestanden}");

        if (zugangsProbeBestanden)
        {
            Console.WriteLine("Die Probe gelingt. Der Zugang zur Krypta öffnet sich.");
        }
        else
        {
            lebensPunkte = Math.Max(0, lebensPunkte - 3);
            Console.WriteLine("Die Probe misslingt. Der Held verliert 3 Lebenspunkte.");
        }

        // Jede gültige Auswahl führt hinein; die Probe entscheidet nur über den Schaden.
        imDungeon = true;

        if (imDungeon)
        {
            Console.WriteLine();
            Console.Write("Inschrift: ");
            string inschrift = Console.ReadLine() ?? "";
            const string schluesselwort = "Krone";

            if (string.IsNullOrWhiteSpace(inschrift))
            {
                Console.WriteLine("Die leere Inschrift gibt kein Zeichen preis.");
            }
            else
            {
                string bereinigteInschrift = inschrift.Trim();
                bool enthaeltSchluesselwort = bereinigteInschrift.Contains(schluesselwort);

                Console.WriteLine($"Erstes Zeichen: {bereinigteInschrift[0]}");
                Console.WriteLine(
                    enthaeltSchluesselwort
                        ? "Die Rune beginnt zu leuchten."
                        : "Die Rune bleibt dunkel.");
                Console.WriteLine($"Die Rune ruft: {bereinigteInschrift.ToUpper()}");
            }

            Console.WriteLine();
            Console.WriteLine("Wie stark ist das gefundene Heilwasser? (0 bis 20)");
            int heilung = LiesZahl(0, maximaleLebensPunkte);
            lebensPunkte = Heile(lebensPunkte, maximaleLebensPunkte, heilung);

            double lebensAnteil = (double)lebensPunkte / maximaleLebensPunkte;
            int lebensProzent = (int)(lebensAnteil * 100);

            Console.WriteLine($"Lebenspunkte: {lebensPunkte}/{maximaleLebensPunkte}");
            Console.WriteLine($"Lebensenergie: {lebensProzent} %");

            string[] gegnerNamen =
            {
                "Knochenwächter",
                "Gruftspinne",
                "Steingolem",
                "Schattenpriester"
            };

            int[] gegnerLebenspunkte = { 10, 8, 14, 12 };
            int[] gegnerRuestung = { 11, 10, 14, 12 };

            Console.WriteLine();
            Console.WriteLine("--- Register der Wächter ---");
            Console.WriteLine($"Anzahl: {gegnerNamen.Length}");
            Console.WriteLine($"Erster Eintrag: {gegnerNamen[0]}");

            for (int index = 0; index < gegnerNamen.Length; index++)
            {
                Console.WriteLine($"{index + 1}. {gegnerNamen[index]}");
            }

            Console.WriteLine("Dasselbe Register ohne Nummern:");

            foreach (string gegnerName in gegnerNamen)
            {
                Console.WriteLine(gegnerName);
            }

            bool paralleleArraysGueltig =
                gegnerNamen.Length == gegnerLebenspunkte.Length &&
                gegnerNamen.Length == gegnerRuestung.Length;

            if (!paralleleArraysGueltig)
            {
                Console.WriteLine("Die Wächterdaten sind unvollständig. Der Kampf wird abgebrochen.");
            }
            else
            {
                int gegnerIndex = Random.Shared.Next(gegnerNamen.Length);
                string gegnerName = gegnerNamen[gegnerIndex];
                int gegnerLp = gegnerLebenspunkte[gegnerIndex];
                int ruestung = gegnerRuestung[gegnerIndex];

                Console.WriteLine();
                Console.WriteLine("Ein Sarkophag öffnet sich.");
                Console.WriteLine($"Index: {gegnerIndex}");
                Console.WriteLine($"Name: {gegnerName}");
                Console.WriteLine($"Lebenspunkte: {gegnerLp}");
                Console.WriteLine($"Rüstung: {ruestung}");

                lebensPunkte = FuehreKampfAus(
                    lebensPunkte,
                    angriffsBonus,
                    gegnerName,
                    gegnerLp,
                    ruestung);

                Console.WriteLine(
                    lebensPunkte > 0
                        ? $"{name} besiegt den Wächter."
                        : $"{name} unterliegt in der ersten Kammer.");
            }
        }

        ZeigeStatus(
            name,
            heldenKlasse,
            staerke,
            geschicklichkeit,
            intelligenz,
            lebensPunkte,
            maximaleLebensPunkte,
            gold,
            imDungeon,
            angriffsBonus);
    }

    static void ZeigeTitel()
    {
        Console.WriteLine("DIE VERLASSENE KRYPTA");
        Console.WriteLine("======================");
        Console.WriteLine("Seit hundert Jahren ist das Tor verschlossen.");
        Console.WriteLine("Niemand aus dem Dorf wagt sich in seine Nähe.");
        Console.WriteLine("Auf dem Tor steht: \"Kehr um!\"");
        Console.WriteLine();
    }

    static void ZeigeKammer()
    {
        Console.WriteLine("Hinter dem Tor liegt eine runde Kammer.");
        Console.WriteLine("Blaues Moos beleuchtet vier steinerne Sarkophage.");
        Console.WriteLine("In der Mitte wartet ein Runentisch.");
        Console.WriteLine();
    }

    static void ZeigeStatus(
        string name,
        string heldenKlasse,
        int staerke,
        int geschicklichkeit,
        int intelligenz,
        int lebensPunkte,
        int maximaleLebensPunkte,
        int gold,
        bool imDungeon,
        int angriffsBonus)
    {
        Console.WriteLine();
        Console.WriteLine("--- Endstand ---");
        Console.WriteLine($"Name: {name}");
        Console.WriteLine($"Klasse: {heldenKlasse}");
        Console.WriteLine($"Stärke: {staerke}");
        Console.WriteLine($"Geschicklichkeit: {geschicklichkeit}");
        Console.WriteLine($"Intelligenz: {intelligenz}");
        Console.WriteLine($"Angriffsbonus: {angriffsBonus}");
        Console.WriteLine($"Lebenspunkte: {lebensPunkte}/{maximaleLebensPunkte}");
        Console.WriteLine($"Gold: {gold}");
        Console.WriteLine($"Im Dungeon: {imDungeon}");
    }

    static int Wuerfeln(int seiten)
    {
        return Random.Shared.Next(1, seiten + 1);
    }

    static int Wuerfeln(int anzahl, int seiten)
    {
        int summe = 0;

        for (int wurfNummer = 1; wurfNummer <= anzahl; wurfNummer++)
        {
            summe += Wuerfeln(seiten);
        }

        return summe;
    }

    static int Wuerfeln()
    {
        return Wuerfeln(20);
    }

    static int BerechneAngriffsBonus(int attribut)
    {
        return attribut / 3;
    }

    static int AttributAuswuerfeln()
    {
        int summe = 0;
        int niedrigsterWurf = 7;

        for (int wurfNummer = 1; wurfNummer <= 4; wurfNummer++)
        {
            int wurf = Wuerfeln(6);
            summe += wurf;

            if (wurf < niedrigsterWurf)
            {
                niedrigsterWurf = wurf;
            }

            Console.WriteLine($"Wurf {wurfNummer}: {wurf}");
        }

        int attribut = summe - niedrigsterWurf;
        Console.WriteLine($"Niedrigster Wurf: {niedrigsterWurf}");
        Console.WriteLine($"Attributswert: {attribut}");
        Console.WriteLine();

        return attribut;
    }

    static int LiesZahl(int minimum, int maximum)
    {
        int geleseneZahl = minimum - 1;

        while (geleseneZahl < minimum || geleseneZahl > maximum)
        {
            Console.Write($"Eingabe ({minimum} bis {maximum}): ");
            string eingabe = Console.ReadLine() ?? "";

            if (!int.TryParse(eingabe, out geleseneZahl))
            {
                Console.WriteLine("Bitte geben Sie eine ganze Zahl ein.");
                geleseneZahl = minimum - 1;
            }
            else if (geleseneZahl < minimum || geleseneZahl > maximum)
            {
                Console.WriteLine($"Die Zahl muss zwischen {minimum} und {maximum} liegen.");
            }
        }

        return geleseneZahl;
    }

    static int Heile(int lebensPunkte, int maximaleLebensPunkte, int menge = 5)
    {
        int nichtUeberMaximum = Math.Min(
            maximaleLebensPunkte,
            lebensPunkte + menge);

        return Math.Clamp(nichtUeberMaximum, 0, maximaleLebensPunkte);
    }

    static bool IstProbeBestanden(int w20, int bonus, int schwierigkeit)
    {
        if (w20 == 1)
        {
            return false;
        }

        if (w20 == 20)
        {
            return true;
        }

        return w20 + bonus >= schwierigkeit;
    }

    static int FuehreKampfAus(
        int spielerLp,
        int spielerBonus,
        string gegnerName,
        int gegnerLp,
        int gegnerRuestung)
    {
        Console.WriteLine();
        Console.WriteLine($"Kampf gegen {gegnerName}");
        Console.WriteLine($"Spieler-LP: {spielerLp}");
        Console.WriteLine($"Gegner-LP: {gegnerLp}");
        Console.WriteLine($"Gegnerrüstung: {gegnerRuestung}");

        int runde = 1;

        while (spielerLp > 0 && gegnerLp > 0)
        {
            Console.WriteLine();
            Console.WriteLine($"--- Runde {runde} ---");

            int spielerW20 = Wuerfeln();
            bool spielerTrifft = IstProbeBestanden(
                spielerW20,
                spielerBonus,
                gegnerRuestung);

            Console.WriteLine(
                $"Spielerangriff: W20 {spielerW20} + Bonus {spielerBonus} " +
                $"gegen Rüstung {gegnerRuestung}");

            if (spielerTrifft)
            {
                int schaden = Wuerfeln(6);
                gegnerLp = Math.Max(0, gegnerLp - schaden);
                Console.WriteLine($"Treffer: {schaden} Schaden.");
            }
            else
            {
                Console.WriteLine("Der Angriff verfehlt sein Ziel.");
            }

            // Ein besiegter Gegner darf nicht mehr zurückschlagen.
            if (gegnerLp <= 0)
            {
                Console.WriteLine($"{gegnerName} wurde besiegt.");
                break;
            }

            int gegenSchaden = Wuerfeln(4);
            spielerLp = Math.Max(0, spielerLp - gegenSchaden);
            Console.WriteLine($"Gegenangriff: {gegenSchaden} Schaden.");
            Console.WriteLine($"Spieler-LP: {spielerLp}");
            Console.WriteLine($"Gegner-LP: {gegnerLp}");

            runde++;
        }

        return spielerLp;
    }
}
