using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ReloadBonus : MonoBehaviour
{
    public float minSpeed, maxSpeed;
    public GameObject BonusExpl;
    
    void Start()
    {
        var RelBonus = GetComponent<Rigidbody>();
        var speed = Random.Range(minSpeed, maxSpeed);
        RelBonus.velocity = new Vector3(0, 0, -speed);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Asteroid" || other.tag == "GameBoundary" || other.tag == "RelBonus" || other.tag == "Lazer")
        {
            return;
        }
        if (other.tag == "Player" || other.tag == "SpaceFighter2" || other.tag == "SpaceFighter3")
        {
            Instantiate(BonusExpl, transform.position, Quaternion.identity);

            if (GameController.instance.difficulty == 0)
            { if (PlayerScript.shotDelay > 0.4f)
                {
                    PlayerScript.shotDelay -= 0.04f;
                } else if (PlayerScript.shotDelay < 0.4f && PlayerScript.shotDelay > 0.2f)
                {
                    PlayerScript.shotDelay -= 0.01f;
                } else if (PlayerScript.shotDelay < 0.2f)
                {
                    PlayerScript.shotDelay -= 0;
                }
            } else if (GameController.instance.difficulty == 1)
            {
                if (PlayerScript.shotDelay > 0.4f)
                {
                    PlayerScript.shotDelay -= 0.01f;
                }
                else if (PlayerScript.shotDelay < 0.4f && PlayerScript.shotDelay > 0.2f)
                {
                    PlayerScript.shotDelay -= 0.005f;
                }
                else if (PlayerScript.shotDelay < 0.2f)
                {
                    PlayerScript.shotDelay -= 0;
                }
            } else if (GameController.instance.difficulty == 2)
            {
                if (PlayerScript.shotDelay > 0.4f)
                {
                    PlayerScript.shotDelay -= 0.005f;
                }
                else if (PlayerScript.shotDelay < 0.4f && PlayerScript.shotDelay > 0.2f)
                {
                    PlayerScript.shotDelay -= 0.001f;
                }
                else if (PlayerScript.shotDelay < 0.2f)
                {
                    PlayerScript.shotDelay -= 0;
                }
            }

            Destroy(gameObject);
        }

    }
}
