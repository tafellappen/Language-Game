using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//script for the speech bubble that tells the player what word they just learned
public class SpeechBubbleScript : MonoBehaviour
{
    private float alpha = 0;
    private float waitTime = 3f;
    private const float WAIT_DURATION = 3f;

    // Start is called before the first frame update
    void Start()
    {
        SetAlpha(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(alpha > 0)
        {
            if(waitTime > 0)
            {
                waitTime -= Time.deltaTime;
            } 
            else
            {
                alpha -= Time.deltaTime;
                if(alpha < 0)
                {
                    alpha = 0;
                }
                SetAlpha(alpha);
            }
        }
    }

    public void TeachWord(string word)
    {
        transform.GetChild(0).GetComponent<Text>().text = "\"" + word + "\"";
        alpha = 1;
        waitTime = WAIT_DURATION;
        SetAlpha(alpha);
    }

    private void SetAlpha(float alpha)
    {
        GetComponent<Image>().color = new Color(1, 1, 1, alpha);
        transform.GetChild(0).GetComponent<Text>().color = new Color(0, 0, 0, alpha);
    }
}
