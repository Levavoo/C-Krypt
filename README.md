# C# Krypt

`C# Krypt` ist ein ausführbares Konsolenprojekt für Visual Studio. Es verbindet die Anforderungen der Quests 1.1 bis 1.20 zu einem durchgängigen kleinen Spiel und dokumentiert die Lernschritte in drei Markdown-Dateien.

## Voraussetzungen

- Visual Studio 2022 mit der Workload **.NET-Desktopentwicklung**, oder
- .NET 8 SDK und ein beliebiger C#-Editor

Das Projekt verwendet bewusst die klassische Struktur mit `Program` und `Main()`. Es enthält noch keine Schleifen, keine selbst geschriebenen Methoden und keine eigenen Fachklassen.

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
│   └── Quests_1.16_bis_1.20.md
├── start.bat
├── start.ps1
└── README.md
```

## Ablauf des Programms

Das Programm:

1. zeigt den Krypta-Prolog,
2. legt den Zustand des Helden an,
3. liest und bereinigt den Namen,
4. liest die Klassenwahl,
5. würfelt vier W6 und streicht genau einen niedrigsten Wurf,
6. prüft einen von drei Zugängen,
7. führt nur bei gültigem Zugang eine W20-Probe durch,
8. untersucht innerhalb der Krypta eine Inschrift,
9. begrenzt die Lebenspunkte nach einer Heilung mit `Math.Clamp()`,
10. zeigt den vollständigen Endstand.

## Gültige Eingaben

Die Quests verlangen für Zahlen ausdrücklich `int.Parse()`. Deshalb müssen Klassenwahl, Zugang und Heilung als gültige ganze Zahlen eingegeben werden. Eine robuste Wiederholung mit `int.TryParse()` wäre ein sinnvoller Ausbau für Kapitel 2, gehört aber noch nicht zu diesem Lernstand.

Beispiele für einen vollständigen Lauf:

```text
Name: Arin
Klasse: 2
Zugang: 2
Inschrift: Ich trage die Krone
Heilung: 3
```

Bei einer ungültigen Zugangszahl wie `9` wird kein W20 gewürfelt. Der Held betritt die Krypta nicht; Inschrift und Heiltrank werden dann übersprungen.

## Empfohlene Testfälle

| Test | Erwartetes Verhalten |
|---|---|
| Leerer oder nur aus Leerzeichen bestehender Name | Der Name wird zu `Namenlos` |
| Klassenwahl `1`, `2`, `3` | Krieger, Schurke oder Magier |
| Andere ganzzahlige Klassenwahl | Abenteurer |
| Zugang `1`, `2`, `3` | Passender Zugangstext und genau eine W20-Probe |
| Zugang `9` | Fehlermeldung, kein W20, `imDungeon` bleibt `false` |
| Leere Inschrift | Sichere Meldung, kein Zugriff auf Zeichen `0` |
| Inschrift mit `Krone` | Rune leuchtet |
| Sehr hohe Heilung | Lebenspunkte enden höchstens bei `20` |
| Negative Heilung | Lebenspunkte enden mindestens bei `0` |

Da die Würfe zufällig sind, ändern sich Stärke, W20 und Probenergebnis zwischen den Läufen.
