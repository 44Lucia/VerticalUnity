using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorMap : MonoBehaviour
{
    [SerializeField] private string Seed;
    [SerializeField] private int CurrentSeed;

    const string glyphs = "abcdefghijklmnopqrstvwxyz0123456789";

    [SerializeField] private int GridDimensionsX;
    [SerializeField] private int GridDimensionsY;

    [SerializeField] private int MinRooms;
    [SerializeField] private int MaxRooms;

    [SerializeField] private GameObject Room;
    [SerializeField] private GameObject StartRoom;
    [SerializeField] private GameObject BossRoom;
    [SerializeField] private GameObject Door;
    [SerializeField] private GameObject Node;

    private int[,] Matrix;
    private int LeftRooms;

    // Start is called before the first frame update
    void Start()
    {
        GetSeed();

        GenerateMatrix();

        Generate();
    }

    private void GetSeed() 
    {
        if (Seed.Length != 8){
            Seed = "";

            for (int i = 0; i < 8; i++){
                Seed += glyphs[Random.Range(0, glyphs.Length)];
            }
        }

        CurrentSeed = Seed.GetHashCode();
        Random.InitState(CurrentSeed);
    }

    private void GenerateMatrix() 
    {
        //0 or 1 door +1 room
        Matrix = new int[GridDimensionsX * 2 - 1, GridDimensionsY * 2 - 1];

        //Room Base
        Matrix[GridDimensionsX - 1, GridDimensionsY - 1] = 2;

        //Number of rooms
        LeftRooms = Random.Range(MinRooms, MaxRooms + 1) - 1;

        PopulateMatrix();
    }

    private void PopulateMatrix() 
    {
        //Pair rooms, odd doors
        for (int i = 2; i < GridDimensionsX * 2 - 3; i = i +2){
            for (int j = 2; j < GridDimensionsY * 2 - 3; j = j +2){
                if (Matrix[i,j] != 0){
                    int random = Random.Range(0, 108);
                    if (random < 20)
                    {
                        if (Matrix[i + 2, j] == 0)
                        {
                            Matrix[i + 1, j] = 1;
                            Matrix[i + 2, j] = Matrix[i, j] + 1;
                            LeftRooms--;
                        }
                        else
                        {
                            Matrix[i + 1, j] = 1;
                        }
                    }
                    else if (random < 40)
                    {
                        if (Matrix[i + 2, j] == 0)
                        {
                            Matrix[i + 1, j] = 1;
                            Matrix[i + 2, j] = Matrix[i, j] + 1;
                            LeftRooms--;
                        }
                        else
                        {
                            Matrix[i + 1, j] = 1;
                        }
                    }
                    else if (random < 60)
                    {
                        if (Matrix[i + 2, j] == 0)
                        {
                            Matrix[i + 1, j] = 1;
                            Matrix[i + 2, j] = Matrix[i, j] + 1;
                            LeftRooms--;
                        }
                        else
                        {
                            Matrix[i + 1, j] = 1;
                        }
                    }
                    else if (random < 80)
                    {
                        if (Matrix[i + 2, j] == 0)
                        {
                            Matrix[i + 1, j] = 1;
                            Matrix[i + 2, j] = Matrix[i, j] + 1;
                            LeftRooms--;
                        }
                        else
                        {
                            Matrix[i + 1, j] = 1;
                        }
                    }
                    else if (random < 100) { }
                    else if (random < 102)
                    {
                        if (Matrix[i + 2, j] == 0)
                        {
                            Matrix[i + 2, j] = Matrix[i, j];
                            Matrix[i + 1, j] = Matrix[i, j];
                        }
                        else
                        {
                            Matrix[i + 1, j] = 1;
                        }
                    }
                    else if (random < 104)
                    {
                        if (Matrix[i + 2, j] == 0)
                        {
                            Matrix[i + 2, j] = Matrix[i, j];
                            Matrix[i + 1, j] = Matrix[i, j];
                        }
                        else
                        {
                            Matrix[i + 1, j] = 1;
                        }
                    }
                    else if (random < 106)
                    {
                        if (Matrix[i + 2, j] == 0)
                        {
                            Matrix[i + 2, j] = Matrix[i, j];
                            Matrix[i + 1, j] = Matrix[i, j];
                        }
                        else
                        {
                            Matrix[i + 1, j] = 1;
                        }
                    }
                    else {
                        if (Matrix[i + 2, j] == 0)
                        {
                            Matrix[i + 2, j] = Matrix[i, j];
                            Matrix[i + 1, j] = Matrix[i, j];
                        }
                        else
                        {
                            Matrix[i + 1, j] = 1;
                        }
                    }
                    if (LeftRooms <= 0) return;
                }
            }
        }
        if (LeftRooms > 0) PopulateMatrix();
    }

    private void Generate() 
    {
        GameObject rooms = new GameObject();
        rooms.name = "Rooms";

        GameObject doors = new GameObject();
        doors.name = "Doors";

        GameObject nodes = new GameObject();
        nodes.name = "Nodes";

        //Nodes
        for (int i = 0; i < GridDimensionsX * 2 -1; i = i +2){
            for (int j = 0; j < GridDimensionsY * 2 - 1; j = j +2){
                Instantiate(Node, new Vector3(i / 2f, j / 2f, 0), Quaternion.identity, nodes.transform);
            }
        }

        for (int i = 0; i < GridDimensionsX * 2 -1; i++){
            for (int j = 0; j < GridDimensionsY * 2 -1; j++){
                if (Matrix[i,j] == 1){
                    Instantiate(Door, new Vector3(i / 2f, j / 2f, 0), Quaternion.identity, doors.transform);
                }
                else if (Matrix[i,j] != 0){
                    Instantiate(Room, new Vector3(i / 2f, j / 2f, 0), Quaternion.identity, rooms.transform);
                }
            }
        }

        Vector3 MaxValue = Vector3.zero;
        for (int i = 0; i < GridDimensionsX * 2 -1; i = 1 + 2){
            for (int j = 0; j < GridDimensionsY * 2 - 1; j = j + 1){
                if (MaxValue.z < Matrix[i,j]){
                    MaxValue = new Vector3(i, j, Matrix[i, j]);
                }
            }
        }

        Instantiate(BossRoom, new Vector3(MaxValue.x / 2f, MaxValue.y / 2f, 0), Quaternion.identity, rooms.transform);
        Instantiate(StartRoom, new Vector3((GridDimensionsX - 1) / 2f, (GridDimensionsY - 1) / 2f, 0), Quaternion.identity, rooms.transform);

        //Readjustments to center
        rooms.transform.position = new Vector3(-GridDimensionsX / 2, -GridDimensionsY / 2);
        doors.transform.position = new Vector3(-GridDimensionsX / 2, -GridDimensionsY / 2);
        nodes.transform.position = new Vector3(-GridDimensionsX / 2, -GridDimensionsY / 2);
    }
}
