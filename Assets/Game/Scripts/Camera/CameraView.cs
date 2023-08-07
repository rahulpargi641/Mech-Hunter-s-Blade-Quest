using Cinemachine;
using UnityEngine;

public class CameraView : MonoBehaviour
{
    public CameraController Controller { private get; set; }

    private Transform playerTransform;
    private CinemachineVirtualCamera virtualCamera;

    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        SetupFollowCamera();
    }

    private void SetupFollowCamera()
    {
        PlayerView playerView = FindAnyObjectByType<PlayerView>();
        if (playerView)
            playerTransform = playerView.transform;

        if (playerTransform && virtualCamera)
        {
            virtualCamera.Follow = playerTransform;
            virtualCamera.LookAt = playerTransform;
        }
    }
}
