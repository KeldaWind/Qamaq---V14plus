﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class scrYamtaLightningButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    bool mouseOn;

    public Text text;

    /*Vector3 BasePosition;
    Vector3 CurrentPosition;
    public float shakingDelay;
    float currentShakingDelay;*/

    public Image fill;
    bool colorSwitch;
    [Header("Colors")]
    public float colorChangeTime;
    float currentColorChangeTime;
    public Color UnlockableOffColor;
    public Color UnlockableColor;
    public Color UnlockedHighColor;
    public Color UnlockedLowColor;
    public Color EquipedHighColor;
    public Color EquipedLowColor;
    public Color LockedColor;
    public Color LockedMouseOnColor;
    Color CurrentColor;
    public Image OuterGlow;
    public Image innerStroke;

    [Header("Particles")]
    public ParticleSystem unlockingParticles;
    public ParticleSystem unlockableParticles;
    public ParticleSystem equipedParticles1;
    public ParticleSystem equipedParticles2;
    public ParticleSystem equipedParticles3;
    public ParticleSystem equipedParticles4;

    [Header("Bumping")]
    public float bumpingTime;
    bool bumping;
    public float bumpingForce;

    [Header("Shaking")]
    public float refuseShakingDuration;
    public float refuseShakingAmount;
    public float buyableShakingDuration;
    public float buyableShakingAmount;
    public float buyingShakingDuration;
    public float buyingShakingAmount;
    public float equipementShakingDuration;
    public float equipementShakingAmount;
    

    // Use this for initialization
    void Start() {
        innerStroke.color = Color.clear;
    }

    // Update is called once per frame
    void Update() {
                
        //Placer dans le premier Slot
        if (mouseOn && Input.GetAxis("Slot One Input") != 0 && scrExperienceManager.ExperienceManager.yamtaLightningUnclocked)
        {
			scrCompetenceManager.CompetenceManager.EquipInFirstSlot (scrCompetenceManager.CompetenceManager.yamtaLightningName);
        }

        //Placer dans le SLot 2
        if (mouseOn && Input.GetAxis("Slot Two Input") != 0 && scrExperienceManager.ExperienceManager.yamtaLightningUnclocked)
        {
			scrCompetenceManager.CompetenceManager.EquipInSecondSlot (scrCompetenceManager.CompetenceManager.yamtaLightningName);
        }

        //Placer dans le Slot 3
        if (mouseOn && Input.GetAxis("Slot Three Input") != 0 && scrExperienceManager.ExperienceManager.yamtaLightningUnclocked)
        {
			scrCompetenceManager.CompetenceManager.EquipInThirdSlot (scrCompetenceManager.CompetenceManager.yamtaLightningName);
        }

        if(scrExperienceManager.ExperienceManager.inCompetenceMenu)
            FeedbackUpdate();

        ParticlesUpdate();

        BumpUpdate();

    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        mouseOn = true;


        if (scrExperienceManager.ExperienceManager.yamtaLightningUnclocked)
            text.text = "Right Click To Use";
        else
            text.text = "Cost : 10 points";

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOn = false;

        text.text = "Base Lightning";

    }

    void FeedbackUpdate()
    {

        OuterGlow.transform.position = transform.position;

        if (scrExperienceManager.ExperienceManager.currentExperience >= scrExperienceManager.ExperienceManager.firstLevelCompCost && !scrExperienceManager.ExperienceManager.yamtaLightningUnclocked)
        {
            //Shake du bouton
            if (mouseOn)
            {

                GetComponent<Screenshake>().shakeAmount = buyableShakingAmount;
                GetComponent<Screenshake>().shakeDuration = Time.deltaTime;

                
            }
        }

        //transform.localPosition = CurrentPosition;
        

        //couleur du logo

            //pas débloqué, pas déblocable
        if(!scrExperienceManager.ExperienceManager.yamtaLightningUnclocked && scrExperienceManager.ExperienceManager.currentExperience < scrExperienceManager.ExperienceManager.firstLevelCompCost)
        {

            if (GetComponent<Screenshake>().shakeDuration > 0)
            {
                if (currentColorChangeTime < 1)
                    currentColorChangeTime += 10 * Time.deltaTime;
            }


            else
            {
                if (currentColorChangeTime > 0)
                    currentColorChangeTime -= 10 * Time.deltaTime;
            }


            CurrentColor.r = Mathf.Lerp(LockedColor.r, LockedMouseOnColor.r, currentColorChangeTime);
            CurrentColor.g = Mathf.Lerp(LockedColor.g, LockedMouseOnColor.g, currentColorChangeTime);
            CurrentColor.b = Mathf.Lerp(LockedColor.b, LockedMouseOnColor.b, currentColorChangeTime);
            CurrentColor.a = Mathf.Lerp(LockedColor.a, LockedMouseOnColor.a, currentColorChangeTime);
            fill.color = CurrentColor;

            GetComponent<Image>().color = new Color(85f / 255f, 85f / 255f, 85f / 255f, 1);

        }
        else
            GetComponent<Image>().color = new Color(1, 1, 1, 1);

        //pas débloqué, déblocable
        if (!scrExperienceManager.ExperienceManager.yamtaLightningUnclocked && scrExperienceManager.ExperienceManager.currentExperience >= scrExperienceManager.ExperienceManager.firstLevelCompCost)
        {
            if (!mouseOn)
            {
                if (Mathf.Abs(currentColorChangeTime - scrExperienceManager.ExperienceManager.currentGlobalColorChangeTime) > 5 * Time.deltaTime)
                {

                    currentColorChangeTime -= 3 * Time.deltaTime;
                }
                else
                {
                    currentColorChangeTime = scrExperienceManager.ExperienceManager.currentGlobalColorChangeTime;

                }

                if (scrExperienceManager.ExperienceManager.competenceBuyableMouseOnSound.isPlaying)
                    scrExperienceManager.ExperienceManager.competenceBuyableMouseOnSound.Stop();

            }
            else
            {
                if (currentColorChangeTime < 1)
                    currentColorChangeTime += 5 * Time.deltaTime;

                if (!scrExperienceManager.ExperienceManager.competenceBuyableMouseOnSound.isPlaying)
                    scrExperienceManager.ExperienceManager.competenceBuyableMouseOnSound.Play();

            }

            CurrentColor.r = Mathf.Lerp(UnlockableOffColor.r, UnlockableColor.r, currentColorChangeTime);
            CurrentColor.g = Mathf.Lerp(UnlockableOffColor.g, UnlockableColor.g, currentColorChangeTime);
            CurrentColor.b = Mathf.Lerp(UnlockableOffColor.b, UnlockableColor.b, currentColorChangeTime);
            CurrentColor.a = 255;

            fill.color = CurrentColor;


        }

        //débloqué
        if (scrExperienceManager.ExperienceManager.yamtaLightningUnclocked)
            innerStroke.color = CurrentColor;

        //débloqué, pas équipé

        if (scrExperienceManager.ExperienceManager.yamtaLightningUnclocked && !scrCompetenceManager.CompetenceManager.yamtaLightningEquiped)
        {

            CurrentColor.r = Mathf.Lerp(UnlockedLowColor.r, UnlockedHighColor.r, currentColorChangeTime);
            CurrentColor.g = Mathf.Lerp(UnlockedLowColor.g, UnlockedHighColor.g, currentColorChangeTime);
            CurrentColor.b = Mathf.Lerp(UnlockedLowColor.b, UnlockedHighColor.b, currentColorChangeTime);
            CurrentColor.a = Mathf.Lerp(UnlockedLowColor.a, UnlockedHighColor.a, currentColorChangeTime);

            if (colorSwitch)
            {
                currentColorChangeTime -= 0.6f * Time.deltaTime;
            }
            else
            {

                currentColorChangeTime += 0.6f * Time.deltaTime;

            }

            if(currentColorChangeTime > 1)
            {
                colorSwitch = true;
            }
            if(currentColorChangeTime < 0)
            {
                colorSwitch = false;
            }

            fill.color = CurrentColor;

        }

        //équipé
        if (scrCompetenceManager.CompetenceManager.yamtaLightningEquiped)
        {

            CurrentColor.r = Mathf.Lerp(EquipedLowColor.r, EquipedHighColor.r, currentColorChangeTime);
            CurrentColor.g = Mathf.Lerp(EquipedLowColor.g, EquipedHighColor.g, currentColorChangeTime);
            CurrentColor.b = Mathf.Lerp(EquipedLowColor.b, EquipedHighColor.b, currentColorChangeTime);
            CurrentColor.a = Mathf.Lerp(EquipedLowColor.a, EquipedHighColor.a, currentColorChangeTime);

            if (colorSwitch)
            {
                currentColorChangeTime -= 0.6f * Time.deltaTime;
            }
            else
            {

                currentColorChangeTime += 0.6f * Time.deltaTime;

            }

            if (currentColorChangeTime > 1)
            {
                colorSwitch = true;
            }
            if (currentColorChangeTime < 0)
            {
                colorSwitch = false;
            }

            fill.color = CurrentColor;

        }

        if (!scrExperienceManager.ExperienceManager.yamtaLightningUnclocked || scrCompetenceManager.CompetenceManager.yamtaLightningEquiped)
        {
            OuterGlow.color = CurrentColor;
        }
        else
        {
            OuterGlow.color = new Color(0.196f, 0.196f, 0.196f, 255);
        }

        

        //Pour éviter que la valeur ne passe en dessous de zéro et que tout pète un plomb : 
        if (currentColorChangeTime < 0)
            currentColorChangeTime = 0;

    }

    public void UnlockingFeedback()
    {
        if (scrExperienceManager.ExperienceManager.yamtaLightningUnclocked)
        {
            unlockingParticles.Play();
            StartCoroutine(Bump());
        }

    }

    void ParticlesUpdate()
    {
        //achetable ou équipé

        if ((scrCompetenceManager.CompetenceManager.yamtaLightningEquiped
    || (!scrExperienceManager.ExperienceManager.yamtaLightningUnclocked
    && scrExperienceManager.ExperienceManager.currentExperience >= scrExperienceManager.ExperienceManager.firstLevelCompCost
    && (CurrentColor.r > 0.17f || mouseOn)))
    && !unlockableParticles.isEmitting)
        {
            unlockableParticles.Play();
        }
        if ((scrExperienceManager.ExperienceManager.yamtaLightningUnclocked
            || scrExperienceManager.ExperienceManager.currentExperience < scrExperienceManager.ExperienceManager.firstLevelCompCost
            || (CurrentColor.r <= 0.17f && !mouseOn))
            && unlockableParticles.isEmitting
            && !scrCompetenceManager.CompetenceManager.yamtaLightningEquiped)
        {
            unlockableParticles.Stop();
        }

        var unlockableParticlesMain = unlockableParticles.main;

        if (mouseOn && !scrExperienceManager.ExperienceManager.yamtaLightningUnclocked)
            unlockableParticlesMain.simulationSpeed = 5;
        else
            unlockableParticlesMain.simulationSpeed = 1;

        if (scrCompetenceManager.CompetenceManager.yamtaLightningEquiped && !equipedParticles1.isPlaying)
        {
            equipedParticles1.Play();
            equipedParticles2.Play();
            equipedParticles3.Play();
            equipedParticles4.Play();
        }
        if (!scrCompetenceManager.CompetenceManager.yamtaLightningEquiped && equipedParticles1.isPlaying)
        {
            equipedParticles1.Stop();
            equipedParticles2.Stop();
            equipedParticles3.Stop();
            equipedParticles4.Stop();
        }

    }

    public void Shaking()
    {

        if (!scrExperienceManager.ExperienceManager.yamtaLightningUnclocked && scrExperienceManager.ExperienceManager.currentExperience < scrExperienceManager.ExperienceManager.firstLevelCompCost)
        {
            GetComponent<Screenshake>().shakeAmount = refuseShakingAmount;
            GetComponent<Screenshake>().shakeDuration = refuseShakingDuration;
            scrExperienceManager.ExperienceManager.competenceBuyRefuseSound.Play();
        }

        if (scrExperienceManager.ExperienceManager.yamtaLightningUnclocked)
        {

            GetComponent<Screenshake>().shakeAmount = buyingShakingAmount;
            GetComponent<Screenshake>().shakeDuration = buyingShakingDuration;

        }


            

    }

    IEnumerator Bump()
    {
        
        bumping = true;
        yield return new WaitForSeconds(bumpingTime);
        bumping = false;
        
    }

    void BumpUpdate()
    {

        if (bumping)
        {
            transform.localScale -= bumpingForce * new Vector3(Time.deltaTime, Time.deltaTime, 0);
        }
        else
        {
            if(transform.localScale.x < 1)
                transform.localScale += bumpingForce * new Vector3(Time.deltaTime, Time.deltaTime, 0);
            if (transform.localScale.x > 1)
                transform.localScale = new Vector3(1, 1, 1);
        }

    }

}
