using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

/// <summary>
/// Панель - Список выбранных стран
/// </summary>
[DisallowMultipleComponent]
public class SelectedPanel : BasePanel
{
    [SerializeField] private Sprite UpSprite;
    [SerializeField] private Sprite DownSprite;
    [SerializeField] private Image AreaUDSprite;
    [SerializeField] private Image PopUDSprite;
    [SerializeField] private Image GDPUDSprite;
    [SerializeField] private CountryString prefabCountryString;
    private bool isUpArea;
    private bool isUpPop;
    private bool isUpGDP;
    private IOrderedEnumerable<CountrySO> orderedCountries;
    private List<CountryString> countriesStrings;
    private Main main;
    

    private void Awake()
    {
        if (UpSprite == null)
            Debug.LogError("Не задан UpSprite");
        if (DownSprite == null)
            Debug.LogError("Не задан DownSprite");
        if(AreaUDSprite == null )
            Debug.LogError("Не задан AreaUDSprite");
        if (PopUDSprite == null)
            Debug.LogError("Не задан PopUDSprite");
        if (GDPUDSprite == null)
            Debug.LogError("Не задан GDPUDSprite");
        if(prefabCountryString ==null)
            Debug.LogError("Не задан prefabCountryString");

        isUpArea = true;
        isUpPop = true;
        isUpGDP = true;
        countriesStrings = new List<CountryString>();
    }

    public override void Start()
    {
        base.Start();
        main = Main.Instance;
        WindowManager.Instance.ScreenSizeChangeEvent += ChangedResolution;
        SetToStartPosition();
    }

    #region Кнопки вверх/вниз

    /// <summary>
    /// U/D по Площади
    /// </summary>
    public void UpDownArea()
    {
        isUpArea = !isUpArea;
        if (isUpArea)
            AreaUDSprite.sprite = UpSprite;
        else
            AreaUDSprite.sprite = DownSprite;
    }
    
    /// <summary>
    /// U/D по Населению
    /// </summary>
    public void UpDownPopulation()
    {
        isUpPop = !isUpPop;
        if (isUpPop)
            PopUDSprite.sprite = UpSprite;
        else
            PopUDSprite.sprite = DownSprite;


    }

    /// <summary>
    /// U/D по ВВП
    /// </summary>
    public void UpDownGDP()
    {
        isUpGDP = !isUpGDP;
        if (isUpGDP)
            GDPUDSprite.sprite = UpSprite;
        else
            GDPUDSprite.sprite = DownSprite;
    }

    #endregion

    #region Кнопки сортировки

    /// <summary>
    /// Сортировка по площади
    /// </summary>
    public void SortedByArea()
    {
        if (isUpArea)    
            orderedCountries = Main.Instance.SelectedCountries.OrderBy(country => country.Area);    //по возрастанию
        else
            orderedCountries = Main.Instance.SelectedCountries.OrderByDescending(country => country.Area);  // по убыванию
        UpdateDisplayTableCountries();
    }
    /// <summary>
    /// Сортировка по населению
    /// </summary>
    public void SortedByPopulation()
    {
        if (isUpPop)
            orderedCountries = Main.Instance.SelectedCountries.OrderBy(country => country.Population);  //по возрастанию
        else
            orderedCountries = Main.Instance.SelectedCountries.OrderByDescending(country => country.Population);  // по убыванию
        UpdateDisplayTableCountries();
    }
    /// <summary>
    /// Сортировка по ВВП
    /// </summary>
    public void SortedByGDP()
    {
        if (isUpGDP)
            orderedCountries = Main.Instance.SelectedCountries.OrderBy(country => country.GDP);  //по возрастанию
        else
            orderedCountries = Main.Instance.SelectedCountries.OrderByDescending(country => country.GDP);  // по убыванию
        UpdateDisplayTableCountries();
    }

    #endregion

    /// <summary>
    /// Загружаем таблицу на панель
    /// </summary>
    public void InitTable()
    {
        float deltaPivot = 1f;
        for (int i = 0; i < Main.Instance.SelectedCount; i++)
        {
            // Новая строка
            CountryString newwCountryString = Instantiate(prefabCountryString, this.transform) as CountryString;
            Vector2 rectpivot = newwCountryString.GetComponent<RectTransform>().pivot;
            newwCountryString.GetComponent<RectTransform>().pivot = new Vector2(rectpivot.x, rectpivot.y + deltaPivot * (countriesStrings.Count - 1));
            countriesStrings.Add(newwCountryString);
            newwCountryString.name = $"String#{countriesStrings.Count - 1}";
        }
        UpdateDisplayTableCountries();
    }

    /// <summary>
    /// Очищаем таблицу
    /// </summary>
    public void ClearTable()
    {
        foreach(var str in countriesStrings)
            Destroy(str.gameObject);
        countriesStrings.Clear();

        orderedCountries = null;
    }
    
    /// <summary>
    /// Обновить содержимое таблицы
    /// </summary>
    private void UpdateDisplayTableCountries()
    {
        if(orderedCountries != null && orderedCountries.Count<CountrySO>() != 0)
        {
            int i = 0;
            foreach( CountrySO countryData in orderedCountries)
                countriesStrings[i++].StringUpdate(countryData);
        }
        else
        {
            Debug.Log("Нет отсортированного списка");
            int i = 0;
            foreach (CountrySO countryData in Main.Instance.SelectedCountries)
                countriesStrings[i++].StringUpdate(countryData);
        }
    }

    /// <summary>
    /// Выполняется при смене разрешения
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    private void ChangedResolution(int width, int height)
    {
        if (!isActive)
            SetToStartPosition();
    }

    /// <summary>
    /// Получить стартовую позицию
    /// </summary>
    /// <returns>стартовая точка</returns>
    public Vector2 GetStartPosition()
    {
        Vector2 newPos = new Vector2(-Screen.width, GetComponent<RectTransform>().anchoredPosition.y);
        return newPos;
    }

    /// <summary>
    /// Перейти на стартовую позицию
    /// </summary>
    public void SetToStartPosition()
    {
        Vector2 newPos = new Vector2(-Screen.width, GetComponent<RectTransform>().anchoredPosition.y);
        GetComponent<RectTransform>().anchoredPosition = newPos;
    }

    /// <summary>
    /// Показать панель
    /// </summary>
    public override void Show()
    {
        if (!isActive)
        {
            this.gameObject.SetActive(true);
            isActive = true;
            Vector2 starSel = GetStartPosition();
            Vector2 endsel = new Vector2(0f, 0f);
            StartCoroutine(PanelMoving(starSel, endsel, MovingTime, 24));
        }
    }

    /// <summary>
    /// Скрыть панель
    /// </summary>
    public override void Hide()
    {
        if (isActive)
        {
            this.gameObject.SetActive(true);
            isActive = false;
            Vector2 starSel = new Vector2(0f, 0f);
            Vector2 endsel = GetStartPosition();
            StartCoroutine(PanelMoving(starSel, endsel, MovingTime, 24));
        }
    }

}
