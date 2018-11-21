using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAudioController : MonoBehaviour
{
    public AudioSource MovementAudio;         // Audio source used to play engine sounds.
    public AudioClip EngineIdling;            // Audio to play when the tank isn't moving.
    public AudioClip EngineDriving;           // Audio to play when the tank is moving.
    public float PitchRange = 0.2f;           // The amount by which the pitch of the engine noises can vary.
    float originalPitch;                      // The pitch of the audio source at the start of the scene.

    public AudioSource ShootingAudio;         // Audio source used to play the shooting audio.
    public AudioClip FireClip;                // Audio that plays when each shot is fired.
    public AudioClip ExplosionClip;           // Audio that plays when each shot is fired.

    float movementInputValue;                 // The current value of the movement input.
    float turnInputValue;                     // The current value of the turn input.

    void Start()
    {
        // Store the original pitch of the audio source.
        originalPitch = MovementAudio.pitch;
        MovementAudio.clip = EngineIdling;
        MovementAudio.Play();
    }

    void Update ()
    {
        movementInputValue = Input.GetAxis("Vertical");
        turnInputValue = Input.GetAxis("Horizontal");

        // If there is no input (the tank is stationary)...
        if (Mathf.Abs(movementInputValue) < 0.1f && Mathf.Abs(turnInputValue) < 0.1f)
        {
            // ... and if the audio source is currently playing the driving clip...
            if (MovementAudio.clip == EngineDriving)
            {
                // ... change the clip to idling and play it.
                MovementAudio.clip = EngineIdling;
                MovementAudio.pitch = Random.Range(originalPitch - PitchRange, originalPitch + PitchRange);
                MovementAudio.Play();
            }
        }
        else
        {
            // Otherwise if the tank is moving and if the idling clip is currently playing...
            if (MovementAudio.clip == EngineIdling)
            {
                // ... change the clip to driving and play.
                MovementAudio.clip = EngineDriving;
                MovementAudio.pitch = Random.Range(originalPitch - PitchRange, originalPitch + PitchRange);
                MovementAudio.Play();
            }
        }
    }

    public void Fire()
    {
        ShootingAudio.clip = FireClip;
        ShootingAudio.Play();
    }

    public void Explosion()
    {
        ShootingAudio.clip = ExplosionClip;
        ShootingAudio.Play();
    }
}