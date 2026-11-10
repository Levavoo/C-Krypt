# Kapitel 3 – Quests 3.4 bis 3.7

## Lernziel

Du entwirfst eine kleine öffentliche Schnittstelle, trennst Objektverhalten von zustandslosen Hilfsmethoden und ordnest das Projekt in mehrere Dateien mit einem gemeinsamen Namespace.

## Quest 3.4 – Öffentliche Schnittstelle, private Umsetzung

Andere Klassen dürfen fragen, ob ein Gegner besiegt ist. Wie die Lebenspunkte begrenzt werden, bleibt ein internes Detail.

```csharp
public bool IstBesiegt()
{
    return Lebenspunkte == 0;
}

private void BegrenzeLebenspunkte()
{
    Lebenspunkte = Math.Max(0, Lebenspunkte);
}
```

`public` ist Teil der bewusst angebotenen Klassenschnittstelle. `private` ist nur innerhalb derselben Klasse erreichbar. Dadurch kann die interne Umsetzung später geändert werden, ohne alle Aufrufer umzubauen.

## Quest 3.5 – Würfelmethoden an einen Ort verschieben

Würfeln gehört nicht zum individuellen Zustand eines Gegners. Deshalb wird eine zustandslose statische Hilfsklasse verwendet:

```csharp
public static class Wuerfel
{
    public static int Wuerfeln(int seiten)
    {
        return Random.Shared.Next(1, seiten + 1);
    }

    public static int W20()
    {
        return Wuerfeln(20);
    }
}
```

Der Gegner kennt dagegen seinen eigenen Schadenswürfel und bietet sein Verhalten als Instanzmethode an:

```csharp
public int WuerfleSchaden()
{
    return Wuerfel.Wuerfeln(SchadensWuerfel);
}
```

Der Unterschied:

| Art | Gehört zu | Beispielaufruf |
|---|---|---|
| Instanzmember | einem konkreten Objekt | `gegner.WuerfleSchaden()` |
| statischer Member | der Klasse selbst | `Wuerfel.W20()` |

Nicht alles sollte `static` sein. Statische Lebenspunkte würden von allen Spielern gemeinsam verwendet.

## Quest 3.6 – Wertkopie und Referenzkopie

Bei `int` wird der Wert kopiert:

```csharp
int a = 10;
int b = a;
b = 4;
// a bleibt 10.
```

Bei einer Klassenvariable wird die Referenz kopiert. Beide Variablen können dann dasselbe Objekt erreichen:

```csharp
Gegner erster = new Gegner("Goblin", 10, 11, 2, 4);
Gegner zweiter = erster;
zweiter.ErhalteSchaden(3);
// erster.Lebenspunkte und zweiter.Lebenspunkte sind beide 7.
```

Eine Referenz darf ausdrücklich leer sein:

```csharp
Gegner? gegnerImRaum = null;

if (gegnerImRaum != null)
{
    Console.WriteLine(gegnerImRaum.Name);
}
```

Das `?` sagt bei aktivierten Nullable-Referenztypen, dass `null` erlaubt ist. Vor einem Memberzugriff muss geprüft werden, ob wirklich ein Objekt vorhanden ist.

## Quest 3.7 – Dateien, Namespace und using

Jede Fachklasse erhält eine passend benannte Datei. Alle Dateien gehören zum selben Projekt und werden gemeinsam übersetzt.

```text
CSharpKrypt/
├── CSharpKrypt.csproj
├── Program.cs
├── Spiel.cs
├── Spieler.cs
├── Gegner.cs
└── Wuerfel.cs
```

Alle C#-Dateien beginnen einheitlich mit:

```csharp
using System;

namespace KryptaSpiel;
```

`namespace` ordnet Typnamen. `using System;` verkürzt Namen wie `System.Console` zu `Console`. Eigene `.cs`-Dateien werden nicht importiert; sie sind bereits Teil des Projekts.

## Was du jetzt können solltest

- Eine kleine öffentliche Schnittstelle begründen.
- `public`, `private`, `internal` und `static` einordnen.
- Instanzmethoden von statischen Methoden unterscheiden.
- Wertkopie und Referenzkopie an einem Beispiel erklären.
- Eine mögliche Nullreferenz sicher prüfen.
- Beschreiben, warum alle Projektdateien zusammen kompiliert werden.

## Checkliste

- [ ] `BegrenzeLebenspunkte()` ist privat.
- [ ] `IstBesiegt()` ist öffentlich.
- [ ] Würfelmethoden sind nur noch in `Wuerfel.cs` vorhanden.
- [ ] Alle fünf C#-Dateien verwenden `namespace KryptaSpiel;`.
- [ ] Das gesamte Projekt wird gestartet, nicht eine einzelne `.cs`-Datei.
