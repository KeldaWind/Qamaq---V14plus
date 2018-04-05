using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrOffensiveCompetence : MonoBehaviour
{

    [Header("Competence Objects")]
    public GameObject lightningPrefab;

    [Header("Competence Values")]
    public float lightningMaxDamages;

    [Header("Competences Visuals")]
    public GameObject globeLightningShot;

    [Header("Cooldowns")]
    public float cooldown;
    public float currentCooldown;


    private void Start()
    {
        currentCooldown = 0;
    }

    void Update()
    {
        if (currentCooldown == 0 && Input.GetMouseButtonDown(1))
            CompetenceActivation();

        if (!GetComponent<scrPlayerPaused>().playerPaused)
            CooldownUpdate();

        AnimationUpdate();
    }

    void CooldownUpdate()
    {
        //gère les cooldowns
        if (currentCooldown > 0)
            currentCooldown -= Time.deltaTime;

        if (currentCooldown < 0)
            currentCooldown = 0;

        if (currentCooldown == 0)
        {

        }
    }


    public void CompetenceActivation()
    {
        //ce qu'il se passe au lancement de la compétence

        Vector3 lightningPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        lightningPos.y = 0;

        currentCooldown = cooldown;

        GameObject lightning = (GameObject)Instantiate(lightningPrefab, lightningPos, transform.rotation);

        lightning.GetComponent<scrOffensiveCompetenceLightning>().lightningMaxDamages = lightningMaxDamages;

        //Instantiate(globeLightningShot, GetComponentInChildren<scrGlobeMovement>().gameObject.transform.position, new Quaternion(0, 0.7f, -0.7f, 0));
    }

    void AnimationUpdate()
    {

    }
}
