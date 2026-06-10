# 🗺️ Iteration 1: Core Map Interaction & UI Prototype

## 🎯 High-Level Goal
The objective of this iteration is to create a functional, self-contained interactive prototype. The player must be able to visually survey a 3D map partition containing **at least 5 sharply colored provinces**, hover over them for context, and left-click a province to instantly display its specific data profile (**ID, Name, Population, and Economy**) inside a minimalist UI screen canvas overlay.

## 📋 Minimum Functional Requirements
1. **Visual Representation:** A 3D surface mesh rendering a flat texture displaying **at least 5 distinct, sharply colored zones**.
2. **Input Capture:** Left-clicking anywhere on the map triggers an engine raycast to read the pixel color under the cursor.
3. **Data Translation:** The `MapManager` translates the sampled color into an $O(1)$ dictionary lookup to fetch the corresponding asset card.
4. **UI Presentation Display:** A minimalist UI layout panel populates and displays the active province profile data (**ID, Name, Population, Economy**) live on the screen upon selection.

## 📋 Definition of Done (DoD)
- [x] Code compiles cleanly with zero errors under the modern Input System API.
- [ ] Interface canvas dynamically displays ID, Name, Population, and Economy metrics upon left-click selection.
- [ ] System handles empty space or invalid colors safely without throwing null references.
- [ ] Project features a data registry tracking at least 5 distinct configured province asset files.
- [ ] All code and data configs sit strictly inside `/Assets/_Project/`.

---

## 🚀 Status: In Progress
