using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScare : MonoBehaviour {

    [SerializeField]
    private GameObject _monster;
    private AudioSource _audio;
    [SerializeField]
    private AudioClip _scareSound;
    public bool played = false;
    public bool trigger = false;

	// Use this for initialization
	void Start () {
        played = false;
        _monster.SetActive(false);
	}

	private void OnTriggerEnter(Collider other)
	{
        trigger = true;
	}

    IEnumerable RemoveOverTime() {
        yield return new WaitForSeconds(0.3f);
        _monster.SetActive(false);
    }

    private void Scream() {
        if (!played) {
            played = true;
            _audio.PlayOneShot(_scareSound);
        }
    }
	// Update is called once per frame
	void Update () {
        if (trigger == true) {
            _monster.SetActive(true);
            Scream();
            RemoveOverTime();
        }
	}
}
