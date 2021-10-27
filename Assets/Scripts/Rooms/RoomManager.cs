using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public int maxNumberOfRooms;
    public GameObject initialRoom, exitRoom;
    public RoomVariance[] roomVariance;
    public MiniMapController miniMapController;
    public static readonly int DISTANCE_BETWEEN_ROOMS = 60;
    private Dictionary<Vector2Int, Room> dungeonMap;
    private Queue<GameObject> roomQueue;

    // Start is called before the first frame update
    void Awake()
    {
        this.dungeonMap = new Dictionary<Vector2Int, Room>();
        this.roomQueue = this.generateRoomQueue();
        this.generateInitialRoom();
    }

    public void tryGenerateAdjoiningRoom(Room previousRoom, Vector2Int corridorDirection)
    {
        Vector2Int newMapCoordinate = previousRoom.mapCoordinate + corridorDirection;
        if (!this.dungeonMap.TryGetValue(newMapCoordinate, out Room ignore))
        {
            if (roomQueue.Count == 1)
            {
                generateRoom(roomQueue.Dequeue(), newMapCoordinate, 10); // Wall off Exit Room
            }
            else if (roomQueue.Count > 0)
            {
                generateRoom(roomQueue.Dequeue(), newMapCoordinate);
            }
            else
            {
                generateRoom(getWeightedRandomRoom(), newMapCoordinate, 10); // Wall off Edge Rooms
            }
        }
    }

    public void generateRoom(GameObject original, Vector2Int origin, int initialDegreeOfChance = 0)
    {
        Room room = Instantiate(
            original,
            new Vector3(origin.x, origin.y) * DISTANCE_BETWEEN_ROOMS,
            Quaternion.identity)
            .GetComponent<Room>();
        room.init(initialDegreeOfChance, origin, dungeonMap, this);
        dungeonMap.Add(origin, room);
    }

    private Queue<GameObject> generateRoomQueue()
    {
        Queue<GameObject> queue = new Queue<GameObject>();
        for (int i = 0; i < maxNumberOfRooms - 1; i++)
        {
            queue.Enqueue(getWeightedRandomRoom());
        }

        queue.Enqueue(exitRoom);
        return queue;
    }

    private GameObject getWeightedRandomRoom()
    {
        double totalWeight = 0;
        foreach (RoomVariance v in this.roomVariance)
        {
            totalWeight += v.weight;
        }

        int idx = 0;
        for (double r = Utils.random.NextDouble() * totalWeight; idx < roomVariance.Length - 1; ++idx)
        {
            r -= roomVariance[idx].weight;
            if (r <= 0.0)
            {
                break;
            }
        }

        return roomVariance[idx].roomPrefab;
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
