# Kapitel 3 – Quests 3.8 bis 3.10

## Lernziel

Du bündelst den Helden in `Spieler`, lässt zwei Objekte über ihre öffentlichen Schnittstellen kämpfen und reduzierst `Program` auf den eigentlichen Programmeinstieg.

## Quest 3.8 – Der Spieler als Objekt

`Spieler` schützt seinen Zustand und berechnet seine eigenen Kampfwerte. Die Lebenspunkte dürfen von außen nicht beliebig gesetzt werden.

Wichtige Properties:

```csharp
public string Name { get; }
public string HeldenKlasse { get; }
public int Staerke { get; }
public int Geschicklichkeit { get; }
public int Intelligenz { get; }
public int MaximaleLebenspunkte { get; }
public int Lebenspunkte { get; private set; }
```

Der Konstruktor setzt maximale und aktuelle Lebenspunkte auf 20. `ErhalteSchaden()` begrenzt bei 0, `Heile()` beim Maximum. Negative Werte werden ignoriert.

Der Klassenname entscheidet, welches Attribut den Angriff bestimmt:

```csharp
public int AngriffsBonus()
{
    if (HeldenKlasse == "Krieger") return Staerke / 3;
    if (HeldenKlasse == "Schurke") return Geschicklichkeit / 3;
    return Intelligenz / 3;
}
```

Weitere Formeln:

```csharp
public int Ruestungswert()
{
    return 10 + Geschicklichkeit / 6;
}

public int WuerfleSchaden()
{
    return Wuerfel.Wuerfeln(6);
}
```

Wichtig: Der Klassenbonus `+1` wird bei der Erschaffung genau einmal auf das passende Attribut addiert. `AngriffsBonus()` addiert ihn nicht erneut.

## Quest 3.9 – Vollständiger Objektkampf

`Spiel` koordiniert. `Spieler` und `Gegner` verwalten ihren Zustand selbst.

Für beide Angriffe gilt dieselbe Trefferregel:

```csharp
private bool Trifft(int w20, int bonus, int ruestung)
{
    if (w20 == 1) return false;
    if (w20 == 20) return true;
    return w20 + bonus >= ruestung;
}
```

Die Reihenfolge einer Runde ist entscheidend:

1. Spieler würfelt W20.
2. Bei einem Treffer erhält der Gegner Schaden.
3. Sofort prüfen, ob der Gegner besiegt ist.
4. Nur ein lebender Gegner darf zurückschlagen.
5. Nach einer vollständigen Runde beide LP-Stände ausgeben.

Das Schleifengerüst:

```csharp
while (!spieler.IstBesiegt() && !gegner.IstBesiegt())
{
    // Spielerangriff
    if (gegner.IstBesiegt())
    {
        break;
    }

    // Gegnerangriff
}
```

## Quest 3.10 – Spiel koordiniert, Program startet

`Spiel.ErstelleSpieler()`:

- liest den Namen,
- würfelt drei Attribute über `Wuerfel.AttributAuswuerfeln()`,
- liest die Klassenwahl robust mit `LiesZahl(1, 3)`,
- erhöht genau das passende Attribut um 1,
- gibt ein vollständiges `Spieler`-Objekt zurück.

`Spiel.Starten()` erzeugt Spieler und Gegner, zeigt den Status, startet den Kampf und meldet Sieg oder Niederlage.

`Program.cs` enthält keine Spielregel mehr:

```csharp
namespace KryptaSpiel;

internal class Program
{
    static void Main()
    {
        Spiel spiel = new Spiel();
        spiel.Starten();
    }
}
```

Damit sind die Verantwortungen klar:

| Klasse | Verantwortung |
|---|---|
| `Program` | Programmeinstieg |
| `Spiel` | Eingaben und Ablauf koordinieren |
| `Spieler` | Heldenzustand und Heldenregeln |
| `Gegner` | Gegnerzustand und Gegnerregeln |
| `Wuerfel` | zustandslose Zufallswürfe |

## Was du jetzt können solltest

- Erklären, warum Lebenspunkte nicht in `Spiel` gesetzt werden.
- Die natürliche 1 und 20 vor der normalen Trefferformel prüfen.
- Einen Gegenangriff nach einem tödlichen Treffer verhindern.
- Ein Objekt aus einer Methode zurückgeben.
- `Program.Main()` auf den Start des Spielobjekts reduzieren.

## Checkliste

- [ ] Spieler startet mit 20/20 LP.
- [ ] Heilung endet höchstens bei 20.
- [ ] Krieger, Schurke und Magier verwenden das passende Attribut.
- [ ] Spieler und Gegner verwenden dieselbe Trefferlogik.
- [ ] Ein besiegter Gegner greift nicht mehr an.
- [ ] Alte Parallelarrays und lose Kampf-LP sind entfernt.
- [ ] Der Ablauf funktioniert über genau fünf C#-Dateien.
