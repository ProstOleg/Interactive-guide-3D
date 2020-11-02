using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Скрит управляющий движением камеры
/// </summary>
public class CameraMoving : MonoBehaviour
{
    public float minSwipeLength = 100f;
    [Header("Ограничения по X:")]
    public float minX = -20;
    public float maxX = 18;
    [Header("Ограничения по Y:")]
    public float minY = -5;
    public float maxY = 10;
    [Header("Ограничения по Z:")]
    public float minZ = -3f;
    public float maxZ = 0f;

    private const float deltaX = 10f;
    private const float deltaY = 1f;
    private const float deltaZ = 0.5f;
    private const int frameCounts = 24;
    private const float MovingTme = 0.2f;

    private Vector2 firstPressPos;
    private Vector2 secondPressPos;
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }
    private void Update()
    {
        
        if (!Main.Instance.selectedPanel.isActive)
        {
            Touch[] myTouches = Input.touches;
            //  Масштабирование 2 касаниями
            if (Input.touches.Length == 2)
            {
                Vector2[] currentswipes = new Vector2[2];
                for (int i = 0; i < Input.touchCount; i++)
                {
                    Touch t = Input.touches[i];
                    if (t.phase == TouchPhase.Began)
                    {
                        firstPressPos = new Vector2(t.position.x, t.position.y);
                    }
                    if (t.phase == TouchPhase.Ended)
                    {
                        secondPressPos = new Vector2(t.position.x, t.position.y);
                        currentswipes[i] = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
                    }
                }
                // Отдалить 
                if (currentswipes[0].x < 0 && currentswipes[0].y < 0 && currentswipes[1].x < 0 && currentswipes[1].y < 0)
                {
                    float clampZ = Mathf.Clamp(cam.transform.position.z - deltaZ, minZ, maxZ);
                    Vector3 newPos = new Vector3(cam.transform.position.x, cam.transform.position.y, clampZ);
                    StartCoroutine(CameraMove(cam.transform.position,newPos,MovingTme,frameCounts));
                }
                // Приблизить 
                else if ((currentswipes[0].x > 0 && currentswipes[0].y > 0 && currentswipes[1].x < 0 && currentswipes[1].y < 0) ||
                    (currentswipes[0].x < 0 && currentswipes[0].y < 0 && currentswipes[1].x > 0 && currentswipes[1].y > 0))
                {   
                    float clampZ = Mathf.Clamp(cam.transform.position.z + deltaZ, minZ, maxZ);
                    Vector3 newPos = new Vector3(cam.transform.position.x, cam.transform.position.y, clampZ);
                    StartCoroutine(CameraMove(cam.transform.position, newPos, MovingTme, frameCounts));
                }

            }


            // Проверка на свайпы 1 касание
            else if (Input.touches.Length == 1)
            {
                Touch t = Input.GetTouch(0);
                if (t.phase == TouchPhase.Began)
                {
                    firstPressPos = new Vector2(t.position.x, t.position.y);
                }
                if (t.phase == TouchPhase.Ended)
                {
                    secondPressPos = new Vector2(t.position.x, t.position.y);
                    Vector3 currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y, 0f);
                    // Не засчитался
                    if (currentSwipe.magnitude < minSwipeLength)
                        return;

                    
                    Vector3 LerpedVector =  Vector3.Lerp(cam.transform.position, cam.transform.position + currentSwipe, Time.deltaTime);
                    float clampX = Mathf.Clamp(LerpedVector.x, minX, maxX);
                    float clampY = Mathf.Clamp(LerpedVector.y, minY, maxY);
                    Vector3 newPos = new Vector3(clampX, clampY, cam.transform.position.z);
                    StartCoroutine(CameraMove(cam.transform.position, newPos, MovingTme, frameCounts));
                }
            }

        }
    }

    /// <summary>
    /// Плавное передвижение камеры
    /// </summary>
    /// <param name="start">Начальная точка</param>
    /// <param name="end">Конечная точка</param>
    /// <param name="moveTime">время движения</param>
    /// <param name="frames">количество кадров</param>
    /// <returns></returns>
    private IEnumerator CameraMove(Vector3 start, Vector3 end, float moveTime, int frames)
    {
        Vector3 delta = end - start;
        float frameTime = moveTime / frames;
        Vector3 frameDelta = delta / frames;
        for (int i = 0; i < frames; i++)
        {
            cam.transform.position += frameDelta;
            yield return new WaitForSeconds(frameTime);
        }
        yield return null;
    }

}
