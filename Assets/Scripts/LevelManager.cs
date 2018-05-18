using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    //player distance at which to spawn next hall
    public float spawnDist = 10.0f;
    //number of hallway units to spawn for this level
    public int levelLength = 5;
    //pause on fail before letting player fall down or not
    public bool letFall = false;
    [SerializeField]
    private bool _autoMove = false;
    //display instructions at start of level
    public bool instructions = false;
    //audio death source
    [SerializeField]
    private AudioClip death1;
    //prefab for hallway
    [SerializeField]
    private GameObject _hallPrefab;
    //most recent hallway object in game
    [SerializeField]
    private GameObject _currentHall;
    [SerializeField]
    private GameObject _player;
    public float _playerSpeed = 2;
    [SerializeField]
    private GameObject _enemy;
    public float _enemySpeed = 2;
    [SerializeField]
    private Text _progressText;
    [SerializeField]
    private GameObject _instructionsDisplay = null;
    [SerializeField]
    private GameObject _levelCompletedDisplay;
    [SerializeField]
    private GameObject _levelFailedDisplay;
    [SerializeField]
    private GameObject _pauseDisplay;
    [SerializeField]
    private float _fireFrequency = 0.5f; //chance of spawning fire @ a spawn point
    [SerializeField]
    private float _furnitureFrequency = 0.5f; //chance of spawning an object @ a spawn point
    [SerializeField]
    private float _furnitureIsObstacleFrequency = 0.1f; //chance an object will become an obstacle
    [SerializeField]
    private float _obstacleTriggerDistance = 8f; //chance an object will become an obstacle
    private Transform _currentHallEnd;
    private Queue<GameObject> _activeHalls = new Queue<GameObject>();
    private int _numInstantiatedHalls = 1;

    public bool _gameRunning;
    [SerializeField]
    private GameObject _camera;
    private bool _done = false;

    void Start()
    {
        _player.GetComponent<Player>().SetProgress(SceneLoader.GetPreviousProgress());
        Time.timeScale = 1;
        _currentHallEnd = _currentHall.transform.Find("End");
        _activeHalls.Enqueue(_currentHall);
        _currentHall.GetComponent<ObstacleSpawn>().Init(_fireFrequency, _furnitureFrequency, _furnitureIsObstacleFrequency, _obstacleTriggerDistance);
        _currentHall.transform.SetParent(transform);
        if (instructions)
        {
            _gameRunning = false;
            _instructionsDisplay.gameObject.SetActive(true);
            _progressText.gameObject.SetActive(false);
        }
        else
        {
            Game_Start();
        }

        _enemy.GetComponent<Enemy>().SetSpeed(_enemySpeed);
        _enemy.GetComponent<Enemy>().AddWayPoint(_currentHall.transform.Find("WayPoint1").position);
        _enemy.GetComponent<Enemy>().AddWayPoint(_currentHall.transform.Find("WayPoint2").position);
        _enemy.GetComponent<Enemy>().AddWayPoint(_currentHall.transform.Find("WayPoint3").position);
        /*
        if (_autoMove && _instructionsDisplay != null)
        {
            _instructionsDisplay.transform.Find("InstructionText").GetComponent<Text>().text =
            "Remember the creature? Now, you're supposed to run away from it.\nSo, you will be move towards where you look.\nYou need to dodge any object that comes in your way\nDo not be scared, but don't think you won't get scared... \n\nPress Start to begin.";
        }
        else if (_instructionsDisplay != null)
        {
            _instructionsDisplay.transform.Find("InstructionText").GetComponent<Text>().text =
            "Remember the creature? Now, you're supposed to run away from it.\n Hold the touchpad and move your arms up and down to move.\nYou will move towards where you look.\nYou need to dodge any object that comes in your way\nDo not be scared, but don't think you won't get scared... \n\nPress Start to begin.";
        }
        */
    }

    void Update()
    {
        if (_gameRunning)
        {
            if (_autoMove)
            {
                //PlayerMovement();
                _player.GetComponent<Player>().AutoMove();
            }
            _enemy.GetComponent<Enemy>().StartMoving();
            UpdateScore();
        }
        if (_activeHalls.Count > 2)
        {
            Destroy(_activeHalls.Dequeue());
        }

        if (_numInstantiatedHalls < levelLength &&
            Vector3.Distance(_player.transform.position, _currentHallEnd.position) <= spawnDist)
        {
            //Quaternion rotation = Quaternion.identity;
            Quaternion rotation = _currentHall.transform.rotation;
            Vector3 angles = rotation.eulerAngles;
            angles.y = (angles.y + 180) % 360;
            rotation = Quaternion.Euler(angles.x, angles.y, angles.z);

            Vector3 scale = _currentHall.transform.localScale;
            GameObject newHall = Instantiate(_hallPrefab, _currentHallEnd.position, rotation);

            //flip new hall
            scale.x = scale.x * -1;
            newHall.transform.localScale = scale;

            //move to correct location
            newHall.transform.position = _currentHallEnd.position;
            newHall.GetComponent<ObstacleSpawn>().Init(_fireFrequency,
                                                       _furnitureFrequency,
                                                       _furnitureIsObstacleFrequency,
                                                       _obstacleTriggerDistance);
            newHall.transform.SetParent(transform);

            //update current hall
            _currentHall = newHall;
            _currentHallEnd = _currentHall.transform.Find("End");
            _activeHalls.Enqueue(_currentHall);
            _numInstantiatedHalls++;
            if (_numInstantiatedHalls == levelLength)
            {
                _currentHall.transform.Find("Corridor").Find("Front_Door").gameObject.SetActive(true);
            }
            _enemy.GetComponent<Enemy>().AddWayPoint(_currentHall.transform.Find("WayPoint1").position);
            _enemy.GetComponent<Enemy>().AddWayPoint(_currentHall.transform.Find("WayPoint2").position);
            _enemy.GetComponent<Enemy>().AddWayPoint(_currentHall.transform.Find("WayPoint3").position);
        }
        else if (_numInstantiatedHalls >= levelLength &&
            Vector3.Distance(_player.transform.position, _currentHallEnd.position) <= 2)
        {
            EndLevel();
        }
    }

    public void Game_Start()
    {
        _gameRunning = true;
        _instructionsDisplay.gameObject.SetActive(false);
        _progressText.gameObject.SetActive(true);
    }


    private void PlayerMovement()
    {
        Vector3 direction = -_player.GetComponent<Player>().GetHorizontalDirection();
        float speed = _player.GetComponent<Player>().GetSpeed();
        float movement = speed * Time.deltaTime;
        transform.Translate(direction * movement);
        //_player.GetComponent<Player>().AddProgress(movement);

    }

    private void UpdateScore()
    {
        _progressText.text = "Score: " + (int)_player.GetComponent<Player>().GetProgress();
    }

    /**
    when player successfully reaches end of level
     */
    private void EndLevel()
    {
        Time.timeScale = 0;
        _done = true;
        _levelCompletedDisplay.gameObject.SetActive(true);
    }

    /**
    called by player on collision
     */
    public void FailLevel()
    {
        _gameRunning = false;
        _done = true;
        StartCoroutine("FailRoutine");
    }

    private IEnumerator FailRoutine()
    {
        if (letFall)
        {
            yield return new WaitForSeconds(1f);
            _levelFailedDisplay.gameObject.SetActive(true);
            yield return new WaitForSeconds(2f);
            Time.timeScale = 0;
            // death1.Play();

        }
        else
        {
            _levelFailedDisplay.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }

    void OnTriggerEnter(Collider other)
    {

        if (letFall)
        {
            //other.tag("death1").Play();

        }
    }

    public void Pause()
    {
        _pauseDisplay.gameObject.SetActive(true);
        Time.timeScale = 0;
        _camera.GetComponent<Sigtrap.ImageEffects.Tunnelling>().enabled = false;
        _gameRunning = false;
    }

    public void Resume()
    {
        _pauseDisplay.gameObject.SetActive(false);
        _camera.GetComponent<Sigtrap.ImageEffects.Tunnelling>().enabled = true;
        if (!_done)
        {
            Time.timeScale = 1;
            _gameRunning = true;
        }
    }

    /**
    load next level - called on click by button
     */
    public void Continue(string levelName)
    {
        SceneLoader.LoadScene(levelName, _player.GetComponent<Player>().GetProgress());
    }

    /**
    restart level - called on click by button
     */
    public void Restart()
    {
        _pauseDisplay.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        SceneLoader.LoadScene("_starter", 0);
    }
}
