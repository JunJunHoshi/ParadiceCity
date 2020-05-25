using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetBattleCharaData : MonoBehaviour
{//バトルに仲間の能力値を反映させる。バトルに必要な仲間のステータスはここから引っ張る。
    public GameObject[] CharaObjArray = new GameObject[4];
    public BattleChara[] BattleCharaArray = new BattleChara[4];
    [SerializeField] public Text[] BattleNames_Text_List = new Text[4];
    [SerializeField] public Text[] BattleHPs_Text_List = new Text[4];
    [SerializeField] public Text[] BattleMPs_Text_List = new Text[4];
    [SerializeField] public GameObject[] BattleCommend_Types = new GameObject[4];
    [SerializeField] public GameObject Masao;
    [SerializeField] Animator battle_animator;
    [SerializeField] Text LogText;
    public Transform[] Battle_Panels = new Transform[4];
    [SerializeField] public GameObject[] Character_Panels;
    ButtonList1 buttonlist;
    [SerializeField] GetEnemyData getEnemyData;
    public int turn = 0;
    List<string> levelupnames = new List<string>();
    [SerializeField] public GameObject RunAwayButton;
    [SerializeField] StoryFsm StoryMessenger;
    // Start is called before the first frame update
    void Start()
    {//CharaObjArrayは常にMasaoのFriendsを参照しているため、常に反映されるはず。
        CharaObjArray = Masao.GetComponent<GetNakama>().Friends;
        buttonlist = this.GetComponent<ButtonList1>();
    }
    public void SetFriendsBattle()
    {
        turn = 0;
        for (int i = 0; i < CharaObjArray.Length; i++)
        {
            if (CharaObjArray[i] != null)
            {
                BattleCharaArray[i] = new BattleChara(); 
                BattleCharaArray[i].name = CharaObjArray[i].GetComponent<BasicData>().name;
                BattleCharaArray[i].MaxHp = CharaObjArray[i].GetComponent<BasicData>().hp;
                BattleCharaArray[i].attack = CharaObjArray[i].GetComponent<BasicData>().attack;
                BattleCharaArray[i].defence = CharaObjArray[i].GetComponent<BasicData>().defence;
                BattleCharaArray[i].cure = CharaObjArray[i].GetComponent<BasicData>().cure;
                BattleCharaArray[i].skilllist = CharaObjArray[i].GetComponent<GetSkill>().SkillList;
                BattleCharaArray[i].CurrentHp = CharaObjArray[i].GetComponent<BasicData>().current_hp;
            }
        }
    }
    public void SetBattle_Pannel()
    {
        for (int i = 0; i < BattleCharaArray.Length; i++)
        {
            if (CharaObjArray[i] != null)
            {
                Character_Panels[i].SetActive(true);
                BattleNames_Text_List[i].text = BattleCharaArray[i].name.ToString();
                BattleHPs_Text_List[i].text = BattleCharaArray[i].CurrentHp.ToString() + "/" + BattleCharaArray[i].MaxHp.ToString();
                BattleMPs_Text_List[i].text = BattleCharaArray[i].Mp.ToString() + "%";
            }
            else Character_Panels[i].SetActive(false);
        }
    }
    public void SetSkill_Pannel()
    {
        for (int i = 0; i < CharaObjArray.Length; i++)
        {
            if (CharaObjArray[i] != null)
            {
                GameObject attack_button = Instantiate(BattleCommend_Types[0]);
                int attacknum = i;
                attack_button.transform.SetParent(Battle_Panels[i], false);
                attack_button.GetComponent<Button>().onClick.AddListener(() => OnClickAttackButton(attacknum));

                for (int j = 0; j < BattleCharaArray[i].skilllist.Count; j++)
                {
                    GameObject command_button = new GameObject();
                    command_button.transform.SetParent(Battle_Panels[i], false);
                    switch (BattleCharaArray[i].skilllist[j].type)
                    {
                        case "attack":
                            command_button = Instantiate(BattleCommend_Types[1]);
                            command_button.transform.SetParent(Battle_Panels[i], false);
                            break;

                        case "hojyo":
                            command_button = Instantiate(BattleCommend_Types[2]);
                            command_button.transform.SetParent(Battle_Panels[i], false);
                            break;

                        case "cure":
                            command_button = Instantiate(BattleCommend_Types[3]);
                            command_button.transform.SetParent(Battle_Panels[i], false);
                            break;
                    }
                    int charanum = i;
                    int skillnum = j;
                    command_button.GetComponent<Button>().onClick.AddListener(() => OnClickSkillButton(charanum, BattleCharaArray[charanum].skilllist[skillnum]));
                    command_button.transform.Find("Text").GetComponent<Text>().text = BattleCharaArray[charanum].skilllist[skillnum].name;
                    command_button.SetActive(true);
                }
            }
        }
    }
    public void GoToNextTurn()
    {//nullもdeadも飛ばす
        turnLotation();
        while (CharaObjArray[turn]==null || CharaObjArray[turn].GetComponent<BasicData>().dead)
        {
            turnLotation();
        }
    }
    public void turnLotation()
    {
        if (turn == 3) turn = 0;
        else turn += 1;
    }

    public void CheckDeadOrNot()
    {//nullもdeadもここで飛ばす
        while (CharaObjArray[turn] == null)
        {
            turnLotation();
        }
        var chara = CharaObjArray[turn].GetComponent<BasicData>();
        if (BattleCharaArray[turn].CurrentHp <= 0 || chara.dead)
        {
            chara.dead = true;
            GoToNextTurn();
        }
        else return;
    }   

    public void CheckAllDeadOrNot()
    {
        int AliveCount = 0;
        for(int i=0; i<CharaObjArray.Length; i++)
        {
            if (CharaObjArray[i] != null)
            {
                if (BattleCharaArray[i].CurrentHp <= 0 || CharaObjArray[i].GetComponent<BasicData>().dead)
                {
                    TurnDeadPanel(i);
                }
                else AliveCount += 1;
            }
        }
        if (AliveCount <= 0)
        {
            battle_animator.SetBool("gameover", true);
        }
    }
    public void TurnDeadPanel(int num)
    {
        CharaObjArray[num].GetComponent<BasicData>().dead = true;
        Character_Panels[num].GetComponent<Image>().color = new Color(1.0f, 0f, 0f, 106 / 255f);
    }
    public void RightOnCharaPanel()
    {
        if (CharaObjArray[turn] == null || CharaObjArray[turn].GetComponent<BasicData>().dead) return;
        Character_Panels[turn].GetComponent<Image>().color = new Color(138/255f, 138/255f, 138/255f, 200/255f);
        Character_Panels[turn].transform.Find("Command (0)").gameObject.SetActive(true);
    }
    public void DeadCheckAll()
    {
        for (int i = 0; i < CharaObjArray.Length; i++)
        {
            if (CharaObjArray[i] != null &&  !CharaObjArray[i].GetComponent<BasicData>().dead)
            {
                Character_Panels[i].GetComponent<Image>().color = new Color(138 / 255f, 138 / 255f, 138 / 255f, 106 / 255f);
            }
        }
    }
    public void RightOffCharaPanel()
    {
        if (CharaObjArray[turn] == null || CharaObjArray[turn].GetComponent<BasicData>().dead) return;
        Character_Panels[turn].GetComponent<Image>().color = new Color(138/255f, 138/255f, 138/255f, 106/255f);
        Character_Panels[turn].transform.Find("Command (0)").gameObject.SetActive(false);
    }

    public void OnClickAttackButton(int charanum)
    {
        ExecuteAttack(charanum);
    }

    public void OnClickSkillButton(int charanum, Skill skill)
    {
        ExecuteSkill(charanum, skill);
    }

    public void ExecuteAttack(int who)
    {
        int damage = BattleCharaArray[who].attack - getEnemyData.enemyDefence;
        if (damage < 0) damage = 0;
        getEnemyData.enemyHp -= damage;
        LogText.text = BattleCharaArray[who].name + "の攻撃\n 敵に" + damage.ToString() + "のダメージ";
        battle_animator.SetBool("attack", true);
    }

    public void ExecuteSkill(int who, Skill skill)
    {//効果、消費元気、ログ argument1はメインの値、argument2は消費元気の値。デフォは10
        int damage = 0;
        switch (skill.value)
        {
            case "Dance":
                BattleCharaArray[who].Mp -= skill.argument2;
                for (int i = 0; i < CharaObjArray.Length; i++)
                {
                    if (CharaObjArray[i] != null)
                    {
                        BattleCharaArray[i].Mp += skill.argument1 * BattleCharaArray[who].Mp / 100;
                    }
                }
                LogText.text = "踊る踊る! " + BattleCharaArray[who].name + "は気が向くままに踊っている!\nみんなの元気が" + (skill.argument1 * BattleCharaArray[who].Mp / 100).ToString() + "上がった";
                UpdateAllMp();
                break;

            case "Cry":
                BattleCharaArray[who].Mp -= skill.argument2;
                damage = skill.argument1 * 5 * BattleCharaArray[who].Mp / 100 - getEnemyData.enemyDefence;
                if (damage < 0) damage = 0;
                getEnemyData.enemyHp -= damage;
                LogText.text = BattleCharaArray[who].name + "はわけもなく泣き出した\nこれは見ていてつらい　敵に" + damage.ToString() + "のダメージ";
                UpdateMp(who);
                break;
            case "Cure":
                BattleCharaArray[who].Mp -= skill.argument2;
                LogText.text = BattleCharaArray[who].name + "の" + skill.name + "\n";
                int cure = skill.argument1 / 100 * BattleCharaArray[who].cure / 2 * BattleCharaArray[who].Mp / 100;
                if (cure <= 0) cure = 0;
                for (int i = 0; i < CharaObjArray.Length; i++)
                {
                    if (CharaObjArray[i] != null && !CharaObjArray[i].GetComponent<BasicData>().dead)
                    {
                        BattleCharaArray[i].CurrentHp += cure;
                        if (BattleCharaArray[i].CurrentHp > BattleCharaArray[i].MaxHp) BattleCharaArray[i].CurrentHp = BattleCharaArray[i].MaxHp;
                    }
                }
                LogText.text += "癒しの力により皆のHPが" + cure + "回復した";
                UpdateAllMp();
                UpdateAllHp();
                break;
            case "Attack":
                BattleCharaArray[who].Mp -= skill.argument2;
                damage = skill.argument1 / 100 * BattleCharaArray[who].attack * BattleCharaArray[who].Mp / 100 - getEnemyData.enemyDefence;
                if (damage < 0) damage = 0;
                getEnemyData.enemyHp -= damage;
                LogText.text = $"{BattleCharaArray[who].name}の{skill.name}!\n敵に{damage}のダメージ";
                UpdateMp(who);
                break;
            case "Defence":
                BattleCharaArray[who].Mp -= skill.argument2;
                int defence = skill.argument1 * BattleCharaArray[who].Mp / 100;
                BattleCharaArray[who].defence += defence;
                LogText.text = $"{skill.name}!\n{BattleCharaArray[who].name}の防御力が{defence}上がった!";
                UpdateMp(who);
                break;
            case "Hunny":
                getEnemyData.hunny = true;
                LogText.text = $"{BattleCharaArray[who].name}はハチミツを作った！\nとてもおいしそうだ";
                break;
            case "JackBrade":
                BattleCharaArray[who].Mp -= skill.argument2;
                damage = (int)(BattleCharaArray[who].attack * 0.7 * BattleCharaArray[who].Mp / 100) - getEnemyData.enemyDefence;
                if (damage < 0) damage = 3;
                getEnemyData.enemyHp -= damage;
                getEnemyData.enemyHp -= damage - 1;
                getEnemyData.enemyHp -= damage - 2;
                LogText.text = $"{BattleCharaArray[who].name}の目にも止まらぬ速さの突き技!\n敵に{damage}のダメージ\n敵に{damage-1}のダメージ\n敵に{damage-2}のダメージ\n";
                UpdateMp(who);
                break;

            default:
                break;

        }
        battle_animator.SetBool(skill.type, true);
    } 
    public void UpdateHp(int num)
    {
        if (BattleCharaArray[num].CurrentHp < 0) BattleCharaArray[num].CurrentHp = 0;
        BattleHPs_Text_List[num].text = BattleCharaArray[num].CurrentHp.ToString() + "/" + BattleCharaArray[num].MaxHp.ToString();
    }
    public void UpdateMp(int num)
    {
        if (BattleCharaArray[num].Mp < 0) BattleCharaArray[num].Mp = 0;
        BattleMPs_Text_List[num].text = BattleCharaArray[num].Mp.ToString() + "%";
    }
    public void UpdateAllHp()
    {
        for(int i=0; i<CharaObjArray.Length; i++)
        {
            if (CharaObjArray[i] != null)
            {
                if (BattleCharaArray[i].CurrentHp < 0) BattleCharaArray[i].CurrentHp = 0;
                BattleHPs_Text_List[i].text = BattleCharaArray[i].CurrentHp.ToString() + "/" + BattleCharaArray[i].MaxHp.ToString();
            }
        }
    }
    public void UpdateAllMp()
    {
        for (int i = 0; i < CharaObjArray.Length; i++)
        {
            if (CharaObjArray[i] != null)
            {
                if (BattleCharaArray[i].Mp < 0) BattleCharaArray[i].Mp = 0;
                BattleMPs_Text_List[i].text = BattleCharaArray[i].Mp.ToString() + "%";
            }
        }
    }
    public void ExportBattleResult()
    {
        for(int i=0; i<CharaObjArray.Length; i++)
        {
            if(CharaObjArray[i] != null)
            {
                CharaObjArray[i].GetComponent<BasicData>().current_hp = BattleCharaArray[i].CurrentHp;
                RightOffCharaPanel();
            }
        }
        turn = 0;
    }

    public void SetKeikennchi()
    {//すべてのキャラに経験値を与えたうえでレベルアップするキャラの名前のリストを返し、アニメーションを動かす。
        bool levelupornot;
        for(int i=0; i<CharaObjArray.Length; i++)
        {
            if(CharaObjArray[i] != null)
            {
                var chara = CharaObjArray[i].GetComponent<BasicData>();
                if(chara.GetKeikennchi(getEnemyData.enemyLevel))
                {
                    battle_animator.SetBool("levelup", true);
                    levelupnames.Add(chara.name);
                }
            }
        }
    }
    public void SetKeikennchiText()
    {//logtextにレベルアップを表示し、levelupするキャラをすべて消去
        for(int i=0; i<levelupnames.Count; i++)
        {
            if (i == 0)
                LogText.text = levelupnames[i] + "は強くなった!\n";              
            else
                LogText.text += levelupnames[i] + "は強くなった!\n";
        }
        levelupnames.Clear();
        battle_animator.SetBool("levelup", false);
    }
    public void SetRunAwayButton()
    {//ちょっと使ってない
        if(GetComponent<Battle_Controller>().BossNumber == 0)
            RunAwayButton.SetActive(true);
        else
            RunAwayButton.SetActive(false);
    }
    public void RemoveRunAwayButton()
    {
        RunAwayButton.SetActive(false);
    }
    public void RunAway()
    {
        if (GetComponent<Battle_Controller>().BossNumber == 0)
        {
            LogText.text = "戦闘放棄ツ！\n一同は一目散に逃げ出した";
            battle_animator.SetBool("runaway", true);
        }
        else
        {
            LogText.text = "あまりの敵の圧により逃げられそうにない‼";
        }
    }
    public void CloseRunAway()
    {
        battle_animator.SetBool("runaway", false);
    }
    public void CloseEnemyDeath()
    {
        battle_animator.SetBool("enemydeath", false);
    }
    public void GameOverLog()
    {
        LogText.text = "一同は負けてしまった...";
    }
    public void ClosegameOver()
    {
        battle_animator.SetBool("gameover", false);
    }
    public void GameOverMessageToStory()
    {//章ごとによって死んだときの戻る場所は異なるためこのようにしている
        for(int i=0; i<CharaObjArray.Length; i++)
        {
            if (CharaObjArray[i] != null)
            {
                BasicData charadata = CharaObjArray[i].GetComponent<BasicData>();
                charadata.dead = false;
                charadata.current_hp = charadata.hp;              
            }
            RightOffCharaPanel();
            turn = 0;
        }
        StoryMessenger.GameOver();
    }
}
