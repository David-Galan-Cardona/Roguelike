using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int width;
    public int height;
    public int X;
    public int Y;
    private bool updatedDoors = false;
    public bool hasBeenCleared = false;
    [SerializeField]
    public List<GameObject> enemies = new List<GameObject>();
    //public bool isStartRoom = false;
    private Enemy_DoorScript Enemy_DoorScript;

    public Room(int x, int y)
    {
        X = x;
        Y = y;
    }
    public Door leftDoor, rightDoor, topDoor, bottomDoor;
    public List<Door> doors = new List<Door>();

    // Start is called before the first frame update
    void Start()
    {
        if (RoomController.instance == null)
        {
            Debug.Log("Wrong scene to press play in");
            return;
        }

        Door[] ds = GetComponentsInChildren<Door>();
        foreach (Door d in ds)
        {
            doors.Add(d);
            switch (d.doorType)
            {
                case Door.DoorType.right:
                    rightDoor = d;
                    break;
                case Door.DoorType.left:
                    leftDoor = d;
                    break;
                case Door.DoorType.top:
                    topDoor = d;
                    break;
                case Door.DoorType.bottom:
                    bottomDoor = d;
                    break;
            }
        }

        RoomController.instance.RegisterRoom(this);

        
    }

    private void Update()
    {
        if (name.Contains("End") && !updatedDoors)
        {
            RemoveUnconnectedDoors();
            updatedDoors = true;
        }
    }

    public void RemoveUnconnectedDoors()
    {
        foreach (Door door in doors)
        {
            switch (door.doorType)
            {
                case Door.DoorType.right:
                    if (getRight() == null)
                    {
                        //door.gameObject.SetActive(false);
                        door.gameObject.GetComponent<BoxCollider2D>().enabled = true;
                        door.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    }
                    break;
                case Door.DoorType.left:
                    if (getLeft() == null)
                    {
                        //door.gameObject.SetActive(false);
                        door.gameObject.GetComponent<BoxCollider2D>().enabled = true;
                        door.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    }
                    break;
                case Door.DoorType.top:
                    if (getTop() == null)
                    {
                        //door.gameObject.SetActive(false);
                        door.gameObject.GetComponent<BoxCollider2D>().enabled = true;
                        door.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    }
                    break;
                case Door.DoorType.bottom:
                    if (getBottom() == null)
                    {
                        //door.gameObject.SetActive(false);
                        door.gameObject.GetComponent<BoxCollider2D>().enabled = true;
                        door.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    }
                    break;
            }
        }
    }
    public Room getRight()
    {
        if (RoomController.instance.DoesRoomExist(X + 1, Y))
        {
            return RoomController.instance.FindRoom(X + 1, Y);
        }
        return null;
    }

    public Room getLeft()
    {
        if (RoomController.instance.DoesRoomExist(X - 1, Y))
        {
            return RoomController.instance.FindRoom(X - 1, Y);
        }
        return null;
    }

    public Room getTop()
    {
        if (RoomController.instance.DoesRoomExist(X, Y + 1))
        {
            return RoomController.instance.FindRoom(X, Y + 1);
        }
        return null;
    }

    public Room getBottom()
    {
        if (RoomController.instance.DoesRoomExist(X, Y - 1))
        {
            return RoomController.instance.FindRoom(X, Y - 1);
        }
        return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
    }

    public Vector3 GetRoomCentre()
    {
        return new Vector3(X * width, Y * height);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            RoomController.instance.OnPlayerEnterRoom(this);
            if (hasBeenCleared == false)
            {
                hasBeenCleared = true;
                //pon el collider de las Doors en true
                foreach (Door door in doors)
                {
                    door.gameObject.GetComponent<BoxCollider2D>().enabled = true;
                }
                foreach (GameObject enemy in enemies)
                {
                    //pon el collider de los hijos de los enemigos en true
                    foreach (Collider2D collider in enemy.GetComponentsInChildren<Collider2D>())
                    {
                        collider.enabled = true;
                    }
                }
            }
        }
    }
}
