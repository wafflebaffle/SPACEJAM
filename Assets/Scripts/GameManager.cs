using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{

    private int _points = 0;
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject addedPointsPrefab;
    [SerializeField] private GameObject trashPrefab;
    [SerializeField] private GameObject missilePrefab;
    private GameState _gameState = GameState.Paused;
    private List<Vector3> _trashSpawnPoints;
    private List<Vector3> _missileSpawnPoints;

    private TMP_Text _pointsTMP;
    private Transform _lostPanel;
    private GameObject _playerObject;
    private AudioSource _backgroundAudioSource;

    private IEnumerator _spawnTrash;
    private IEnumerator _spawnMissile;

    private AudioResource _thrashAudio;
    private AudioResource _missileAudio;
    
    void Start()
    {
        _pointsTMP = canvas.transform.Find("PointsText").GetComponent<TMP_Text>();
        _lostPanel = canvas.transform.Find("LostPanel");
        _lostPanel.gameObject.SetActive(false);
        _playerObject = GameObject.Find("Player");
        _backgroundAudioSource = GameObject.Find("SoundManager").GetComponent<AudioSource>();

        _thrashAudio = Resources.Load<AudioResource>("collect_trash");
        _missileAudio = Resources.Load<AudioResource>("missile_explosion");
        
        _trashSpawnPoints = new List<Vector3>();
        GameObject trashSPs = GameObject.Find("SpawnPoints_Trash");
        foreach (Transform trans in trashSPs.transform)
        {
            _trashSpawnPoints.Add(trans.position);
        }

        _missileSpawnPoints = new List<Vector3>();
        GameObject missileSPs = GameObject.Find("SpawnPoints_Missile");
        foreach (Transform trans in missileSPs.transform)
        {
            _missileSpawnPoints.Add(trans.position);
        }
        Destroy(trashSPs);
        
        UpdatePointsText();
        
        //InvokeRepeating(nameof(AddP), 3.0f, 3.0f);
        _spawnTrash = SpawnTrashCo(3f);
        _spawnMissile = SpawnMissileCo(10f);
        //InvokeRepeating(nameof(SpawnTrash), 3.0f, 3.0f);
        //InvokeRepeating(nameof(SpawnMissile), 5.0f, 10.0f);
        _gameState = GameState.Playing;
        StartCoroutine(_spawnTrash);
        StartCoroutine(_spawnMissile);
    }

    public void MissileTouchedAsteroid()
    {
        AudioSource newAudio = _backgroundAudioSource.gameObject.AddComponent<AudioSource>();
        newAudio.resource = _missileAudio;
        newAudio.Play();
        StartCoroutine(DeleteAudioSource(newAudio.clip.length, newAudio));
    }

    private IEnumerator DeleteAudioSource(float time, AudioSource source)
    {
        yield return new WaitForSeconds(time);
        Debug.Log($"waited {time}");
        Destroy(source);
    }
    
    public IEnumerator SpawnTrashCo(float time)
    {
        while (true)
        {
            if (_gameState == GameState.Paused) break;
            SpawnTrash();
            yield return new WaitForSeconds(time);
        }
    }

    public IEnumerator SpawnMissileCo(float time)
    {
        while (true)
        {
            if (_gameState == GameState.Paused) break;
            SpawnMissile();
            yield return new WaitForSeconds(time);
        }
    }

    private void Died()
    {
        _backgroundAudioSource.Stop();
        _lostPanel.gameObject.SetActive(true);
        SetGameState(GameState.Paused);
        foreach (GameObject trash in GameObject.FindGameObjectsWithTag("Trash"))
        {
            trash.GetComponent<TrashManager>().Pause();
        }
        StopCoroutine(_spawnTrash);
        StopCoroutine(_spawnMissile);
    }
    
    public void SpawnMissile()
    {
        GameObject missile = Instantiate(missilePrefab);
        Vector3 startPos = _missileSpawnPoints[Random.Range(0, _missileSpawnPoints.Count)];
        missile.transform.position = startPos;
        missile.GetComponent<MissileManager>().Setup(_playerObject, this);
    }

    public void SpawnTrash()
    {
        GameObject trash = Instantiate(trashPrefab);
        Vector3 startPos = _trashSpawnPoints[Random.Range(0, _trashSpawnPoints.Count)];
        trash.GetComponent<TrashManager>().Setup(startPos);
    }
    
    public void AddP() => AddPoints(1);

    public void AddPoints(int points)
    {
        _points += points;
        UpdatePointsText();
        Instantiate(addedPointsPrefab, _pointsTMP.transform).GetComponent<TMP_Text>().text = $"+ {points}";
    }

    public void CatchTrash()
    {
        AddPoints(50);
        AudioSource newAudio = _backgroundAudioSource.gameObject.AddComponent<AudioSource>();
        newAudio.resource = _thrashAudio;
        newAudio.Play();
    }
    
    private void UpdatePointsText()
    {
        _pointsTMP.text = $"Points: <#5de381>{FormattableString.Invariant($"{_points:N0}")}";
    }

    public enum GameState
    {
        Playing, Paused
    }
    public GameState GetGameState() => _gameState;
    public void SetGameState(GameState gameState) => _gameState = gameState;

    public void Collided()
    {
        SetGameState(GameState.Paused);
        Died();
        // add death screen or check for lives
    }
}
