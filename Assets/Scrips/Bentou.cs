using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bentou : MonoBehaviour
{
    public string BentouName;
    [SerializeField] private GameObject bentouPanel;
    [SerializeField] private Text bentouPanelText;
    [SerializeField] private GameObject bentouButton;
    [SerializeField] private Animator bentouAnimator;
    [SerializeField] private Fungus.Flowchart StoryFlowChart;
    [SerializeField] private GetNakama MasaoFriends;
    [SerializeField] public CharaDataArray charaDataArray;

    public void Start()
    {
        if (StoryFlowChart.GetBooleanVariable("BenTou"))
        {
            GetBentou();
        }
    }
    public void GetBentou()
    {
        bentouButton.SetActive(true);
    }

    public void UseBentou()
    {
        bentouPanelText.text = $"<color=orange>{BentouName}</color>を皆で美味しく頂いた！\n一同は元気満タンになった!!";
        for (int i = 0; i < 4; i++)
        {
            if (MasaoFriends.Friends[i] != null)
            {
                MasaoFriends.Kaifuku(i);
            }
        }
        bentouPanel.SetActive(true);
        Invoke("DeleteBentou", 2f);
    }

    public void DeleteBentou()
    {
        bentouPanel.SetActive(false);
        bentouButton.SetActive(false);
        StoryFlowChart.SetBooleanVariable("BenTou", false);
    }
    public void StartSave()
    {
        charaDataArray.mainChara.bentouname = BentouName;
    }
    public void GetSave()
    {
        BentouName = charaDataArray.mainChara.bentouname;
        if (StoryFlowChart.GetBooleanVariable("BenTou"))
        {
            GetBentou();
        }
    }
}
