using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using System.IO;

[Serializable]
public class CharaDataArray : MonoBehaviour
{
    [SerializeField] MoveController MasaruMove;
    [SerializeField] TimeKeeper timeKeeper;
    [SerializeField] StoryFsm StoryFsm;
    [SerializeField] GameObject SavePanel;
    [SerializeField] Text SaveText;
    [SerializeField] Bentou bentou;

    const string SAVE_CHARA_PATH = "CharaSave.txt";
    const string SAVE_MAIN_PATH = "MainSave.txt";
    const string SAVE_BOSS_PATH = "BossSave.txt";
    const string SAVE_STORY_PATH = "StorySave.txt";
    const string SAVE_EVENT_PATH = "EventSave.txt";

    public CharaArray charaArray = new CharaArray();
    public MainChara mainChara = new MainChara();
    public BossArray bossArray = new BossArray();
    public StoryFlagArray storyFlagArray = new StoryFlagArray();
    public EventArray eventArray = new EventArray();

   [Serializable]
   public class CharaArray
    {
        public CharaData[] charaDatas = new CharaData[100];
    }
    [Serializable]
   public class CharaData
    {
        public int id;
        public int growcount;
        public string name;
        public int hp;
        public int attack;
        public int defence;
        public int craft;
        public int cure;
        public int love;
        public int current_hp;
        public int level;
        public int keikennchi;
        public bool dead;
        public int NakamaNum;
        public bool destroy;
    }
    [Serializable]
    public class MainChara
    {
        public int time;
        public int world;
        public float x;
        public float y;
        public float z;
        public string bentouname;
    }
    [Serializable]
    public class StoryFlagArray
    {
        public int[] storyFlagArray = new int[50];
    }
    [Serializable]
    public class BossArray
    {
        public int[] BossDead = new int[50];
    }
    public class EventArray
    {
        public int[] Events = new int[50];
    }
    public void Awake()
    {
        var charainfo = new FileInfo(Application.dataPath + SAVE_CHARA_PATH);
        var charareader = new StreamReader(charainfo.OpenRead());
        var charajson = charareader.ReadToEnd();
        charaArray = JsonUtility.FromJson<CharaArray>(charajson);

        var maininfo = new FileInfo(Application.dataPath + SAVE_MAIN_PATH);
        var mainreader = new StreamReader(maininfo.OpenRead());
        var mainjson = mainreader.ReadToEnd();
        mainChara = JsonUtility.FromJson<MainChara>(mainjson);

        var bossinfo = new FileInfo(Application.dataPath + SAVE_BOSS_PATH);
        var bossreader = new StreamReader(bossinfo.OpenRead());
        var bossjson = bossreader.ReadToEnd();
        bossArray = JsonUtility.FromJson<BossArray>(bossjson);

        var storyinfo = new FileInfo(Application.dataPath + SAVE_STORY_PATH);
        var storyreader = new StreamReader(storyinfo.OpenRead());
        var storyjson = storyreader.ReadToEnd();
        storyFlagArray = JsonUtility.FromJson<StoryFlagArray>(storyjson);

        var eventinfo = new FileInfo(Application.dataPath + SAVE_EVENT_PATH);
        var eventreader = new StreamReader(eventinfo.OpenRead());
        var eventjson = eventreader.ReadToEnd();
        eventArray = JsonUtility.FromJson<EventArray>(eventjson);
    }
    public void Save()
    {
        MasaruMove.DeactivePlayer();
        MasaruMove.StartSave();
        timeKeeper.StartSave();
        StoryFsm.StartSave();
        bentou.StartSave();
        BroadcastMessage("StartSave");
        SaveText.text = "セーブしています.....";
        Invoke("TurnToJson", 4.0f);
    }
    public void GetData()
    {
        MasaruMove.GetSave();
        timeKeeper.GetSave();
        BroadcastMessage("GetSave");
        StoryFsm.GetSave();
        bentou.GetSave();
    }

    public void PushIntoData(CharaData charadata)
    {
        charaArray.charaDatas[charadata.id] = charadata;
    }
    public CharaData GetCharaData(int id)
    {
        return charaArray.charaDatas[id];
    }
    public void TurnToJson()
    {
        //Jsonにシリアリズ
        var charajson = JsonUtility.ToJson(charaArray);
        var charapath = Application.dataPath + SAVE_CHARA_PATH;
        var charawriter = new StreamWriter(charapath);
        charawriter.WriteLine(charajson);
        charawriter.Flush();
        charawriter.Close();

        var mainjson = JsonUtility.ToJson(mainChara);
        var mainpath = Application.dataPath + SAVE_MAIN_PATH;
        var mainwriter = new StreamWriter(mainpath);
        mainwriter.WriteLine(mainjson);
        mainwriter.Flush();
        mainwriter.Close();

        var bossjson = JsonUtility.ToJson(bossArray);
        var bosspath = Application.dataPath + SAVE_BOSS_PATH;
        var bosswriter = new StreamWriter(bosspath);
        bosswriter.WriteLine(bossjson);
        bosswriter.Flush();
        bosswriter.Close();

        var storyjson = JsonUtility.ToJson(storyFlagArray);
        var storypath = Application.dataPath + SAVE_STORY_PATH;
        var storywriter = new StreamWriter(storypath);
        storywriter.WriteLine(storyjson);
        storywriter.Flush();
        storywriter.Close();

        var eventjson = JsonUtility.ToJson(eventArray);
        var eventpath = Application.dataPath + SAVE_EVENT_PATH;
        var eventwriter = new StreamWriter(eventpath);
        eventwriter.WriteLine(eventjson);
        eventwriter.Flush();
        eventwriter.Close();

        SaveText.text = "セーブが完了しました‼";
        Invoke("DeletePanel", 0.8f);
    }
    public void DeletePanel()
    {
        MasaruMove.ActivePlayer();
        SavePanel.SetActive(false);
    }
}
