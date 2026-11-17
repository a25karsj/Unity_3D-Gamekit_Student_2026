using UnityEngine;
using FMOD.Studio;
using FMODUnity;

[CreateAssetMenu(fileName = "InteractableAudio", menuName = "Scriptable Objects/InteractableAudio")]
public class InteractableAudio : ScriptableObject
{
   // Declares a EventReference that holds a reference to our specific event
   [SerializeField] private EventReference pickupHealthAudio, boxDestroyAudio, switchAudio, doorAudio;

   //Declares a method that should play the pickupHealth sound without any 3D positioning
   public void PickUpHealthPlay()
   {
      RuntimeManager.PlayOneShot(pickupHealthAudio);
   }
   
   //Declared a method that should play the boxDestroyAudio with 3D-positioning by referencing a transform position
   public void BoxDestroyPlay(Transform objectTransform)
   {
      RuntimeManager.PlayOneShot(boxDestroyAudio, objectTransform.position);
   }

   public void SwitchPlay(Transform objectTransform)
   {
      RuntimeManager.PlayOneShot(switchAudio, objectTransform.position);
   }
   
   public void DoorPlay(Transform objectTransform)
   {
      RuntimeManager.PlayOneShot(doorAudio, objectTransform.position);
      
   }
}
