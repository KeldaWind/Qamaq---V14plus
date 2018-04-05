using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrDestroyOnAnimationEnd : MonoBehaviour {

    Animator objectAnimator;
    Animation objectAnimation;

    void destroyEndAnimation()
    {
        Destroy(gameObject);
    }
}
