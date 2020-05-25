using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryFsm : MonoBehaviour
{
    public Fungus.Flowchart flowchart;
    [SerializeField] GameObject MainChara;
    [SerializeField] GameObject Lily;
    [SerializeField] CharaDataArray charaDataArray;
    [SerializeField] GetEnemyData BattleEnemyData;
    List<Fungus.Variable> variableList = new List<Fungus.Variable>();
    // Start is called before the first frame update
    void Start()
    {
        flowchart = GetComponent<Fungus.Flowchart>();
    }

    public void LilyFound()
    {
        flowchart.SendFungusMessage("LilyFound");
    }
    
    public void BackToHome()
    {
        GetMapPos.MAPX = 36f;
        GetMapPos.MAPY = 11.5f;
        MainChara.GetComponent<GetMapPos>().GetPos(0) ;
    }

    public void LilyGoToBed1()
    {
        Lily.transform.position = new Vector2(50.53f, 20.97f);
        Lily.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
    public void StoryStart(string story)
    {//各章の始まり（カーテンOpenの後）はbooleanで制御
        flowchart.SetBooleanVariable(story,true);
    }
    public void SetLilyToGarden()
    {
        Lily.transform.position = new Vector2(10f, 16f);
        Lily.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
    public void GameOver()
    {
        if (0 < TimeKeeper.CURRENTTIME && TimeKeeper.CURRENTTIME < 7)
            flowchart.SendFungusMessage("Chap1gameover");
        else if (BattleEnemyData.enemyName == "鋼のガダル")
            flowchart.SendFungusMessage("Gadarugameover");
        else if (TimeKeeper.CURRENTTIME == 8)
            flowchart.SendFungusMessage("FirstHaisen");
        else if (TimeKeeper.CURRENTTIME == 9)
            flowchart.SendFungusMessage("SecondHaisen");
        else if (TimeKeeper.CURRENTTIME == 10)
            flowchart.SendFungusMessage("ThirdHaisen");
        else
            flowchart.SendFungusMessage("Gameovererror");
    }
    public void StartSave()
    {
        variableList = flowchart.GetPublicVariables();
        for(int i=0; i<variableList.Count; i++)
        {
            if(flowchart.GetBooleanVariable(variableList[i].Key))
                charaDataArray.storyFlagArray.storyFlagArray[i] = 1;
            else
                charaDataArray.storyFlagArray.storyFlagArray[i] = 0;
        }
    }
    public void GetSave()
    {
        variableList = flowchart.GetPublicVariables();
        for (int i = 0; i < variableList.Count; i++)
        {
            if (charaDataArray.storyFlagArray.storyFlagArray[i] == 1)
            {
                flowchart.SetBooleanVariable(variableList[i].Key, true);
            }
            else
            {
                flowchart.SetBooleanVariable(variableList[i].Key, false);
            }
        }
    }
}
