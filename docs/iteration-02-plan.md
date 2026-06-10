# ⏳ Iteration 2: Dynamic Simulation & Game Loop Prototype

## 🎯 High-Level Goal
The objective of this iteration is to transition the project from a static map viewer into a living strategy simulation. We will introduce a centralized **Time Manager (Game Loop)** that fires periodic "Ticks". Upon each tick, the engine will loop through a decoupled runtime state layer to dynamically calculate tax income and simulate population growth based on underlying province statistics.

## 📋 Minimum Functional Requirements
1. **The Game Clock (`TimeManager.cs`):** A centralized core script that handles time flow (Pause, 1x speed, 2x speed) and broadcasts an `OnMonthTick` event.
2. **Decoupled Runtime State (`RuntimeProvinceState.cs`):** A wrapper framework that instances province data at runtime so that demographic simulation variables can grow dynamically without modifying the original `.asset` ScriptableObject files on disk.
3. **Economic Simulation Loop (`EconomyEngine.cs`):** A subsystem that captures the `OnMonthTick` event, calculates tax revenue from all runtime province states, and accumulates it into a global treasury tracking system.
4. **UI Dashboard Canvas:** A persistent horizontal header UI bar tracking the running Game Date string and the Player's live Treasury balance gold values.

## 📋 Definition of Done (DoD)
- [ ] Pressing the "Spacebar" successfully toggles between pausing and unpausing the simulation clock.
- [ ] Every game "month," the player's total gold dynamically increases based on the sum total of all province economies.
- [ ] Left-clicking a province displays its *live, updating* population count inside the info panel rather than a static baseline asset value.
- [ ] State changes are decoupled so that runtime game data changes do not overwrite the original `.asset` ScriptableObject files on disk.