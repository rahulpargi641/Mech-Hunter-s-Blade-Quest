
public class PickupsModel
{
    public int HealOrbGain { get; private set; }
    public PickupsController Controller { private get; set; }
    public PickupsModel()
    {
        HealOrbGain = 20;
    }
}
