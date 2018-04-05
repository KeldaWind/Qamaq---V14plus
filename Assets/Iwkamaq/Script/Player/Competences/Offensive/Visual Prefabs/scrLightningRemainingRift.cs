using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrLightningRemainingRift : MonoBehaviour {

    [Header("Values")]
    public float spawnMinSpeed;
    public float spawnMaxSpeed;
    public float minDistance;
    public float maxDistance;

    [Header("Objects")]
    public GameObject[] lightningResidualsPrefabs;

    void Start()
    {
        StartCoroutine(SpawnRandomResidual());
    }

    void Update () {

	}

    IEnumerator SpawnRandomResidual()
    {
        yield return new WaitForSeconds(Random.Range(spawnMinSpeed, spawnMaxSpeed));

        Instantiate(lightningResidualsPrefabs[Random.Range(0, lightningResidualsPrefabs.Length)], transform.position + new Vector3(Mathf.Sign(Random.Range(-1f, 1f)) * Random.Range(minDistance, maxDistance), 0, Mathf.Sign(Random.Range(-1f, 1f)) * Random.Range(minDistance, maxDistance)), transform.rotation);
        
        StartCoroutine(SpawnRandomResidual());
    }
}
