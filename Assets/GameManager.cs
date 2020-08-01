using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform leftObstacle;
    [SerializeField] Transform rightObstacle;
    [SerializeField] Camera cam;
    [SerializeField] Canvas mainCanvas;
    [SerializeField] Text positionDebug;
    [SerializeField] Text canvasWidthDebug;
    [SerializeField] Text cameraWidthDebug;
    [SerializeField] Text heartText;
    [SerializeField] Text scoreText;
    [SerializeField] Text fuelText;
    [SerializeField] Save save; //will make it public later.

    Rocket rocket;
    float defaultWidth = 1600;
    float currentWidth;
    private float defaultPositionX;
    private float currentPositionX;
    public float currentObstacleDistance;

    #region Singleton
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    // Start is called before the first frame update.
    void Start()
    {
        defaultPositionX = rightObstacle.position.x;
        cam = Camera.main;
        rocket = Rocket.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        currentWidth = GetCanvasWidth();
        if (defaultWidth != currentWidth)
        {
            OnResolutionChanged();
            currentObstacleDistance = currentPositionX;
            positionDebug.text = "Current Position: " + currentPositionX.ToString();
            canvasWidthDebug.text = "Canvas Width: " + GetCanvasWidth();
            cameraWidthDebug.text = "Camera Width: " + GetCameraWidth();
        }
        HeartCount();
    }

    private void HeartCount()
    {
        heartText.text = rocket.life.ToString();
    }

    private void OnResolutionChanged()
    {
        // run resolution.
        if (defaultWidth > currentWidth)
        {
            currentPositionX = defaultPositionX / (defaultWidth / currentWidth);
        }
        else
        {
            currentPositionX = defaultPositionX * (currentWidth / currentWidth);
        }
        defaultWidth = currentWidth;
        leftObstacle.position = new Vector3(-currentPositionX - 4f, leftObstacle.position.y, leftObstacle.position.z);
        rightObstacle.position = new Vector3(currentPositionX + 4f, rightObstacle.position.y, rightObstacle.position.z);
    }
    private float GetCameraWidth()
    {
        return cam.pixelWidth;
    }

    private float GetCanvasWidth()
    {
        return mainCanvas.GetComponent<RectTransform>().rect.width;
    }

    private bool IsObjectExist(GameObject gameObject)
    {
        if (gameObject != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
