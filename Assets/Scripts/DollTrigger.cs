using UnityEngine;

public class DollTrigger : MonoBehaviour
{
    [SerializeField] private GameObject doll;
    private bool _hasBeenTriggered = false;

    public void OnTriggerEnter(Collider other)
    {
        if (!_hasBeenTriggered)
        {
            Debug.Log("doll moving");
            doll.GetComponent<Animator>().Play("move_around");
            AkSoundEngine.PostEvent("Play_doll_laugh", doll);
            
            // Trigger only once
            _hasBeenTriggered = true;
        }
    }
}