using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicController : MonoBehaviour
{
    private Vector2 pointerPosition;
    private Vector2 aimDirection;

    private bool canFireball = true;
    private float fireballCD = 5.0f;
    private float fireballCounter;
    
    private bool canTornado  = true;
    private float tornadoCD = 5.0f;
    private float tornadoCounter;
    
    private bool canRock = true;
    private float rockCD = 5.0f;
    private float rockCounter;
    
    private bool canSuck = true;
    private float suckCD = 5.0f;
    private float suckCounter; 

    [SerializeField]
    private GameObject fireballPrefab;
    [SerializeField]
    private GameObject tornadoPrefab;
    [SerializeField]
    private GameObject rockPrefab;
    [SerializeField]
    private GameObject suckPrefab;

    /*[SerializeField]
    private AudioSource rockSoundEffect;
    [SerializeField]
    private AudioSource tornadoSoundEffect;
    [SerializeField]
    private AudioSource fireballSoundEffect;*/

    [SerializeField] private AudioSource rockSoundEffect;
    [SerializeField] private AudioSource tornadoSoundEffect;
    [SerializeField] private AudioSource fireballSoundEffect;


    // Start is called before the first frame update
    void Start()
    {
        fireballCounter = 0.0f;
        tornadoCounter = 0.0f;
        rockCounter = 0.0f;
        suckCounter = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        aimDirection = (pointerPosition - (Vector2)transform.position).normalized;

        if (!canFireball)
        {
            fireballCounter += Time.deltaTime;
            if (fireballCounter >= fireballCD)
            {
                canFireball = true;
                fireballCounter = 0.0f;
            }
        }
        else if (!canTornado)
        {
            tornadoCounter += Time.deltaTime;
            if (tornadoCounter >= tornadoCD)
            {
                canTornado = true;
                tornadoCounter = 0.0f;
            }
        }
        else if (!canRock)
        {
            rockCounter += Time.deltaTime;
            if (rockCounter >= rockCD)
            {
                canRock = true;
                rockCounter = 0.0f;
            }
        }
        else if (!canSuck)
        {
            suckCounter += Time.deltaTime;
            if (suckCounter >= suckCD)
            {
                canSuck = true;
                suckCounter = 0.0f;
            }
        }
    }

    public void setPointerPosition(Vector2 pointerPosition)
    {
        this.pointerPosition = pointerPosition;
    }

    public void Execute(string MagicType)
    {
        switch (MagicType)
        {
            case "Fireball":
                this.Fireball();
                break;
            case "Tornado":
                this.Tornado();
                break;
            case "Rock":
                this.Rock();
                break;
            case "Suck":
                this.Suck();
                break;
        }
    }

    private void Fireball()
    {
        if (!canFireball)
        {
            return;
        }
        canFireball = false;

        GameObject fireball = Instantiate(fireballPrefab, transform.position, transform.rotation);
        fireball.transform.right = aimDirection;
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(aimDirection.x, aimDirection.y) * 5f;
        fireballSoundEffect.Play();
    }

    private void Tornado()
    {
        if (!canTornado)
        {
            return;
        }
        canTornado = false;

        Vector3 offset = transform.position + new Vector3(0f, 0.25f, 0f);
        GameObject tornado = Instantiate(tornadoPrefab, offset, transform.rotation);
        Rigidbody2D rb = tornado.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(aimDirection.x, aimDirection.y) * 7f;
        tornadoSoundEffect.Play();
    }

    private void Suck()
    {
        if (!canSuck)
        {
            return;
        }
        canSuck = false;
        GameObject suck = Instantiate(suckPrefab, pointerPosition, transform.rotation);
    }

    private void Rock()
    {
        if (!canRock)
        {
            return;
        }
        canRock = false;
        
        StartCoroutine(SpawnRocks());
        rockSoundEffect.Play();

    }

    private IEnumerator SpawnRocks()
    {
        for (int i = 0; i < 3; i++) 
        {
            GameObject rock = Instantiate(rockPrefab, transform.position, transform.rotation);
            rock.transform.right = aimDirection;
            Rigidbody2D rb = rock.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(aimDirection.x, aimDirection.y) * 2.5f;
            yield return new WaitForSeconds(0.5f);
        }
    }


}
