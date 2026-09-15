# Quests 1.16 bis 1.20

Dieses Kapitel verarbeitet Strings sicher und unterscheidet zwischen einem rechnerisch möglichen Wert und einem fachlich gültigen Spielzustand.

## Quest 1.16 Das erste Flüstern

**Ziel:** Die Inschrift nur dann einlesen, wenn der Held tatsächlich in der Krypta ist.

**Konzepte und Fähigkeiten:** bedingter Programmabschnitt, Stringvergleich, Gültigkeitsbereich lokaler Variablen.

```csharp
if (imDungeon)
{
    Console.Write("Inschrift: ");
    string inschrift = Console.ReadLine() ?? "";
    string schluesselwort = "Krone";

    if (inschrift == schluesselwort)
    {
        Console.WriteLine("Die Rune beginnt zu leuchten.");
    }
    else
    {
        Console.WriteLine("Die Rune bleibt dunkel.");
    }
}
```

Der Vergleich mit `==` prüft in C# den Inhalt der Strings. Er unterscheidet Groß- und Kleinschreibung und verlangt hier noch eine exakte Übereinstimmung.

## Quest 1.17 Die Inschrift entziffern

**Ziel:** Äußere Leerzeichen entfernen und das Schlüsselwort auch in einem längeren Text erkennen.

**Konzepte und Fähigkeiten:** Unveränderlichkeit von Strings, `Trim()`, `Contains()`, `ToUpper()`.

```csharp
string bereinigteInschrift = inschrift.Trim();
bool enthaeltSchluesselwort = bereinigteInschrift.Contains(schluesselwort);

if (enthaeltSchluesselwort)
{
    Console.WriteLine("Die Rune beginnt zu leuchten.");
}
else
{
    Console.WriteLine("Die Rune bleibt dunkel.");
}

Console.WriteLine($"Die Rune ruft: {bereinigteInschrift.ToUpper()}");
```

Stringmethoden verändern den ursprünglichen String nicht, sondern liefern einen neuen String. `ToUpper()` wandelt Buchstaben um, entfernt aber **keine** Leerzeichen. Die Inschrift `ich habe die Krone!` wird daher korrekt zu `ICH HABE DIE KRONE!`.

Der Zugriff auf `bereinigteInschrift[0]` wäre an dieser Stelle noch unsicher, weil die Eingabe leer sein könnte.

## Quest 1.18 Die sichere Deutung

**Ziel:** Leere Namen und Inschriften vor weiteren Stringoperationen abfangen.

**Konzepte und Fähigkeiten:** `string.IsNullOrWhiteSpace()`, sichere Reihenfolge, Indexzugriff, Datenbereinigung.

```csharp
if (string.IsNullOrWhiteSpace(name))
{
    name = "Namenlos";
}
else
{
    name = name.Trim();
}
```

Für die Inschrift gilt dieselbe Reihenfolge:

```csharp
if (string.IsNullOrWhiteSpace(inschrift))
{
    Console.WriteLine("Die leere Inschrift gibt kein Zeichen preis.");
}
else
{
    string bereinigteInschrift = inschrift.Trim();
    Console.WriteLine($"Erstes Zeichen: {bereinigteInschrift[0]}");

    // Contains() und ToUpper() werden erst im sicheren Zweig verwendet.
}
```

Ein Stringindex beginnt bei `0`. Ein leerer String besitzt kein erstes Zeichen und würde beim Zugriff eine `IndexOutOfRangeException` verursachen. Die Prüfung muss deshalb **vor** dem Indexzugriff stehen.

**Tests:** normaler Text, nur Leerzeichen und eine Inschrift mit `Krone`.

## Quest 1.19 Der Anteil des Lichts

**Ziel:** Verstehen, warum eine korrekte Rechnung trotzdem einen ungültigen Spielwert ergeben kann.

**Konzepte und Fähigkeiten:** explizites Casting, Prozentrechnung, technischer und fachlicher Wertebereich.

```csharp
int untersuchteLebensPunkte = 18;
const int untersuchtesMaximum = 20;
int untersuchteHeilung = 7;

int ungepruefteLebensPunkte = untersuchteLebensPunkte + untersuchteHeilung;
double ungepruefterLebensAnteil =
    (double)ungepruefteLebensPunkte / untersuchtesMaximum;
int ungepruefterProzentwert = (int)(ungepruefterLebensAnteil * 100);

Console.WriteLine($"Lebensenergie: {ungepruefterProzentwert} %");
```

Die Rechnung `25 / 20 = 1.25 = 125 %` ist mathematisch richtig. Fachlich sind jedoch höchstens 20 Lebenspunkte erlaubt. Dieser Untersuchungsblock ist nur ein Lernexperiment und wird nicht in die endgültige Spielfassung kopiert.

Die Variablennamen des Experiments unterscheiden sich bewusst vom Hauptprogramm, damit das Beispiel auch einzeln eingefügt werden kann, ohne doppelte Deklarationen zu erzeugen.

## Quest 1.20 Das gebändigte Lebenslicht

**Ziel:** Den neuen Lebenswert auf den erlaubten Bereich von `0` bis zum Maximum begrenzen.

**Konzepte und Fähigkeiten:** Benutzereingabe, `Math.Clamp()`, fachliche Validierung, Prozentwert aus validierten Daten.

```csharp
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
```

`Math.Clamp(wert, minimum, maximum)` liefert:

- das Minimum, wenn der Wert zu klein ist,
- das Maximum, wenn der Wert zu groß ist,
- ansonsten den unveränderten Wert.

Erst der begrenzte Wert wird gespeichert und zur Prozentrechnung verwendet.

| Ausgangswert | Heilung | Ergebnis | Prozent |
|---:|---:|---:|---:|
| 18 | 7 | 20 | 100 % |
| 4 | 3 | 7 | 35 % |
| -2 | 0 | 0 | 0 % |

Im tatsächlichen Spiel hängt der Ausgangswert davon ab, ob die W20-Probe Schaden verursacht hat. Eine feste Beispielausgabe wie `90 %` ist deshalb nicht für jeden zufälligen Lauf korrekt.

## Abschließende Ausgabe

Der Kapitelstand endet mit allen sechs geforderten Werten:

```csharp
Console.WriteLine("--- Endstand ---");
Console.WriteLine($"Name: {name}");
Console.WriteLine($"Klasse: {heldenKlasse}");
Console.WriteLine($"Stärke: {staerke}");
Console.WriteLine($"Lebenspunkte: {lebensPunkte}/{maximaleLebensPunkte}");
Console.WriteLine($"Gold: {gold}");
Console.WriteLine($"Im Dungeon: {imDungeon}");
```

## Kapitelcheck

- Inschrift und Heilung werden nur innerhalb der Krypta verarbeitet.
- Leere Namen werden zu `Namenlos`.
- Leere Inschriften lösen keinen Indexzugriff aus.
- `Trim()`, `Contains()` und `ToUpper()` werden im sicheren Zweig verwendet.
- Der Prozentwert wird aus den begrenzten Lebenspunkten berechnet.
- `Math.Clamp()` hält den Zustand zwischen `0` und `20`.
- Der Endstand zeigt Name, Klasse, Stärke, Lebenspunkte, Gold und Dungeonstatus.
