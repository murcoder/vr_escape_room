using System.Collections;
using Tilia.Locomotors.Teleporter;
using UnityEngine;
using Zinnia.Data.Type;
using Zinnia.Rule;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject Flashlight;
    private bool _flashlightActive = false;
    private bool _flashlightFound = false;
    private bool _menuHasBeenGrabbed = false;

    [SerializeField] GameObject menuObject;
    [SerializeField] Transform spawnPointStartRoom;
    [SerializeField] Transform spawnPointPauseRoom;

    public TeleporterFacade teleporter;
    public Transform playArea;
    public Transform headOrientation;
    public Transform pauseLocation;
    public Transform startLocation;
    public Transform gameLocation;
    public GameObject playerBody;

    private bool _inPauseMenu = true;
    private bool _pressed = false;

    public void Start()
    {
        AkSoundEngine.PostEvent("Play_horror_ambience_1_short", gameObject);
        if (!startLocation)
        {
            startLocation = this.GetStartLocation();
        }

        if (!pauseLocation)
        {
            pauseLocation = this.GetPauseLocation();
        }
    }

    public void menuIsGrabbed()
    {
        _menuHasBeenGrabbed = true;
    }

    public void menuIsNotGrabbed()
    {
        _menuHasBeenGrabbed = false;
    }

    private Transform GetPauseLocation()
    {
        return GameObject.FindGameObjectWithTag("PausePosition").transform;
    }

    private Transform GetStartLocation()
    {
        return GameObject.FindGameObjectWithTag("StartPosition").transform;
    }

    public void SwitchRooms()
    {
        TransformData teleportDestination;
        playerBody.SetActive(false);
        if (_inPauseMenu)
        {
            gameLocation.position =
                new Vector3(headOrientation.position.x, playArea.position.y, headOrientation.position.z);

            Vector3 right = Vector3.Cross(playArea.up, headOrientation.forward);
            Vector3 forward = Vector3.Cross(right, playArea.up);

            gameLocation.rotation = Quaternion.LookRotation(forward, playArea.up);

            teleportDestination = new TransformData(startLocation);

            GameObject ghost = GameObject.FindGameObjectWithTag("ghost");
            if (ghost)
            {
                AkSoundEngine.PostEvent("Play_whispers", ghost);
            }
        }
        else
        {
            gameLocation.position =
                new Vector3(headOrientation.position.x, playArea.position.y, headOrientation.position.z);

            Vector3 right = Vector3.Cross(playArea.up, headOrientation.forward);
            Vector3 forward = Vector3.Cross(right, playArea.up);

            gameLocation.rotation = Quaternion.LookRotation(forward, playArea.up);

            teleportDestination = new TransformData(pauseLocation);
        }

        teleporter.Teleport(teleportDestination);
        _inPauseMenu = !_inPauseMenu;
        StartCoroutine(RestartPlayerBody());
    }

    private IEnumerator RestartPlayerBody()
    {
        yield return new WaitForSeconds(1);
        playerBody.SetActive(true);
    }

    public void PlayGabSound()
    {
        switch (gameObject.tag)
        {
            case "book":
                AkSoundEngine.PostEvent("Play_grab_book_short", gameObject);
                break;
            case "bowl":
                AkSoundEngine.PostEvent("Play_grab_bowl_short", gameObject);
                break;
            default:
                AkSoundEngine.PostEvent("Play_grab_book_short", gameObject);
                break;
        }
    }

    public void RespawnMenu()
    {
        // Avoid press/release button problem
        _pressed = !_pressed;
        if (_pressed && !_menuHasBeenGrabbed)
        {
            Transform spawnPoint = spawnPointStartRoom;
            if (_inPauseMenu)
            {
                spawnPoint = spawnPointPauseRoom;
            }

            GameObject menuObjectCopy = Instantiate(menuObject, spawnPoint.localPosition, Quaternion.identity);
            menuObjectCopy.SetActive(true);
            Destroy(menuObject);
            menuObject = menuObjectCopy;
        }
    }

    public void ResetGame()
    {
        playerBody.SetActive(false);
        _inPauseMenu = true;
        if (!_menuHasBeenGrabbed)
        {
            RespawnMenu();
        }

        teleporter.Teleport(GetPauseLocation());
        StartCoroutine(RestartPlayerBody());
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void FlashlightFirstTimeGrabbed()
    {
        _flashlightFound = true;
    }

    public void ActivateFlashlight()
    {
        _flashlightActive = true;
    }

    public void DisableFlashlight()
    {
        _flashlightActive = false;
    }


    public void ToggleLight(Light lightSource)
    {
        if (_flashlightFound)
            AkSoundEngine.PostEvent("Play_click", Flashlight);
        if (_flashlightActive)
        {
            lightSource.enabled = !lightSource.enabled;
        }
    }
}