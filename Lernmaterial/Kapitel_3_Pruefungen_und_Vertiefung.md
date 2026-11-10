# Kapitel 3 – Pflichtläufe, Prüfungen und freiwillige Vertiefung

## Die sechs Pflichtläufe

Diese Tests prüfen nicht nur, ob das Programm startet, sondern ob die Objektregeln korrekt verteilt sind. Temporäre Testzeilen danach wieder entfernen.

| Lauf | Vorbereitung | Erwartung |
|---|---|---|
| 1 | Zwei Gegner erzeugen, nur einen verletzen | Der zweite bleibt unverändert. |
| 2 | Gegner mehr Schaden als vorhandene LP geben | LP enden bei 0. |
| 3 | Spieler verletzen und über das Maximum heilen | LP enden bei 20. |
| 4 | Je einen Krieger, Schurken und Magier erzeugen | Passendes Attribut bestimmt den Angriff. |
| 5 | Kampf bis zum Sieg laufen lassen | Besiegter Gegner greift nicht mehr an. |
| 6 | Kampf bis zur Niederlage laufen lassen | Das Spiel endet kontrolliert. |

Beispiel für die ersten drei Prüfungen:

```csharp
Gegner erster = new Gegner("Goblin", 10, 11, 2, 4);
Gegner zweiter = new Gegner("Skelett", 8, 12, 1, 6);
erster.ErhalteSchaden(100);

Console.WriteLine(erster.Lebenspunkte); // 0
Console.WriteLine(zweiter.Lebenspunkte); // 8

Spieler held = new Spieler("Arin", "Krieger", 15, 12, 9);
held.ErhalteSchaden(7);
held.Heile(100);
Console.WriteLine(held.Lebenspunkte); // 20
```

## Gezielte Prüfung der Trefferregel

Setze nur den jeweiligen W20-Wert vorübergehend fest:

- `1` verfehlt trotz sehr hohem Bonus.
- `20` trifft trotz sehr hoher Rüstung.
- `W20 + Bonus == Rüstung` trifft wegen `>=`.

Danach die festen Werte wieder durch `Wuerfel.W20()` ersetzen.

## Prüfungsstopps

### Die Baupläne der Wächter

Erkläre ohne Vorlage:

- Klasse und Objekt,
- Feld und Property,
- `get`, `set` und `private set`,
- Aufgabe eines Konstruktors,
- warum zwei `new`-Aufrufe zwei unabhängige Zustände erzeugen.

### Der Spiegel der Instanzen

Erkläre ohne Vorlage:

- `public`, `private` und `internal`,
- Instanzmember und statische Member,
- Werttyp und Referenztyp,
- Bedeutung einer möglichen Nullreferenz,
- Unterschied zwischen `namespace` und `using`.

### Das Siegel der Objekte

Erkläre ohne Vorlage:

- welche Klasse welchen Zustand schützt,
- warum `Wuerfel` statisch ist,
- warum `Spiel` koordiniert, aber keine LP direkt setzt,
- warum `Program` nur noch den Spielstart enthält.

## Freiwillige Erweiterung A – Konstruktorverkettung

Der Endstand enthält einen bequemen Standardkonstruktor:

```csharp
public Gegner(string name)
    : this(name, 10, 11, 2, 4)
{
}
```

Die eigentliche Initialisierung bleibt im vollständigen Konstruktor. So müssen Gültigkeitsregeln nicht dupliziert werden.

## Freiwillige Erweiterung B – Referenzexperiment

```csharp
static void Trifft(Gegner ziel)
{
    ziel.ErhalteSchaden(3);
}

static void Ersetze(Gegner ziel)
{
    ziel = new Gegner("Skelett", 8, 12, 1, 6);
}
```

`Trifft` verändert das gemeinsam erreichbare Objekt. `Ersetze` ersetzt nur die lokale Kopie der Referenz. Die Variable des Aufrufers zeigt weiterhin auf das ursprüngliche Objekt.

Objektidentität kann geprüft werden:

```csharp
Gegner erster = new Gegner("Goblin");
Gegner zweiter = erster;
Gegner dritter = new Gegner("Goblin");

Console.WriteLine(object.ReferenceEquals(erster, zweiter)); // True
Console.WriteLine(object.ReferenceEquals(erster, dritter)); // False
```

Gleiche Daten bedeuten nicht dasselbe Objekt.

## Freiwillige Erweiterung C – Eigener Standardgegner

Entwirf einen Gegner und begründe jeden Wert:

```csharp
Gegner steinwaechter = new Gegner(
    "Steinwächter",
    14,
    14,
    1,
    6);
```

Prüfe anschließend:

- Der Name erklärt die Rolle.
- LP und Rüstung passen zur gewünschten Schwierigkeit.
- Angriffsbonus und Schadenswürfel sind nachvollziehbar.
- Jede Konstruktorvariante erzeugt sofort ein vollständiges Objekt.

## Abschlusscheck

Wenn du jede Frage in einem Satz beantworten kannst, sind die Kernideen verbunden:

1. Was beschreibt eine Klasse, und was erzeugt `new`?
2. Warum darf fremder Code Lebenspunkte nicht beliebig setzen?
3. Wie stellt ein Konstruktor einen gültigen Anfangszustand her?
4. Warum benötigt ein Würfelaufruf kein Würfelobjekt?
5. Warum kann eine Objektänderung über zwei Variablen sichtbar sein?
6. Was bedeutet `null`, und wie wird eine mögliche Nullreferenz markiert?
7. Was ordnet ein Namespace, und was verkürzt `using`?
8. Welche Klassen speichern Zustand, und welche Klasse koordiniert den Ablauf?
