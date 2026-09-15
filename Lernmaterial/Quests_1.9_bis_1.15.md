# Quests 1.9 bis 1.15

Dieses Kapitel verbindet Vergleiche, Bedingungen und Zufallszahlen zu einer zusammenhängenden Spielregel. Entscheidend ist nicht nur, was berechnet wird, sondern auch, wann ein Block ausgeführt werden darf.

## Quest 1.9 Die drei Klassenrunen

**Ziel:** Die eingegebene Zahl mit den drei bekannten Klassenwerten vergleichen.

**Konzepte und Fähigkeiten:** Vergleichsoperator `==`, boolesche Variablen, Diagnoseausgaben.

```csharp
bool istKrieger = klassenWahl == 1;
bool istSchurke = klassenWahl == 2;
bool istMagier = klassenWahl == 3;

Console.WriteLine($"Kriegerrune: {istKrieger}");
Console.WriteLine($"Schurkenrune: {istSchurke}");
Console.WriteLine($"Magierrune: {istMagier}");
```

Bei der Auswahl `2` ist nur `istSchurke` wahr. `=` weist einen Wert zu, während `==` zwei Werte vergleicht. Die Diagnoseausgaben können nach dem Test aus der finalen Spielfassung entfernt werden.

## Quest 1.10 Das Siegel erkennt die Wahl

**Ziel:** Mehrere Wahrheitswerte zu einer gemeinsamen Aussage verbinden.

**Konzepte und Fähigkeiten:** logisches ODER `||`, logisches NICHT `!`, Wahrheitswerte kombinieren.

```csharp
bool bekannteKlassenWahl = istKrieger || istSchurke || istMagier;
bool unbekannteKlassenWahl = !bekannteKlassenWahl;
```

`bekannteKlassenWahl` ist wahr, sobald mindestens eine Rune wahr ist. `!` kehrt das Ergebnis um. Testwerte `1`, `3` und `9` zeigen die beiden möglichen Zustände.

## Quest 1.11 Das Zeichen der Klasse

**Ziel:** Aus der geprüften Zahl einen verständlichen Klassennamen erzeugen.

**Konzepte und Fähigkeiten:** `if`, `else if`, Kontrollfluss, Zuweisung abhängig von Bedingungen.

```csharp
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
```

Eine zusammenhängende `if`-Kette führt nur den ersten passenden Zweig aus. Der ternäre Ausdruck setzt für eine unbekannte Wahl den Standardwert `Abenteurer`.

## Quest 1.12 Vier Knochen, drei bleiben

**Ziel:** Vier W6 würfeln, den niedrigsten bestimmen und genau einmal streichen.

**Konzepte und Fähigkeiten:** Zufallsbereiche, `Random.Shared.Next()`, verschachteltes `Math.Min()`, arithmetische Ausdrücke.

```csharp
int wurf1 = Random.Shared.Next(1, 7);
int wurf2 = Random.Shared.Next(1, 7);
int wurf3 = Random.Shared.Next(1, 7);
int wurf4 = Random.Shared.Next(1, 7);

int niedrigsterWurf = Math.Min(Math.Min(wurf1, wurf2), Math.Min(wurf3, wurf4));
int staerke = wurf1 + wurf2 + wurf3 + wurf4 - niedrigsterWurf;
```

Die obere Grenze von `Next(1, 7)` ist ausgeschlossen; mögliche Ergebnisse sind daher `1` bis `6`. Der niedrigste Zahlenwert wird nur einmal subtrahiert, auch wenn mehrere Würfel denselben Tiefstwert zeigen.

## Quest 1.13 Ein Wurf zu früh

**Ziel:** Einen logischen Fehler erkennen, obwohl der Code kompiliert.

Der absichtlich falsche Zwischenstand würfelt sofort nach der Eingabe:

```csharp
int zugang = int.Parse(Console.ReadLine() ?? "");
int w20 = Random.Shared.Next(1, 21);
Console.WriteLine($"W20: {w20}");
```

Die beiden letzten Zeilen dürften erst nach einer gültigen Auswahl ausgeführt werden. Bei Zugang `9` kompiliert und läuft dieser Zwischenstand, verletzt aber die Spielregel. Das ist ein **Logikfehler**, kein Syntaxfehler.

## Quest 1.14 Der richtige Pfad

**Ziel:** Den Würfel- und Bonusblock durch eine Bereichsprüfung schützen.

**Konzepte und Fähigkeiten:** Vergleiche, logisches UND `&&`, verschachtelte Verzweigungen, Gültigkeitsbereich einer Variablen.

```csharp
bool gueltigerZugang = zugang >= 1 && zugang <= 3;

if (!gueltigerZugang)
{
    Console.WriteLine("Dieser Zugang existiert nicht.");
}
else
{
    int zugangsBonus = staerke;
    int w20 = Random.Shared.Next(1, 21);

    // Zugangstext und Probe stehen vollständig im gültigen Zweig.
}
```

Beide Vergleiche müssen wahr sein. Bei `9` werden weder Bonus noch W20 erzeugt. Die konkrete Zugangsmeldung wird innerhalb des gültigen Zweigs durch eine weitere `if`-Kette ausgewählt.

## Quest 1.15 Das Urteil des W20

**Ziel:** Sonderregeln und eine allgemeine Schwierigkeit in der richtigen Reihenfolge prüfen.

**Konzepte und Fähigkeiten:** priorisierte Bedingungen, zusammengesetzte Berechnung, Zustandsänderung, Kontrollfluss.

```csharp
int gesamtErgebnis = w20 + zugangsBonus;
bool erfolg;

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

if (!erfolg)
{
    lebensPunkte -= 3;
}

imDungeon = true;
```

Die natürliche `1` scheitert immer, die natürliche `20` gelingt immer. Erst danach gilt die allgemeine Prüfung ab Gesamtergebnis `24`. Jeder gültige Zugang führt anschließend in die Krypta; ein Misserfolg verursacht lediglich drei Schadenspunkte.

Für einen gezielten Test darf `w20` kurz durch `1` und danach durch `20` ersetzt werden. Im fertigen Projekt muss wieder `Random.Shared.Next(1, 21)` stehen.

## Kapitelcheck

- Die Klassenwahl erzeugt drei boolesche Einzelvergleiche.
- Unbekannte Zahlen ergeben die Klasse `Abenteurer`.
- Vier W6 werden einzeln gespeichert; genau ein niedrigster Wurf entfällt.
- Nur die Zugänge `1` bis `3` erzeugen einen W20.
- Natürliche `1` und `20` werden vor Schwierigkeit `24` geprüft.
- Nach jedem gültigen Zugang ist `imDungeon` wahr.
- Ein Misserfolg kostet drei Lebenspunkte.
