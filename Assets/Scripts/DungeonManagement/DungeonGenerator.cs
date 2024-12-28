using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public DungeonGenerationData dungeonGenerationData;
    private List<Vector2Int> dungeonRooms;
    //random number
    private int rand;

    private void Start()
    {
        dungeonRooms = DungeonCrawlerController.GenerateDungeon(dungeonGenerationData);
        SpawnRooms(dungeonRooms);
    }

    private void SpawnRooms(IEnumerable<Vector2Int> Rooms)
    {
        
        RoomController.instance.LoadRoom("Start", 0, 0);
        foreach (Vector2Int roomLocation in Rooms)
        {
            //assign rand to a random number between 0 and 4
            rand = Random.Range(0, 13);
            switch (rand)
            {
                case 0:
                    RoomController.instance.LoadRoom("Room1", roomLocation.x, roomLocation.y);
                    break;
                case 1:
                    RoomController.instance.LoadRoom("Room1", roomLocation.x, roomLocation.y);
                    break;
                case 2:
                    RoomController.instance.LoadRoom("Room1", roomLocation.x, roomLocation.y);
                    break;
                case 3:
                    RoomController.instance.LoadRoom("Room1", roomLocation.x, roomLocation.y);
                    break;
                case 4:
                    RoomController.instance.LoadRoom("Room2", roomLocation.x, roomLocation.y);
                    break;
                case 5:
                    RoomController.instance.LoadRoom("Room2", roomLocation.x, roomLocation.y);
                    break;
                case 6:
                    RoomController.instance.LoadRoom("Room2", roomLocation.x, roomLocation.y);
                    break;
                case 7:
                    RoomController.instance.LoadRoom("Room2", roomLocation.x, roomLocation.y);
                    break;
                case 8:
                    RoomController.instance.LoadRoom("Room4", roomLocation.x, roomLocation.y);
                    break;
                case 9:
                    RoomController.instance.LoadRoom("Room4", roomLocation.x, roomLocation.y);
                    break;
                case 10:
                    RoomController.instance.LoadRoom("Room4", roomLocation.x, roomLocation.y);
                    break;
                case 11:
                    RoomController.instance.LoadRoom("Room4", roomLocation.x, roomLocation.y);
                    break;
                case 12:
                    RoomController.instance.LoadRoom("Room3", roomLocation.x, roomLocation.y);
                    break;
            }
        }
    }
}
