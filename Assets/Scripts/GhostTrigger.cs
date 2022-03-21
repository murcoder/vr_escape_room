using UnityEngine;

public class GhostTrigger : MonoBehaviour
{
    [SerializeField] private GameObject ghost;
    private bool _hasBeenTriggered = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!_hasBeenTriggered)
        {
            if (!ghost)
            {
                ghost = GameObject.FindGameObjectWithTag("ghost");
            }
            Debug.Log("Ghost has been triggered");
            //if (other.CompareTag("player"))
            //{
            Debug.Log("ghost active");
            ghost.GetComponent<RandomMove>().enabled = true;
            AkSoundEngine.PostEvent("Play_footsteps_ghost", ghost);
            // }
            
            
            // Trigger only once
            _hasBeenTriggered = true;
        }
    }
}
