using System;
using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
using UnityEngine.UI;

public class ShowProfile : MonoBehaviour
{
    [SerializeField] GameObject[] PanelLists = new GameObject[4];
    private GameObject chara;
    [SerializeField] private GameObject Panel;
    List<string> taglist = new List<string>();
    List<Skill> skilllist = new List<Skill>();
    [SerializeField] GameObject modorubutton;
    [SerializeField] public GameObject nakamabutton;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PanelGo(int number)
    {
        modorubutton.SetActive(true);
        nakamabutton.SetActive(false);

        if (number == -1)
        {
            chara = this.gameObject;
            for (int i = 0; i < 4; i++)
            {
                PanelLists[i].SetActive(false);
            }
        }

        else
        {
            if (GetComponent<GetNakama>().Friends[number] != null)
            {
                chara = GetComponent<GetNakama>().Friends[number];
                for (int i = 0; i < 4; i++)
                {
                    PanelLists[i].SetActive(false);
                }
            }
           
        }

        SendMessage("DeactivePlayer");
        string TAGS = " ";
        string SKILLS = " ";
        Panel.SetActive(true);
        Panel.transform.Find("name").GetComponent<Text>().text =
            chara.gameObject.GetComponent<BasicData>().name + "(" + chara.gameObject.GetComponent<BasicData>().sex + ")";
        Panel.transform.Find("age").GetComponent<Text>().text =
            (TimeKeeper.CURRENTTIME - chara.gameObject.GetComponent<BasicData>().birth) + "/" +
            chara.gameObject.GetComponent<BasicData>().maxage;
        Panel.transform.Find("hp").GetComponent<Text>().text =
            chara.GetComponent<BasicData>().current_hp.ToString()+"/"+chara.gameObject.GetComponent<BasicData>().hp.ToString();
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

        taglist = chara.gameObject.GetComponent<GetTag>().tag;
        skilllist = chara.gameObject.GetComponent<GetSkill>().SkillList;

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




    public void PanelBack()
    {
        modorubutton.SetActive(false);
        nakamabutton.SetActive(true);
        Panel.SetActive(false);        
        for (int i = 0; i < 4; i++)
        {
            if(GetComponent<GetNakama>().Friends[i] != null)
            PanelLists[i].SetActive(true);
        }
    }
   
}

