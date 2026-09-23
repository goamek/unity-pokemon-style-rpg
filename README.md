# Unity Pokémon-Style RPG

The gameplay code from a top-down, Pokémon-style RPG I built in Unity (C#). Players explore an overworld, talk to NPCs, battle wild pokemon and trainers, and fight through a boss dungeon.

> This repo contains **source code only**. Art, scenes, and third-party libraries are left out, so it won't open as a Unity project. It's here to show how the game is built.

<!-- Add a screenshot or GIF here, e.g. ![Battle](docs/battle.gif) -->

## What's implemented

- **Turn-based battle system**: a state machine for action and move selection, turn order by move priority then speed, critical hits, accuracy/evasion, and fainting and switch-ins
- **Damage and stats**: main-series damage and stat formulas, stat stages from -6 to +6, and a full 18-type effectiveness chart
- **Status conditions**: poison, burn, paralysis, freeze, and sleep, each defined as data with hooks (`OnStart`, `OnBeforeMove`, `OnAfterTurn`)
- **Trainer AI encounters**: trainers spot the player through a field-of-view trigger, walk over, and start a battle
- **Tile-based movement**: grid-snapped movement with collision checks and directional sprite animation
- **Scene transitions**: portals fade out, load the next scene, and place the player at the matching portal
- **Game flow**: a top-level `GameController` state machine (free roam, battle, dialog, cutscene, paused) that routes input to the right system

## Code tour

```
Scripts/
├── GameController.cs     Top-level game state machine
├── Pokemon.cs            Pokemon instance: stats, HP, moves, status, damage
├── PokemonBase.cs        Species data (ScriptableObject) and the type chart
├── MoveBase.cs           Move data (ScriptableObject) and move effects
├── Battle/               Battle system, HUD, HP bar, party screen
├── Character/            Player, NPC, and trainer controllers; movement and animation
├── Core/                 Objects that persist between scenes, screen fader
├── Data/                 Status condition definitions
├── GamePlay/             Dialog, physics layers, grass encounters, interaction interfaces
├── SceneManagement/      Scene portals
└── Util/                 Frame-based sprite animator
```

**Good places to start reading:**

- [`Battle/BattleSystem.cs`](Scripts/Battle/BattleSystem.cs): the battle loop and turn resolution
- [`Data/ConditionsDB.cs`](Scripts/Data/ConditionsDB.cs): status effects as data-driven lambdas
- [`Pokemon.cs`](Scripts/Pokemon.cs): the damage formula and stat stages
- [`Character/TrainerController.cs`](Scripts/Character/TrainerController.cs): the trainer encounter sequence

## Design notes

- **Data-driven content.** Species and moves are ScriptableObjects, so new pokemon and moves are created in the editor without code changes.
- **Interfaces for world interactions.** Anything the player can talk to implements `IInteractable`, and anything triggered by stepping on it (grass, trainer vision, portals) implements `IPlayerTriggerable`. The player controller doesn't need to know what it's interacting with.
- **Coroutine-driven sequencing.** Battle turns, dialog, and cutscenes are written as coroutines, so multi-step sequences read top to bottom.

## Built with

- Unity 2022.3 (URP 2D)
- C#
- [DOTween](http://dotween.demigiant.com/) for tweened battle animations and fades (not included)
- TextMesh Pro for UI text (not included)

## Disclaimer

This is a non-commercial fan project made for learning. It is not affiliated with or endorsed by Nintendo, Game Freak, Creatures Inc., or The Pokémon Company. Pokémon and all related names are trademarks of their respective owners.
