```mermaid
classDiagram
    class ICommand {
      <<interface>>
      +Execute(coords)
      +GetBody()
      +SetBody(body)
      +PlaceCondition(x, y)
    }

    class AttackCommand {
      +IBattleBoard Board
      +Point Target
      +int PlayerID
      +string Nickname
      +Execute(coords)
    }

    class PlaceCommand {
      +BattleBoard Board
      +IShip Ship
      +int PlayerID
      +Execute(coords)
    }

    class TurretAttackCommand {
      -ITurret _turret
      +BattleBoard Board
      +Execute(coords)
    }

    class MoveCommand {
      +Execute(coords)
    }

    class IBattleBoard {
      <<interface>>
      +Display()
      +GetField(x, y)
      +PutCommand(command, silent)
      +AttackPoint(target)
    }

    class BattleBoard {
      -Field[,] _field
      +width int
      +height int
      +FieldsInitialization()
      +GetSaveState() BattleBoardMemento
      +Restore(memento)
    }

    class BattleBoardProxy {
      -BattleBoard _board
      +Display()
      +AttackPoint(target)
    }

    class Field {
      +int X
      +int Y
      +char Character
      +IShip ShipReference
    }

    class BattleBoardMemento {
      +Field[,] State
    }

    class IGameMode {
      <<interface>>
      +CreateBoard(x, y)
      +ShipmentDelivery()
      +AssignResources()
      +BuyShip(wallet)
    }

    class ClassicGameMode {
        +CreateBoard(x, y)
        +ShipmentDelivery()
    }

    class WarhammerGameMode {
        +CreateBoard(x, y)
        +AssignResources()
        +BuyShip(wallet)
    }

    class SimulationMode {
        +CreateBoard(x, y)
        +ShipmentDelivery()
    }

    class DuelGameMode {
        +CreateBoard(x, y)
        +ShipmentDelivery()
    }

    class GameModeFactory {
      <<abstract>>
      +GetGameMode() IGameMode*
    }

    class ClassicModeFactory {
        +GetGameMode() IGameMode
    }

    class WarhammerModeFactory {
        +GetGameMode() IGameMode
    }

    class AudioManager {
      <<singleton>>
      +Play(fileName)
      +Stop(fileName)
      +ChangeVolume(fileName, v)
    }

    class StatBar {
      +int value
      +int currentValue
      +Show()
      +Decrease(amount)
    }

    class IAI {
      <<interface>>
      +GetNextMove(width, height, board) Point
      +AddTargetNeighbors(hitPoint, width, height)
      +ClearTargets()
    }

    class BaseAI {
      <<abstract>>
      #Random _rnd
      #Queue~Point~ _targetsToHit
      #HashSet~Point~ _alreadyShot
      #Orientation _orientation
      +AddTargetNeighbors(hitPoint, width, height)
      +ClearTargets()
      #EnqueueIfValid(p, width, height)
    }

    class SimpleAI {
      +GetNextMove(width, height, board) Point
    }

    class MediumAI {
      +GetNextMove(width, height, board) Point
      -FindLargestUnshotArea(width, height) List~Point~
    }

    class HardAI {
      +GetNextMove(width, height, board) Point
    }

    class INetworkingProxy {
      <<interface>>
      +NetworkWriteAndReadStrings(v1, v2) Task
      +NetworkWriteAndReadAsync~T~(data) Task
    }

    class NetworkingProxy {
      <<sealed>>
      -TcpClient _client
      -StreamReader _reader
      -StreamWriter _writer
      -string _role
      +NetworkWriteAndReadAsync~T~(data) Task
    }

    class AIDifficulty {
      <<enumeration>>
      Easy
      Medium
      Hard
    }

    class NetworkingTaskState {
      <<enumeration>>
      None
      NameShipment
      CountShips
    }

    class ActionManager {
      <<singleton>>
      -static ActionManager _instance
      -List~IActionManager~ _observers
      +static Instance$ ActionManager
      +Attach(observer)
      +Detach(observer)
      +NotifyObservers(details)
      +LogAction(details)
      +ClearObservers()
    }

    class IActionManager {
      <<interface>>
      +Update(details)
    }

    class GameLogger {
      -Window _logWindow
      -UIController _ui
      +Update(details)
    }

    class StatisticTracker {
      -Dictionary~int, PlayerStats~ _allPlayerStats
      -List~GameActionDetails~ _actionHistory
      +Update(details)
      +GetHistory()
      +GetStats(playerId)
    }

    class GameActionDetails {
      +int PlayerID
      +string Nickname
      +string ActionType
      +Point Coords
      +HitResult Result
    }

    class PlayerStats {
      +int TotalShots
      +int Hits
      +int Misses
      +double Accuracy
    }

    class IScenario {
      <<interface>>
      +Act()
      +AsyncAct() Task
      +ConnectScenario(key, scenario)
    }

    class Scenario {
      <<abstract>>
      #Dictionary~string, IScenario~ _scenarios
      +Act()
      +AsyncAct() Task
      +ConnectScenario(key, scenario)
    }

    class MainMenuScenario {
      +AsyncAct() Task
    }

    class SingleplayerScenario {
      -IAI _ai
      -IGameMode _gameMode
      +Act()
    }

    class MultiplayerScenario {
      -INetworkingProxy _network
      +AsyncAct() Task
      -RunServer() Task
      -RunClient() Task
    }

    class SimulationScenario {
      -IAI _ai1
      -IAI _ai2
      +Act()
    }

    class VictoryScenario {
      -string _winnerName
      -StatisticTracker _stats
      +Act()
    }

    class ReplayScenario {
      -List~GameActionDetails~ _history
      +Act()
    }

    class ChooseGameModeScenario {
      -bool _multi
      +AsyncAct() Task
      -ChooseDifficulty() AIDifficulty
    }

    class OptionsScenario {
      -IWindowBuilder _builder
      -UIController _controller
      +AsyncAct() Task
    }

    class AuthorsScenario {
      -List~string~ _authors
      +AuthorsScenario()
      +AsyncAct() Task
    }

    class IWindowBuilder {
      <<interface>>
      +SetPosition(x, y)
      +SetSize(w, h)
      +AddComponent(component)
      +Build() Window
    }

    class WindowBuilder {
      -Window _window
      +Build() Window
    }

    class Window {
      -List~IComponentUI~ _components
      +DrawAndStart() string
      +Add(component)
    }

    class IComponentUI {
      <<interface>>
      +Print(width)
      +GetOption() string
      +HandleKey(key) string
    }

    class Button {
        -string _text
        +Print(width)
        +HandleKey(key) string
    }

    class CheckBox {
        -bool _checked
        +Print(width)
        +HandleKey(key) string
    }

    class TextBox {
        -string _content
        +Print(width)
        +HandleKey(key) string
    }

    class IntegerSideBar {
        -int _value
        +Print(width)
        +HandleKey(key) string
    }

    class UserSettings {
      <<singleton>>
      +string Nickname
      +int MusicVolume
      +UpdateSettings(options)
    }

    class Env {
      <<static>>
      +SetColor(f, b)
      +CursorPos(x, y)
      +Wait(ms)
    }

    class Drawing {
      <<static>>
      +DrawASCII(key, x, y)
      +DrawRectangleArea(x, y, w, h)
    }

    class UIController {
        -Window _activeWindow
        +DrawAndStart() string
    }

    class WindowUI {
        +Init()
    }

    class IShip {
      <<interface>>
      +Name() string
      +GetSize() int
      +TakeHit(coords, damage) HitResult
      +IsSunk() bool
    }

    class BaseShip {
      <<abstract>>
      #int size
      #List~Point~ placement
      #List~Tuple~ _body
      +Locate(coords)
    }

    class CarrierShip {
        +Name() "Carrier"
        +GetSize() 5
    }

    class BattleshipShip {
        +Name() "Battleship"
        +GetSize() 4
    }

    class DestroyerShip {
        +Name() "Destroyer"
        +GetSize() 3
    }

    class SubmarineShip {
        +Name() "Submarine"
        +GetSize() 3
    }

    class Advanced40KShip {
      <<abstract>>
      #StatBar _healthBar
      #int _health
      #List~ITurret~ _turrets
      +ShowHealthBar()
      +AudioPlayAttack()
      +TakeHit(coords, damage) HitResult
    }

    class ITurret {
      <<interface>>
      +GetAimBody() List
      +MinDmg() int
      +MaxDmg() int
      +ActionCost() int
    }

    class ShurikenCannon {
      +ActionCost() 1
      +MinDmg() 18
      +MaxDmg() 25
      +GetAimBody() List
    }

    class EisenhansArtyllery {
      +ActionCost() 3
      +MinDmg() 53
      +MaxDmg() 70
      +GetAimBody() List
    }

    class RaiderShip {
        +AudioPlayReady()
    }

    class EisenhansShip {
        +AudioPlayReady()
    }

    class FirePrismShip {
        +AudioPlayReady()
    }

    class ShipFactory {
      +CreateShip(shipType) IShip$
    }

    class Fraction {
      <<enumeration>>
      Drukhari
      BloodRavens
      BielTan
      SaxonyEmpire
    }

    ICommand <|.. AttackCommand
    ICommand <|.. PlaceCommand
    ICommand <|.. TurretAttackCommand
    ICommand <|.. MoveCommand
    IBattleBoard <|.. BattleBoard
    IBattleBoard <|.. BattleBoardProxy
    BattleBoardProxy --> BattleBoard
    BattleBoard *-- Field
    BattleBoard ..> BattleBoardMemento
    IGameMode <|.. ClassicGameMode
    IGameMode <|.. WarhammerGameMode
    IGameMode <|.. SimulationMode
    IGameMode <|.. DuelGameMode
    OptionsScenario ..> UserSettings
    UserSettings ..> AudioManager
    OptionsScenario ..> UIController
    GameModeFactory <|-- ClassicModeFactory
    GameModeFactory <|-- WarhammerModeFactory
    ClassicModeFactory ..> ClassicGameMode
    WarhammerModeFactory ..> WarhammerGameMode
    IAI <|.. BaseAI
    BaseAI <|-- SimpleAI
    SimpleAI <|-- MediumAI
    MediumAI <|-- HardAI
    INetworkingProxy <|.. NetworkingProxy
    ActionManager o-- IActionManager
    IActionManager <|.. GameLogger
    IActionManager <|.. StatisticTracker
    IActionManager ..> GameActionDetails
    StatisticTracker *-- PlayerStats
    IScenario <|.. Scenario
    Scenario <|-- MainMenuScenario
    Scenario <|-- ChooseGameModeScenario
    Scenario <|-- SingleplayerScenario
    Scenario <|-- MultiplayerScenario
    Scenario <|-- SimulationScenario
    Scenario <|-- VictoryScenario
    Scenario <|-- ReplayScenario
    Scenario <|-- OptionsScenario
    Scenario <|-- AuthorsScenario
    SingleplayerScenario ..> VictoryScenario
    SimulationScenario ..> VictoryScenario
    VictoryScenario ..> ReplayScenario
    IWindowBuilder <|.. WindowBuilder
    WindowBuilder o-- Window
    Window *-- IComponentUI
    IComponentUI <|.. Button
    IComponentUI <|.. CheckBox
    IComponentUI <|.. TextBox
    IComponentUI <|.. IntegerSideBar
    UIController ..> Window
    VictoryScenario ..> IWindowBuilder
    WindowUI ..> Env
    Advanced40KShip "1" *-- "many" ITurret
    ITurret <|.. ShurikenCannon
    ITurret <|.. EisenhansArtyllery
    IShip <|.. BaseShip
    BaseShip <|-- CarrierShip
    BaseShip <|-- BattleshipShip
    BaseShip <|-- DestroyerShip
    BaseShip <|-- SubmarineShip
    ShipFactory ..> IShip
    BaseShip <|-- Advanced40KShip
    Advanced40KShip <|-- EisenhansShip
    Advanced40KShip <|-- RaiderShip
    Advanced40KShip <|-- FirePrismShip
