using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public string tagToCheck = "Player";
    private bool _checked = false;
    private int _position;

    private Animator _anim;
    private MeshRenderer _rend;

    public bool Checked { get => _checked; set => _checked = value; }
    public int Position { get => _position; set => _position = value; }

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _rend = GetComponentInChildren<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagToCheck) && !_checked)
            CheckpointController.Instance.ActivateCheckpoint(_position);
    }
    
    public void PlayAnimation()
    {
        _anim.Play("CheckpointGet");
        LeanTween.value(1, 0, 1).setOnUpdate((float val) => { _rend.material.SetFloat("_BlendAlpha", val); });
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
