# 🖥️ Interface Specification: Minimalist Province Panel

The presentation layer will display underlying simulation variables without hard-linking visual scripts directly to raycasting logic loops.

## 📐 Screen Space Canvas Layout
A standard screen-space UI Canvas will hold a panel anchored to the viewport:

[ UI CANVAS SCREEN OVERLAY ]
┌────────────────────────────────────────────────────────┐
│                                                        │
│                                                        │
│                                       ┌──────────────┐ │
│                                       │ PROVINCE INFO│ │
│                                       ├──────────────┤ │
│                                       │ ID:   [Text] │ │
│                                       │ Name: [Text] │ │
│                                       │ Pop:  [Text] │ │
│                                       │ Econ: [Text] │ │
│                                       └──────────────┘ │
└────────────────────────────────────────────────────────┘

## 🧩 Display Fields
* **ID Text:** System-level identification tracking.
* **Name Text:** Localized province display name.
* **Population Text:** Numeric player readout tracking regional population.
* **Economy Text:** Numeric rating (e.g., 1-10 base development) representing regional infrastructure weight.