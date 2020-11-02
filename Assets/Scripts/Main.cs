using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
/// <summary>
/// Основной управляющий скрипт. Должен присутствовать на игровой сцене
/// </summary>
[DisallowMultipleComponent]
public sealed class Main : MonoBehaviour
{
    public InfoPanel infoPanel;
    public TopPanel topPanel;
    public SelectedPanel selectedPanel;
    public static Main Instance;
    [HideInInspector] public MarkCity lastSelectedCity;
    private List<CountrySO> selectedCountries;
    private Object[] allmarks;
    
    public int SelectedCount { get => selectedCountries.Count; }
    public List<CountrySO> SelectedCountries { get => selectedCountries; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (infoPanel == null)
            Debug.LogError("Не задан infoPanel");
        if (topPanel == null)
            Debug.LogError("Не задан topPanel");
        if (selectedPanel == null)
            Debug.LogError("Не задан selectedPanel");
        
        selectedCountries = new List<CountrySO>();
        allmarks = FindObjectsOfType(typeof(MarkCountry));
    }

    /// <summary>
    /// Находиться ли страна в списке выбранных
    /// </summary>
    /// <param name="country">Странна</param>
    /// <returns>true - находится, false - нет</returns>
    public bool isCountrySelected(CountrySO country)
    {
        if (country == null)
            return false;
        foreach(CountrySO sel in selectedCountries)
        {
            if (sel == country)
                return true;
        }
        return false;
    }  

    /// <summary>
    /// Очистить список выбранных стран
    /// </summary>
    public void ClearSelectedCountry()
    {
        selectedCountries.Clear();
        topPanel.TextUpdate(selectedCountries.Count);   // Обновляем надпись
        topPanel.Hide();
        // убираем галочки и возвращаем метки
        foreach (Object markObj in allmarks)
        {
            MarkCountry markcountry = markObj as MarkCountry;
            markcountry.Uncheck();
        }
        if (lastSelectedCity != null)
            lastSelectedCity.checkBuilding.SetActive(false);
    }

    /// <summary>
    /// Перейти к панели выбранных
    /// </summary>
    public void GotoSelectedPanel()
    {
        topPanel.Hide();
        infoPanel.Hide();
        selectedPanel.InitTable();
        selectedPanel.Show();
    }

    /// <summary>
    /// Вернуться из панели выбранных
    /// </summary>
    public void ReturnFromSelectedPanel()
    {
        selectedPanel.Hide();
        selectedPanel.ClearTable();
        topPanel.Show();
    }

    /// <summary>
    /// Добавить страну в список выбранных
    /// </summary>
    /// <param name="country"></param>
    public void AddToSelectedCountries(CountrySO country)
    {
        // проверка на совпадение
        if (!selectedCountries.Contains(country))
        {
            selectedCountries.Add(country);                 // Добавляем к списку
            topPanel.TextUpdate(selectedCountries.Count);   // Обновляем надпись
            if (lastSelectedCity != null && isCountrySelected(lastSelectedCity.country))
                lastSelectedCity.checkBuilding.SetActive(true);
        }
        else
            Debug.Log("Пытались в список добавить дубль");
    }
}
