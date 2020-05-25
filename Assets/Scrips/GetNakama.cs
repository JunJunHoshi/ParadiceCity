using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetNakama : MonoBehaviour
{
    //Friends[]: 仲間の情報を参照するリスト。
    //Panel[]: 仲間の情報を出力するパネル。
    private int nakama;
    [SerializeField] public GameObject[] Panel = new GameObject[4];
    public GameObject[] Friends = new GameObject[4];
    public GameObject[] HikaeFriends = new GameObject[4];
    [SerializeField] public Fungus.Flowchart FlowChart;
    [SerializeField] GameObject HikaeButton;
    private GameObject obj1;
    private GameObject obj2;

    // Start is called before the first frame update
    void Start()
    {
        nakama = 0;
        obj1 = new GameObject();
        obj2 = new GameObject();
    }

    // Update is called once per frame
    void GetFriends()
    {//仲間をゲットした瞬間、nakamaフラグを立てる。
        nakama = 1;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Character")
        {              
            if (nakama == 1)
            {//仲間にできるぜ
                if (CheckFriendNum() >= 4)
                {
                    FlowChart.SendFungusMessage("TooManyFriends");
                    for(int i =0; i<4; i++)
                    {
                        if (HikaeFriends[i] == null)
                        {
                            HikaeFriends[i] = other.gameObject;
                            other.gameObject.GetComponent<BasicData>().NakamaNum = i+1 + 4;
                            other.gameObject.GetComponent<DestroyMyself>().number = i;
                            break;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (Friends[i] == null)
                        {
                            Friends[i] = other.gameObject;
                            other.gameObject.GetComponent<BasicData>().NakamaNum = i+1;
                            other.gameObject.GetComponent<DestroyMyself>().number = i;
                            break;
                        }
                    }
                }
                nakama = 0;
            }
        }
    }


    public void SetNakamaList()
    {
        if (Panel[0].activeSelf  ||　Panel[1].activeSelf || Panel[2].activeSelf || Panel[3].activeSelf)
        {//どの仲間パネルも光ってない時は普通に動ける           
                for (int i = 0; i < 4; i++)
                {
                    Panel[i].SetActive(false);
                }
            this.SendMessage("ActivePlayer");
        }
        
        else 
        {//deactiveにするけど、仲間ボタンは消さないでね。
            this.SendMessage("DeactivePlayer");;
            GetComponent<ShowProfile>().nakamabutton.GetComponent<NakamaButton_OnOff>().On();
            ShowProfilePanel();
        }
    }   

    public void ShowProfilePanel()
    {
        for (int i = 0; i < 4; i++)
        {
            if (Friends[i] != null)
            {//パネル表示

                Panel[i].SetActive(true);
                Panel[i].transform.Find("name").GetComponent<Text>().text =
                    Friends[i].gameObject.GetComponent<BasicData>().name + "(" + Friends[i].gameObject.GetComponent<BasicData>().sex + ")";
                Panel[i].transform.Find("age").GetComponent<Text>().text =
                    (TimeKeeper.CURRENTTIME - Friends[i].gameObject.GetComponent<BasicData>().birth) + "/" +
                    Friends[i].gameObject.GetComponent<BasicData>().maxage;
                Panel[i].transform.Find("hp").GetComponent<Text>().text =
                    Friends[i].gameObject.GetComponent<BasicData>().current_hp.ToString();
                Panel[i].transform.Find("attack").GetComponent<Text>().text =
                    Friends[i].gameObject.GetComponent<BasicData>().attack.ToString();
                Panel[i].transform.Find("defence").GetComponent<Text>().text =
                    Friends[i].gameObject.GetComponent<BasicData>().defence.ToString();
                Panel[i].transform.Find("craft").GetComponent<Text>().text =
                    Friends[i].gameObject.GetComponent<BasicData>().craft.ToString();
                Panel[i].transform.Find("cure").GetComponent<Text>().text =
                    Friends[i].gameObject.GetComponent<BasicData>().cure.ToString();
                Panel[i].transform.Find("love").GetComponent<Text>().text =
                    Friends[i].gameObject.GetComponent<BasicData>().love.ToString();
            }
        }
    }
    
    public int CheckFriendNum()
    {
        int n = 0;
        for(int i=0; i< Friends.Length; i++)
        {
            if(Friends[i] != null)
            {
                n += 1;
            }
        }
        return n;
    }
    public void ExcuteChange(int friendnum, int hikaenum)
    {
        obj1 = Friends[friendnum];
        obj2 = HikaeFriends[hikaenum];
        Friends[friendnum] = obj2;
        HikaeFriends[hikaenum] = obj1;
    }
    public void Kaifuku(int num)
    {
        BasicData charadata = Friends[num].GetComponent<BasicData>();
        charadata.dead = false;
        charadata.current_hp = charadata.hp;
    }
    public void RemoveNakama(string name)
    {
        foreach(GameObject chara in Friends)
        {
            if (chara == null)
                continue;
            if(chara.name == name)
            {
                chara.GetComponent<BasicData>().NakamaNum = 0;
                chara.GetComponent<BasicData>().CharaDestroy();
            }
        }
    }
    public void RemoveHikae(string name)
    {
        foreach (GameObject chara in HikaeFriends)
        {
            if (chara == null)
                continue;
            if (chara.name == name)
            {
                chara.GetComponent<BasicData>().NakamaNum = 0;
                chara.GetComponent<BasicData>().CharaDestroy();
            }
        }
    }
}
