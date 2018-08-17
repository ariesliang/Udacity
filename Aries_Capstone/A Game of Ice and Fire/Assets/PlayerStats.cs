using System;

[Serializable]
public class PlayerStats {

    public int level;
    public int spawnCount;
    public int obstacleMovingSpeed;

    public PlayerStats (int level, int spawnCount, int obstacleMovingSpeed)
    {
        this.level = level;
        this.spawnCount = spawnCount;
        this.obstacleMovingSpeed = obstacleMovingSpeed;
    }

}
