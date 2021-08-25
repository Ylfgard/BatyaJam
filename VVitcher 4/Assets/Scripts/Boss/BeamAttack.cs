using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamAttack : MonoBehaviour {
    public int beamDamage;
    public float selfDestroyTimer;

    [SerializeField] private GameObject _laserBeam;

    private float _beamRotationSpeed = 40f;
    private float _beamTravelDistance = 4f;
    private float _beamTravelSpeed = 1f;
    private float _laserMoveSpeed = 10f;
    private Vector3 _beamStartPositionOffset = new Vector3(2, 0, 0);
    private float _beamStartRotationOffset = 60f;
    private float _beamCurrentRotation;
    private GameObject _laserLeft;
    private GameObject _laserRight;
    private bool _canTravel = false;
    private Vector3 _startPos;

    void Start()
    {
        _startPos = transform.localPosition;

        _laserLeft = Instantiate(_laserBeam, transform) as GameObject;
        _laserRight = Instantiate(_laserBeam, transform) as GameObject;

        _laserRight.transform.position += _laserLeft.transform.rotation * _beamStartPositionOffset;
        _laserRight.transform.rotation *= Quaternion.Euler(0, _beamStartRotationOffset, 0);
        _laserLeft.transform.position -= _laserLeft.transform.rotation * _beamStartPositionOffset;
        _laserLeft.transform.rotation *= Quaternion.Euler(0, -_beamStartRotationOffset, 0);
    }

    void Update()
    {
        if (selfDestroyTimer > 0)
        {
            selfDestroyTimer -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }

        _beamCurrentRotation = _laserRight.transform.localRotation.y;
        if (_beamCurrentRotation > 0f)
        {
            _laserLeft.transform.rotation = Quaternion.RotateTowards(_laserLeft.transform.rotation, transform.rotation, _beamRotationSpeed * Time.deltaTime);
            _laserRight.transform.rotation = Quaternion.RotateTowards(_laserRight.transform.rotation, transform.rotation, _beamRotationSpeed * Time.deltaTime);
        }
        else _canTravel = true;
        if (_canTravel && _beamTravelDistance > 0)
        {
            Vector3 destination = _startPos + new Vector3(_beamTravelDistance, 0, 0);

            if (transform.localPosition == destination)
            {
                _beamTravelDistance = -_beamTravelDistance;
            }
            else
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, destination, _beamTravelSpeed * Time.deltaTime);
            }
        }
        if (_canTravel && _beamTravelDistance < 0)
        {
            Vector3 destination = _startPos + new Vector3(_beamTravelDistance, 0, 0);

            if (transform.localPosition == destination)
            {
                _beamTravelDistance = 0;
            }
            else
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, destination, _beamTravelSpeed * Time.deltaTime);
            }
        }
        if (_beamTravelDistance == 0)
        {
            _laserRight.transform.localPosition = Vector3.MoveTowards(_laserRight.transform.localPosition, _laserRight.transform.localPosition + new Vector3(1, 0, 0), _laserMoveSpeed * Time.deltaTime);
            _laserLeft.transform.localPosition = Vector3.MoveTowards(_laserLeft.transform.localPosition, _laserLeft.transform.localPosition + new Vector3(-1, 0, 0), _laserMoveSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerMain player = collision.gameObject.GetComponent<PlayerMain>();
        if (player == null) return;
        player.TakeDamage(beamDamage);
    }
}
