using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GetTag : MonoBehaviour
{
    private BasicData basic;
    private int[] grow;
    public GetSkill skill;
    [SerializeField] public List<int> tagIdList = new List<int>();
    [SerializeField] public List<string> tag = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        basic = GetComponent<BasicData>();
        grow = GetComponent<GrowData>().Ori;
        skill = GetComponent<GetSkill>();
    }
    
    public void TimeGo()
    {
        GetTagList();
    }
    public void GetTagList()
    {
        if (grow[0] > 20 && !tagIdList.Contains(1) && !tagIdList.Contains(2))
        {
            if (grow[0] > 30)
            {
                tag.Add("大賢者");
                tagIdList.Add(1);
            }
            else
            {
                tag.Add("賢者");
                tagIdList.Add(2);
            }
        }

        if (grow[1] > 20 && !tagIdList.Contains(3) && !tagIdList.Contains(4))
        {
            if (grow[1] > 30)
            {
                tag.Add("馬鹿力");
                tagIdList.Add(3);
            }
            else
            {
                tag.Add("怪力");
                tagIdList.Add(4);
            }
        }

        if (grow[2] > 20 && !tagIdList.Contains(5) && !tagIdList.Contains(6))
        {
            if (grow[2] > 30)
            {
                tag.Add("鉄人");
                tagIdList.Add(5);
            }
            else
            {
                tag.Add("頑丈");
                tagIdList.Add(6);
            }
        }
        if (grow[3] > 20 && basic.sex == "w" && !tagIdList.Contains(7) && !tagIdList.Contains(8))
        {
            if (grow[3] > 30)
            {
                tag.Add("絶世美人");
                tagIdList.Add(7);
            }
            else
            {
                tag.Add("美人");
                tagIdList.Add(8);
            }
        }
        if (grow[4] > 20 && !tagIdList.Contains(9) && !tagIdList.Contains(10))
        {
            tag.Add("不死身");
            tagIdList.Add(9);
        }
        if (grow[5] > 30 && basic.attack > 800 && basic.love > 800 && !tagIdList.Contains(10))
        {
            tag.Add("勇者");
            tagIdList.Add(10);
        }
        if (grow[6] > 20 && !tagIdList.Contains(11) && !tagIdList.Contains(12))
        {
            if (grow[6] > 30)
            {
                tag.Add("熱血");
                tagIdList.Add(11);
            }
            else
            {
                tag.Add("情熱");
                tagIdList.Add(12);
            }
        }
        if (grow[7] > 20 && !tagIdList.Contains(13) && !tagIdList.Contains(14))
        {
            if (grow[7] > 30)
            {
                tag.Add("コミュ力お化け");
                tagIdList.Add(13);
            }
            else
            {
                tag.Add("社交的");
                tagIdList.Add(14);
            }
        }
        if (grow[8] > 20 && !tagIdList.Contains(15) && !tagIdList.Contains(16))
        {
            if (grow[8] > 30)
            {
                tag.Add("聖人");
                tagIdList.Add(15);
            }
            else
            {
                tag.Add("善人");
                tagIdList.Add(16);
            }
        }
        if (grow[9] > 20 && !tagIdList.Contains(17) && !tagIdList.Contains(18))
        {
            if (grow[9] > 30)
            {
                tag.Add("芸人");
                tagIdList.Add(17);
            }
            else
            {
                tag.Add("人気者");
                tagIdList.Add(18);
            }
        }
        skill.LetsGetSkill();
    }
}
