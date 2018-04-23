using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatTextManager : MonoBehaviour {
    public static float _health;

    private static CombatTextManager _instance;

    public GameObject _textPrefab;

    public RectTransform _canvasTransform;

    public float _speed;

    public Vector3 direction;


    public static CombatTextManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<CombatTextManager>();    
            }

            return Instance;


        }
    }
	
    public void CreateText(Vector3 position, string text)
    {
        GameObject sct = (GameObject)Instantiate(_textPrefab , position, Quaternion.identity);

        sct.transform.SetParent(_canvasTransform);
        sct.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        sct.GetComponent<CombatText>().Initialize(_speed, direction);
        sct.GetComponent<Text>().text = text;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
