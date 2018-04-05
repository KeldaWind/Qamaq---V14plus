using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrGlobeLightningShot : MonoBehaviour {

    public float translateSpeed;
    public float lifeTime;

    private void Update()
    {
        Debug.Log(transform.rotation);

        transform.Translate(new Vector3(0, 0, 1) * translateSpeed, Space.World);

        if (lifeTime > 0)
            lifeTime -= Time.deltaTime;

        if (lifeTime < 0)
            Destroy(gameObject);
    }

}
