public class PlayerHealth : Entity
{
    private new void Awake()
    {
        base.Awake();
    }

    public override void Die()
    {
        GameManager.Main.EndGame();
        Destroy(this);
    }
}