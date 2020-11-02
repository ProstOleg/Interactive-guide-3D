using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Верхняя панель
/// </summary>
public class TopPanel : BasePanel
{
    [SerializeField] private Text textCounts;
    private Main main;

    public override void Start()
    {
        base.Start();
        if (textCounts == null)
            Debug.LogError("не задан textCounts");
        main = Main.Instance;
    }

    /// <summary>
    /// Обновить надпись 
    /// </summary>
    /// <param name="counts">количество</param>
    public void TextUpdate(int counts)
    {
        textCounts.text = $"Выбрано {counts} стран";
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
            Vector2 starTop = this.transform.position;
            Vector2 endTop = this.transform.position - new Vector3(0f, 100f, 0f);
            StartCoroutine(PanelMoving(starTop, endTop, MovingTime, 24));
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
            Vector2 starTop = this.transform.position;
            Vector2 endTop = this.transform.position + new Vector3(0f, 100f, 0f);
            StartCoroutine(PanelMoving(starTop, endTop, MovingTime, 24));
        }
    }
}
