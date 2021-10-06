using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static RoomOptions;

public class Room : MonoBehaviour
{
    public RoomOptions roomOptions;
    public Vector2Int mapCoordinate;
    public int degreeOfChance = 0;

    public Dictionary<Vector2Int, bool?> corridorExistsDictionary;

    void Awake()
    {
        this.corridorExistsDictionary = new Dictionary<Vector2Int, bool?>();
        this.corridorExistsDictionary.Add(Vector2Int.up, null);
        this.corridorExistsDictionary.Add(Vector2Int.down, null);
        this.corridorExistsDictionary.Add(Vector2Int.left, null);
        this.corridorExistsDictionary.Add(Vector2Int.right, null);
    }

    public void generateRandomCorridors(Dictionary<Vector2Int, Room> currentMap)
    {
        foreach (Transform t in roomOptions.corridorOrigins.OrderBy(x => Utils.random.Next()))
        {
            generateCorridor(currentMap, new Vector2Int((int)t.up.x, (int)t.up.y));
        }
    }

    public void wallOffUnreachableNeighbors(Dictionary<Vector2Int, Room> currentMap)
    {
        wallOff(currentMap, mapCoordinate + Vector2Int.up, Vector2Int.up, Vector2Int.down); // try-create top wall
        wallOff(currentMap, mapCoordinate + Vector2Int.down, Vector2Int.down, Vector2Int.up); // try-create bottom wall
        wallOff(currentMap, mapCoordinate + Vector2Int.left, Vector2Int.left, Vector2Int.right); // try-create left wall
        wallOff(currentMap, mapCoordinate + Vector2Int.right, Vector2Int.right, Vector2Int.left); // try-create right wall
    }

    private void wallOff(Dictionary<Vector2Int, Room> currentMap, Vector2Int adjacentCoordinate, Vector2Int originDirection, Vector2Int neighborOriginDirection)
    {
        // First grab our neighbor, check if they exist, then check if they have a corridor:
        if (currentMap.TryGetValue(adjacentCoordinate, out Room neighbor) && !hasCorridor(neighbor, neighborOriginDirection))
        {
            // If yes, create a wall:
            Transform wallOrigin = roomOptions.corridorOrigins.First(t => (new Vector2Int((int)t.up.x, (int)t.up.y)) == originDirection);
            Instantiate(
                roomOptions.wallPrefab,
                wallOrigin.position,
                wallOrigin.rotation
            );
            this.corridorExistsDictionary[originDirection] = false;
        }
    }

    private bool hasCorridor(Room neighbor, Vector2Int corridorDirection)
    {
        if (neighbor == null) return false;

        bool? exists = neighbor.corridorExistsDictionary[corridorDirection];
        return exists.HasValue && exists.Value;
    }

    private void generateCorridor(Dictionary<Vector2Int, Room> currentMap, Vector2Int corridorDirection)
    {
        Room neighbor = null;
        currentMap.TryGetValue(mapCoordinate + corridorDirection, out neighbor);

        bool isWall = this.corridorExistsDictionary[corridorDirection].HasValue && !this.corridorExistsDictionary[corridorDirection].Value;

        // If we don't own a wall in that direction AND our neighbor doesn't have a corridor connected to us:
        if (!isWall && (neighbor == null || !hasCorridor(neighbor, -corridorDirection)))
        {
            Transform corridorOrigin = roomOptions.corridorOrigins.First(t => (new Vector2Int((int)t.up.x, (int)t.up.y)) == corridorDirection);
            if (hasChanceToBeGenerated())
            {
                Corridor corridor = Instantiate(
                    roomOptions.corridorPrefab,
                    corridorOrigin.position,
                    corridorOrigin.rotation
                )
                .GetComponent<Corridor>();
                corridor.parentRoom = this;
                this.corridorExistsDictionary[corridorDirection] = true;
            }
            else
            {
                Instantiate(
                    roomOptions.wallPrefab,
                    corridorOrigin.position,
                    corridorOrigin.rotation
                );
                this.corridorExistsDictionary[corridorDirection] = false;
            }
        }
    }

    private bool hasChanceToBeGenerated()
    {
        int chanceToGenerate = Mathf.RoundToInt(Mathf.Pow(0.5f, degreeOfChance) * 100.0f);
        int rollADie = Utils.getRandomInteger(0, 101);

        degreeOfChance++;

        return rollADie <= chanceToGenerate;
    }
}
