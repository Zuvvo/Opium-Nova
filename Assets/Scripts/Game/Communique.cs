using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Communique : MonoBehaviour
{
    public GameObject Panel;
    public Text Text;
    public Animator Animator;

    public void SetCommunique(string text)
    {
        if (Animator.GetBool("animate"))
        {
            Animator.Rebind();
        }
        Text.text = text;
        Animator.SetBool("animate", true);
    }

    private IEnumerator WaitForAnimationEnd()
    {
        yield return new WaitForSeconds(Animator.GetCurrentAnimatorStateInfo(0).length*2);
        Animator.SetBool("animate", false);
        StopAllCoroutines();
    }
}
