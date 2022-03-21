using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishedTrigger : MonoBehaviour
{
    [SerializeField] private GameObject monster;
    
    private void OnTriggerEnter(Collider other)
    {
        monster.SetActive(true);
        monster.GetComponent<Animator>().Play("Scream");
        AkSoundEngine.PostEvent("Play_slender", monster);
        StartCoroutine(ExitApplication(5.0f));
    }

    private IEnumerator ExitApplication(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Debug.Log("THE END");
        Application.Quit();
    }
}
