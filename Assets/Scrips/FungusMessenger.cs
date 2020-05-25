using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FungusMessenger : MonoBehaviour
{
    public Fungus.Flowchart MyFlowchart;
    [SerializeField] public List<int> ChapterList;
    [SerializeField] public string BentouName;
    [SerializeField] public Bentou Bentou;
    [SerializeField] public Fungus.Flowchart StoryFlowChart;
    [SerializeField] public string StoryMessage;
    // Start is called before the first frame update
    void Start()
    {
        MyFlowchart= this.GetComponent<Fungus.Flowchart>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {//時代が新しいモノから操作する。
     //MessageDoneが1になったら既にメッセージはいっているためそれ以前の時代のchapを読む必要はない
        while (collision.gameObject.tag == "Player")
        {
            if (10 <= TimeKeeper.CURRENTTIME && ChapterList.Contains(7))
            {
                MyFlowchart.SendFungusMessage("chap7");
                break;
            }
            if (9 <= TimeKeeper.CURRENTTIME && ChapterList.Contains(6))
            {
                MyFlowchart.SendFungusMessage("chap6");
                break;
            }
            if (8 <= TimeKeeper.CURRENTTIME && ChapterList.Contains(5) && StoryFlowChart.GetBooleanVariable("FirstHaisen")==true)
            {
                MyFlowchart.SendFungusMessage("chap5");
                break;
            }
            if (8 <= TimeKeeper.CURRENTTIME && ChapterList.Contains(4))
            {
                MyFlowchart.SendFungusMessage("chap4");
                break;
            }
            if (7 <= TimeKeeper.CURRENTTIME && ChapterList.Contains(3))
            {
                MyFlowchart.SendFungusMessage("chap3");
                break;
            }

            if (4 <= TimeKeeper.CURRENTTIME && ChapterList.Contains(2))
            {
                MyFlowchart.SendFungusMessage("chap2");
                break;
            }
            if (1 <= TimeKeeper.CURRENTTIME && ChapterList.Contains(1))
            {
                MyFlowchart.SendFungusMessage("chap1");
                break;
            }
            break;
        }
    }
    public void PushBentou()
    {
        Bentou.BentouName = BentouName;
        Bentou.GetBentou();
        StoryFlowChart.SetBooleanVariable("BenTou", true);
    }
    public void StoryStart()
    {
        StoryFlowChart.gameObject.GetComponent<StoryFsm>().StoryStart(StoryMessage);
    }
}
