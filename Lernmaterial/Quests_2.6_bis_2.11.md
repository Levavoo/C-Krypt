# Quests 2.6 bis 2.11

Quest 2.6 bildet die Brücke in diesen Abschnitt: Nachdem drei Attribute mit derselben Methode erzeugt werden, verbindet ein `switch` die Klassenwahl mit dem passenden Attribut. Anschließend kommen Arrays, zwei Schleifenarten, Zufallsindizes und robuste Zahleneingaben hinzu.

## Quest 2.6 als Ausgangspunkt

```csharp
int staerke = AttributAuswuerfeln();
int geschicklichkeit = AttributAuswuerfeln();
int intelligenz = AttributAuswuerfeln();
```

Alle drei Variablen besitzen dieselbe technische Entstehungsregel, aber eine andere fachliche Bedeutung. Diese Trennung wird in Quest 2.7 für die Klassenlogik benötigt.

## Quest 2.7 Das vollständige Steinsiegel

**Ziel:** Die Klassenwahl mit einem `switch` auswerten und genau ein Attribut verstärken.

**Neue Konzepte:** `switch`, `case`, `default`, `break`, fallbezogene Zustandsänderungen.

```csharp
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
        heldenKlasse = "Abenteurer";
        angriffsBonus = 0;
        break;
}
```

Die Aufgabenstellung legt keine konkrete Erhöhung fest. Das Projekt verwendet einheitlich `+2`. `break` beendet den ausgewählten Fall. `default` schützt die Logik auch dann, wenn die Eingabeprüfung später geändert wird.

**Tests:** `1`, `2`, `3` und vorübergehend `9`. Bei `9` steigt kein Attribut und der Bonus bleibt `0`.

## Quest 2.8 Das Register der Wächter

**Ziel:** Mehrere gleichartige Werte in einem Array speichern und vollständig durchlaufen.

**Neue Konzepte:** Arrayinitialisierung, `Length`, Index, `for`, `foreach`.

```csharp
string[] gegnerNamen =
{
    "Knochenwächter",
    "Gruftspinne",
    "Steingolem",
    "Schattenpriester"
};

Console.WriteLine(gegnerNamen.Length);
Console.WriteLine(gegnerNamen[0]);
```

Nummerierte Ausgabe mit `for`:

```csharp
for (int index = 0; index < gegnerNamen.Length; index++)
{
    Console.WriteLine($"{index + 1}. {gegnerNamen[index]}");
}
```

Direkte Ausgabe mit `foreach`:

```csharp
foreach (string gegnerName in gegnerNamen)
{
    Console.WriteLine(gegnerName);
}
```

`for` stellt den Index bereit und eignet sich für sichtbare Nummern. `foreach` liefert direkt jedes Element und ist lesbarer, wenn kein Index gebraucht wird.

## Quest 2.9 Ein Deckel bewegt sich

**Ziel:** Einen zufälligen, garantiert gültigen Arrayindex auswählen.

```csharp
int gegnerIndex = Random.Shared.Next(gegnerNamen.Length);
string gegnerName = gegnerNamen[gegnerIndex];
```

Bei vier Elementen liefert `Next(4)` nur `0`, `1`, `2` oder `3`. Genau das sind die gültigen Indizes. `Next(gegnerNamen.Length + 1)` wäre falsch und könnte einen Laufzeitfehler verursachen.

**Lerngewinn:** Ein Index verbindet die zufällige Auswahl später mit allen parallelen Gegnerdaten.

## Quest 2.10 Der erlaubte Bereich

**Ziel:** Unterschiedliche fehlerhafte Eingaben erkennen, ohne eine Ausnahme auszulösen.

**Neue Konzepte:** `int.TryParse()`, `out`, kombinierte technische und fachliche Validierung.

```csharp
string eingabe = Console.ReadLine() ?? "";

if (!int.TryParse(eingabe, out int geleseneZahl))
{
    Console.WriteLine("Bitte geben Sie eine ganze Zahl ein.");
}
else if (geleseneZahl < 1 || geleseneZahl > 3)
{
    Console.WriteLine("Die Zahl muss zwischen 1 und 3 liegen.");
}
else
{
    Console.WriteLine($"Gültige Auswahl: {geleseneZahl}");
}
```

`TryParse()` beantwortet zuerst die technische Frage: Ist der Text eine ganze Zahl? Die Bereichsprüfung beantwortet danach die fachliche Frage: Ist diese Zahl hier erlaubt?

**Tests:** `2`, `abc`, leere Eingabe, `0` und `4`.

## Quest 2.11 Das geduldige Zahlenschloss

**Ziel:** Die Eingabe so lange wiederholen, bis eine gültige Zahl vorliegt.

**Neue Konzepte:** `while`-Schleife, Schleifenbedingung, allgemeine Validierungsmethode.

```csharp
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
```

Verwendung:

```csharp
int klassenWahl = LiesZahl(1, 3);
int zugangsWahl = LiesZahl(1, 3);
```

Nach einem fehlgeschlagenen `TryParse()` wird wieder ein Wert außerhalb des Bereichs gesetzt. Dadurch bleibt die `while`-Bedingung wahr und die Eingabe wird erneut angefordert.

## Abschnittscheck

- Die Klasse bestimmt Attribut und Angriffsbonus über einen `switch`.
- Vier Gegner stehen in einem `string[]`.
- `for` und `foreach` durchlaufen dasselbe Array vollständig.
- Der Zufallsindex liegt immer zwischen `0` und `Length - 1`.
- `TryParse()` unterscheidet Nichtzahlen von unzulässigen Zahlen.
- `LiesZahl()` beendet sich erst mit einem Wert innerhalb der übergebenen Grenzen.
