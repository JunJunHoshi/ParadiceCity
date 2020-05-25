using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagController : MonoBehaviour
{//誰に、なんのフラグを立てるか　引数がなくてもFingusからメソッドを呼び出しただけでフラグを立てられるようにする
    [SerializeField] public List<Fungus.Flowchart> TargetFlowChartList = new List<Fungus.Flowchart>();
    [SerializeField] public List<string> TargetValue = new List<string>();

    public void FlagUp1()
    {
        TargetFlowChartList[0].SetBooleanVariable(TargetValue[0], true);
    }
    public void FlagUp2()
    {
        TargetFlowChartList[1].SetBooleanVariable(TargetValue[1], true);
    }
    public void FlagUp3()
    {
        TargetFlowChartList[2].SetBooleanVariable(TargetValue[2], true);
    }
    public void FlagUp4()
    {
        TargetFlowChartList[3].SetBooleanVariable(TargetValue[3], true);
    }
    public void FlagUp5()
    {
        TargetFlowChartList[4].SetBooleanVariable(TargetValue[4], true);
    }

    public void FlagDown1()
    {
        TargetFlowChartList[0].SetBooleanVariable(TargetValue[0], false);
    }
    public void FlagDown2()
    {
        TargetFlowChartList[1].SetBooleanVariable(TargetValue[1], false);
    }
    public void FlagDown3()
    {
        TargetFlowChartList[2].SetBooleanVariable(TargetValue[2], false);
    }
    public void FlagDown4()
    {
        TargetFlowChartList[3].SetBooleanVariable(TargetValue[3], false);
    }
    public void FlagDown5()
    {
        TargetFlowChartList[4].SetBooleanVariable(TargetValue[4], false);
    }
}
