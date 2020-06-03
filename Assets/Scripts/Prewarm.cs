using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prewarm : MonoBehaviour
{
    public GameObject[] PrewarmObject;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < PrewarmObject.Length; i++)
        {
            AudioSource audioSource = new AudioSource();
            GameObject g = Instantiate(PrewarmObject[i], transform.position, transform.rotation);
            if (g.TryGetComponent<AudioSource>(out audioSource))
            {
                {
                    audioSource.volume = 0;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
