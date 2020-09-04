using UnityEditor;
using UnityEngine;

public class MultiplyCreate : EditorWindow
{
    #region Переменные
    [SerializeField] private GameObject gameObj;
    private int count;
    private bool isAdvanced = false;
    private string objName = "multiplied";
    bool isGrouped;
    string parrentsName = "Parrent";
    Vector3 startPosition;
    float spaceBetween;
    string[] directions = { "X", "-X", "Y", "-Y", "Z", "-Z" };
    int directionIndex;
    private Vector3 spawnPosition;    
    private Quaternion startRotation;
    private Quaternion nextRotation;

    private Vector3 startRot, nextRot;  //Временные переменные
    #endregion

    [MenuItem("DeadLords/Create Multiply Objects %#d", false, 1)]
    public static void MultiplyWindow()
    {        
        EditorWindow.GetWindow(typeof(MultiplyCreate));
    }

    private void OnGUI()
    {
        GUILayout.Label("Настройки создания объектов", EditorStyles.boldLabel);

        if (Selection.activeGameObject)
        {
            gameObj = Selection.activeGameObject;   //Выделенный объект попадает в поле объекта
            startPosition = gameObj.transform.position; //Позиция объекта записывается в поле позиции старта

            startRot = gameObj.transform.rotation.eulerAngles;
            startRotation = gameObj.transform.rotation; //Поворот записывается в поле стартового поворота
        }
            

        gameObj = EditorGUILayout.ObjectField("Объект умножения", gameObj, typeof(GameObject), true) as GameObject;

        count = EditorGUILayout.IntSlider("Кол-во объектов", count, 1, 100);

        spaceBetween = EditorGUILayout.Slider("Расстояние между объектами", spaceBetween, 1, 50);

        directionIndex = EditorGUILayout.Popup("Направление умножения", directionIndex, directions);

        startPosition = EditorGUILayout.Vector3Field("Расположение первого объекта", startPosition);

        startRot = EditorGUILayout.Vector3Field("Поворот первого объекта", startRot);
        startRotation = Quaternion.Euler(startRot);

        nextRot = EditorGUILayout.Vector3Field("Поворот следующих объектов", nextRot);
        nextRotation = Quaternion.Euler(nextRot);

        #region Доп. параметры
        GUILayout.Space(10);
        isAdvanced = EditorGUILayout.BeginToggleGroup("Доп. параметры", isAdvanced);

        objName = EditorGUILayout.TextField("Имя объектов", objName);

        isGrouped = EditorGUILayout.BeginToggleGroup("Группировать под один объект", isGrouped);
        parrentsName = EditorGUILayout.TextField("Имя родительского объекта", parrentsName);
        EditorGUILayout.EndToggleGroup();

        EditorGUILayout.EndToggleGroup();
        #endregion

        if (GUILayout.Button("Создать объекты"))
        {
            CreateObject();
        }
    }

