using UnityEngine;
using System.Collections;

public class MonsterMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject _monster;
    public static float moveSpeed = 0.8f;
    public Vector3 aiDirection = Vector3.forward;

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.forward * Time.deltaTime * moveSpeed;
        _monster.SetActive(true);
        Invoke("Disappear", 20);

    }

    private void Disappear()
    {
        _monster.SetActive(false);
    }

}