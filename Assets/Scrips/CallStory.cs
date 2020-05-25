using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallStory : MonoBehaviour
{
    [SerializeField] Fungus.Flowchart fungusFlowChart;
    
    public void StoryStart()
    {
        fungusFlowChart.SendFungusMessage("story1start");
    }
}
