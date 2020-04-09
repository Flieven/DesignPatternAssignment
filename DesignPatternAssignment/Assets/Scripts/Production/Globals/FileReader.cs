using System.Collections;
using System.Collections.Generic;
using System.IO;
using TowerDefense;
using UnityEngine;

public class FileReader : MonoBehaviour
{
    private int mapXSize = 0;
    private int mapYSize = 0;
    private int[,] mapData;

    private string mapString;

    private List<string> waveList = new List<string>();
    private Dictionary<int, Int2> waveData = new Dictionary<int, Int2>();

    public void Read(string mapName)
    {
        //Assets / Resources / MapSettings / map_1.txt
        string filePath = "Assets/Resources/" + ProjectPaths.RESOURCES_MAP_SETTINGS + mapName + ".txt";

        using (StreamReader reader = new StreamReader(filePath))
        {
            List<string> stringList = new List<string>();
            bool Waves = false;

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();

                if (stringList.Count == 0) { mapXSize = line.Length; }

                stringList.Add(line);
            }

            foreach (string stringData in stringList)
            {
                if (stringData == "#") { Waves = true; }
                if (!Waves)
                {
                    mapString += stringData;
                }
                else
                {
                    if (stringData != "#") { waveList.Add(stringData); }
                }
            }

            mapYSize = mapString.Length / mapXSize;
        }

        SetupMapArray();
        SetupWaves();
    }

    void SetupMapArray()
    {
        mapData = new int[mapXSize, mapYSize];

        int index = 0;

        char[] charArray = mapString.ToCharArray();

        for (int y = 0; y < mapYSize; y++)
        {
            for (int x = 0; x < mapXSize; x++)
            {
                mapData[x, y] = int.Parse(charArray[index].ToString());
                //Debug.Log(x + " | " + y + " : " + mapString[index]);
                index++;
            }
        }

    }

    void SetupWaves()
    {
        int waveIndex = 0;

        foreach (string wave in waveList)
        {
            string[] waveSplit = wave.Split(' ');

            waveData.Add(waveIndex, new Int2(int.Parse(waveSplit[0]), int.Parse(waveSplit[1])));
            //Debug.Log(waveIndex + " = " + waveData[waveIndex].x + " | " + waveData[waveIndex].y);
            waveIndex++;
        }
    }

    public int[,] getMapData { get { return mapData; } }

    public Dictionary<int, Int2> getWaveData { get { return waveData; } }

}
