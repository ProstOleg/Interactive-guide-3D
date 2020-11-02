using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Метка для городов
/// </summary>
public class MarkCity : MonoBehaviour
{
    public CountrySO country;
    [SerializeField] private GameObject building;
    [SerializeField] private float touchDelay = 0f;

    private float timeTouchBegan;
    private bool isHideIcon;
    private SpriteRenderer rend;
    private void Start()
    {
        if (country == null)
            Debug.LogError($"Не задан CountrySO у {this.name}");
        if (building == null)
            Debug.LogError($"Не задана достопримечательность у {this.name}");

        rend = building.GetComponentInChildren<SpriteRenderer>();
        checkBuilding.SetActive(false);
        building.SetActive(false);
    }

    /// <summary>
    /// Включение метки
    /// </summary>
    public void On()
    {
        this.gameObject.SetActive(true);
        if (building != null)
            building.SetActive(false);
    }

    /// <summary>
    /// Отключение метки
    /// </summary>
    public void Off()
    {
        this.gameObject.SetActive(false);
        if (building != null)
        {
            building.SetActive(true);
            if (Main.Instance.isCountrySelected(this.country))
                checkBuilding.SetActive(true);
            else
                checkBuilding.SetActive(false);
        }
    }

    /// <summary>
    /// Возвращает ссылку на галочку для Building
    /// </summary>
    public GameObject checkBuilding
    {
        get
        {
            if (building != null)
                return rend.gameObject;
            else
                return null;
        }
    } 

    private void OnMouseDown()
    {
        timeTouchBegan = Time.time;
    }

    private void OnMouseUp()
    {
        float timeTouchEnd = Time.time;
        if (timeTouchEnd - timeTouchBegan >= touchDelay )
        {

            if (Main.Instance.lastSelectedCity != null)
                Main.Instance.lastSelectedCity.On();
            Main.Instance.lastSelectedCity = this;
            Main.Instance.lastSelectedCity.Off();
        }
    }
}
