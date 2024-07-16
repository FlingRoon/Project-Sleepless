using UnityEngine;

public abstract class WendigoBaseState
{
    public abstract void EnterState(WendigoScript WendigoScript);
    public abstract void UpdaterState(WendigoScript WendigoScript);
    public abstract void Other(WendigoScript WendigoScript);
}
