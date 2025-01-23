using UnityEngine;

public enum ModeGame
{
    Play,
    Pause,
    EndGame
}
public class GameManager : Singleton<GameManager>
{
    [SerializeField] private DataGame _dataGame;
    [SerializeField] private SpawnBullet _spawnBullet;
    [SerializeField] private SpawnWall _spawnWall;
    [SerializeField] private int scorce;
    public ModeGame mode;

    private int index = 0;
    public int Scorce
    {
        get => scorce;
        set
        {
            scorce = value;
            UIManager.Instance.TextScore.text = scorce.ToString();
            if (scorce <= 300)
            {
                _spawnWall.DataWall = _dataGame.DataWalls[0];
            }else if (scorce > 300 && scorce <= 600)
            {
                _spawnWall.DataWall = _dataGame.DataWalls[1];
            }
            else
            {
                _spawnWall.DataWall = _dataGame.DataWalls[2];
            }


            if (scorce % 180 == 0)
            {
                index += 1;
                if (index >= _dataGame.DataBullet.Count)
                {
                    index -= 1;
                }
                _spawnBullet.DataBullet = _dataGame.DataBullet[index];
            }
        }
    }

    private void Start()
    {
        _spawnWall.DataWall = _dataGame.DataWalls[0];
        _spawnBullet.DataBullet = _dataGame.DataBullet[index];
    }
}
