# 🗄️ Database Expansion: 5-Province Simulation Profiling

The data layer must be extended beyond basic identification keys to handle core socioeconomic attributes across our 5 mandatory validation territories.

## 🧬 Fields Addendum
The `ProvinceData.cs` blueprint is appended with the following metrics:
* `public int population;` // Total number of citizens in the region
* `public int economy;`    // Economic baseline / Development level value

## 🎨 Mandatory Color-ID Mapping Table
To fulfill the 5-province prototype minimum, the following asset cards must be created and allocated completely unique color identities:

| Asset File Name | ID | Province Name | Population | Economy | Target RGB Color Signature |
| :--- | :--- | :--- | :--- | :--- | :--- |
| `Province_Red` | 1 | Altdorf | 45000 | 8 | `RGBA(255, 0, 0, 255)` |
| `Province_Green` | 2 | Marienburg | 62000 | 10 | `RGBA(0, 255, 0, 255)` |
| `Province_Blue` | 3 | Middenheim | 28000 | 5 | `RGBA(0, 0, 255, 255)` |
| `Province_Yellow`| 4 | Nuln | 35000 | 7 | `RGBA(255, 255, 0, 255)` |
| `Province_Cyan` | 5 | Talabheim | 19000 | 4 | `RGBA(0, 255, 255, 255)` |