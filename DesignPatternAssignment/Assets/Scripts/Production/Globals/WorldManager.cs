using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(FileReader))]
public class WorldManager : MonoBehaviour
{
    #region map and file reader
    [Header("Map Name")]
    [SerializeField] private string mapName = "";

    private FileReader fileReader;

    private AI.Dijkstra dijkstra;
    #endregion

    #region Prefabs
    [Header("Prefabs")]
    [SerializeField] private GameObject MapBuilderPrefab = null;
    private GameObject MapBuilderObject = null;
    MapBuilder builder = null;

    [SerializeField] private GameObject DictionaryPrefab = null;
    private GameObject DictionaryObject = null;

    [SerializeField] private GameObject UnitManagerPrefab = null;
    private GameObject UnitManager = null;

    [SerializeField] private GameObject PlayerManagerPrefab = null;
    private GameObject PlayerManager = null;
    #endregion

    private int currentWave = 0;
    Dictionary<int, Int2> waveData;

    [Header("UI")]
    [SerializeField] private GameObject HealthPanel = null;
    [SerializeField] private GameObject GameOverPanel = null;

    [Header("Misc")]
    [SerializeField] private float displacement = 2.0f;
    private bool gameIsOver = false;

    private void Awake()
    {

        fileReader = transform.GetComponent<FileReader>();

        fileReader.Read(mapName);

        waveData = fileReader.getWaveData;

        if (!DictionaryObject)
        {
            DictionaryObject = Instantiate(DictionaryPrefab);
            DictionaryObject.transform.parent = transform;

            DictionaryObject.GetComponent<TileDictionary>().SetupDictionary();
            DictionaryObject.GetComponent<UnitDictionary>().SetupDictionary();
        }

        if (!MapBuilderObject)
        {
            MapBuilderObject = Instantiate(MapBuilderPrefab);
            MapBuilderObject.transform.parent = transform;

            builder = MapBuilderObject.GetComponent<MapBuilder>();
            builder.setMap = fileReader.getMapData;
            builder.setTileDictionary = DictionaryObject.GetComponent<TileDictionary>();
            builder.BuildMap();
        }

        dijkstra = new AI.Dijkstra(builder.getPath);

        if (!UnitManager)
        {
            UnitManager = Instantiate(UnitManagerPrefab);
            UnitManager.transform.parent = transform;

            UnitManager.GetComponent<UnitManager>().setUnitDictionary = DictionaryObject.GetComponent<UnitDictionary>();
            UnitManager.GetComponent<UnitManager>().setPath = (List<Vector2Int>)dijkstra.FindPath(builder.getStart, builder.getEnd);
            UnitManager.GetComponent<UnitManager>().RunWave(waveData[currentWave].x, waveData[currentWave].y);
        }

        if(!PlayerManager)
        {
            PlayerManager = Instantiate(PlayerManagerPrefab);
            PlayerManager.transform.parent = transform;
            PlayerManager.GetComponent<PlayerHealthListener>().SetupListener(HealthPanel.transform.Find("HealthText").gameObject.GetComponent<Text>());
            PlayerManager.GetComponent<PlayerBehaviour>().setupPlayer();
        }

    }

    public void GameOver()
    {
        gameIsOver = true;
        UnitManager.GetComponent<UnitManager>().GameOver();
        GameOverPanel.SetActive(true);
    }

    public void IncrementWave()
    {
        if(currentWave < waveData.Count && !gameIsOver)
        {
            currentWave++;
            UnitManager.GetComponent<UnitManager>().RunWave(waveData[currentWave].x, waveData[currentWave].y);
        }
    }

    public void DamagePlayer(int Damage) 
    {
        PlayerManager.GetComponent<PlayerBehaviour>().TakeDamage(Damage);
    }

    public bool isGameOver { get { return gameIsOver; } }

    public float getDisplacement {  get { return displacement; } }
}
