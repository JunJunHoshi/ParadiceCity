using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FungusAnimatorConnecter : MonoBehaviour
{
    public Animator MyAnimator;
    public Fungus.Flowchart MyFlowChart;
    public SpriteRenderer MasaoSprite;
    // Start is called before the first frame update
    void Start()
    {
        MyAnimator = GetComponent<Animator>();
        MyFlowChart = GetComponent<Fungus.Flowchart>();
    }

    public void ToFungus(string message)
    {
        MyFlowChart.SendFungusMessage(message);
    }
    public void GoNext()
    {
        MyAnimator.SetBool("GoNext", true);
    }
    public void DeleteNext()
    {
        MyAnimator.SetBool("GoNext", false);
    }
    public void DeleteMasao()
    {
        MasaoSprite.enabled = false;
    }
    public void MakeMasao()
    {
        MasaoSprite.enabled = true;
    }
    public void StopAnimator()
    {
        MyAnimator.enabled = false;
    }
    public void RestartAnimation()
    {
        MyAnimator.enabled = true;
    }
}
