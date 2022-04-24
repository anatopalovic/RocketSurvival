using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    // PARAMETERS
    // CACHE
    // STATE
    public CoalisionHandler ch;

    [SerializeField] AudioClip mainEngine;
    [SerializeField] float mainThurst = 1000;
    [SerializeField] float secondaryThrust = 100;


    [SerializeField] ParticleSystem particalsMainBooster;
    [SerializeField] ParticleSystem particalsLeftBooster;
    [SerializeField] ParticleSystem particalsRightBooster;

    Rigidbody rb;
    AudioSource audioS;

    bool coalisionEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioS = GetComponent<AudioSource>();
        ch = GetComponent<CoalisionHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
        ProcessCheats();
    }

    void ProcessCheats()
    {
        if (Input.GetKey(KeyCode.L))
        {
            LoadNextLevel();
        }
        if (Input.GetKey(KeyCode.C))
        {
            DisableCollisions();
        }
    }

    void LoadNextLevel()
    {
       ch.StartNextLevelSequence();
    }

    void DisableCollisions()
    {
        coalisionEnabled = !coalisionEnabled;
        ch.enabled = coalisionEnabled;
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    private void StopThrusting()
    {
        audioS.Stop();
        particalsMainBooster.Stop();
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThurst * Time.deltaTime);

        if (!audioS.isPlaying)
            audioS.PlayOneShot(mainEngine);

        if (!particalsMainBooster.isPlaying)
            particalsMainBooster.Play();
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            MoveLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            MoveRight();
        }
        else
        {
            StopMoving();
        }
    }

    private void StopMoving()
    {
        particalsLeftBooster.Stop();
        particalsRightBooster.Stop();
    }

    private void MoveRight()
    {
        if (!particalsLeftBooster.isPlaying)
            particalsLeftBooster.Play();
        rb.freezeRotation = true;
        transform.Rotate(Vector3.back * secondaryThrust * Time.deltaTime);
        rb.freezeRotation = false;
    }

    private void MoveLeft()
    {
        if (!particalsRightBooster.isPlaying)
            particalsRightBooster.Play();
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * secondaryThrust * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
