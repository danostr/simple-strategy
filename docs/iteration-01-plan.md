# 📅 Iteration 1: Core Map Interaction Prototype

## 🎯 High-Level Goal
The objective of this iteration is to create a functional proof-of-concept where a player can visually see a 3D map partition and click on individual regions to instantly retrieve their corresponding data profile. There is no active game strategy or AI loop in this phase—strictly input registration and data validation.

## 📋 Minimum Functional Requirements
To consider Iteration 1 complete, the project must successfully demonstrate the following sequence:
1. **Visual Representation:** A 3D surface mesh must render a flat texture displaying at least 3 distinct, sharply colored zones.
2. **Input Capture:** Left-clicking anywhere on that 3D surface must trigger an engine raycast.
3. **Data Translation:** The system must read the pixel color under the cursor and look it up in a database.
4. **Console Output:** The system must print the correct, human-readable name of the clicked province to the Unity Console logs with zero delay.

## 🏁 Definition of Done (DoD)
- [ ] Code compiles cleanly with zero errors or warnings in the Unity Editor.
- [ ] Clicking on a non-province area (empty space) does not crash the system.
- [ ] All new scripts and data configurations sit entirely inside the `/Assets/_Project/` directory.
- [ ] Changes are committed to Git using standard Conventional Commits formatting.