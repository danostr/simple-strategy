# 🗄️ Database Specification: Province Data

Instead of baking data directly into structural scene files or monolithic code databases, every single region in the world exists as a modular scriptable asset file (`.asset`) generated from a core configuration template.

## 🧬 Data Fields (Phase 1 Template)
| Field Name | Data Type | Purpose | Example Value |
| :--- | :--- | :--- | :--- |
| `provinceID` | `int` | System-level loops, tracking, and save game indexing. | `101` |
| `provinceName`| `string` | The user-facing localized display text. | `"Paris"` |
| `colorIdentity`| `Color` | The exact mathematical RGB identifier matching the map graphic. | `RGBA(255, 0, 0, 255)` |

## 🛠️ Validation Protocol
* Every assigned `colorIdentity` must be completely unique. Two provinces sharing the exact same RGB color values will result in data collisions.
* Alpha threshold value must remain clamped at 255 (completely opaque) to ensure accuracy during texture pixel sampling.