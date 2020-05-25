using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonList1 : MonoBehaviour
{
    [SerializeField] public List<GameObject> BattleCommend_Button_List1 = new List<GameObject>();
    [SerializeField] public List<GameObject> BattleCommend_Button_List2 = new List<GameObject>();
    [SerializeField] public List<GameObject> BattleCommend_Button_List3 = new List<GameObject>();
    [SerializeField] public List<GameObject> BattleCommend_Button_List4 = new List<GameObject>();

    public void SetButton(int i, int j, GameObject buttontype)
    {
        Debug.Log(buttontype);
        switch (i)
        {
            case 0:
                BattleCommend_Button_List1[j] = buttontype;
                BattleCommend_Button_List1[j].SetActive(false);
                break;

            case 1:
                BattleCommend_Button_List2[j] = buttontype;
                BattleCommend_Button_List2[j].SetActive(true);
                break;
            case 2:
                BattleCommend_Button_List3[j] = buttontype;
                BattleCommend_Button_List3[j].SetActive(true);
                break;
            case 3:
                BattleCommend_Button_List4[j] = buttontype;
                BattleCommend_Button_List4[j].SetActive(false);
                break;
            default:
                break;
        }
    }
}
