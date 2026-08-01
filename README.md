# Mall Breakout

<<<<<<< HEAD
A top-down Unity survival game - clear a zombie-infested mall across five levels, built to demonstrate modular, event-driven software architecture as much as gameplay.
=======
Mall Breakout is a top-down Unity action game with five playable levels. The player clears zombies, collects level items, opens a chest, and exits to the next stage.
>>>>>>> b9fab3291d030ffb473014827cd1ae90536ec55a

## Gameplay Loop
Enter a level → defeat enemies → collect items → open the chest → exit unlocks → progress to the next level. Progress saves automatically between sessions.

## Controls
- Move: WASD / Arrow keys (or on-screen joystick on mobile)
- Attack: Left mouse button (or Attack button on mobile)
- Abilities: Q (Dash), F (Heal)
- Interact: E (or Interact button on mobile)
- Pause: Escape

## Architecture Overview
Gameplay objects (Player, Enemies, Interactables) never reference each other directly — they communicate through interfaces (for commands) or events (for notifications). Managers (SaveManager, AudioManager, LevelManager) are Singletons that listen to events rather than being called directly by gameplay code. See in-code comments and the Design Patterns section below for specifics.

## Design Patterns
- **Singleton** — GameManager, SaveManager, AudioManager, LevelManager
- **Observer** — health changes, enemy defeated, item collected, level completed all drive independent listeners (UI, audio, save stats) with zero direct coupling
- **Strategy** — player abilities (`DashAbility`, `HealAbility`) are interchangeable `IAbility` implementations auto-detected by `PlayerAbilities`
- **State** — enemy AI (`IdleState`, `ChaseState`, `AttackState`, `DefensiveState`) via `IEnemyState`
- **Object Pooling** — `ObjectPool` / `EnemyPoolManager` for enemy reuse instead of repeated Instantiate/Destroy

## Interfaces
`IDamageable`, `IInteractable`, `IAbility`, `IAttackable`, `ICollectable` — each kept narrow and single-purpose (Interface Segregation), reducing dependencies between systems.

## Save System
`SaveData` (level progression, player stats, inventory, settings) is serialized to JSON via Newtonsoft.Json, stored at `Application.persistentDataPath`. JSON was chosen for human-readability during development, clean handling of nested data, and broad tooling support. Scope is intentionally limited to progression data — no world-state (enemy positions, player coordinates) is persisted; dying or restarting reloads the level fresh.

## REST API
`LeaderboardService` integrates jsonbin.io for a simple online leaderboard (player name, enemies defeated, deaths). Handles offline/failure cases gracefully — a failed request shows a clear UI message rather than blocking or crashing gameplay.

## Algorithms
- **Sorting** — insertion sort (`LeaderboardSorter`) ranks leaderboard entries by score descending. Chosen over built-in sort to demonstrate manual implementation; O(n²) cost is irrelevant at leaderboard-sized entry counts.
- **Searching** — linear search (`NearestEnemyFinder`) finds the closest active enemy to a position. A full spatial-partitioning structure would be unnecessary overhead at this enemy-count scale.
- **Navigation** — direct-seek movement in `ChaseState` computes a normalized direction vector toward the player each frame. Full pathfinding (NavMesh/A*) would be disproportionate for these open, low-obstacle levels.

## Unit Testing
9 EditMode NUnit tests covering damage calculation (including floor-at-zero), inventory add/check, level unlock propagation, leaderboard sort correctness (including empty-list edge case), and nearest-enemy search (including empty-list edge case). Run via `Window > General > Test Runner > EditMode > Run All`.

## Platform Support
- Windows: keyboard/mouse input
- WebGL: same input scheme, tested for save persistence across sessions
- Android: on-screen virtual joystick + action buttons via `MobileInputProvider`, gated with `#if UNITY_ANDROID || UNITY_IOS` conditional compilation; `MobileControlsVisibility` hides these controls on non-touch platforms

## Levels
1. Supermarket — 2 enemies, 1 collectible, 1 chest
2. Food Court — 3 enemies, 2 collectibles, 1 chest
3. Clothing Store — 4 enemies, 3 collectibles, 1 chest
4. Electronics — 7 enemies, 3 collectibles, 2 chests
5. Parking Roof — 8 enemies, 4 collectibles, 4 chests

## Known Limitations
- Continue Game resumes at the start of the most recently unlocked level, not exact mid-level position — by design, since world-state isn't persisted
- Player/enemy sprites are side-view art; vertical-only movement doesn't rotate facing (no true 8-directional sprite set used)

## Asset Sources
- itch.io
- Unity Asset Store