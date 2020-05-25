using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMapPos : MonoBehaviour
{
    public static float MAPX;
    public static float MAPY;
    public int World;
    // Start is called before the first frame update
    public void GetPos(int world)
    {
        if (MAPX != null && MAPY != null)
        transform.position = new Vector3(MAPX, MAPY,0);
        if (world == 1)
        {
            //worldmapに登場時
            transform.Find("Main Camera").GetComponent<Camera>().fieldOfView = 67f;
            GetComponent<MoveController>().SPEED = 4.5f;
            World = 1;
        }
        else
        {
            transform.Find("Main Camera").GetComponent<Camera>().fieldOfView = 70f;
            GetComponent<MoveController>().SPEED = 5f;
            World = 0;
        }
    }
}
