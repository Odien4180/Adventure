public class HitContainer
{
    private int damage;
    public int Damage => damage;
}

public interface IHittable
{
    void Hit(HitContainer hitContainer);
}