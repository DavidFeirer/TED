# TED - Aufteilung
| Aufgabe | Person | Aufwand |
|---|---|---|
| Konzept + CodeBasis erstellen  | Alle | 4 Stunden |
| Aufgabe 1 - Mikro/Makro  | Selina Friesenbichler | 5 Stunden |
| Aufgabe 2 - Design  | Selina Friesenbichler | 3 Stunden |
| Aufgabe 3 - Implementierung I | David Feirer | 10 Stunden |
| Aufgabe 4 - Implementierung II | Kevin Mild | 6 Stunden |
| Aufgabe 5 - Implementierung III | Dario Wagner | 7 Stunden |
| Aufgabe 6 - Qualität & Monitoring | Selina Friesenbichler | 2 Stunden |
| Aufgabe 7 - BPEL | Dario Wagner | 2 Stunden |
| Aufgabe 8 - Präsi | Alle | 3 Stunden |
| Aufgabe 9 - Funktionstüchtiges Projekt | Alle | 1 1/2 Stunden |
| Aufgabe 10 - Zusatzaufgabe | / | / |

### Aufgabe 3
* Consul Agenten Starten
    * ```consul agent --dev```
* Fragenevaluierungsservice starten
    * ```dotnet run --launch-profile fragenevaluierung-1```
    * ```dotnet run --launch-profile fragenevaluierung-2```
    * ```dotnet run --launch-profile fragenevaluierung-3```
* Fragenservice starten

### Aufgabe 5
**Hinweis**  
Damit der Auswertungsservice über die Queue Einträge vom Antwort Service bekommt muss lokal ein RabbitMQ Server laufen.  
-> Dazu RabbitMQ Server auf der RabbitMQ Seite runterladen und starten. Ansonsten ist keine weitere Config notwendig.
