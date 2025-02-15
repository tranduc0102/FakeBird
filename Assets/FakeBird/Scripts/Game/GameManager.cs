using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField] private Transform backGround;
    private Vector3 originBackGround;
    public ModeGame mode;

    private int index = 0;
    public int Scorce
    {
        get => scorce;
        set
        {
            scorce = value;
            if (PlayerPrefs.GetInt("HightScore", 0) <= value)
            {
                PlayerPrefs.SetInt("HightScore", value);
            }
            UIManager.Instance.TextScore.text ="Score: " + scorce;
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

    protected override void Awake()
    {
        base.KeepAlive(false);
        base.Awake();
    }

    private void Start()
    {
        _spawnWall.DataWall = _dataGame.DataWalls[0];
        _spawnBullet.DataBullet = _dataGame.DataBullet[index];
        originBackGround = backGround.position;
    }

    public void PlayAgain()
    {
        mode = ModeGame.Play;
        DeActiveWall();
        Scorce = 0;
        backGround.position = originBackGround;
        UIManager.Instance.PlayAgain();
    }

    private void DeActiveWall()
    {
        foreach (Transform wall in _spawnWall.transform)
        {
            PoolingManager.Despawn(wall.gameObject);
        }
    }

    public void BackHome()
    {
        //SceneManager.LoadSceneAsync("Home");
    }
}
