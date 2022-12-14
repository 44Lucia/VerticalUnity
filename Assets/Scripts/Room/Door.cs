using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Room destinationRoom;

    [SerializeField] private Transform newPosPlayer;

    private Collider collider;

    [SerializeField] private bool isOpenTreasureDoor;

    [Header("Music")]
    [SerializeField] private AudioClip m_openDoorClip;

    private void Start()
    {
        RoomManager.Instance.GetCurrentRoom.AddDoor(this);
        collider = GetComponent<Collider>();
    }

    public void OpenDoor() 
    {
        collider.isTrigger = true;
        AudioManager.Instance.UIEffectsAudioSource.PlayOneShot(m_openDoorClip);
        Debug.Log("puerta abierta");
    }

    public void GoToNextRoom() 
    {
        RoomManager roomManager = RoomManager.Instance;

        //roomManager.ActiveRoom(roomManager.CurrentRoom, false);
        if (!isOpenTreasureDoor)
        {
            CameraMovement.Instance.UpdatePosition();
            roomManager.SetRoomTo(destinationRoom);
            PlayerController.Instance.SetPosition(newPosPlayer.position);
        }
    }

   private void OnTriggerEnter(Collider other)
   {
        if (other.CompareTag("Player")){
            GoToNextRoom();
        }
    }

    public bool SetTreasureDoorState { get => isOpenTreasureDoor; set => isOpenTreasureDoor = value; }
}
