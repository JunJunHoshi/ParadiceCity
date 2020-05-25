using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSkill : MonoBehaviour
{
    private BasicData basic;
    private int[] grow;
    private GetTag tag;
    private int age;
    [SerializeField] public List<Skill> SkillList = new List<Skill>();
    public Skill[] BattleSkillArray;
    [SerializeField] public List<int> SkillIdList = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        basic = GetComponent<BasicData>();
        grow = GetComponent<GrowData>().Ori;
        tag = GetComponent<GetTag>();
        BattleSkillArray = new Skill[5];
    }

    // Update is called once per frame

    public void LetsGetSkill()
    {//上の方ほど習う優先順が高いと思え。特に固有スキルは
        if (SkillList.Count >= 4)
        {
            return;
        }
        age = TimeKeeper.CURRENTTIME - basic.birth;
        if(tag.tag.Contains("美人") && age>5 && CheckSkill(1))
        {
            SkillList.Add(SetSkill(1,"魅惑の踊り", "cure", "Dance",10));
        }
        if (tag.tag.Contains("美人") && age > 10 && CheckSkill(2))
        {
            SkillList.Add(SetSkill(2, "泣き落とし", "attack", "Cry",100));
        }
        if(basic.name == "ハニル" && CheckSkill(3))
        {
            SkillList.Add(SetSkill(3, "蜂蜜作り", "hojyo", "Hunny", 0));
        }
        if(basic.name == "ギルマイク" && CheckSkill(4))
        {
            SkillList.Add(SetSkill(4, "ジャックブレイド", "attack", "JackBrade", 100, 30));
        }
        if (basic.name == "マサル" && age>16 && CheckSkill(5))
        {
            SkillList.Add(SetSkill(5, "はだか踊り", "cure", "Dance", 25));
        }
        if (basic.name == "リリー" && CheckSkill(6))
        {
            SkillList.Add(SetSkill(6, "癒しの手", "cure", "Cure", 100, 30));
        }
        if (basic.name == "スタッド" && CheckSkill(7))
        {
            SkillList.Add(SetSkill(7, "鉄壁の構え", "hojyo", "Defence", 10));
        }
        PushintoBattleSkillArray();
    }
    public bool CheckSkill(int num)
    {
        return !SkillIdList.Contains(num);
    }
    public void PushintoBattleSkillArray()
    {
        for(int i=0; i<SkillList.Count; i++)
        {
            if (i<=4)
            BattleSkillArray[i] = SkillList[i];
        }
    }
    public Skill SetSkill(int id, string name, string type, string value, int argument1, int argument2 = 10, int argument3 = 0)
    {
        Skill skill = new Skill();
        skill.id = id;
        skill.name = name;
        skill.type = type;
        skill.value = value;
        skill.argument1 = argument1;
        skill.argument2 = argument2;
        skill.argument3 = argument3;
        SkillIdList.Add(id);
        return skill;
    }
   
}
