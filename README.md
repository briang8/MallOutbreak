# Mall Breakout

Mall Breakout is a top-down Unity action game with five playable levels. The player clears zombies, collects level items, opens a chest, and exits to the next stage.

## Gameplay Loop

Enter a level, defeat the enemies, collect the items, open the chest, unlock the exit, and continue to the next level.

## Rubric Coverage

### Level System and Progression

- Five levels are present: Supermarket, Food Court, Clothing Store, Electronics, and Parking Roof.
- Progress is saved in JSON through `SaveManager`.
- `LevelManager` handles level loading, completion, and unlocking the next level.
- `LevelButton` shows locked, unlocked, and completed states without relying on text labels.

### Player System

- `PlayerMovement` handles movement and mobile or desktop input.
- `PlayerHealth` handles damage, healing, death, and health events.
- `PlayerCombat` handles melee attacks and enemy damage.
- `PlayerInteraction` handles doors and other interactables.
- `PlayerAbilities`, `HealAbility`, and `DashAbility` provide two interchangeable abilities.

### Enemy System

- `EnemyBase` provides shared enemy logic and a state machine hook.
- `ZombieWalker`, `ZombieRunner`, and `ZombieBrute` each behave differently.
- `IdleState`, `ChaseState`, `AttackState`, and `DefensiveState` keep behaviour modular.

### Interaction System

- `Door`, `Chest`, and `Collectible` are reusable interactable objects.
- `LevelObjectiveTracker` keeps the exit locked until the level conditions are met.
- Collectibles are picked up through triggers, and the chest grants a reward item.

### Advanced C# Concepts

- Interfaces are used for damage, attack, interaction, collection, ability use, and enemy state behaviour.
- Delegates and events keep health, inventory, enemy defeat, and level completion loosely coupled.
- Singletons are used for game-wide services such as save, audio, and level flow.

### Design Patterns

- Singleton: `SaveManager`, `LevelManager`, `AudioManager`, and `GameManager`.
- Observer: UI and audio react to gameplay events.
- Strategy: abilities and enemy behaviours can be swapped without rewriting the caller.
- State: enemy AI switches between idle, chase, attack, and defensive states.
- Object Pooling: `ObjectPool` and `EnemyPoolManager` reduce spawn overhead.

### Data and API

- `SaveData` stores progression, player stats, inventory, and settings.
- `LeaderboardService` integrates a REST API for submitting and fetching leaderboard data.
- `LeaderboardSorter` and `NearestEnemyFinder` support gameplay and UI logic.

### Algorithms

- Nearest-enemy search: finds the closest active enemy for targeting and testing.
- Leaderboard sorting: orders scores for the top entries.
- Level unlock logic: advances progression when a level is completed.

### UI and Audio

- UI scripts cover the main menu, settings, health bar, inventory, pause screen, game over screen, and level completion screen.
- `AudioManager` centralizes music and sound effects.
- `GameStateUI` listens for gameplay events and updates the screen state.

### Testing

- Edit mode tests cover damage, inventory, level unlocking, search, and sorting.
- The current test set gives a strong baseline for the required unit-test count.

### Platform Support

- Mobile input is handled by `MobileInputProvider`.
- `MobileControlsVisibility` hides touch controls for editor, standalone, and WebGL builds.
- The project is structured to support Windows, WebGL, and mobile builds with platform-specific input and UI handling.

## Level Order

1. Level 1: Supermarket
2. Level 2: Food Court
3. Level 3: Clothing Store
4. Level 4: Electronics
5. Level 5: Parking Roof

## Level Objectives

- Level 1: 2 enemies, 1 collectible, 1 chest
- Level 2: 3 enemies, 2 collectibles, 1 chest
- Level 3: 4 enemies, 3 collectibles, 1 chest
- Level 4: 7 enemies, 3 collectibles, 2 chests
- Level 5: 8 enemies, 4 collectibles, 4 chests

## Controls

- Move: WASD or arrow keys
- Attack: Left mouse button
- Interact: E
- Abilities: Q and F
- Pause: Escape

## Notes

- The exit stays locked until the required enemies, collectibles, and chests are completed for that level.
- Opening the chest gives the player a reward item before leaving the level.
- Progress is saved between runs, so later levels unlock as you complete earlier ones.

## Asset Sources

https://itch.io/
https://assetstore.unity.com/
