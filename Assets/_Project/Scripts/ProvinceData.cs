using UnityEngine;

[CreateAssetMenu(fileName = "NewProvinceData", menuName = "Strategy/Province Data")]
public class ProvinceData : ScriptableObject
{
    [Header("System Identifiers")]
    public int provinceID;
    public string provinceName;

    [Header("Map Generation")]
    public Color colorIdentity = Color.white;

    [Header("Socioeconomic Simulation")]
    public int population;
    public int economy;
}