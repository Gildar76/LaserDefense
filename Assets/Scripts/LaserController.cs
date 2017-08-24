using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    public LineRenderer lr;
    public Transform firingPosition;
    public Light beamLight;
    public float fadeSpeed = 3;
    public float length = 100;

    private Color beamColor;
    private float lightIntencity;
    private bool fireLeftRight;

    private AudioSource fireSound;
    private PlayerController playerController;
    private AudioSource[] audios;

    private void Start()
    {
        audios = GetComponents<AudioSource>();

        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        beamLight = GetComponentInChildren<Light>();
        lr = GetComponent<LineRenderer>();
        fireSound = GetComponent<AudioSource>();
        beamColor = lr.material.GetColor("_TintColor");
        lightIntencity = beamLight.intensity;



    }


    private void Update()
    {

        lr.material.SetColor("_TintColor", Color.Lerp(lr.material.GetColor("_TintColor"), Color.black, Time.deltaTime * fadeSpeed));
        beamLight.intensity = Mathf.Lerp(beamLight.intensity, 0.0f, fadeSpeed * Time.deltaTime);


    }

    public bool Fire()
    {

        RaycastHit rayHit;

        Ray ray = new Ray(transform.position, transform.up);
        if (Physics.Raycast(ray, out rayHit))
        {
            audios[1].Play();
            audios[0].Play();
            GameObject.Instantiate(SpawnManager.instance.explosion, rayHit.point, transform.rotation);
            rayHit.collider.gameObject.SetActive(false);
            rayHit.collider.gameObject.transform.position = new Vector3(0, 30.0f, 0f);
            GameManager.instance.ChangePower(2.0f);
            GameManager.instance.AddScore(10);
            lr.SetPosition(1, new Vector3(0, rayHit.distance / 1, 0 ));
            lr.material.SetColor("_TintColor", beamColor);
            beamLight.intensity = lightIntencity;
            return true;
        }

        //fireSound.Play();

        return false;

    }

}
