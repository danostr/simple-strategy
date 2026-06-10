# 🏗️ System Architecture Overview

To ensure the game can scale to thousands of provinces without bottlenecking the CPU, Iteration 1 introduces a strictly decoupled **Three-Layer Engine Model**.

[INPUT LAYER: MapRaycaster]
   │ (Detects 3D click -> Extracts 2D texture UV -> Grabs Pixel Color)
   ▼
[SIMULATION LAYER: MapManager]
   │ (Receives Color -> Queries Registry Dictionary)
   ▼
[DATA LAYER: ProvinceData]
     (Holds static ID, Name, and Color Asset Data)

## 🧩 The Three Core Layers

### 1. The Data Layer (`ProvinceData`)
This layer handles pure information storage. It does not contain game logic, handling of clicks, or physics systems. It is built using Unity's `ScriptableObject` framework, meaning each province exists as an independent, lightweight data file inside your project folders. 
* **Responsibility:** Holds variables like `provinceID`, `provinceName`, and `colorIdentity`.
* **Isolation:** It knows absolutely nothing about cameras, raycasts, or 3D meshes.

### 2. The Input Layer (`MapRaycaster`)
This layer handles physics calculations and user inputs. It bridges the player's mouse click with the physical world geometry inside the scene.
* **Responsibility:** Fires an invisible line (Raycast) from the 3D camera to the map mesh when a click is detected, extracts the exact mathematical **UV coordinate** (from `0.0` to `1.0`) of the impact point, samples the color pixel at that spot on the texture map, and passes that raw color directly to the Simulation Layer.
* **Isolation:** It knows nothing about what country owns a province, what the province name is, or game balance rules. It only tracks raw pixel colors.

### 3. The Simulation Layer (`MapManager`)
This layer acts as the centralized brain and database registry of your game board. 
* **Responsibility:** It maintains a master runtime lookup dictionary linking every unique `Color` to its matching `ProvinceData` file. When the Input Layer screams *"The player clicked on Pure Blue!"*, the `MapManager` looks up that blue key, grabs the correct asset card, and reads its properties.
* **Isolation:** It doesn't handle the physics math of *how* the click happened; it just waits to process the color values delivered by the input layer.

---

## ⚙️ Architectural Principles

* **Separation of Concerns:** By keeping data, input math, and database registry completely separate, you can change your 3D graphics completely without breaking your statistics engine. Similarly, you can tweak your gameplay stats without corrupting your map art assets.
* **Performance Baseline:** We completely avoid giving every single province an independent 3D mesh collider component. Having thousands of physical colliders constantly checking for collisions destroys CPU performance. Instead, we use a single flat global collider for the entire world map mesh, relying entirely on graphics texture lookup arrays to pinpoint exactly where the user clicked.