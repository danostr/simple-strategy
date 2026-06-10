# 🗺️ Core Mechanics: Color-Coded Texture Lookup

The engine reads world geography by mapping 3D screen space projections into a flat, raw pixel data grid using standard UV spatial layout coordinate systems.



## 🔄 The Interaction Loop Execution
1. **Screen Point Input:** The user clicks the screen. A physics raycast shoots from the camera vector toward the global map mesh.
2. **UV Remapping:** The engine calculates the collision intercept and returns the UV vector coordinate (a normalized float coordinate running from `0.0` to `1.0` along the X and Y bounds of the geometry).
3. **Pixel Sampling:** The system reads the corresponding pixel mapping coordinates directly off the designated ID texture.
4. **Dictionary Translation:** The retrieved color value serves as a search key inside the `MapManager` system to fetch and output the corresponding `ProvinceData` structural module.