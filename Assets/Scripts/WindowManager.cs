using UnityEngine;

/// <summary>
/// Менеджер изменений окна
/// </summary>
public class WindowManager : MonoBehaviour
{

    public delegate void ScreenSizeChangeEventHandler(int Width, int Height);       
    public event ScreenSizeChangeEventHandler ScreenSizeChangeEvent;
    public static WindowManager Instance;                           //  Синглтон
    private Vector2 lastScreenSize;                                 //  Сохраняем размер экрана

    protected virtual void OnScreenSizeChange(int Width, int Height)
    {
        if (ScreenSizeChangeEvent != null)
            ScreenSizeChangeEvent(Width, Height);
    }

    void Awake()
    {
        Instance = this;
        lastScreenSize = new Vector2(Screen.width, Screen.height);
    }

    void Update()
    {
        Vector2 screenSize = new Vector2(Screen.width, Screen.height);
        if (this.lastScreenSize != screenSize)
        {
            this.lastScreenSize = screenSize;
            OnScreenSizeChange(Screen.width, Screen.height);                        //  Запускаем событие
        }
    }

}