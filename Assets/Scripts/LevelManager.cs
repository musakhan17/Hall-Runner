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
    //prefab for hallway
    [SerializeField]
    private GameObject _hallPrefab;
    //most recent hallway object in game
    [SerializeField]
    private GameObject _currentHall;
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private Text _progressText;
    [SerializeField]
    private GameObject _levelCompletedDisplay;
    [SerializeField]
    private GameObject _levelFailedDisplay;
    [SerializeField]
    private float _fireFrequency = 0.5f; //chance of spawning fire @ a spawn point
    [SerializeField]
    private float _furnitureFrequency = 0.5f; //chance of spawning an object @ a spawn point
    [SerializeField]
    private float _furnitureIsObstacleFrequency = 0.1f; //chance an object will become an obstacle
    private Transform _currentHallEnd;

    private Queue<GameObject> _activeHalls = new Queue<GameObject>();
    private int _numInstantiatedHalls = 1;
    private bool _flipNext = true;

    void Start()
    {
        _player.GetComponent<Player>().SetProgress(SceneLoader.GetPreviousProgress());
        Time.timeScale = 1;
        _currentHallEnd = _currentHall.transform.Find("End");
        _activeHalls.Enqueue(_currentHall);
        _currentHall.GetComponent<ObstacleSpawn>().Init(_fireFrequency, _furnitureFrequency, _furnitureIsObstacleFrequency);

    }

    void Update()
    {
        UpdateScore();

        if (_activeHalls.Count > 2)
        {
            Destroy(_activeHalls.Dequeue());
        }

        if (_numInstantiatedHalls < levelLength &&
            Vector3.Distance(_player.transform.position, _currentHallEnd.position) <= spawnDist)
        {
            Quaternion rotation = Quaternion.identity;
            if (_flipNext)
            {
                rotation = Quaternion.Euler(0, 180, 0);
            }
            _flipNext = !_flipNext;

            Vector3 scale = _currentHall.transform.localScale;
            GameObject newHall = Instantiate(_hallPrefab, _currentHallEnd.position, rotation);
            newHall.GetComponent<ObstacleSpawn>().Init(_fireFrequency,
                                                       _furnitureFrequency,
                                                       _furnitureIsObstacleFrequency);

            //flip new hall
            scale.x = scale.x * -1;
            newHall.transform.localScale = scale;

            //move to correct location
            newHall.transform.position = _currentHallEnd.position;


            //update current hall
            _currentHall = newHall;
            _currentHallEnd = _currentHall.transform.Find("End");
            _activeHalls.Enqueue(_currentHall);
            _numInstantiatedHalls++;
            if (_numInstantiatedHalls == levelLength)
            {
                _currentHall.transform.Find("Corridor").Find("Front_Door").gameObject.SetActive(true);
            }
        }
        else if (_numInstantiatedHalls >= levelLength &&
            Vector3.Distance(_player.transform.position, _currentHallEnd.position) <= 1)
        {
            EndLevel();
        }


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
        _levelCompletedDisplay.gameObject.SetActive(true);
    }

    /**
    called by player on collision
     */
    public void FailLevel()
    {
        StartCoroutine("FailRoutine");
    }

    private IEnumerator FailRoutine()
    {
        yield return new WaitForSeconds(1f);
        _levelFailedDisplay.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0;
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
