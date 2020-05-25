using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackCurtainController : MonoBehaviour
{
    [SerializeField] public Fungus.Flowchart storyflowchart;
    [SerializeField] public Animator AntenAnimator;
    [SerializeField] public MoveController MasaruMove;

    public void CurtainOpen()
    {
        storyflowchart.SendFungusMessage("Curtainopen");
    }
    public void AntenFinish()
    {
        AntenAnimator.SetBool("Antenstart", false);
    }
    public void FadeInFinish()
    {
        AntenAnimator.SetBool("Fadein", false);
    }
    public void FadeIn()
    {
        AntenAnimator.SetBool("Fadein", true);
    }

    public void FadeOutFinish()
    {
        AntenAnimator.SetBool("Fadeout", false);
    }
    public void FadeOut()
    {
        AntenAnimator.SetBool("Fadeout", true);
    }
    public void MapMoveStart()
    {
        AntenAnimator.SetBool("Mapmove", true);
    }
    public void MoveFinish()
    {
        AntenAnimator.SetBool("Mapmove", false);
    }
    public void StopMasaru()
    {
        MasaruMove.DeactivePlayer();
    }
    public void GoMasaru()
    {
        MasaruMove.ActivePlayer();
    }
}
    

