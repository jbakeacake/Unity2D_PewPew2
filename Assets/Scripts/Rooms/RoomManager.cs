using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public int maxNumberOfRooms;
    public GameObject initialRoom, exitRoom;
    public RoomVariance[] roomVariance;
    private Dictionary<Vector2, GameObject> dungeonMap;


    // Start is called before the first frame update
    void Start()
    {
        this.dungeonMap = new Dictionary<Vector2, GameObject>();
        this.generateInitialRoom();
    }

    public void generateRoom(GameObject original, Vector2 origin)
    {
        GameObject roomClone = Instantiate(
            original,
            origin,
            Quaternion.identity
        );
        Room room = roomClone.GetComponent<Room>();
        room.generateRandomCorridors(origin, dungeonMap);

        dungeonMap.Add(origin, roomClone);
    }
    
    private void generateInitialRoom()
    {
        Vector2 origin = Vector2.zero;
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
