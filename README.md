# C# Krypt

`C# Krypt` ist ein ausführbares Konsolen-Lernprojekt für Visual Studio. Kapitel 3 baut auf den ersten beiden Wochen auf und ersetzt lose Variablen sowie parallele Gegnerarrays durch Klassen, Objekte, Properties und klar getrennte Verantwortungen.

## Voraussetzungen

- Visual Studio 2022 mit der Workload **.NET-Desktopentwicklung**, oder
- .NET 8 SDK und ein beliebiger C#-Editor

Das Projekt verwendet bewusst eine klassische `Program`-Klasse mit `Main()`. Alle eigenen Typen liegen im Namespace `KryptaSpiel`.

## Start in Visual Studio

1. Öffne `CSharpKrypt.sln`.
2. Warte, bis Visual Studio das Projekt geladen hat.
3. Starte mit `F5` oder ohne Debugging mit `Strg+F5`.

Alternativ kann unter Windows `start.bat` oder `start.ps1` ausgeführt werden. Beide Skripte suchen das Projekt relativ zu ihrem Speicherort.

## Projektstruktur

```text
C# Krypt/
├── CSharpKrypt.sln
├── CSharpKrypt/
│   ├── CSharpKrypt.csproj
│   ├── Program.cs
│   ├── Spiel.cs
│   ├── Spieler.cs
│   ├── Gegner.cs
│   └── Wuerfel.cs
├── Lernmaterial/
│   ├── Quests_1.1_bis_1.8.md
│   ├── Quests_1.9_bis_1.15.md
│   ├── Quests_1.16_bis_1.20.md
│   ├── Quests_2.1_bis_2.6.md
│   ├── Quests_2.6_bis_2.11.md
│   ├── Quests_2.11_bis_2.14.md
│   ├── Quest_2.0_und_Bonusquests.md
│   ├── Quests_3.0_bis_3.3.md
│   ├── Quests_3.4_bis_3.7.md
│   ├── Quests_3.8_bis_3.10.md
│   └── Kapitel_3_Pruefungen_und_Vertiefung.md
├── start.bat
├── start.ps1
└── README.md
```

## Architektur in Kapitel 3

| Datei | Verantwortung |
|---|---|
| `Program.cs` | Erzeugt `Spiel` und startet den Ablauf. |
| `Spiel.cs` | Koordiniert Eingaben, Objekterzeugung und Kampf. |
| `Spieler.cs` | Schützt Heldenzustand und berechnet Heldenwerte. |
| `Gegner.cs` | Schützt Gegnerzustand und berechnet Gegnerschaden. |
| `Wuerfel.cs` | Stellt zustandslose Würfelmethoden bereit. |

`Spiel` greift nicht direkt auf geschützte Lebenspunkte zu. Schaden und Heilung laufen über Methoden der betroffenen Objekte. So bleibt jede Zustandsregel an genau einem Ort.

## Ablauf des Programms

Das Programm:

1. zeigt den Titel der Krypta,
2. liest und bereinigt den Heldennamen,
3. würfelt Stärke, Geschicklichkeit und Intelligenz mit 4W6 ohne den niedrigsten Wurf,
4. validiert die Klassenwahl mit `LiesZahl()`,
5. erhöht genau das passende Klassenattribut um 1,
6. erzeugt ein vollständiges `Spieler`-Objekt,
7. erzeugt einen Gegner über Konstruktorverkettung,
8. zeigt die abgeleiteten Kampfwerte,
9. führt einen rundenbasierten Objektkampf aus,
10. beendet den Ablauf kontrolliert mit Sieg oder Niederlage.

## Kampfregeln

- Krieger nutzen Stärke, Schurken Geschicklichkeit und Magier Intelligenz für den Angriffsbonus.
- Angriffsbonus: passendes Attribut geteilt durch 3.
- Spielerrüstung: `10 + Geschicklichkeit / 6`.
- Spielerschaden: `1W6`.
- Der Standard-Goblin besitzt 10 LP, Rüstung 11, Angriffsbonus 2 und verursacht `1W4` Schaden.
- Eine natürliche `1` verfehlt immer.
- Eine natürliche `20` trifft immer.
- Sonst trifft `W20 + Bonus >= Rüstung`.
- Nach einem tödlichen Spielerangriff gibt es keinen Gegenangriff.

## Robuste Eingabe

Die Klassenwahl akzeptiert nur ganze Zahlen von 1 bis 3. `LiesZahl()` verwendet `int.TryParse()` und fragt nach einer ungültigen Eingabe erneut.

Beispiel:

```text
Name des Helden: Arin
Klasse: 2
```

Würfe und Kampfausgang sind zufällig und ändern sich zwischen den Läufen.

## Empfohlene Tests

| Test | Erwartetes Verhalten |
|---|---|
| Leerer Name | Spieler heißt `Namenlos`. |
| Klassenwahl `abc`, `0` oder `4` | Fehlermeldung und erneute Eingabe. |
| Zwei getrennte Gegnerobjekte | Schaden an einem verändert den anderen nicht. |
| Schaden über vorhandene LP | Lebenspunkte enden bei 0. |
| Heilung über das Maximum | Spieler-LP enden bei 20. |
| Krieger, Schurke, Magier | Das jeweils passende Attribut bestimmt den Angriff. |
| Natürliche 1 oder 20 | Automatischer Fehlschlag beziehungsweise Treffer. |
| Tödlicher Spielerangriff | Gegner greift nicht mehr an. |

Die genauen temporären Testschritte und freiwilligen Referenzexperimente stehen im Lernmaterial. Der ausführbare Endstand enthält bewusst keinen Testcode und noch keine Räume, Listen, Dictionaries, Enums oder Vererbung.
