using UnityEngine;

public class CameraService : MonoBehaviour
{
    [SerializeField] CameraView cameraView;
    private CameraController cameraController;
    void Awake()
    {
        cameraController = new CameraController(cameraView);
    }
}
