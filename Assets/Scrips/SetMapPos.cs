using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetMapPos : MonoBehaviour
{
    [SerializeField] float x;
    [SerializeField] float y;
    [SerializeField] private float world;
    [SerializeField] Animator BlackCurtain;
    [SerializeField] GameObject MainChara;
    // Start is called before the first frame update
 
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            MainChara = other.gameObject;
            BlackCurtain.SetBool("Mapmove", true);
            PositionSet();
            Invoke("MoveOut", 0.5f);
        }
    }

    public void MoveOut()
    {
        if(MainChara.tag == "Player")
        MainChara.SendMessage("GetPos", world);
    }
    public void PositionSet()
    {
        GetMapPos.MAPX = x;
        GetMapPos.MAPY = y;
    }
}
