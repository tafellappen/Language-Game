using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    private Animator intro;
    private AnimatorStateInfo info;
    public GameObject btn;

    // Start is called before the first frame update
    void Awake()
    {
        intro = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        info = intro.GetCurrentAnimatorStateInfo(0);
        if (info.normalizedTime >= 1)
        {
            btn.SetActive(true);
        }
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("DialogueTesting");
    }
}
