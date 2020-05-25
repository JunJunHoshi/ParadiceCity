using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NakamaRemover : MonoBehaviour
{
    [SerializeField] private List<string> removeNameList;
    [SerializeField] private GetNakama getNakama;
    [SerializeField] public List<BasicData> removeCharaList;

    public void RemoveNakama()
    {
        foreach(string name in removeNameList)
        {
            getNakama.RemoveNakama(name);
            getNakama.RemoveHikae(name);
        }
    }
    public void RemoveChara()
    {
        if (removeCharaList != null)
        {
            for (int i = 0; i < removeCharaList.Count; i++)
            {
                if (removeCharaList[i] != null)
                {
                    removeCharaList[i].CharaDestroy();
                }
            }
        }
    }
}
