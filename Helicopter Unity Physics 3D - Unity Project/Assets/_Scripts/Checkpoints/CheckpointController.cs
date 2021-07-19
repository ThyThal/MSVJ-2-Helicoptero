using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    [SerializeField] private List<Checkpoint> _checkpoints = new List<Checkpoint>();
    private int currentCheckpoint = 0;
    private AudioSource _source;

    public event Action onAllCheckpointsReached;

    static private CheckpointController instance;
    public static CheckpointController Instance { get => instance; }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        _source = GetComponent<AudioSource>();

        for (int i = 0; i < _checkpoints.Count; i++)
        {
            _checkpoints[i].Position = i;
        }
    }
    
    public void ActivateCheckpoint(int position)
    {
        if (position == currentCheckpoint)
        {
            _checkpoints[currentCheckpoint].Checked = true;
            _checkpoints[currentCheckpoint].PlayAnimation();
            currentCheckpoint++;
            _source.Play();
        }

        if (currentCheckpoint == _checkpoints.Count)
            onAllCheckpointsReached?.Invoke();
    }
}
