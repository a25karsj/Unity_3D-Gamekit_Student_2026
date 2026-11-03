using Gamekit3D;
using UnityEngine;

public class SurvivalMod : MonoBehaviour
{
    [SerializeField] private float thirstLevel = 100f;
    [SerializeField] private float thirstSpeed;
    [SerializeField] private float timer = 10f;

    [SerializeField] private bool thirsty;
    [SerializeField] private bool parched;

    [SerializeField] private GameObject playerObject;
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       playerObject = GameObject.FindWithTag("Player"); 
    }

    // Update is called once per frame
    void Update()
    {
        //Subtracting the current value of the thirstLevel with thirstSpeed. thirstSpeed is normalized
        //using Time.deltaTime, so that it's frame rate independent. Math.Clamp restricts the thirstLevel
        //between its current max value and 0.
        thirstLevel -= Mathf.Clamp(value: thirstSpeed*Time.deltaTime, min: 0, max: thirstLevel);
        
       //if statement checks if thirstLevel is equal to, or lower, than 50 AND if thirsty = false
        if (thirstLevel <= 50f && thirsty == false)
        {
            Debug.Log(message:"Getting Thirsty...");
            thirsty = true;
        }
        
        //if statement check if thirstLevel is equal to, or lower, than 50 and if thirsty)
        if (thirstLevel <= 0 && thirsty)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                
                Damageable.DamageMessage msg = new Damageable.DamageMessage()
                {
                    amount = 1,
                    damager = this,
                    damageSource = new Vector3(x: 0, y: 0, z: 0),
                    direction = new Vector3(x: 0, y: 0, z: 0),
                    throwing = false,
                    stopCamera = false,
                };
                playerObject.GetComponent<Damageable>().ApplyDamage(msg);
                Debug.Log(message: "Damage by Thirst");
                timer = 10f;
            }
        }
    }
}
