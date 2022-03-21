using UnityEngine;

public class BowlTrigger : MonoBehaviour
{
    public void OpenExitDoor()
    {
            GameObject door = GameObject.FindGameObjectWithTag("exitDoor");
            Debug.Log("exit door open");
            door.GetComponent<Animator>().Play("exit_door_open");
            AkSoundEngine.PostEvent("Play_door_open", door);
            AkSoundEngine.PostEvent("Play_horror_string", gameObject);
    }
}