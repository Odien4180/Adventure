using UnityEngine;

public abstract class Controllable : MonoBehaviour
{
    private IHitPipeline hittable;
    public IHitPipeline Hittable
    {
        get => hittable;
        protected set => hittable = value;
    }
    public virtual void Hit(HitContainer hitContainer)
    {
        hittable?.Hit(hitContainer);
    }

    public virtual void Die() { }
}