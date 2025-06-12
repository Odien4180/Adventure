using UnityEngine;

public class HitContainer
{
    private int damage;
    public int Damage => damage;
}

public interface IHitPipeline
{
    void Hit(HitContainer hitContainer);
}