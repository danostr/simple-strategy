# ⏳ Iteration 2: Dynamic Simulation & Game Loop Prototype

## 🎯 High-Level Goal
The objective of this iteration is to transition the project from a static map viewer into a living strategy simulation. We will introduce a centralized **Time Manager (Game Loop)** that fires periodic "Ticks" (e.g., Monthly/Yearly turns). Upon each tick, the engine will process the population and economy values of all registered provinces to dynamically generate gold income and simulate population growth.

## 📋 Minimum Functional Requirements
1. **The Game Clock:** A centralized `TimeManager.cs` script that handles game speed (Pause, 1x, 2x) and broadcasts a `OnGameTick` event.
2. **Economic Assembly:** A script module that loops through the `MapManager` registry on every tick, calculates revenue based on regional `economy` stats, and accumulates it into a global treasury counter.
3. **Demographic Shift:** A growth loop that increases a province's `population` stat over time based on its economic development baseline.
4. **UI Dashboard Upgrades:** A persistent top-bar UI overlay showing the Global Date (e.g., "January 1, 1444") and the Player's Treasury balance.

## 📋 Definition of Done (DoD)
- [ ] Pressing the "Spacebar" successfully toggles between pausing and unpausing the simulation clock.
- [ ] Every game "month," the player's total gold dynamically increases based on the sum total of all province economies.
- [ ] Left-clicking a province displays its *live, updating* population count inside the info panel rather than a static baseline asset value.
- [ ] State changes are decoupled so that runtime game data changes do not overwrite the original `.asset` ScriptableObject files on disk.