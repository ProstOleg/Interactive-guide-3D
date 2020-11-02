using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ScriptableObject с данными о стране
/// </summary>
[CreateAssetMenu(fileName = "New CountryData", menuName = "CountryData", order = 51)]
public class CountrySO : ScriptableObject
{
    public string Name = "NewCountry";  // Страна
    public int Area = 0;                // Площадь
    public int Population = 0;          // Население
    public float GDP = 0;               // GDP(ВВП в триллионах)
}