    /// <summary>
    /// Создание объектов в зависимости от выбранных параметров
    /// </summary>
    private void CreateObject()
    {
        if (gameObj && isGrouped)
        {
            GameObject parrent = new GameObject(parrentsName);

            switch (directionIndex)
            {
                //-x
                case 1:
                    for (int i = 0; i < count; i++)
                    {
                        if (i == 0)
                        {
                            GameObject temp = Instantiate(gameObj, startPosition, startRotation, parrent.transform);
                            temp.name = objName;
                        }
                        else
                        {
                            spawnPosition -= Vector3.right * spaceBetween;

                            GameObject temp = Instantiate(gameObj, spawnPosition, nextRotation, parrent.transform);
                            temp.name = objName + "(" + i + ")";
                        }
                    }
                    break;
                //+y
                case 2:
                    for (int i = 0; i < count; i++)
                    {
                        if (i == 0)
                        {
                            GameObject temp = Instantiate(gameObj, startPosition, Quaternion.identity, parrent.transform);
                            temp.name = objName;
                        }
                        else
                        {
                            spawnPosition += Vector3.up * spaceBetween;

                            GameObject temp = Instantiate(gameObj, spawnPosition, Quaternion.identity, parrent.transform);
                            temp.name = objName + "(" + i + ")";
                        }
                    }
                    break;
                //-y
                case 3:
                    for (int i = 0; i < count; i++)
                    {
                        if (i == 0)
                        {
                            GameObject temp = Instantiate(gameObj, startPosition, Quaternion.identity, parrent.transform);
                            temp.name = objName;
                        }
                        else
                        {
                            spawnPosition -= Vector3.up * spaceBetween;

                            GameObject temp = Instantiate(gameObj, spawnPosition, Quaternion.identity, parrent.transform);
                            temp.name = objName + "(" + i + ")";
                        }
                    }
                    break;
                //+z
                case 4:
                    for (int i = 0; i < count; i++)
                    {
                        if (i == 0)
                        {
                            GameObject temp = Instantiate(gameObj, startPosition, Quaternion.identity, parrent.transform);
                            temp.name = objName;
                        }
                        else
                        {
                            spawnPosition += Vector3.forward * spaceBetween;

                            GameObject temp = Instantiate(gameObj, spawnPosition, Quaternion.identity, parrent.transform);
                            temp.name = objName + "(" + i + ")";
                        }
                    }
                    break;
                //+z
                case 5:
                    for (int i = 0; i < count; i++)
                    {
                        if (i == 0)
                        {
                            GameObject temp = Instantiate(gameObj, startPosition, Quaternion.identity, parrent.transform);
                            temp.name = objName;
                        }
                        else
                        {
                            spawnPosition -= Vector3.forward * spaceBetween;

                            GameObject temp = Instantiate(gameObj, spawnPosition, Quaternion.identity, parrent.transform);
                            temp.name = objName + "(" + i + ")";
                        }
                    }
                    break;
                //+x Ибо по умолчанию это именно +x
                default:
                    for (int i = 0; i < count; i++)
                    {
                        if (i == 0)
                        {
                            GameObject temp = Instantiate(gameObj, startPosition, Quaternion.identity, parrent.transform);
                            temp.name = objName;
                        }
                        else
                        {
                            spawnPosition += Vector3.right * spaceBetween;

                            GameObject temp = Instantiate(gameObj, spawnPosition, Quaternion.identity, parrent.transform);
                            temp.name = objName + "(" + i + ")";
                        }
                    }
                    break;
            }
        }
        else if (gameObj)
        {
            switch (directionIndex)
            {
                //-x
                case 1:
                    for (int i = 0; i < count; i++)
                    {
                        if (i == 0)
                        {
                            GameObject temp = Instantiate(gameObj, startPosition, startRotation);
                            temp.name = objName;
                        }
                        else
                        {
                            spawnPosition -= Vector3.right * spaceBetween;
                            nextRot = nextRot * i + startRot;
                            nextRotation = Quaternion.Euler(nextRot);
                            
                            GameObject temp = Instantiate(gameObj, spawnPosition, nextRotation);
                            temp.name = objName + "(" + i + ")";
                        }
                    }
                    break;
                //+y
                case 2:
                    for (int i = 0; i < count; i++)
                    {
                        if (i == 0)
                        {
                            GameObject temp = Instantiate(gameObj, startPosition, startRotation);
                            temp.name = objName;
                        }
                        else
                        {
                            spawnPosition -= Vector3.right * spaceBetween;
                            nextRot = nextRot * i + startRot;
                            nextRotation = Quaternion.Euler(nextRot);

                            GameObject temp = Instantiate(gameObj, spawnPosition, nextRotation);
                            temp.name = objName + "(" + i + ")";
                        }
                    }
                    break;
                //-y
                case 3:
                    for (int i = 0; i < count; i++)
                    {
                        if (i == 0)
                        {
                            GameObject temp = Instantiate(gameObj, startPosition, startRotation);
                            temp.name = objName;
                        }
                        else
                        {
                            spawnPosition -= Vector3.right * spaceBetween;
                            nextRot = nextRot * i + startRot;
                            nextRotation = Quaternion.Euler(nextRot);

                            GameObject temp = Instantiate(gameObj, spawnPosition, nextRotation);
                            temp.name = objName + "(" + i + ")";
                        }
                    }
                    break;
                //+z
                case 4:
                    for (int i = 0; i < count; i++)
                    {
                        if (i == 0)
                        {
                            GameObject temp = Instantiate(gameObj, startPosition, startRotation);
                            temp.name = objName;
                        }
                        else
                        {
                            spawnPosition -= Vector3.right * spaceBetween;
                            nextRot = nextRot * i + startRot;
                            nextRotation = Quaternion.Euler(nextRot);

                            GameObject temp = Instantiate(gameObj, spawnPosition, nextRotation);
                            temp.name = objName + "(" + i + ")";
                        }
                    }
                    break;
                //+z
                case 5:
                    for (int i = 0; i < count; i++)
                    {
                        if (i == 0)
                        {
                            GameObject temp = Instantiate(gameObj, startPosition, startRotation);
                            temp.name = objName;
                        }
                        else
                        {
                            spawnPosition -= Vector3.right * spaceBetween;
                            nextRot = nextRot * i + startRot;
                            nextRotation = Quaternion.Euler(nextRot);

                            GameObject temp = Instantiate(gameObj, spawnPosition, nextRotation);
                            temp.name = objName + "(" + i + ")";
                        }
                    }
                    break;
                //+x Ибо по умолчанию это именно +x
                default:
                    for (int i = 0; i < count; i++)
                    {
                        if (i == 0)
                        {
                            GameObject temp = Instantiate(gameObj, startPosition, startRotation);
                            temp.name = objName;
                        }
                        else
                        {
                            spawnPosition -= Vector3.right * spaceBetween;
                            nextRot = nextRot * i + startRot;
                            nextRotation = Quaternion.Euler(nextRot);

                            GameObject temp = Instantiate(gameObj, spawnPosition, nextRotation);
                            temp.name = objName + "(" + i + ")";
                        }
                    }
                    break;
            }

        }
    }
}
