[System.Serializable]
public class UserGameData
{
    public int level;
    public float experience;
    public int gold;
    public int jewel;
    public int heart;

    public int dailyBestScore;  // 일일 최고 무한모드 점수


    public void Reset()
    {

        level = 1;
        experience = 0;
        gold = 0;
        jewel = 0;
        heart = 80;
        dailyBestScore = 0;

    }
}


