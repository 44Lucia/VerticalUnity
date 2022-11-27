using UnityEngine;

public class Door : MonoBehaviour
{
    //Asignarle la localización de respawneo y mover la camara tmb
    //Next Position
    //[SerializeField] private Vector3 newPosCamera;

    //Ref player
    //[SerializeField] private Transform camera;

    [SerializeField] Room destinationRoom;

    [SerializeField] private Transform newPosPlayer;

    private Collider collider;

    private void Start()
    {
        RoomManager.Instance.GetCurrentRoom.AddDoor(this);
        collider = GetComponent<Collider>();
        //camera = Camera.main.transform;
    }

    public void OpenDoor() 
    {
        collider.isTrigger = true;
        Debug.Log("puerta abierta");
    }

    public void GoToNextRoom() 
    {
        RoomManager roomManager = RoomManager.Instance;

        roomManager.ActiveRoom(roomManager.CurrentRoom, false);
        roomManager.CurrentRoom = destinationRoom;
        roomManager.ActiveRoom(destinationRoom, true);
        PlayerController.Instance.SetPosition(newPosPlayer.position);
    }

   private void OnTriggerEnter(Collider other)
   {
        if (other.CompareTag("Player")){
            GoToNextRoom();
            Debug.Log("uwu");
        }
    }
}
