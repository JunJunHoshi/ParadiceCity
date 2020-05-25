using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] public int id;
    [SerializeField] public CharaDataArray charaDataArray;
    [SerializeField] public List<int> EmergeTimeList;
    [SerializeField] public Transform CameraPos;
    [SerializeField] public float Camerax;
    [SerializeField] public float Cameray;
    public int Done;

    public void Start()
    {
        TimeGo();
    }
    public void EventDone()
    {
        Done = 1;
        GetComponent<BoxCollider2D>().enabled = false;
        transform.Find("Root").gameObject.SetActive(false);
    }
    public void StartSave()
    {
        charaDataArray.eventArray.Events[id] = Done;
    }

    public void GetSave()
    {
        Done = charaDataArray.eventArray.Events[id];
        if(Done > 0)
        {
            Destroy(gameObject);
        }
        TimeGo();
    }
    public void TimeGo()
    {
        foreach (var EmergeTime in EmergeTimeList)
        {
            if (EmergeTime == TimeKeeper.CURRENTTIME)
            {
                this.gameObject.SetActive(true);
                GetComponent<BoxCollider2D>().enabled = true;
                transform.Find("Root").gameObject.SetActive(true);
                break;
            }
            else if (EmergeTime > TimeKeeper.CURRENTTIME)
            {
                GetComponent<BoxCollider2D>().enabled = false;
                transform.Find("Root").gameObject.SetActive(false);
            }
            else if (EmergeTime < TimeKeeper.CURRENTTIME)
            {
                this.gameObject.SetActive(false);
            }
        }
    }
    public void SetCameraPosition()
    {
        CameraPos.position = new Vector3(Camerax, Cameray, 0);
    }
}
