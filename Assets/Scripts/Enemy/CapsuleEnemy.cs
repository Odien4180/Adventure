using System;

public class CapsuleEnemy : EnemyBase
{
    private class CapsuleEnemyHitPipeline : IHitPipeline, IDisposable
    {
        private CapsuleEnemy enemy;
        public CapsuleEnemyHitPipeline(CapsuleEnemy enemy)
        {
            this.enemy = enemy;
        }

        public void Hit(HitContainer hitContainer)
        {

        }

        public void Dispose()
        {
            enemy = null;
        }
    }

    private void Start()
    {
        Hittable = new CapsuleEnemyHitPipeline(this);
    }
}

