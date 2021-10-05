using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public int maxNumberOfRooms;
    public GameObject initialRoom, exitRoom;
    public RoomVariance[] roomVariance;
    private Dictionary<Vector2Int, Room> dungeonMap;
    private static readonly int DISTANCE_BETWEEN_ROOMS = 50;

    // Start is called before the first frame update
    void Start()
    {
        this.dungeonMap = new Dictionary<Vector2Int, Room>();
        this.generateInitialRoom();
    }

    public void tryGenerateAdjoiningRoom(Room previousRoom, Vector2Int corridorDirection)
    {
        Vector2Int newMapCoordinate = previousRoom.mapCoordinate + corridorDirection;
        if (!this.dungeonMap.TryGetValue(newMapCoordinate, out Room newRoom))
        {
            Vector2Int previousRoomPosition = new Vector2Int((int)previousRoom.gameObject.transform.position.x, (int)previousRoom.gameObject.transform.position.y);
            generateRoom(initialRoom, newMapCoordinate * DISTANCE_BETWEEN_ROOMS);
        }
    }

    public void generateRoom(GameObject original, Vector2Int origin)
    {
        Room room = Instantiate(
            original,
            new Vector3(origin.x, origin.y),
            Quaternion.identity)
            .GetComponent<Room>();
        Vector2Int newMapCoordinate = origin / DISTANCE_BETWEEN_ROOMS;
        room.mapCoordinate = newMapCoordinate;
        room.wallOffUnreachableNeighbors(dungeonMap);
        room.generateRandomCorridors(dungeonMap);

        dungeonMap.Add(newMapCoordinate, room);
    }

    private void generateInitialRoom()
    {
        Vector2Int origin = Vector2Int.zero;
        generateRoom(initialRoom, origin);
    }


    [Serializable]
    public struct RoomVariance
    {
        [Range(0, 1)]
        public float weight;
        public GameObject roomPrefab;
    }
}
