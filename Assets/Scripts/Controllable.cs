using UnityEngine;

public abstract class Controllable : MonoBehaviour, IHittable
{
    public abstract void Hit(HitContainer hitContainer);

    public virtual void Die() { }
}