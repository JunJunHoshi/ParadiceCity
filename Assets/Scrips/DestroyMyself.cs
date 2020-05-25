using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMyself : MonoBehaviour
{
    // number: 自分が仲間のうちのどの順番にいるのかを示す変数.
    
    public int number;
    
    // Start is called before the first frame update
    void Start()
    {
        number = 0;
    }

    // Update is called once per frame
    void Dest()
    {
        Invoke("death",0.5f);
    }

    void death()
    {
        //二度と触れないテキトーな配置に飛ばす
        for (int i = 0; i < 6; i++)
        {
            if(number==i)
            transform.position = new Vector3(-25, 60, 0);
        }
    }
}
