# 🗡️ Console Roguelike RPG - Object-Oriented Design Project

An advanced console-based Roguelike game written in C# (.NET 9.0). This project was created as a comprehensive demonstration of applying software engineering best practices (SOLID, Loose Coupling) and classic design patterns (Gang of Four) in game architecture.

![Gameplay Screenshot](add_link_to_your_gameplay_screenshot_here.jpg)

---

## ✨ Gameplay Features

- **Procedural Dungeon Generation:** The board is built on the fly based on a randomized Theme (Library, Treasury, Metal), which dictates the wall layouts, monster pools, and generated loot.
- **Advanced Sound Propagation:** Player actions (e.g., picking up heavy weapons) generate noise. Enemies utilize a BFS (Breadth-First Search) algorithm to determine if the sound reached them by tracing paths around walls, and they actively react to it.
- **Group Behaviors and Species:** Monsters belong to specific species. The death of one individual immediately affects the remaining living entities of that species on the map (e.g., Goblins panic and lose attack power, Skeletons become enraged and gain strength).
- **Active Ecosystem:** Enemies do not just stand still—they actively and randomly roam the dungeons. A *Zone of Control* mechanic ensures they stop wandering and engage in combat when the player approaches.
- **Deep Inventory & Combat:** Backpack management, consumable items, various attack types, and multi-layered statistics (Strength, Dexterity, Wisdom, etc.).

---

## 📐 Architecture & Design Patterns

The project places a massive emphasis on flexibility, modularity, and scalability. Below are the core design patterns utilized in the codebase:

### 1. Observer — *Custom Event Bus*

Implemented entirely from scratch **without using built-in C# events (`event`, `Action`)** to maintain the strict purity of the classic GOF pattern.

- **`SoundBus` & `DeathBus`**: These guarantee extreme loose coupling. The player "broadcasts" noise, and a dying enemy "broadcasts" their death. Other enemies independently decide to listen (`Attach`), calculate distances, and modify their own stats without direct object references.

### 2. Abstract Factory

- **`IDungeonTheme`**: Manages the thematic consistency of the dungeons. The factory ensures that Mages and "smart" books spawn in the `LibraryTheme`, and provides unique biome-specific artifacts. This completely removes the need for hardcoded `if`/`switch` type-checking during map generation.

### 3. Strategy

- **Logging (`ILogStrategy`)**: Allows dynamic swapping of the history-saving mechanism (currently `FileLogStrategy`), facilitating future expansions (e.g., Database logging) without modifying the core logging module.
- **Dungeon Layouts (`IDungeonLayout`)**: Separates the algorithms that carve corridors and chambers (e.g., `CorridorsLayout`, `CentralRoomLayout`) from the themes themselves, elegantly preventing Circular Dependencies.

### 4. Singleton

- **`GameLogger` & Event Buses**: Provide a global and safe access point for writing logs and broadcasting events across every module (Entity, Action, Board) without passing references through multiple constructors.

### 5. Decorator

- Utilized within the item system (e.g., `SmartDecorator`), allowing for dynamic and infinite stacking of stats, magical effects, and upgrades on base weapons and items.

---

## 🚀 Getting Started

### Requirements

- .NET 9.0 SDK
- A system terminal or console emulator with full UTF-8 support (e.g., Windows Terminal is highly recommended).

### Installation & Launch

1. Clone the repository:

```bash
git clone [link-to-your-repo]
```

2. Ensure the `config.ini` file is located in the root directory and contains the required data:

```ini
[Game]
PlayerName = YourName
LogPath = game_logs.txt
```

3. Build and run the project using the following command:

```bash
dotnet run --project OODProject
```
