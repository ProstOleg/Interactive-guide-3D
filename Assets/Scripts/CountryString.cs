using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI Строка с данными о стране
/// </summary>
public class CountryString : MonoBehaviour
{
    [SerializeField] private Text countryText;
    [SerializeField] private Text areaText;
    [SerializeField] private Text popText;
    [SerializeField] private Text gdpText;

    private void Start()
    {
        if (countryText == null)
            Debug.LogError($"У {this.name} не задан countryText");
        if (areaText == null)
            Debug.LogError($"У {this.name} не задан areaText");
        if (popText == null)
            Debug.LogError($"У {this.name} не задан popText");
        if (gdpText == null)
            Debug.LogError($"У {this.name} не задан gdpText");
    }

    /// <summary>
    /// Обновить данные в строке
    /// </summary>
    /// <param name="country">Данные о стране</param>
    public void StringUpdate(CountrySO country)
    {
        countryText.text = country.Name;
        areaText.text = country.Area.ToString();
        popText.text = country.Population.ToString();
        gdpText.text = country.GDP.ToString();
    }

}

