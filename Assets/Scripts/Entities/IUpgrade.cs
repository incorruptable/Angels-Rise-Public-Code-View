public interface IUpgrade
{
    void Apply(Player player);
}

public class ExtraLifeUpgrade : IUpgrade
{
    private int lifeIncrease;
    public ExtraLifeUpgrade(int lifeIncrease)
    {
        this.lifeIncrease = lifeIncrease;
    }
    public void Apply(Player player) 
    {
        player.SetHealth(player.GetHealth()+lifeIncrease);
    }
}

public class SpeedBoost : IUpgrade
{
    private float speedIncrease;
    public SpeedBoost(float speedBoost)
    {
        this.speedIncrease = speedBoost;
    }

    public void Apply(Player player)
    {
        player.SetSpeed(player.GetSpeed() + speedIncrease);
    }
}