using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private List<GameObject> platforms_;
    private List<GameObject> turrets_;
    private List<GameObject> spawners_;
    private List<GameObject> powerups_;
    private GameObject player_;
    private PlayerCharacter player_script_;
    private Text score_ui_;
    private Text health_ui_;
    private UIBehaviour ui_script_;
    private GameObject camera_;

    private float _score;
    public float score
    {
        get { return _score; }
        set 
        {
            _score = value;
            if (score_ui_ != null)
                score_ui_.text = "" + _score;
        }
    }

    private static GameManager instance_;
    public static GameManager Instance
    {
        get { return instance_; }
    }

    public enum GameState
    {
        PLAYING,
        DEAD,
        VICTORY
    }

    private GameState _game_state_;
    public GameState currentGameState
    {
        get { return _game_state_; }
        set
        {
            _game_state_ = value;

            switch (value)
            {
                case GameState.PLAYING:
                    ToggleActiveList(turrets_, true);
                    TurnSpawnersOn();
                    player_.transform.position = new Vector3(0, 1.48f, 0);
                    player_script_.health = 100;
                    player_script_.damage = 20;
                    score = 0;
                    ActivatePowerups();
                    player_script_.TogglePlayerKeyboard(true);
                    player_.SetActive(true);
                    ui_script_.ShowUI(0);
                    break;

                case GameState.DEAD:
                    player_script_.TogglePlayerKeyboard(false);
                    ToggleActiveList(turrets_, false);
                    ui_script_.ShowUI(1);
                    break;

                case GameState.VICTORY:
                    player_script_.TogglePlayerKeyboard(false);
                    player_.SetActive(false);
                    ui_script_.ShowUI(2);
                    break;
            }
        }
    }

    private void Awake()
    {
        if (instance_ != null && instance_ != this)
        {
            Destroy(gameObject);
            return;
        }
        instance_ = this;
        DontDestroyOnLoad(gameObject);
        Init();
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        if (currentGameState == GameState.PLAYING)
        {
            ChangePlatforms(player_script_.is_phasing_);
            if (currentGameState == GameState.PLAYING)
            {
                camera_.transform.position = new Vector3(12 + player_.transform.position.x, camera_.transform.position.y, camera_.transform.position.z);
            }
        }
    }

    void Init()
    {
        score = 0;
        platforms_ = new List<GameObject>();
        InitPlatforms();
        turrets_ = new List<GameObject>();
        InitTurrets();
        spawners_ = new List<GameObject>();
        InitSpawners();
        powerups_ = new List<GameObject>();
        InitPowerups();
        player_ = GameObject.Find("Player");
        player_script_ = player_.GetComponent<PlayerCharacter>();
        ui_script_ = GameObject.Find("Canvas").GetComponent<UIBehaviour>();
        health_ui_ = GameObject.Find("Life").GetComponent<Text>();
        score_ui_ = GameObject.Find("Score").GetComponent<Text>();
        camera_ = GameObject.FindGameObjectWithTag("MainCamera");
        currentGameState = GameState.PLAYING;
    }

    void InitPlatforms()
    {
        Transform platforms = GameObject.Find("Scenery").transform.Find("Platforms");
        foreach (Transform child in platforms)
        {
            platforms_.Add(child.gameObject);
        }
    }

    void InitTurrets()
    {
        Transform turrets = GameObject.Find("Turrets").transform;
        foreach (Transform child in turrets)
        {
            turrets_.Add(child.gameObject);
        }
    }

    void InitSpawners()
    {
        Transform spawners = GameObject.Find("Spawners").transform;
        foreach (Transform child in spawners)
        {
            spawners_.Add(child.gameObject);
        }
    }

    void InitPowerups()
    {
        Transform powerups = GameObject.Find("PowerUps").transform;
        foreach (Transform child in powerups)
        {
            powerups_.Add(child.gameObject);
        }
    }

    void ActivatePowerups()
    {
        foreach (GameObject go in powerups_)
        {
            go.SetActive(true);
        }
    }

    void TurnSpawnersOn()
    {
        foreach (GameObject spawner in spawners_)
        {
            EnemySpawner es = spawner.GetComponent<EnemySpawner>();
            es.Activate();
        }
    }

    public void ToggleActiveList(List<GameObject> objs, bool toggle)
    {
        foreach (GameObject obj in objs)
        {
            obj.SetActive(toggle);
        }
    }

    void ChangePlatforms(bool isPhasing)
    {
        foreach (GameObject platform in platforms_)
        {
            Collider platform_col = platform.GetComponent<Collider>();
            platform_col.enabled = !isPhasing;
        }
    }

    public void RefreshHealth()
    {
        health_ui_.text = "" + player_.GetComponent<PlayerCharacter>().health;
    }

    public void playerDied()
    {
        currentGameState = GameState.DEAD;
    }

    public void playerWon()
    {
        currentGameState = GameState.VICTORY;
    }

    public void Play()
    {
        currentGameState = GameState.PLAYING;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}