
public class CameraController
{
    private CameraView view;
    public CameraController(CameraView view)
    {
        this.view = view;

        view.Controller = this;
    }
}
