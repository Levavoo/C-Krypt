# Quests 2.11 bis 2.14

Quest 2.11 ist die Eingangsbedingung für diesen Abschnitt: Erst wenn Auswahlwerte zuverlässig validiert werden, können zusammengehörige Gegnerdaten ausgewählt und sicher an eine Kampfmethode übergeben werden.

## Quest 2.11 als Ausgangspunkt

```csharp
int klassenWahl = LiesZahl(1, 3);
int zugangsWahl = LiesZahl(1, 3);
```

Die Methode garantiert nach ihrer Rückkehr, dass der Wert im verlangten Bereich liegt. Nachfolgende Programmteile benötigen deshalb keine erneute Prüfung derselben Eingabe.

## Quest 2.12 Der Wächter erhält Werte

**Ziel:** Name, Lebenspunkte und Rüstung desselben Gegners mit einem gemeinsamen Index auswählen.

**Neue Konzepte:** parallele Arrays, gemeinsamer Index, Längenkonsistenz.

```csharp
string[] gegnerNamen =
{
    "Knochenwächter",
    "Gruftspinne",
    "Steingolem",
    "Schattenpriester"
};

int[] gegnerLebenspunkte = { 10, 8, 14, 12 };
int[] gegnerRuestung = { 11, 10, 14, 12 };
```

Konsistenzprüfung:

```csharp
bool paralleleArraysGueltig =
    gegnerNamen.Length == gegnerLebenspunkte.Length &&
    gegnerNamen.Length == gegnerRuestung.Length;
```

Gemeinsame Auswahl:

```csharp
int gegnerIndex = Random.Shared.Next(gegnerNamen.Length);
string gegnerName = gegnerNamen[gegnerIndex];
int gegnerLp = gegnerLebenspunkte[gegnerIndex];
int ruestung = gegnerRuestung[gegnerIndex];
```

Alle Werte an Position `2` beschreiben beispielsweise den Steingolem. Verschiedene Indizes würden Datensätze vermischen.

**Lerngewinn:** Parallele Arrays bilden einfache Datensätze ab. In einem späteren OOP-Kapitel kann daraus eine Klasse `Gegner` entstehen.

## Quest 2.13 Die Kammer schließt sich

**Ziel:** Den vollständigen Gegnerdatensatz an eine Kampfmethode übergeben und den veränderten Spielerzustand zurückerhalten.

```csharp
static int FuehreKampfAus(
    int spielerLp,
    int spielerBonus,
    string gegnerName,
    int gegnerLp,
    int gegnerRuestung)
{
    Console.WriteLine($"Kampf gegen {gegnerName}");
    Console.WriteLine($"Spieler-LP: {spielerLp}");
    Console.WriteLine($"Gegner-LP: {gegnerLp}");
    Console.WriteLine($"Gegnerrüstung: {gegnerRuestung}");

    return spielerLp;
}
```

Aufruf:

```csharp
lebensPunkte = FuehreKampfAus(
    lebensPunkte,
    angriffsBonus,
    gegnerName,
    gegnerLp,
    ruestung);
```

Die Zuweisung ist entscheidend: Der Rückgabewert ersetzt den bisherigen Lebenspunktestand in `Main()`.

## Quest 2.14 Der Wächter der ersten Kammer

**Ziel:** Den vorbereiteten Datentransport zu einem vollständigen rundenbasierten Kampf erweitern.

**Neue Konzepte:** zustandsgesteuerte `while`-Schleife, Abbruch mit `break`, priorisierte Trefferregeln, frühes Verhindern eines Gegenangriffs.

Die Aufgabenstellung nennt keine Schadensformel. Das Lernprojekt verwendet deshalb diese klare Ergänzung:

- Ein erfolgreicher Spielerangriff verursacht `1W6` Schaden.
- Ein noch lebender Gegner verursacht beim Gegenangriff `1W4` Schaden.
- Die natürliche `1` verfehlt immer.
- Die natürliche `20` trifft immer.
- Sonst trifft `W20 + Angriffsbonus`, wenn das Ergebnis mindestens der Rüstung entspricht.

Kern der Kampfschleife:

```csharp
while (spielerLp > 0 && gegnerLp > 0)
{
    int spielerW20 = Wuerfeln();
    bool spielerTrifft = IstProbeBestanden(
        spielerW20,
        spielerBonus,
        gegnerRuestung);

    if (spielerTrifft)
    {
        int schaden = Wuerfeln(6);
        gegnerLp = Math.Max(0, gegnerLp - schaden);
    }

    if (gegnerLp <= 0)
    {
        break; // Kein Gegenangriff eines besiegten Gegners.
    }

    int gegenSchaden = Wuerfeln(4);
    spielerLp = Math.Max(0, spielerLp - gegenSchaden);

    Console.WriteLine($"Spieler-LP: {spielerLp}");
    Console.WriteLine($"Gegner-LP: {gegnerLp}");
}

return spielerLp;
```

Die Schleifenbedingung beschreibt unmittelbar das Ende des Kampfes: Sobald eine Seite keine Lebenspunkte mehr besitzt, gibt es keine weitere Runde. Die zusätzliche Prüfung nach dem Spielerangriff verhindert, dass ein bereits besiegter Gegner noch Schaden verursacht.

**Gezielte Tests:** Für die Sonderfälle kann `spielerW20` vorübergehend auf `1` beziehungsweise `20` gesetzt werden. Danach muss wieder `Wuerfeln()` verwendet werden.

## Abschnittscheck

- Klassen- und Zugangseingaben werden vor der Verwendung validiert.
- Drei gleich lange Arrays beschreiben jeweils denselben Gegnerindex.
- Alle fünf Kampfparameter kommen sichtbar in der Methode an.
- Die Kampfschleife endet sicher mit Sieg oder Niederlage.
- Ein besiegter Gegner führt keinen Gegenangriff mehr aus.
- Die verbleibenden Spieler-LP werden an `Main()` zurückgegeben.
