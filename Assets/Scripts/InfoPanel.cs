using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Панель с информацией о стране
/// </summary>
public class InfoPanel : BasePanel
{
    [SerializeField] private Text AreaText;
    [SerializeField] private Text GDPText;
    [SerializeField] private Text PopulationText;
    private float delta;
    private void Awake()
    {
        if (AreaText == null)
            Debug.LogError("не задан AreaText");
        if (GDPText == null)
            Debug.LogError("не задан GDPText");
        if (PopulationText == null)
            Debug.LogError("не задан PopulationText");
    }

    public override void Start()
    {
        base.Start();
        delta = (panel.rect.width) / 2;
    }


    /// <summary>
    /// Передать данные о странне
    /// </summary>
    /// <param name="country"></param>
    public void DataSet(CountrySO country)
    {
        AreaText.text = $"Площадь {country.Area} км";
        GDPText.text = $"ВВП {country.GDP} трлн долл.";
        PopulationText.text = $"Население {country.Population}";
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
            Vector2 starInfo = this.transform.position;
            Vector2 endInfo = this.transform.position - new Vector3(delta , 0f, 0f);
            StartCoroutine(PanelMoving(starInfo, endInfo, MovingTime, 24));
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
            Vector2 starInfo = this.transform.position;
            Vector2 endInfo = this.transform.position + new Vector3(delta, 0f, 0f);
            StartCoroutine(PanelMoving(starInfo, endInfo, MovingTime, 24));
        }
    }
}
