using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillDictionary : MonoBehaviour
{
    public BattleChara[] BattleCharaArray = new BattleChara[4];
    public GameObject[] CharaobjArray = new GameObject[4];
    [SerializeField] Text LogText;
    [SerializeField] public Text[] BattleHPs_Text_List = new Text[4];
    [SerializeField] public Text[] BattleMPs_Text_List = new Text[4];
    [SerializeField] public Animator battle_animator;
    private void Start()
    {
        BattleCharaArray = GetComponent<GetBattleCharaData>().BattleCharaArray;
        CharaobjArray = GetComponent<GetBattleCharaData>().CharaObjArray;
    }
    public void ExecuteSkill(int who, Skill skill)
    {//効果、消費元気、ログ argument1はメインの値、argument2は消費元気の値。デフォは10
        switch (skill.value)
        {
            case "dance":
                BattleCharaArray[who].Mp -= skill.argument2;
                for (int i=0; i<CharaobjArray.Length; i++)
                {
                    BattleCharaArray[i].Mp += skill.argument1*BattleCharaArray[who].Mp/100;
                    BattleMPs_Text_List[i].text = BattleCharaArray[i].Mp.ToString() + "%";                   
                }
                LogText.text = "踊る踊る! " + name + "は気が向くままに踊っている!\nみんなの元気が" + (skill.argument1 * BattleCharaArray[who].Mp / 100).ToString() + "上がった。";
                battle_animator.SetBool(skill.type, true);
                break;

            default:
                break;

        }
    }
}
