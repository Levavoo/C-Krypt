# Kapitel 3 – Quests 3.0 bis 3.3

## Lernziel

Du wandelst die Gegnerdaten aus Woche 2 von parallelen Variablen in echte Objekte um. Dabei lernst du den Unterschied zwischen Klasse und Objekt, ersetzt öffentliche Felder durch Properties und stellst mit einem Konstruktor einen gültigen Anfangszustand her.

## Quest 3.0 – Den Stand aus Woche 2 sichern

Bevor eine größere Umstrukturierung beginnt, muss der aktuelle Stand funktionieren. Starte das Programm und prüfe Namenseingabe, Klassenwahl, Würfe und Kampf. Erst danach wird umgebaut.

Wichtig für den späteren Vergleich:

- Der Held besitzt 20 Lebenspunkte.
- Die Klassen verwenden unterschiedliche Attribute.
- Ein Treffer nutzt W20, Bonus und Rüstung.
- Ein besiegter Gegner greift nicht mehr an.

Die Woche-2-Lösung ist der Ausgangspunkt, aber nicht die Endstruktur. Parallele Arrays und lose Kampfvariablen werden in Kapitel 3 ersetzt.

## Quest 3.1 – Zwei Wächter als Objekte

Eine Klasse ist ein Bauplan. Ein Objekt ist eine konkrete Instanz dieses Bauplans.

```csharp
public class Gegner
{
    public string Name = "Unbekannter Gegner";
    public int Lebenspunkte = 10;

    public void ErhalteSchaden(int schaden)
    {
        Lebenspunkte = Math.Max(0, Lebenspunkte - schaden);
    }
}
```

Mit zwei `new`-Aufrufen entstehen zwei unabhängige Objekte:

```csharp
Gegner goblin = new Gegner();
Gegner skelett = new Gegner();

goblin.Name = "Goblin";
skelett.Name = "Skelett";
skelett.ErhalteSchaden(5);
```

Der Goblin bleibt unverändert. Beide Variablen haben denselben Datentyp, zeigen aber auf verschiedene Instanzen.

### Zwischenprüfung

Erzeuge zwei Gegner, verletze nur einen und gib beide Lebenspunkte aus. Wenn nur ein Wert sinkt, ist die Objekttrennung verstanden.

## Quest 3.2 – Properties und Kapselung

Öffentliche Felder erlauben fremdem Code jede Änderung. Dann könnte ein Aufrufer `Lebenspunkte = -500` setzen. Properties begrenzen, was von außen erlaubt ist.

```csharp
public string Name { get; }
public int Lebenspunkte { get; private set; }
public int Ruestungswert { get; }
public int AngriffsBonus { get; }
public int SchadensWuerfel { get; }
```

`get` erlaubt das Lesen. `private set` erlaubt das Ändern nur innerhalb derselben Klasse. Schaden wird daher ausschließlich über eine Methode verarbeitet:

```csharp
public void ErhalteSchaden(int schaden)
{
    if (schaden <= 0)
    {
        return;
    }

    Lebenspunkte -= schaden;
    BegrenzeLebenspunkte();
}
```

Negative und null große Schadenswerte haben keine Wirkung. Sehr hoher Schaden endet bei 0.

## Quest 3.3 – Gültige Objekte durch Konstruktoren

Ein Konstruktor läuft automatisch bei `new`. Er hat den Namen der Klasse und keinen Rückgabetyp.

```csharp
public Gegner(
    string name,
    int lebensPunkte,
    int ruestungswert,
    int angriffsBonus,
    int schadensWuerfel)
{
    Name = string.IsNullOrWhiteSpace(name)
        ? "Unbekannter Gegner"
        : name.Trim();
    Lebenspunkte = Math.Max(1, lebensPunkte);
    Ruestungswert = ruestungswert;
    AngriffsBonus = angriffsBonus;
    SchadensWuerfel = Math.Max(1, schadensWuerfel);
}
```

Danach kann ein Gegner nicht mehr halb fertig erzeugt werden:

```csharp
Gegner goblin = new Gegner("Goblin", 10, 11, 2, 4);
Gegner skelett = new Gegner("Skelett", 8, 12, 1, 6);
```

## Was du jetzt können solltest

- Klasse, Objekt und Objektvariable unterscheiden.
- Erklären, warum zwei `new`-Aufrufe unabhängige Zustände erzeugen.
- Mit `private set` einen Wert nach außen schützen.
- Einen Konstruktor ohne Rückgabetyp schreiben.
- Beim Erzeugen einen gültigen Namen und mindestens 1 Start-LP sicherstellen.

## Checkliste

- [ ] `Gegner` liegt in `Gegner.cs`.
- [ ] Zwei Gegner beeinflussen einander nicht.
- [ ] Lebenspunkte sind außen nur lesbar.
- [ ] Negativer Schaden wird ignoriert.
- [ ] Hoher Schaden endet bei 0.
- [ ] Ein leerer Gegnername wird ersetzt.
- [ ] Ein Gegner startet mit mindestens 1 LP.
