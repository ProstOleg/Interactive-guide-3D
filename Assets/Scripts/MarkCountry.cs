using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Метка для страны
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class MarkCountry : MonoBehaviour
{
    [SerializeField] private CountrySO country;
    [SerializeField] private Sprite checkSprite;
    [SerializeField] private Sprite tagSprite;
    [SerializeField] private float touchDelay = 0.5f;

    [HideInInspector] public bool isCheck;
    private float timeTouchBegan;
    private SpriteRenderer spriteRend;
    private Main main;

    private void Start()
    {
        if (country == null)
            Debug.LogError("Не задан CountrySO");
        if(checkSprite == null)
            Debug.LogError("Не задан checkSprite");
        if (tagSprite == null)
            Debug.LogError("Не задан tagSprite");
        spriteRend = this.GetComponent<SpriteRenderer>();
        main = Main.Instance;
    }

    private void OnMouseDown()
    {
        main.infoPanel.DataSet(country);
        main.infoPanel.Show();
        timeTouchBegan = Time.time;
    }

    private void OnMouseUp()
    {
        float timeTouchEnd = Time.time;
        if (timeTouchEnd - timeTouchBegan >= touchDelay && !isCheck)
        {
            // Галочка
            isCheck = true;
            spriteRend.sprite = checkSprite;
            main.AddToSelectedCountries(this.country);     // +1 к выбранным странам

            main.infoPanel.Hide();
            main.topPanel.Show();
        }
    }

    /// <summary>
    /// Вернуть геометку
    /// </summary>
    public void Uncheck()
    {
        isCheck = false;
        spriteRend.sprite = tagSprite;
    }
   
}
