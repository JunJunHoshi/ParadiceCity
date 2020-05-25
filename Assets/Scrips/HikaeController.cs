using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HikaeController : MonoBehaviour
{
    [SerializeField] public GameObject[] HikaePanel = new GameObject[4];
    [SerializeField] public GetNakama GetNakama;
    [SerializeField] public GameObject SukettoButotn;
    [SerializeField] public GameObject NakamaButton;
    [SerializeField] public GameObject GetBackButton;
    [SerializeField] public GameObject ExchangeButton;
    [SerializeField] public GameObject Masao;
    [SerializeField] public GameObject Panel;
    [SerializeField] private GameObject ExchangePanel;
    [SerializeField] private Text[] ExchangePanelElementNames;
    [SerializeField] private GameObject[] ExchangePanelElements;
    [SerializeField] private GameObject ExchangeFinishPanel;
    [SerializeField] private Text ExchangeFinishPanelText;
    public GameObject[] HikaeFriends;
    [SerializeField] Animator HikaeAnimator;
    private int changeNum = 0;
    private string whoGoFriends;
    private string whoComeToHikae;

    public void Start()
    {
        HikaeFriends = GetNakama.HikaeFriends;
    }
    public void SetHikaeList()
    {
        if (HikaeFriends[0]!=null && HikaeAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hikae_Idle"))
        {
            ShowHikaeList();
            TurnOnShowList();
            TurnOffBackToIdle();
            Masao.SendMessage("DeactivePlayer");
            SukettoButtonOn();
        }
        if (HikaeAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hikae_List"))
        {
            HideHikaeList();
            TurnOnBackToIdle();
            TurnOffShowList();
            Masao.SendMessage("ActivePlayer");
        }
    }
    public void ShowHikaeList()
    {
        for (int i = 0; i < 4; i++)
        {
            if (HikaeFriends[i]!=null)
            {//パネル表示

                HikaePanel[i].SetActive(true);
                HikaePanel[i].transform.Find("name").GetComponent<Text>().text =
                    HikaeFriends[i].gameObject.GetComponent<BasicData>().name + "(" + HikaeFriends[i].gameObject.GetComponent<BasicData>().sex + ")";
                HikaePanel[i].transform.Find("age").GetComponent<Text>().text =
                    (TimeKeeper.CURRENTTIME - HikaeFriends[i].gameObject.GetComponent<BasicData>().birth) + "/" +
                    HikaeFriends[i].gameObject.GetComponent<BasicData>().maxage;
                HikaePanel[i].transform.Find("hp").GetComponent<Text>().text =
                    HikaeFriends[i].gameObject.GetComponent<BasicData>().current_hp.ToString();
                HikaePanel[i].transform.Find("attack").GetComponent<Text>().text =
                    HikaeFriends[i].gameObject.GetComponent<BasicData>().attack.ToString();
                HikaePanel[i].transform.Find("defence").GetComponent<Text>().text =
                    HikaeFriends[i].gameObject.GetComponent<BasicData>().defence.ToString();
                HikaePanel[i].transform.Find("craft").GetComponent<Text>().text =
                    HikaeFriends[i].gameObject.GetComponent<BasicData>().craft.ToString();
                HikaePanel[i].transform.Find("cure").GetComponent<Text>().text =
                    HikaeFriends[i].gameObject.GetComponent<BasicData>().cure.ToString();
                HikaePanel[i].transform.Find("love").GetComponent<Text>().text =
                    HikaeFriends[i].gameObject.GetComponent<BasicData>().love.ToString();
            }
            else
            {
                HikaePanel[i].SetActive(false);
            }
        }
    }
    public void HideHikaeList()
    {
        for(int i=0; i<4; i++)
        {
            HikaePanel[i].SetActive(false);
        }
    }
    public void ShowHikaeProfile (int num)
    {
        PushIntoChangeNum(num);
        HikaeAnimator.SetBool("showprofile", true);
        GameObject chara = HikaeFriends[num];
        string TAGS = " ";
        string SKILLS = " ";
        Panel.SetActive(true);
        Panel.transform.Find("name").GetComponent<Text>().text =
            chara.gameObject.GetComponent<BasicData>().name + "(" + chara.gameObject.GetComponent<BasicData>().sex + ")";
        Panel.transform.Find("age").GetComponent<Text>().text =
            (TimeKeeper.CURRENTTIME - chara.gameObject.GetComponent<BasicData>().birth) + "/" +
            chara.gameObject.GetComponent<BasicData>().maxage;
        Panel.transform.Find("hp").GetComponent<Text>().text =
            chara.GetComponent<BasicData>().current_hp.ToString() + "/" + chara.gameObject.GetComponent<BasicData>().hp.ToString();
        Panel.transform.Find("attack").GetComponent<Text>().text =
            chara.gameObject.GetComponent<BasicData>().attack.ToString();
        Panel.transform.Find("defence").GetComponent<Text>().text =
            chara.gameObject.GetComponent<BasicData>().defence.ToString();
        Panel.transform.Find("craft").GetComponent<Text>().text =
            chara.gameObject.GetComponent<BasicData>().craft.ToString();
        Panel.transform.Find("cure").GetComponent<Text>().text =
            chara.gameObject.GetComponent<BasicData>().cure.ToString();
        Panel.transform.Find("love").GetComponent<Text>().text =
            chara.gameObject.GetComponent<BasicData>().love.ToString();

        List<string> taglist = chara.gameObject.GetComponent<GetTag>().tag;
        List<Skill> skilllist = chara.gameObject.GetComponent<GetSkill>().SkillList;

        for (int i = 0; i < taglist.Count; i++)
        {
            TAGS += taglist[i] + " ";
        }
        Panel.transform.Find("tag").GetComponent<Text>().text = TAGS;


        for (int i = 0; i < skilllist.Count; i++)
        {
            SKILLS += skilllist[i].name + " ";
        }
        Panel.transform.Find("skill").GetComponent<Text>().text = SKILLS;
    }
    public void HideHikaeProfilePanel()
    {
        Panel.SetActive(false);
    }
    public void TurnOnShowList()
    {
        HikaeAnimator.SetBool("showlist", true);
    }
    public void TurnOffShowList()
    {
        HikaeAnimator.SetBool("showlist", false);
    }
    public void TurnOnBackToIdle()
    {
        HikaeAnimator.SetBool("backtoidle", true);
    }
    public void TurnOffBackToIdle()
    {
        HikaeAnimator.SetBool("backtoidle", false);
    }
    public void TurnOnBackToList()
    {
        HikaeAnimator.SetBool("backtolist", true);
    }
    public void TurnOffBackToList()
    {
        HikaeAnimator.SetBool("backtolist", false);
    }
    public void TurnOnButton(string button)
    {
        HikaeAnimator.SetBool(button, true);
    }
    public void TurnOffButton(string button)
    {
        HikaeAnimator.SetBool(button, false);
    }
    public void NakamaButtonOn()
    {
        NakamaButton.SetActive(true);
    }
    public void NakamaButtonOff()
    {
        NakamaButton.SetActive(false);
    }
    public void GetBackButtonOn()
    {
        GetBackButton.SetActive(true);
    }
    public void GetBackButtonOff()
    {
        GetBackButton.SetActive(false);
    }
    public void ExchangeButtonOn()
    {
        ExchangeButton.SetActive(true);
    }
    public void ExchangeButtonOff()
    {
        ExchangeButton.SetActive(false);
    }
    public void SukettoButtonOn()
    {
        SukettoButotn.SetActive(true);
    }
    public void SukettoButtonOff()
    {
        SukettoButotn.SetActive(false);
    }
    public void ShowExchangePanel()
    {
        ExchangePanel.SetActive(true);
        UpdateExchangePanel();
    }
    public void HideExchangePanel()
    {
        ExchangePanel.SetActive(false);
    }
    public void UpdateExchangePanel()
    {
        for(int i=0; i<4; i++)
        {
            if (GetNakama.Friends[i] != null)
            {
                ExchangePanelElementNames[i].text = GetNakama.Friends[i].GetComponent<BasicData>().name;
                ExchangePanelElements[i].SetActive(true);
            }
            else
            {
                ExchangePanelElementNames[i].text = "空席";
                ExchangePanelElements[i].SetActive(true); 
            }
        }
    }
    public void PushIntoChangeNum(int num)
    {
        changeNum = num;
    }
    public void ExcuteExchange(int friendnum)
    {
        whoGoFriends = HikaeFriends[changeNum].GetComponent<BasicData>().name;
        if (Masao.GetComponent<GetNakama>().Friends[friendnum] != null)
        {
            whoComeToHikae = Masao.GetComponent<GetNakama>().Friends[friendnum].GetComponent<BasicData>().name;
            Masao.GetComponent<GetNakama>().ExcuteChange(friendnum, changeNum);
            HikaeFriends[changeNum].gameObject.GetComponent<BasicData>().NakamaNum = changeNum+5;
            Masao.GetComponent<GetNakama>().Friends[friendnum].gameObject.GetComponent<BasicData>().NakamaNum = friendnum+1;
        }
        else
        {
            Masao.GetComponent<GetNakama>().Friends[friendnum] = HikaeFriends[changeNum];
            HikaeFriends[changeNum] = null;
            Masao.GetComponent<GetNakama>().Friends[friendnum].gameObject.GetComponent<BasicData>().NakamaNum = friendnum+1;
            whoComeToHikae = "empty";
        }
    }
    public void ExchangeFinish()
    {
        ExchangeFinishPanel.SetActive(true);
        if (whoComeToHikae == "empty")
        {
            ExchangeFinishPanelText.text = $"<color=yellow>{whoGoFriends}</color>が助けに来てくれた！";
        }
        else
        {
            ExchangeFinishPanelText.text = $"<color=yellow>{whoGoFriends}</color>と<color=yellow>{whoComeToHikae}</color>を交代した";
        }
    }
    public void HideExchangeFinishPanel()
    {
        ExchangeFinishPanel.SetActive(false);
        Masao.SendMessage("ActivePlayer");
    }
}
