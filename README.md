# C# Krypt

`C# Krypt` ist ein ausführbares Konsolenprojekt für Visual Studio. Es verbindet die Anforderungen aus Kapitel 1 und 2 zu einem durchgängigen kleinen Spiel. Der Quelltext wächst dabei vom linearen Programm zu einem methodenbasierten Ablauf mit validierten Eingaben, Arrays, Schleifen und einem ersten Kampf.

## Voraussetzungen

- Visual Studio 2022 mit der Workload **.NET-Desktopentwicklung**, oder
- .NET 8 SDK und ein beliebiger C#-Editor

Das Projekt verwendet bewusst die klassische Struktur mit `Program` und `Main()`. Kapitel 2 ergänzt eigene statische Methoden, Parameter, Rückgabewerte, Überladungen, Standardparameter, Arrays sowie `for`-, `foreach`- und `while`-Schleifen. Eigene Fachklassen folgen erst in einem späteren Kapitel.

## Start in Visual Studio

1. Öffne `CSharpKrypt.sln`.
2. Warte, bis Visual Studio das Projekt geladen hat.
3. Starte es mit `F5` oder mit **Starten ohne Debugging** über `Strg+F5`.

Alternativ kann unter Windows `start.bat` oder `start.ps1` ausgeführt werden. Beide Skripte suchen das Projekt relativ zu ihrem eigenen Speicherort; der entpackte Ordner kann deshalb verschoben werden.

## Projektstruktur

```text
C# Krypt/
├── CSharpKrypt.sln
├── CSharpKrypt/
│   ├── CSharpKrypt.csproj
│   └── Program.cs
├── Lernmaterial/
│   ├── Quests_1.1_bis_1.8.md
│   ├── Quests_1.9_bis_1.15.md
│   ├── Quests_1.16_bis_1.20.md
│   ├── Quests_2.1_bis_2.6.md
│   ├── Quests_2.6_bis_2.11.md
│   ├── Quests_2.11_bis_2.14.md
│   └── Quest_2.0_und_Bonusquests.md
├── start.bat
├── start.ps1
└── README.md
```

## Ablauf des Programms

Das Programm:

1. zeigt den Krypta-Prolog,
2. legt den Zustand des Helden an,
3. liest und bereinigt den Namen,
4. erzeugt Stärke, Geschicklichkeit und Intelligenz mit derselben 4W6-Methode,
5. validiert Klassen- und Zugangswahl mit `LiesZahl()`,
6. verstärkt über einen `switch` das passende Klassenattribut,
7. ordnet jedem Zugang über einen `switch`-Ausdruck das passende Attribut zu,
8. führt die W20-Probe über `IstProbeBestanden()` durch,
9. untersucht innerhalb der Krypta eine Inschrift,
10. begrenzt die Lebenspunkte nach einer Heilung,
11. durchläuft ein Wächterregister mit `for` und `foreach`,
12. wählt über einen gemeinsamen Index einen vollständigen Gegnerdatensatz,
13. führt einen rundenbasierten Kampf aus,
14. zeigt den vollständigen Endstand über `ZeigeStatus()`.

## Gültige Eingaben

Kapitel 2 ersetzt die direkten `int.Parse()`-Aufrufe für interaktive Auswahlwerte durch `LiesZahl()`. Die Methode verwendet `int.TryParse()` und wiederholt die Eingabe, bis eine ganze Zahl innerhalb des verlangten Bereichs vorliegt.

Beispiele für einen vollständigen Lauf:

```text
Name: Arin
Klasse: 2
Zugang: 2
Inschrift: Ich trage die Krone
Heilung: 3
```

Bei einer ungültigen Eingabe erklärt `LiesZahl()` den Fehler und fragt erneut. Erst eine gültige Zahl beendet die Eingabeschleife; deshalb erreicht der Ablauf die nächste Spielphase nur mit einer erlaubten Klassen- oder Zugangswahl.

## Empfohlene Testfälle

| Test | Erwartetes Verhalten |
|---|---|
| Leerer oder nur aus Leerzeichen bestehender Name | Der Name wird zu `Namenlos` |
| Klassenwahl `1`, `2`, `3` | Krieger, Schurke oder Magier |
| Klassenwahl `abc`, leer, `0` oder `4` | Verständliche Ablehnung und erneute Eingabe |
| Zugang `1`, `2`, `3` | Passender Zugangstext und genau eine W20-Probe |
| Zugang `9` | Bereichsmeldung und erneute Eingabe; noch kein W20 |
| Leere Inschrift | Sichere Meldung, kein Zugriff auf Zeichen `0` |
| Inschrift mit `Krone` | Rune leuchtet |
| Heilung kleiner `0` oder größer `20` | Bereichsmeldung und erneute Eingabe |
| Hohe gültige Heilung | Lebenspunkte enden höchstens bei `20` |

Da die Würfe zufällig sind, ändern sich Attribute, Gegnerauswahl, Kampf und Probenergebnis zwischen den Läufen.

## Kampfregel des Lernprojekts

Die Aufgabenstellung definiert den Trefferwurf, aber keine Schadenshöhe. Das Projekt ergänzt deshalb eine kleine nachvollziehbare Regel:

- Ein erfolgreicher Spielerangriff verursacht `1W6` Schaden.
- Ein überlebender Wächter verursacht mit seinem Gegenangriff `1W4` Schaden.
- Eine natürliche `1` verfehlt immer, eine natürliche `20` trifft immer.
- Sonst muss `W20 + Angriffsbonus` mindestens die Rüstung erreichen.
- Ein besiegter Gegner darf nicht mehr zurückschlagen.
