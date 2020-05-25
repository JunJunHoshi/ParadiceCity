using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeKeeper : MonoBehaviour
{
    [SerializeField] public static int CURRENTTIME = 1;
    [SerializeField] public Text YearText;
    [SerializeField] public static List<int> CHAPYEARLIST = new List<int>();
    [SerializeField] private List<int> chapyearlist;
    // Start is called before the first frame update
    void Awake()
    {
        CHAPYEARLIST = chapyearlist;
        Invoke("BroadCastGetTagAndSkill", 1.5f);
        YearText.text = "~ 西暦: "+CURRENTTIME+"年 ~";
    }

    public void BroadCastGetTagAndSkill()
    {
        BroadcastMessage("GetTagList");
    }

    void OneYear()
    {
        CURRENTTIME += 1;
        BroadcastMessage("TimeGo");
        YearText.text = "~ 西暦: "+CURRENTTIME+"年 ~";
    }

    void ThreeYear()
    {
        CURRENTTIME += 3;
        BroadcastMessage("TimeGo");
        YearText.text = "~ 西暦: "+CURRENTTIME+"年 ~";
    }

    void EightYear()
    {
        CURRENTTIME += 8;
        BroadcastMessage("TimeGo");
        YearText.text = "~ 西暦: "+CURRENTTIME+"年 ~";
    }

    public void StartSave()
    {
        GetComponent<CharaDataArray>().mainChara.time = CURRENTTIME;
    }

    public void CharaRemove(List<int> numList)
    {
        BroadcastMessage("CharaDelete", numList);
    }

    public void GetSave()
    {
        CURRENTTIME = GetComponent<CharaDataArray>().mainChara.time;
        BroadCastGetTagAndSkill();
        YearText.text = "~ 西暦: " + CURRENTTIME + "年 ~";
    }
}
