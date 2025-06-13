using UnityEngine;

public class CapsuleEnemy : EnemyBase
{
    public override void Hit(HitContainer hitContainer)
    {
        FloatingText.Create(hitContainer.Damage.ToString(), transform.position + new Vector3(0, 1, 0), .5f);
    }
}

