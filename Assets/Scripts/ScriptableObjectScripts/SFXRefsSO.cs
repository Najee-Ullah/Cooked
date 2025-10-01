using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SFXRefsSO : ScriptableObject
{
    public AudioClip[] chopSound;
    public AudioClip[] trashSound;
    public AudioClip[] objectPickUpSound;
    public AudioClip[] objectDropSound;
    public AudioClip[] deliveryFail;
    public AudioClip[] deliverySuccess;
    public AudioClip stoveSizzleLoop;
    public AudioClip[] warning;
}
