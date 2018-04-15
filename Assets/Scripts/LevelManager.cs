using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private Text _levelEndText;
    private Transform _currentHallEnd;

    private Queue<GameObject> _activeHalls = new Queue<GameObject>();
    private int _numInstantiatedHalls = 1;
    private bool _flipNext = true;

    void Start()
    {
        _currentHallEnd = _currentHall.transform.Find("End");
        _activeHalls.Enqueue(_currentHall);

    }

    // Update is called once per frame
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
        else if(_numInstantiatedHalls >= levelLength &&
            Vector3.Distance(_player.transform.position, _currentHallEnd.position) <= 1)
        {
            EndLevel();
        }


    }

    private void UpdateScore()
    {
        _progressText.text = "Score: " + (int) _player.GetComponent<Player>().GetProgress();
    }

    private void EndLevel()
    {
        Time.timeScale = 0;
        _levelEndText.text = "Level Completed";
        _levelEndText.gameObject.SetActive(true);
    }

    public void FailLevel()
    {
        StartCoroutine("FailRoutine");
    }

    private IEnumerator FailRoutine()
    {
        yield return new WaitForSeconds(1f);
        _levelEndText.text = "Level Failed";
        _levelEndText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0;

    }
}
