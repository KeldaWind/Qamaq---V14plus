using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(scrPlayer))]
public class scrObjectLife : MonoBehaviour {

    [Header("Life")]
    public float maxLife;
    public float currentLife;
    bool dead;

    [Header("Recover")]
    public float recoveringTime;
    public float currentRecoveringTime;

    [Header("Experience")]
    public int enemyExperience;

	[Header ("Feedback")]
	public GameObject Player;
	public GameObject deathParticlesSmall;
	public GameObject deathParticlesMedium;
	public GameObject deathParticlesBig;
	private float shakeAmount ;
	private float shakeDuration ;

	// Use this for initialization
	void Start () {
        currentLife = maxLife;

        currentRecoveringTime = 0;
    }
	
	// Update is called once per frame
	void Update () {
		if(currentLife <= 0 && !dead) 
        {
            StartCoroutine(Death());
        }

        RecoveringUpdate();
    }

    void RecoveringUpdate()
    {
        if (currentRecoveringTime > 0)
            currentRecoveringTime -= Time.deltaTime;

        if (currentRecoveringTime < 0)
            currentRecoveringTime = 0;
    }

    IEnumerator Death()
    {
        dead = true;

        yield return new WaitForSeconds(0.2f);

        scrPlayerExperience.PlayerExperience.GainExperience(enemyExperience);
		if (maxLife <= 50) {
			
			scrCameraGlobalMovement.CameraManager.GetComponent<Screenshake> ().shakeAmount = 0.3f;
			scrCameraGlobalMovement.CameraManager.GetComponent<Screenshake> ().shakeDuration = 0.1f;
			Instantiate(deathParticlesSmall, transform.position, new Quaternion(-transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w));
		}

		if (maxLife <= 250 && maxLife>50) {

			scrCameraGlobalMovement.CameraManager.GetComponent<Screenshake> ().shakeAmount = 0.5f;
			scrCameraGlobalMovement.CameraManager.GetComponent<Screenshake> ().shakeDuration = 0.2f;
			Instantiate(deathParticlesMedium, transform.position, new Quaternion(-transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w));
		}


        Destroy(transform.parent.gameObject);
    }
}
