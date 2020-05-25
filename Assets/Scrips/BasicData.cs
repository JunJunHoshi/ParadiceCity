using System.Collections;
using System.Collections.Generic;
using System.Management.Instrumentation;
using UnityEngine;

public class BasicData : MonoBehaviour
{
    [SerializeField] GetNakama getNakama;
    [SerializeField] int id;
    [SerializeField] public string name;
    [SerializeField] public string sex;
    [SerializeField] public  int maxage;
    [SerializeField] public  int birth;
    [SerializeField] public  int hp;
    [SerializeField] public  int attack;
    [SerializeField] public  int defence;
    [SerializeField] public  int craft;
    [SerializeField] public  int cure;
    [SerializeField] public  int love;
    [SerializeField] public bool dontshowprofile;
    public int current_hp;
    public int level;
    public int keikennchi;
    public bool dead = false;
    public int NakamaNum = 0;
    [SerializeField] public CharaDataArray charaDataArray;
    private CharaDataArray.CharaData charaData = new CharaDataArray.CharaData();
    public List<int> MyChapterList;

    private int[] BasicGrowMin = new int[6];
    private int[] BasicGrowMax = new int[6];

    private int i;
    int growcount = 0;

    public void StartSave()
    {
        charaData.id = id;
        charaData.growcount = growcount;
        charaData.name = name;
        charaData.hp = hp;
        charaData.current_hp = current_hp;
        charaData.attack = attack;
        charaData.defence = defence;
        charaData.craft = craft;
        charaData.cure = cure;
        charaData.love = love;
        charaData.level = level;
        charaData.dead = dead;
        charaData.keikennchi = keikennchi;
        charaData.NakamaNum = NakamaNum;
        charaDataArray.PushIntoData(charaData);
    }
    public void GetSave()
    {
        charaData = charaDataArray.GetCharaData(id);
        if (charaData.destroy) Destroy(gameObject);
        growcount = charaData.growcount;
        hp = charaData.hp;
        current_hp = charaData.current_hp;
        attack = charaData.attack;
        defence = charaData.defence;
        craft = charaData.craft;
        cure = charaData.cure;
        love = charaData.love;
        level = charaData.level;
        dead = charaData.dead;
        keikennchi = charaData.keikennchi;
        NakamaNum = charaData.NakamaNum;
        if (NakamaNum > 0 && NakamaNum < 9)
        {
            if (NakamaNum < 5)
            {
                getNakama.Friends[NakamaNum - 1] = this.gameObject;
            }
            else
            {
                getNakama.HikaeFriends[NakamaNum - 5] = this.gameObject;
            }
            transform.position = new Vector3(-25, 60, 0);
        }
        if (TimeKeeper.CURRENTTIME - birth <= 0 || TimeKeeper.CURRENTTIME - birth >= maxage)
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    } 
    // Start is called before the first frame update
    void Start()
    {
        BasicGrowMin = GetComponent<GrowData>().BasicGrowMin;
        BasicGrowMax = GetComponent<GrowData>().BasicGrowMax;
        MyChapterList = GetComponent<FungusMessenger>().ChapterList;
        current_hp = hp;
        if (TimeKeeper.CURRENTTIME - birth <= 0 || TimeKeeper.CURRENTTIME-birth >= maxage || TimeKeeper.CHAPYEARLIST[MyChapterList[0]] > TimeKeeper.CURRENTTIME)
        {
                gameObject.GetComponent<Collider2D>().enabled = false;
                gameObject.GetComponent<SpriteRenderer>().enabled = false;          
        }
        //紀元前より前に生まれてるんだったらその分growcountを増やせ
        if (birth < 0)
        {
            growcount -= birth;
        }
    }

    public void TimeGo()
    {
        if (TimeKeeper.CURRENTTIME - birth > 0)
        {
            if (TimeKeeper.CHAPYEARLIST[MyChapterList[0]] <= TimeKeeper.CURRENTTIME)
            {
                gameObject.GetComponent<Collider2D>().enabled = true;
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        i = TimeKeeper.CURRENTTIME - birth - growcount;
        if (TimeKeeper.CURRENTTIME - birth <= maxage)
        {
            for (int n = 0; n < i; n++)
                GrowUp();
            dead = false;
        }
        else
            CharaDestroy();
    }
    public bool GetKeikennchi(int enemylevel)
    {
        keikennchi += enemylevel;
        if (keikennchi >= level*1.5 && !dead)
        {
            level += 1;
            keikennchi = 0;
            GrowUp();
            return true;
        }
        else return false;
    }

    // Update is called once per frame
    void GrowUp()
    {
         hp += Random.Range(BasicGrowMin[0],BasicGrowMax[0])/2;
        if (hp <= 0) hp = 0;
         attack += Random.Range(BasicGrowMin[1],BasicGrowMax[1])/2;
        if (attack <= 0) attack = 1;
        defence += Random.Range(BasicGrowMin[2],BasicGrowMax[2])/2;
        if (defence <= 0) defence = 1;
        craft += Random.Range(BasicGrowMin[3],BasicGrowMax[3])/2;
        if (craft <= 0) craft = 1;
        cure += Random.Range(BasicGrowMin[4],BasicGrowMax[4])/2;
        if (cure <= 0) cure = 1;
        love += Random.Range(BasicGrowMin[5],BasicGrowMax[5])/2;
        if (love <= 0) love = 1;
        current_hp = hp;
         growcount += 1;
    }
    public void CharaDestroy()
    {
        charaDataArray.charaArray.charaDatas[id].destroy = true;
        Destroy(gameObject);
    }
    public void CharaDelete(List<int> numList)
    {
        if (numList.Contains(this.id))
        {
            CharaDestroy();
        }
    }
}
