using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anten : MonoBehaviour
{
    [SerializeField] public GameObject BlackCurtain;
    [SerializeField] public GameObject RedCurtain;
    [SerializeField] public GameObject BlueCurtain;
    [SerializeField] public GameObject Title;
    [SerializeField] public Animator TitleAnimation;
    [SerializeField] public Animator BlackCurtainAnimation;
   public void ExecuteTitleAnten()
    {
        if(Title.activeSelf)
        TitleAnimation.SetBool("Titleawake", true);
    }
    public void ExecuteAnten()
    {
        BlackCurtain.SetActive(true);
        BlackCurtainAnimation.SetBool("Antenstart", true);
    }

    public void RedCurtain_On()
    {
        RedCurtain.SetActive(true);
    }
    public void RlueCurtain_On()
    {
        BlueCurtain.SetActive(true);
    }
    public void RedCurtain_Off()
    {
        RedCurtain.SetActive(false);
    }
    public void BlueCurtain_Off()
    {
        RedCurtain.SetActive(false);
    }
    public void StopBlackCurtainAni()
    {
        BlackCurtainAnimation.enabled = false;
    }
    public void RestartBlackCurtainAni()
    {
        BlackCurtainAnimation.enabled = true;
    }
}
