using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// allows the object to fade out when triggered
public class Fader : MonoBehaviour
{
    private float alpha = 1;
    private bool fading = false;
    private float fadeWait = 1.5f;
    private const float FADE_SPEED = 0.5f; // opacity per second

    void Update()
    {
        if(fading) {
            if(fadeWait > 0) {
                fadeWait -= Time.deltaTime;
            }
            else if(alpha > 0) {
                alpha -= FADE_SPEED * Time.deltaTime;
                if(alpha < 0) {
                    alpha = 0;
                }
                GetComponent<Image>().color = new Color(1, 1, 1, alpha);
            }
        }
    }

    public void FadeOut() {
        fading = true;
    }
}
