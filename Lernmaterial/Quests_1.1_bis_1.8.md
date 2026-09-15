# Quests 1.1 bis 1.8

Dieses Kapitel baut das Grundgerüst der Krypta auf. Die Quests werden in derselben `Program.cs` fortgeführt. Temporäre Fehler und Testwerte dienen nur zum Lernen und werden anschließend wieder entfernt.

## Quest 1.1 Das gebrochene Zeichen

**Ziel:** Eine klassische C#-Konsolenanwendung mit einem sichtbaren Einstiegspunkt erstellen.

**Konzepte und Fähigkeiten:** `using`, Klasse, Methode `Main()`, Codeblöcke, Anweisungen, Semikolon, Compilerfehler lesen.

```csharp
using System;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("DIE VERLASSENE KRYPTA");
        Console.WriteLine("Sie stehen vor einem verwitterten Steintor.");
    }
}
```

`Main()` ist der Einstiegspunkt. `Console.WriteLine()` gibt Text aus und beginnt danach eine neue Zeile. Das Semikolon beendet die jeweilige Anweisung. Wird es testweise entfernt, meldet der Compiler einen Syntaxfehler; die fehlerhafte Variante gehört nicht in den fertigen Code.

**Test:** Beide Texte müssen in getrennten Zeilen erscheinen.

## Quest 1.2 Spuren im Nebel

**Ziel:** Mehrere Ausgaben zu einem lesbaren Prolog ordnen.

**Konzepte und Fähigkeiten:** Reihenfolge von Anweisungen, Stringliterale, Aufbau einer Konsolenausgabe.

```csharp
Console.WriteLine("DIE VERLASSENE KRYPTA");
Console.WriteLine("======================");
Console.WriteLine("Seit hundert Jahren ist das Tor verschlossen.");
Console.WriteLine("Niemand aus dem Dorf wagt sich in seine Nähe.");
```

Die Anweisungen werden von oben nach unten ausgeführt. Die Trennlinie besitzt keine technische Bedeutung, verbessert aber die Lesbarkeit.

## Quest 1.3 Die Warnung erwacht

**Ziel:** Sonderzeichen und mehrere sichtbare Zeilen in Strings verwenden.

**Konzepte und Fähigkeiten:** Escape-Sequenzen `\n` und `\"`, Kommentare, Unterschied zwischen Quelltext und sichtbarer Ausgabe.

```csharp
// Prolog: Die Spielwelt und die Warnung am Eingang werden vorgestellt.
Console.WriteLine("Nebel kriecht über die Stufen.\nEin kalter Wind zieht auf.");
Console.WriteLine("Auf dem Tor steht: \"Kehr um!\"");
```

`\n` erzeugt innerhalb eines Strings einen Zeilenumbruch. `\"` stellt ein Anführungszeichen dar, ohne den String vorzeitig zu beenden. Ein guter Kommentar erklärt die Absicht des Abschnitts.

## Quest 1.4 Der Zustandsstein

**Ziel:** Veränderliche Spielwerte speichern und ausgeben.

**Konzepte und Fähigkeiten:** Variablendeklaration, Initialisierung, `int`, `bool`, Stringinterpolation.

```csharp
int lebensPunkte = 20;
int gold = 8;
bool imDungeon = false;

Console.WriteLine("--- Aktueller Zustand ---");
Console.WriteLine($"Lebenspunkte: {lebensPunkte}");
Console.WriteLine($"Gold: {gold}");
Console.WriteLine($"Im Dungeon: {imDungeon}");
```

`int` speichert ganze Zahlen. `bool` besitzt nur die Werte `true` und `false`. Das `$` vor einem String erlaubt, Werte in `{...}` einzusetzen.

## Quest 1.5 Das unveränderliche Maß

**Ziel:** Einen unveränderlichen Maximalwert als gemeinsame fachliche Grenze verwenden.

**Konzepte und Fähigkeiten:** `const`, Wiederverwendung eines Wertes, Compilerprüfung.

```csharp
const int maximaleLebensPunkte = 20;
int lebensPunkte = maximaleLebensPunkte;

Console.WriteLine($"Maximale Lebenspunkte: {maximaleLebensPunkte}");
```

Eine Konstante erhält ihren Wert bei der Deklaration und kann danach nicht neu zugewiesen werden. Eine Testzeile wie `maximaleLebensPunkte = 30;` muss einen Compilerfehler erzeugen und anschließend entfernt werden.

## Quest 1.6 Der Held antwortet

**Ziel:** Text von der Konsole einlesen und weiterverwenden.

**Konzepte und Fähigkeiten:** `Console.Write()`, `Console.ReadLine()`, Null-Ersatzoperator `??`, `string`, `Length`.

```csharp
Console.Write("Name des Helden: ");
string name = Console.ReadLine() ?? "";

Console.WriteLine($"{name} steht mit {lebensPunkte} Lebenspunkten und {gold} Gold vor der Krypta.");
Console.WriteLine($"Der Name hat {name.Length} Zeichen.");
```

`Write()` lässt den Cursor in derselben Zeile. `ReadLine()` wartet auf die Eingabetaste. Es kann theoretisch `null` liefern; `?? ""` ersetzt diesen Fall durch einen leeren String. Die endgültige Absicherung eines leeren Namens folgt in Quest 1.18.

## Quest 1.7 Die Kraftanzeige

**Ziel:** Einen ganzzahligen Zustand korrekt als Prozentwert darstellen.

**Konzepte und Fähigkeiten:** Ganzzahldivision, expliziter Cast, `double`, Abschneiden durch `(int)`.

```csharp
double lebensAnteil = (double)lebensPunkte / maximaleLebensPunkte;
int lebensProzent = (int)(lebensAnteil * 100);

Console.WriteLine($"Verbleibende LP: {lebensProzent} %");
```

Ohne Cast würde beispielsweise `15 / 20` als `int / int` den Wert `0` ergeben. Durch `(double)lebensPunkte` wird die Division als Kommarechnung ausgeführt: `15 / 20` wird `0.75`, anschließend `75` Prozent. Für den Test kann `lebensPunkte` vorübergehend auf `15` gesetzt werden; im finalen Programm wird der Startwert wiederhergestellt.

## Quest 1.8 Drei Runen im Stein

**Ziel:** Eine Zahlenauswahl zunächst als Text empfangen und anschließend umwandeln.

**Konzepte und Fähigkeiten:** Eingabeaufforderung, `int.Parse()`, Typumwandlung, Variablennamen.

```csharp
Console.WriteLine("1 = Krieger");
Console.WriteLine("2 = Schurke");
Console.WriteLine("3 = Magier");
Console.Write("Klasse: ");

string klassenEingabe = Console.ReadLine() ?? "";
int klassenWahl = int.Parse(klassenEingabe);
Console.WriteLine($"Gewählte Zahl: {klassenWahl}");
```

Konsoleneingaben kommen als Text an. `int.Parse()` interpretiert gültigen Zahlentext als `int`. Nichtnumerischer Text erzeugt in diesem Lernstand eine `FormatException`; `TryParse()` und Eingabewiederholungen sind bewusst noch nicht Teil des Kapitels.

## Kapitelcheck

- Die klassische Struktur mit `Program` und `Main()` ist sichtbar.
- Titel, Prolog, Kommentar und Warnung werden korrekt ausgegeben.
- Lebenspunkte, Gold und Dungeonstatus sind typisiert.
- Der Maximalwert ist konstant.
- Name und Klassenwahl kommen aus der Konsole.
- Der Prozentwert verwendet vor der Division einen Cast zu `double`.
