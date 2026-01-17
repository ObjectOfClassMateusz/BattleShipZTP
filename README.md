⚓ Battleship

Projekt zaawansowanej gry w statki zrealizowany w języku C#, wykorzystujący architekturę opartą na wzorcach projektowych. 
Gra oferuje trzy unikalne tryby rozgrywki, od klasycznej bitwy po strategiczny wariant w świecie Warhammer 40k.

<img width="1220" height="766" alt="1" src="https://github.com/user-attachments/assets/3def3687-9a70-4df8-ad14-71aab86687c9" />

🎮 Tryby Rozgrywki (Gameplay Modes)

Projekt oferuje trzy główne moduły, z których każdy wprowadza nowe mechaniki i wyzwania.

1. Classic Mode (Klasyczna Bitwa)
   
To wierne odwzorowanie tradycyjnej gry w statki, skupione na czystej logice i zgadywaniu pozycji przeciwnika.

  Plansza: Standardowy wymiar 12x12 pól.

  Flota: Gracz otrzymuje pełną, zbalansowaną flotę startową (od potężnych Lotniskowców po małe Okręty Podwodne).
  
  Mechanika: Brak dodatkowych zasobów – liczy się tylko celność Twoich strzałów.
  
<img width="1080" height="496" alt="image" src="https://github.com/user-attachments/assets/def22c9f-9f90-4117-8ccb-7bb51c273950" />

2. Single Mode (Pojedynek / Duel)
   
Tryb przeznaczony do błyskawicznych rozgrywek, gdzie jeden błąd może oznaczać koniec partii.

  Mini-Plansza: Rozgrywka toczy się na ograniczonym obszarze 5x5 pól.
  
  Jeden Cel: Każdy gracz dysponuje tylko jednym statkiem (Pancernikiem).
  
  Zasada: Kto pierwszy namierzy jednostkę wroga, wygrywa pojedynek.

3. Warhammer 40k Mode (Strategia Ekonomiczna)

Najbardziej rozbudowany tryb, który przekształca grę w statki w bitwę strategiczną z systemem zarządzania zasobami.

  Zarządzanie Zasobami i Frakcje
  
  System Walut: Gracze dysponują Requisition (1750), Energy (210) oraz Action Points (2) na start.
  
  Wybór Armii: Możliwość gry różnymi frakcjami (np. Drukhari, Saxony Empire), które posiadają unikalne modele statków ASCII.
  
<img width="1163" height="573" alt="image" src="https://github.com/user-attachments/assets/624e9a05-2205-49d8-a49f-d0f938764717" />

🤖 Poziomy Trudności AI

Gra oferuje trzy inteligentne tryby przeciwnika, z których każdy wykorzystuje inne algorytmy decyzyjne:

  🟢 Easy (Simple AI): Zapamiętuje oddane strzały i po trafieniu ma 70% szans na atakowanie sąsiednich pól. Unika strzelania bezpośrednio przy zatopionych wrakach.
  
  🟡 Medium (Medium AI): Wykorzystuje algorytm przeszukiwania obszarów (BFS), aby identyfikować największe puste przestrzenie na planszy i tam kierować ogień.
  
  🔴 Hard (Hard AI): Najbardziej efektywny tryb. Łączy analizę obszarów z metodą szachownicy (strzały co drugie pole), co pozwala mu wykryć każdy statek przy minimalnej liczbie prób.

⌨️ Obsługa i Skróty 

Strzałki/WSAD - Poruszanie kursorem po planszy

Enter - Potwierdzenie strzału/Rozstawienie statku

R - Rotacja statku podczas rozstawiania (jeśli tryb pozwala)

B - Otwarcie Zbrojowni (Tylko tryb Warhammer)

🛠️ Konfiguracja Terminala

Aby gra wyświetlała się poprawnie (zwłaszcza grafiki ASCII i okna zakupów), należy skonfigurować terminal:

  Rozmiar Okna: 160x60 znaków.
  
  Tryb Uruchamiania: W Riderze/Visual Studio ustaw opcję "Run in external console".
  
  Czcionka: Zalecana czcionka monospaced (np. Consolas, Cascadia Code).

⚙️ Technologie:

Język: C# 12 / .NET 8.

Biblioteki: NAudio (dźwięk), System.Console (interfejs graficzny).

🏗️ Architektura i Wzorce Projektowe

Projekt został zaprojektowany zgodnie z zasadami czystego kodu i wykorzystuje następujące wzorce:

  Command: Obsługa akcji na planszy (PlaceCommand, AttackCommand).
  
  Factory: Tworzenie statków (ShipFactory) i trybów gry (GameModeFactory).
  
  Proxy: Zarządzanie dostępem do planszy bitwy (BattleBoardProxy).
  
  Observer: Logowanie zdarzeń i śledzenie statystyk przez ActionManager.
  
  Memento: Zapisywanie stanu planszy do celów powtórek (Replay).
  
  Strategy: Dynamiczna wymiana logiki i zasad gry poprzez interfejs IGameMode.
  
  Singleton: Globalny dostęp do unikalnych instancji zarządzających dźwiękiem (AudioManager) i ustawieniami.
  
  Template: Definiowanie szkieletu działania gry w klasie bazowej Scenario, którą rozwijają konkretne scenariusze.
  
  Facade: Uproszczony interfejs do zarządzania rysowaniem elementów UI za pomocą klasy Drawing.
