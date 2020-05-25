using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VillageEnter : MonoBehaviour
{
    [SerializeField] float x;
    [SerializeField] float y;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D (Collision2D other)
    {
        Invoke("MoveScene",1f);
    }

    void MoveScene()
    {
        GetMapPos.MAPX = x;
        GetMapPos.MAPY = y;
    }
}
