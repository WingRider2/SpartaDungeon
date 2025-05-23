using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerReStart : MonoBehaviour
{
    void OnReStart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
