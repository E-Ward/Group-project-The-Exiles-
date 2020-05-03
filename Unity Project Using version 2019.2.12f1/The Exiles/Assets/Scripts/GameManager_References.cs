using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_References : MonoBehaviour
{
    public string playerTag;
    public static string _playerTag;

    public string enemyTag;
    public static string _enemyTag;

    public static GameObject _player;

    public GameObject Animal1;
    public GameObject Animal2;
    public GameObject Animal3;
    public GameObject Animal4;

    private void OnEnable()
    {
        if(playerTag =="")
        {
            Debug.LogWarning("Enter the name of the player in the ");
        }
        if (enemyTag == "")
        {
            Debug.LogWarning("Enter the name of the enemy in the ");
        }

        _playerTag = playerTag;
        _enemyTag = enemyTag;

        _player = GameObject.FindGameObjectWithTag(_playerTag);
}
}
