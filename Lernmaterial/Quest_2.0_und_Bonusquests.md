# Quest 2.0 und Bonusquests 2A bis 2D

Diese Datei sammelt den vorbereitenden Methodenversuch und die freiwilligen Vertiefungen. Die Testaufrufe werden nach der Kontrolle wieder entfernt; die wiederverwendbaren Methoden bleiben im finalen Projekt.

## Quest 2.0 Aus Echo wird Titel

**Ziel:** Vor dem eigentlichen Refactoring beobachten, dass ein Methodenaufruf denselben gespeicherten Code mehrfach ausführen kann.

```csharp
static void ZeigeEcho()
{
    Console.WriteLine("Eine Stimme flüstert aus dem Treppenschacht.");
}
```

Vorübergehender Test in `Main()`:

```csharp
ZeigeEcho();
ZeigeEcho();
```

Der Satz steht nur einmal im Quelltext der Methode, erscheint aber zweimal. In Quest 2.1 wird dieser Versuch in `ZeigeTitel()` umbenannt und sinnvoll in den Spielablauf integriert.

## Bonusquest 2A Drei Würfelrunen

**Ziel:** Mehrere Methoden mit demselben Namen, aber unterschiedlichen Parameterlisten bereitstellen.

```csharp
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
```

Testaufrufe:

```csharp
Console.WriteLine($"2W6: {Wuerfeln(2, 6)}");
Console.WriteLine($"3W8: {Wuerfeln(3, 8)}");
Console.WriteLine($"W20: {Wuerfeln()}");
```

Das nennt man **Methodenüberladung**. Der Compiler wählt die Variante anhand der Anzahl und Typen der Argumente. Nur `Wuerfeln(int seiten)` enthält die eigentliche Zufallslogik.

## Bonusquest 2B Das Wasser heilt

**Ziel:** Einen optionalen Parameter mit einem Standardwert verwenden.

```csharp
static int Heile(
    int lebensPunkte,
    int maximaleLebensPunkte,
    int menge = 5)
{
    int nichtUeberMaximum = Math.Min(
        maximaleLebensPunkte,
        lebensPunkte + menge);

    return Math.Clamp(nichtUeberMaximum, 0, maximaleLebensPunkte);
}
```

```csharp
int standardHeilung = Heile(12, 20);     // Menge 5
int starkeHeilung = Heile(12, 20, 10);   // Menge 10
```

Wird das dritte Argument weggelassen, setzt C# automatisch `5` ein. `Math.Min()` verhindert Werte über dem Maximum; `Math.Clamp()` schützt zusätzlich die Untergrenze.

## Bonusquest 2C Die drei Wege im Spiegel

**Ziel:** Einen Wert mit einem kompakten `switch`-Ausdruck auswählen.

```csharp
int zugangsBonus = zugangsWahl switch
{
    1 => staerke,
    2 => geschicklichkeit,
    3 => intelligenz,
    _ => 0
};
```

Ein `switch`-Ausdruck erzeugt einen Wert. Er unterscheidet sich dadurch vom `switch`-Statement aus der Klassenwahl, das mehrere Anweisungen pro Fall ausführt. `_` ist der Auffangfall.

## Bonusquest 2D Die Frage des W20

**Ziel:** Die komplette Regel einer Probe durch einen verständlichen Methodenaufruf ausdrücken.

```csharp
static bool IstProbeBestanden(
    int w20,
    int bonus,
    int schwierigkeit)
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
```

Aufruf:

```csharp
int schwierigkeit = 24;
bool erfolg = IstProbeBestanden(w20, zugangsBonus, schwierigkeit);
```

Der Methodenname liest sich wie eine Frage, der Rückgabewert ist die Antwort. Die Sonderregeln stehen vor der allgemeinen Berechnung und beenden die Methode sofort mit `return`.

Testfälle:

| W20 | Bonus | Schwierigkeit | Ergebnis | Grund |
|---:|---:|---:|---|---|
| 1 | 30 | 24 | `false` | natürliche 1 |
| 20 | 0 | 24 | `true` | natürliche 20 |
| 15 | 10 | 24 | `true` | 25 erreicht Schwierigkeit |
| 10 | 5 | 24 | `false` | 15 reicht nicht |

## Gesamtfortschritt nach Kapitel 2

- Methoden trennen Verantwortlichkeiten.
- Parameter transportieren Werte in Methoden.
- Rückgabewerte transportieren Ergebnisse zurück.
- Überladungen bieten mehrere Aufrufformen derselben Operation.
- Standardparameter machen ein Argument optional.
- `for`, `foreach` und `while` lösen unterschiedliche Wiederholungsprobleme.
- Arrays speichern mehrere gleichartige Werte.
- `TryParse()` verbindet Eingabeprüfung und Kontrollfluss.
- `switch`-Statements führen Anweisungen aus; `switch`-Ausdrücke erzeugen Werte.
- `Main()` koordiniert den Ablauf, während Fachregeln in benannten Methoden liegen.
