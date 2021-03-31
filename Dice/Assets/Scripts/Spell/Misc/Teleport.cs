using System.Collections;
using System.Collections.Generic;
using Button.Utils;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject teleportMesh;
    public float teleportTimer = 1;
    public float maxTeleportTimer = 1;
    public float cooldownTimer = 10;
    public float maxCooldownTimer = 10;
    bool secondPress = false;
    Vector3 meshNewPos;
    public float distance = 4;
    public GameObject particalPrefab;

    [SerializeField]
    private LayerMask layerMask;
    private GameSettings gameSettings;

    // Start is called before the first frame update
    void Start()
    {
        teleportMesh.SetActive(false);
        teleportMesh.transform.position = transform.position;
        gameSettings = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameSettings>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ButtonMapping.GetButton(gameSettings.controllerType, EButtonActions.Teleport) && cooldownTimer < 0)
        {
            teleportMesh.SetActive(true);
            if (!secondPress)
            {
                teleportMesh.transform.position = transform.position;
                teleportMesh.transform.position += transform.forward * distance;
                secondPress = true;
                teleportTimer = maxTeleportTimer;
            }
        }

        if (ButtonMapping.GetButton(gameSettings.controllerType, EButtonActions.Teleport) && teleportTimer < 0)
        {
            if (secondPress)
            {
                teleportMesh.SetActive(false);
                cooldownTimer = maxCooldownTimer;
                transform.position = teleportMesh.transform.position;
                teleportMesh.transform.position = transform.position;
                secondPress = false;
                GameObject teleport = Instantiate(particalPrefab, transform.position, transform.rotation) as GameObject;
                Destroy(teleport, 5.0f);
            }
        }

        if (secondPress)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward * distance, out hit, distance + 1, ~layerMask))
            {
                //Vector3 newPos = new Vector3(transform.position.x - (rayDist - hit.distance), transform.position.y, transform.position.z);
                //transform.position = newPos;
                teleportMesh.transform.position = transform.position;
                teleportMesh.transform.position += teleportMesh.transform.forward * (hit.distance - 1);
            }
            else
            {
                teleportMesh.transform.position = transform.position;
                teleportMesh.transform.position += teleportMesh.transform.forward * distance;
            }


        }


        cooldownTimer -= Time.deltaTime;
        teleportTimer -= Time.deltaTime;
    }
}
