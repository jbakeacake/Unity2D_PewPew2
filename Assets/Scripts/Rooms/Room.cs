using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static RoomOptions;

public class Room : MonoBehaviour
{
    public RoomOptions roomOptions;
    private int degreeOfChance;
    void Start()
    {
        this.degreeOfChance = 0;
    }
    public void generateRandomCorridors(Vector2 origin, Dictionary<Vector2, GameObject> currentMap)
    {
        // Shuffle our corridor positions to make it look more random:
        CorridorPosition[] shuffledPositions = roomOptions.corridorPositions.OrderBy(ign => Utils.random.Next()).ToArray();
        for (int i = 0; i < shuffledPositions.Length; i++)
        {
            if (!shuffledPositions[i].isInstantiated && canGenerateCorridor(origin, shuffledPositions[i], currentMap))
            {
                Instantiate(
                    roomOptions.corridorPrefab,
                    shuffledPositions[i].transform.position,
                    shuffledPositions[i].transform.rotation
                );
                shuffledPositions[i].isCorridor = true;
                shuffledPositions[i].isInstantiated = true;
            }
            else if (!shuffledPositions[i].isInstantiated)
            {
                Instantiate(
                    roomOptions.wallPrefab,
                    shuffledPositions[i].transform.position,
                    shuffledPositions[i].transform.rotation
                );
                shuffledPositions[i].isCorridor = false;
                shuffledPositions[i].isInstantiated = true;
            }
        }
    }

    private void wallOffUnreachableNeighbors(Vector2 origin, Dictionary<Vector2, GameObject> currentMap)
    {
        tryCreateWall(origin + Vector2.up, currentMap, Direction.BOTTOM, Direction.TOP); // try-create top wall
        tryCreateWall(origin + Vector2.down, currentMap, Direction.TOP, Direction.BOTTOM); // try-create bottom wall
        tryCreateWall(origin + Vector2.left, currentMap, Direction.RIGHT, Direction.LEFT); // try-create left wall
        tryCreateWall(origin + Vector2.right, currentMap, Direction.LEFT, Direction.RIGHT); // try-create right wall
    }

    private void tryCreateWall(Vector2 adjacentCoordinate, Dictionary<Vector2, GameObject> currentMap, Direction connectingDirection, Direction localDirection)
    {
        if (currentMap.TryGetValue(adjacentCoordinate, out GameObject adjacent) && !hasConnectingDoor(adjacent, connectingDirection))
        {
            CorridorPosition corridorPosition = getCorridorPosition(localDirection);
            corridorPosition.isCorridor = false;
            corridorPosition.isInstantiated = true;
            Instantiate(
                roomOptions.wallPrefab,
                corridorPosition.transform.position,
                corridorPosition.transform.rotation
            );
        }
    }

    private bool hasConnectingDoor(GameObject adjacentRoom, Direction connectingDoorDirection)
    {
        CorridorPosition corridorPosition = getCorridorPosition(connectingDoorDirection);
        return corridorPosition.isCorridor;
    }

    private CorridorPosition getCorridorPosition(Direction direction)
    {
        return roomOptions.corridorPositions
            .Where(adjPos => adjPos.direction == direction)
            .First();
    }

    private bool canGenerateCorridor(Vector2 origin, CorridorPosition corridorPosition, Dictionary<Vector2, GameObject> currentMap)
    {
        int chanceOfCorridor = Mathf.RoundToInt(Mathf.Pow(0.5f, degreeOfChance) * 100.0f);
        int diceRoll = Utils.getRandomInteger(0, 101);

        bool successfulRoll = diceRoll <= chanceOfCorridor;
        bool doesAdjacentCorridorExist = false;
        Vector2 adjacentOrigin = Vector2.zero;
        Direction connectingDoorDirection = Direction.NONE;
        switch (corridorPosition.direction) {
            case Direction.TOP:
                adjacentOrigin = origin + Vector2.up;
                connectingDoorDirection = Direction.BOTTOM;
                break;
            case Direction.BOTTOM:
                adjacentOrigin = origin + Vector2.down;
                connectingDoorDirection = Direction.TOP;
                break;
            case Direction.LEFT:
                adjacentOrigin = origin + Vector2.left;
                connectingDoorDirection = Direction.RIGHT;
                break;
            case Direction.RIGHT:
                adjacentOrigin = origin + Vector2.right;
                connectingDoorDirection = Direction.LEFT;
                break;
        }
        if (successfulRoll && currentMap.TryGetValue(adjacentOrigin, out GameObject room)) {
            CorridorPosition adjacentCorridorPosition = room.GetComponent<Room>().getCorridorPosition(connectingDoorDirection);
            doesAdjacentCorridorExist = adjacentCorridorPosition.isCorridor && adjacentCorridorPosition.isInstantiated;
        }

        bool canGenerateCorridor = successfulRoll && !doesAdjacentCorridorExist;
        if (canGenerateCorridor)
        {
            degreeOfChance++;
        }

        return successfulRoll && !doesAdjacentCorridorExist;
    }
}
