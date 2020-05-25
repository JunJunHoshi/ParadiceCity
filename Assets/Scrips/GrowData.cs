using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowData : MonoBehaviour
{
    //BasicGrowMinとBasicGrowMaxでBasicDataの成長値となんのTagを取れるかが決まる。
    public int[] BasicGrowMin = new int[] {0,0,0,0,0,0};
    public int[] BasicGrowMax = new int[] {0,0,0,0,0,0};
    [SerializeField]public int[] Ori = new int[10];
    // Start is called before the first frame update
    //BasicGrowDataについて
    //0 HP
    //1 攻撃
    //2 守備(攻撃の半分くらい)
    //3 加工
    //4 回復
    //5 愛情 の成長率を担当
    
    //Oriデータについて
    //0 知性(回復基幹)
    //1 暴力性(攻撃基幹)
    //2 頑丈(守備基幹)
    //3 色気(MP基幹)
    //4 健康(体力基幹)
    //5 勇敢(攻撃と守備)
    //6 情熱(加工基幹)
    //7 社交性(愛情)
    //8 やさしさ
    //9 面白さ
    
    void Start()
    {
        BasicGrowMax[0] += (Ori[4] + Ori[2]/3 + Ori[0]/4)*3; 
        BasicGrowMax[1] += (int) (Ori[1]*1.3) - Ori[8]/2 + (int)(Ori[5]*0.5) + Ori[6]/2;
        BasicGrowMax[2] += (int) ((Ori[2]*1.4) + Ori[5]/2 - Ori[1]/8)/2;
        BasicGrowMax[3] += (int) (Ori[6] * 1.3) - Ori[7]/3 / 2 + (int) (Ori[1] * 0.6);
        BasicGrowMax[4] += (int) (Ori[0]*1.2) - Ori[1]/2 + Ori[6]/3 + (int)(Ori[8]*0.8);
        BasicGrowMax[5] += Ori[0]/3 + Ori[8]/2 + Ori[6]/3 + Ori[7]/4 + Ori[9]/2;
        
        BasicGrowMin[0] += ((int)(Ori[4]*0.8) + Ori[2]/4 + Ori[0]/4)*3; 
        BasicGrowMin[1] += (int) (Ori[1]) - Ori[8]/2 + Ori[5]/2 + Ori[6]/4;
        BasicGrowMin[2] += (int) ((Ori[2]) + Ori[5]/3 - Ori[1]/8)/2;
        BasicGrowMin[3] += (int) (Ori[6]) - Ori[7]/3 + (int) (Ori[1] * 0.5);
        BasicGrowMin[4] += (int) (Ori[0]) - Ori[1]/2 + Ori[6]/4 + (int)(Ori[8]/2);
        BasicGrowMin[5] += Ori[0]/4 + Ori[8]/4 + Ori[6]/5 + Ori[7]/4 + Ori[9]/3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
