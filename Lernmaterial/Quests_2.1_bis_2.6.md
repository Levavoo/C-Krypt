# Quests 2.1 bis 2.6

Kapitel 2 beginnt mit dem wichtigsten Refactoring-Schritt: Das lange lineare Programm aus Kapitel 1 wird in benannte Methoden zerlegt. Danach werden Eingaben an Methoden übergeben, Ergebnisse zurückgegeben und wiederverwendbare Würfel- und Attributsregeln aufgebaut.

## Lernfortschritt dieses Abschnitts

```text
lange Main-Methode
→ benannte Programmbausteine
→ Parameter
→ Rückgabewerte
→ wiederverwendbare Spiellogik
→ Schleife für wiederholte Würfe
```

## Quest 2.1 Aus Echo wird Titel

**Ziel:** Titel und Vorgeschichte aus `Main()` in eine eigene Methode verschieben.

**Neue Konzepte:** Methodendeklaration, Methodenaufruf, `static`, `void`, Verantwortlichkeit einer Methode.

```csharp
static void Main(string[] args)
{
    ZeigeTitel();
}

static void ZeigeTitel()
{
    Console.WriteLine("DIE VERLASSENE KRYPTA");
    Console.WriteLine("======================");
    Console.WriteLine("Seit hundert Jahren ist das Tor verschlossen.");
    Console.WriteLine("Niemand aus dem Dorf wagt sich in seine Nähe.");
}
```

`static void ZeigeTitel()` besteht aus vier Teilen:

- `static`: Die Methode gehört zur Klasse und kann direkt aus der ebenfalls statischen `Main()` aufgerufen werden.
- `void`: Die Methode gibt keinen Wert zurück.
- `ZeigeTitel`: Der Name beschreibt ihre Aufgabe.
- `()`: Die Methode erwartet noch keine Eingabewerte.

**Lerngewinn:** `Main()` muss nicht jede Einzelanweisung enthalten. Sie kann den Ablauf durch verständliche Methodennamen beschreiben.

**Test:** Der Titel erscheint genau einmal und sieht unverändert aus.

## Quest 2.2 Die Stimmen der Kammer

**Ziel:** Einen zweiten unabhängigen Ausgabeblock erstellen und die Aufrufreihenfolge beobachten.

```csharp
static void Main(string[] args)
{
    ZeigeTitel();
    ZeigeKammer();
}

static void ZeigeKammer()
{
    Console.WriteLine("Hinter dem Tor liegt eine runde Kammer.");
    Console.WriteLine("Blaues Moos beleuchtet vier steinerne Sarkophage.");
    Console.WriteLine("In der Mitte wartet ein Runentisch.");
}
```

Methoden werden in der Reihenfolge ihrer Aufrufe ausgeführt. Vorübergehende Kontrollausgaben wie `ZeigeTitel startet` helfen beim Nachvollziehen, werden aber nach dem Test wieder entfernt.

**Lerngewinn:** Die Position einer Methodendeklaration in der Klasse bestimmt nicht die Ausführungsreihenfolge. Entscheidend ist, wann die Methode aufgerufen wird.

## Quest 2.3 Der Status spricht

**Ziel:** Aktuelle Werte aus `Main()` an eine allgemeine Ausgabemethode übergeben.

**Neue Konzepte:** Parameter, Argumente, lokale Gültigkeit, mehrere Datentypen in einer Methodensignatur.

```csharp
static void ZeigeStatus(
    string name,
    string heldenKlasse,
    int lebensPunkte)
{
    Console.WriteLine($"Name: {name}");
    Console.WriteLine($"Klasse: {heldenKlasse}");
    Console.WriteLine($"Lebenspunkte: {lebensPunkte}");
}
```

Aufruf:

```csharp
ZeigeStatus(name, heldenKlasse, lebensPunkte);
```

Die Variablen beim Aufruf sind **Argumente**. Die Namen in der Methodendeklaration sind **Parameter**. Die fertige Projektmethode nimmt zusätzlich Attribute, Maximum, Gold, Dungeonstatus und Angriffsbonus entgegen.

**Lerngewinn:** Die Methode enthält keine fest eingebauten Heldendaten. Sie kann verschiedene Zustände anzeigen, solange passende Argumente übergeben werden.

## Quest 2.4 Die Würfelrune

**Ziel:** Eine allgemeine Würfelmethode schreiben, die das Ergebnis zurückgibt.

**Neue Konzepte:** Rückgabetyp, `return`, Parameter als Konfiguration, exklusive Obergrenze von `Random.Shared.Next()`.

```csharp
static int Wuerfeln(int seiten)
{
    return Random.Shared.Next(1, seiten + 1);
}
```

Beispiele:

```csharp
int w6 = Wuerfeln(6);   // 1 bis 6
int w20 = Wuerfeln(20); // 1 bis 20
```

Die obere Grenze von `Random.Shared.Next(minimum, maximum)` ist ausgeschlossen. Deshalb wird `seiten + 1` übergeben.

**Test:** Mehrere W6- und W20-Würfe bleiben immer in ihrem Bereich.

## Quest 2.5 Der Bonus im Stein

**Ziel:** Eine Berechnung kapseln und ihr Ergebnis in `Main()` speichern.

```csharp
static int BerechneAngriffsBonus(int attribut)
{
    return attribut / 3;
}
```

Aufruf:

```csharp
int angriffsBonus = BerechneAngriffsBonus(staerke);
Console.WriteLine($"Angriffsbonus: {angriffsBonus}");
```

Da beide Operanden `int` sind, wird ganzzahlig dividiert: Aus `14 / 3` wird `4`. Die Methode entscheidet nicht, welches Attribut benutzt wird; diese Entscheidung trifft später die Klassenwahl.

**Lerngewinn:** Eine Methode kann einen Wert empfangen, verarbeiten und als neuen Wert zurückgeben.

## Quest 2.6 Das vollständige Ritual

**Ziel:** Die 4W6-Regel einmal implementieren und für Stärke, Geschicklichkeit und Intelligenz wiederverwenden.

**Neue Konzepte:** `for`-Schleife, Zählvariable, Akkumulator, laufendes Minimum, mehrfacher Methodenaufruf.

```csharp
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
    }

    Console.WriteLine($"Niedrigster Wurf: {niedrigsterWurf}");
    return summe - niedrigsterWurf;
}
```

Verwendung:

```csharp
int staerke = AttributAuswuerfeln();
int geschicklichkeit = AttributAuswuerfeln();
int intelligenz = AttributAuswuerfeln();
```

Der Startwert `7` liegt über jedem möglichen W6-Ergebnis. Schon der erste Wurf wird daher zum neuen Minimum. `summe` ist ein Akkumulator: In jedem Durchlauf wird der neue Wurf addiert.

**Wertebereich:** Vier W6 ergeben mindestens `4` und höchstens `24`. Nach dem Abzug eines niedrigsten Wurfs liegt das Attribut zwischen `3` und `18`.

## Abschnittscheck

- `Main()` koordiniert Methoden statt alle Details selbst auszuführen.
- Ausgaben liegen in `ZeigeTitel()`, `ZeigeKammer()` und `ZeigeStatus()`.
- `Wuerfeln(int seiten)` gibt einen passenden Zufallswert zurück.
- `BerechneAngriffsBonus()` kapselt die ganzzahlige Bonusberechnung.
- `AttributAuswuerfeln()` verwendet genau vier Schleifendurchläufe.
- Stärke, Geschicklichkeit und Intelligenz entstehen aus derselben Regel.
