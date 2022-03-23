using System.Collections;
using UnityEngine;

public class BookTrigger : MonoBehaviour
{
    [SerializeField] private Light flashlight;

    public void OpenDoor()
    {
        GameObject door = GameObject.FindGameObjectWithTag("door1");
        door.GetComponent<Animator>().Play("door_open");
        AkSoundEngine.PostEvent("Play_door_open", door);
        StartCoroutine(TurnOffFlashlight(1.0f));
        StartCoroutine(StartNextLevel(5.0f));
    }

    /**
     * Turn off the flashlight
     */
    private IEnumerator TurnOffFlashlight(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        flashlight.enabled = false;
        AkSoundEngine.PostEvent("Play_stamps", gameObject);
    }
    
    /**
     * Turn off light and play sounds for next level
     */
    private IEnumerator StartNextLevel(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        GameObject doll = GameObject.FindGameObjectWithTag("doll");
        AkSoundEngine.PostEvent("Play_strange_laugh", doll);
    }
}