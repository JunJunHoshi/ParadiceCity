using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveController : MonoBehaviour
{
    [SerializeField] public float SPEED = 1.0f;
    [SerializeField] public GameObject Panel;
    [SerializeField] public BattleEncounter battleEncounter;
    [SerializeField] public GameObject HikaeButton;
    [SerializeField] public GameObject BentouButton;
    [SerializeField] public GameObject SaveButton;
    [SerializeField] public Fungus.Flowchart StoryFlowChart;
    [SerializeField] public CharaDataArray charaDataArray;
    private Rigidbody2D rigidBody;
    private Vector2 inputAxis;
    private bool controllable;
    private int keydom;
    private Animator animator;
    GameObject chara;
    List<string> taglist = new List<string>();
    List<Skill> skilllist = new List<Skill>();
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        controllable = true;
        keydom = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (controllable)
        {
            inputAxis.x = Input.GetAxis("Horizontal");
            inputAxis.y = Input.GetAxis("Vertical");


            if (keydom >= 0)
            {
                if (Input.GetKey("right") && (keydom == 0 || keydom == 1))
                {
                    animator.SetFloat("x", 1.0f);
                    animator.SetFloat("y", 0f);
                    keydom = 1;
                    battleEncounter.WalkInMap();
                }
                else if (Input.GetKey("left") && (keydom == 0 || keydom == 2))
                {
                    animator.SetFloat("x", -1.0f);
                    animator.SetFloat("y", 0f);
                    keydom = 2;
                    battleEncounter.WalkInMap();
                }
                else if (Input.GetKey("up") && (keydom == 0 || keydom == 3))
                {
                    animator.SetFloat("y", 1.0f);
                    animator.SetFloat("x", 0f);
                    keydom = 3;
                    battleEncounter.WalkInMap();
                }
                else if (Input.GetKey("down") && (keydom == 0 || keydom == 4))
                {
                    animator.SetFloat("y", -1.0f);
                    animator.SetFloat("x", 0f);
                    keydom = 4;
                    battleEncounter.WalkInMap();
                }
                else
                {
                    keydom = 0;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Character")
        {
            chara = other.gameObject;
            taglist = other.gameObject.GetComponent<GetTag>().tag;
            skilllist = other.gameObject.GetComponent<GetSkill>().SkillList;            
            if (other.gameObject.GetComponent<BasicData>().dontshowprofile == false) Invoke("PanelShow",0);          
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Character")
        {
            Panel.SetActive(false);
        }
    }


    private void FixedUpdate()
    {
        rigidBody.velocity = inputAxis.normalized * SPEED;
    }

    public void ActivePlayer()
    {//主人公が動いたときは、以下のようなふるまいをする。パネルを畳み、その代わりボタンをonにする
        controllable = true;
        rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        Panel.SetActive(false);
        GetComponent<ShowProfile>().nakamabutton.GetComponent<NakamaButton_OnOff>().On();
        HikaeButton.SetActive(true);
        if (StoryFlowChart.GetBooleanVariable("BenTou")) BentouButton.SetActive(true);
        SaveButton.SetActive(true);
    }

    public void DeactivePlayer()
    {//主人公が止まるときは、以下のようなふるまいをする。nakamaボタンをoffにするため、勝手にメニューは見れない。
        controllable = false;
        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        GetComponent<ShowProfile>().nakamabutton.GetComponent<NakamaButton_OnOff>().Off();
        HikaeButton.SetActive(false);
        BentouButton.SetActive(false);
        SaveButton.SetActive(false);
    }

    public void PanelShow()
    {
        string TAGS = " ";
        string SKILLS = " ";
        Panel.SetActive(true);
        Panel.transform.Find("name").GetComponent<Text>().text = chara.gameObject.GetComponent<BasicData>().name+"("+chara.gameObject.GetComponent<BasicData>().sex+")";
        Panel.transform.Find("age").GetComponent<Text>().text = (TimeKeeper.CURRENTTIME-chara.gameObject.GetComponent<BasicData>().birth)+"/"+chara.gameObject.GetComponent<BasicData>().maxage;
        Panel.transform.Find("hp").GetComponent<Text>().text = chara.gameObject.GetComponent<BasicData>().hp.ToString();
        Panel.transform.Find("attack").GetComponent<Text>().text = chara.gameObject.GetComponent<BasicData>().attack.ToString();
        Panel.transform.Find("defence").GetComponent<Text>().text = chara.gameObject.GetComponent<BasicData>().defence.ToString();
        Panel.transform.Find("craft").GetComponent<Text>().text = chara.gameObject.GetComponent<BasicData>().craft.ToString();
        Panel.transform.Find("cure").GetComponent<Text>().text = chara.gameObject.GetComponent<BasicData>().cure.ToString();
        Panel.transform.Find("love").GetComponent<Text>().text = chara.gameObject.GetComponent<BasicData>().love.ToString();
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
    public void StartSave()
    {
        charaDataArray.mainChara.x = gameObject.transform.position.x;
        charaDataArray.mainChara.y = gameObject.transform.position.y;
        charaDataArray.mainChara.z = gameObject.transform.position.z;
        charaDataArray.mainChara.world = gameObject.GetComponent<GetMapPos>().World;
    }
    public void GetSave()
    {
        gameObject.transform.position = new Vector3(charaDataArray.mainChara.x, charaDataArray.mainChara.y, charaDataArray.mainChara.z);
        gameObject.GetComponent<GetMapPos>().World = charaDataArray.mainChara.world;
    }
}