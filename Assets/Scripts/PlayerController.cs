using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    float rotateSpeed;
    [SerializeField] float rotateMin, rotateMax, rotateIncrease, rotateDecay, firepower;
    [SerializeField] AudioClip spitSound;
    int health = 5;
    public TextMeshProUGUI healthText;
    
    private Rigidbody2D PlayerRB;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerRB = GetComponent<Rigidbody2D>();
        
    }
    // Update is called once per frame
    void Update()
    {
        healthText.text = health.ToString();        

        if (health > 0)
        {
            if (rotateSpeed <= rotateMin)
            {
                rotateSpeed = rotateMin;
            }
            else
            {
                rotateSpeed -= rotateDecay;
            }
            if (rotateSpeed > rotateMax)
            {
                rotateSpeed = rotateMax;
            }
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
            {
                shoot();
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                this.transform.position = Vector2.zero;
            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                health++;
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                health = Mathf.FloorToInt(rotateSpeed/2);
            }

            //if(notWorking())
            //    {
            //    Work();
            //    }

            this.gameObject.transform.Rotate(new Vector3(0,0,rotateSpeed));
            Debug.Log(rotateSpeed);
        }
    }

    void shoot()
    {
        GameObject pooledProjectile = Pooler.SharedInstance.GetPooledObject();
        if (pooledProjectile != null)
        {
            rotateSpeed += rotateIncrease;
            pooledProjectile.transform.position = transform.position;
            pooledProjectile.transform.rotation = gameObject.transform.rotation;
            pooledProjectile.SetActive(true);

            PlayerRB.AddRelativeForce(transform.up * firepower, ForceMode2D.Impulse);

        }
    }
}