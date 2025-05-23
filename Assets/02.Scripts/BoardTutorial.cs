using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardTutorial : MonoBehaviour
{
    public string Text;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            StartCoroutine(ShowText());
        }
        
    }

    IEnumerator ShowText()
    {
        UIManager.Instance.TutorialText.text = Text;
        yield return new WaitForSecondsRealtime(5.0f);
        UIManager.Instance.TutorialText.text = "";
    }
}
