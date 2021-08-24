using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamAttack : MonoBehaviour {
    [SerializeField] private GameObject _laserBeam;

    private Vector3 _beamStartPositionOffset = new Vector3(1, 0, 0);
    private float _beamStartRotationOffset = 40f;

    void Start()
    {
        Instantiate(_laserBeam, transform.position + transform.rotation * _beamStartPositionOffset, transform.rotation * Quaternion.Euler(0, _beamStartRotationOffset, 0));
        Instantiate(_laserBeam, transform.position - transform.rotation * _beamStartPositionOffset, transform.rotation * Quaternion.Euler(0, -_beamStartRotationOffset, 0));
    }

    void Update()
    {

    }
}
