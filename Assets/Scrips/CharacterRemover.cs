using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRemover : MonoBehaviour
{
    [SerializeField] public List<int> CharaNumList = new List<int>();
    [SerializeField] public TimeKeeper timeKeeper;
    
    public void RemoveChara()
    {
        timeKeeper.CharaRemove(CharaNumList);
    }
}
