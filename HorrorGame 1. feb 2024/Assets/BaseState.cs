using UnityEngine;

public abstract class BaseState
{
    public abstract void EnterState(PlayerScript playerScript);
    public abstract void UpdaterState(PlayerScript playerScript);
    public abstract void Other(PlayerScript playerScript);
}
