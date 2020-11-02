using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Общий класс для UI панели
/// </summary>
public class BasePanel : MonoBehaviour
{
    [SerializeField] protected float MovingTime = 0.5f;
    [HideInInspector] public bool isActive;
    protected RectTransform panel;

    public virtual void Start()
    {
        panel = GetComponent<RectTransform>();
    }

    /// <summary>
    /// Показать панель
    /// </summary>
    public virtual void Show() {}

    /// <summary>
    /// Скрыть панель
    /// </summary>
    public virtual void Hide() {}

    /// <summary>
    /// (Courutine) Плавное движение панелей
    /// </summary>
    /// <param name="start">Начальная позиция</param>
    /// <param name="end">Конечная позиция</param>
    /// <param name="showTime">Время движения</param>
    /// <param name="frames">Количество кадров</param>
    /// <returns></returns>
    public IEnumerator PanelMoving(Vector2 start, Vector2 end, float showTime, int frames)
    {
        Vector2 delta = end - start;
        float frameTime = showTime / frames;
        Vector2 frameDelta = delta / frames;
        for (int i = 0; i < frames; i++)
        {
            panel.anchoredPosition += frameDelta;
            yield return new WaitForSeconds(frameTime);
        }
        yield return null;
    }



}
