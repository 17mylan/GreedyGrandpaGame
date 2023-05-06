using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EZCameraShake;
using UnityEngine.SceneManagement;
using TMPro;


public class PlayerShooting : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject BulletPrefab;
    public GameObject wheel1, wheel2;
    public GameObject GrenadePrefab;
    public GameObject GameOver;
    public GameObject PlayerUI;
    [Header("Transforms")]
    public Transform GrenadeSpawn;
    public Transform BulletSpawn, BulletSpawn2;
    public Transform outMarchandPosition;
    [Header("Floats Variables")]
    public float playerHealth = 100f;
    public float playerMaxHealth = 100f;
    public float BackwardsRecoil = 1f;
    public float BulletSpeed = 50f;
    public float TimeBetweenShots = 0.3333f;
    private float m_timeStamp = 0f;
    public float DestroyBulletAfterSeconds = 2f;
    public float MaxHeat = 100f;
    public float AddHeat = 2f;
    public float CooldownHeat = 2f;
    public float WeaponHeat = 0f;
    public float walkSoundDelay, walkSoundTimestamp;
    public float grenadeDelay = 2.5f;
    public float GrenadeSpeed = 20f;
    [Header("Integer Variables")]
    public int grenadeNumber = 3;
    [Header("Booleans Variables")]
    public bool isMaxHeat = false;
    public bool isWalking;
    public bool cooldownGrenade = false;
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip shootSong, walkSong, deathSong;
    [Header("Other")]
    public ParticleSystem fire1, fire2;
    public UI_Overheating overheatingUI;
    public Rigidbody playerRigidbody;
    public float maxHeightDifference = 0.1f;
    private GameManager gameManager;
    private UI_ShopMenu uI_ShopMenu;
    public GameObject shopUI;
    public TextMeshProUGUI grenadeCounterText;
    
    void Start()
    {
        gameManager = GameManager.instance;
        GameManager.instance.UpdateMoneyHUD();
        uI_ShopMenu = FindObjectOfType<UI_ShopMenu>();
        GameOver.SetActive(false);
        PlayerUI.SetActive(true);
        grenadeCounterText.text = grenadeNumber.ToString();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            if (grenadeNumber >= 1)
            {
                ThrowGrenade();
            }
        }
        //TextWeaponHeat.text = WeaponHeat.ToString();
        if (GetComponent<Rigidbody>().velocity.magnitude < 0.1f)
        {
            wheel1.SetActive(false);
            wheel2.SetActive(false);
        }
        else
        {
            wheel1.SetActive(true);
            wheel2.SetActive(true);
        }
        if (GetComponent<Rigidbody>().velocity.magnitude > 3f && Time.time > walkSoundTimestamp)
        {
            audioSource.PlayOneShot(walkSong);
            walkSoundTimestamp = Time.time + walkSoundDelay;
        }
        if(isMaxHeat)
        {
            fire1.Stop();
            fire2.Stop();
        }
    }
    void ThrowGrenade()
    {
        if (!cooldownGrenade)
        {
            if(grenadeNumber >= 1)
            {
                grenadeNumber--;
                grenadeCounterText.text = grenadeNumber.ToString();
                var grenade = (GameObject)Instantiate(GrenadePrefab, GrenadeSpawn.position, GrenadeSpawn.rotation);
                grenade.GetComponent<Rigidbody>().velocity = grenade.transform.forward * GrenadeSpeed;
                StartCoroutine(CooldownGrenade());
            }

        }
    }
    IEnumerator CooldownGrenade()
    {
        cooldownGrenade = true;
        yield return new WaitForSeconds(grenadeDelay);
        cooldownGrenade = false;
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("MarchandDetector"))
        {
            shopUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    public void leftMarchandMenu()
    {
        gameObject.transform.position = outMarchandPosition.position;
        GameManager.instance.UpdateMoneyHUD();
    }
    public void TakePlayerDamage(float damagePoints)
    {
        playerHealth -= damagePoints;
        if (playerHealth <= 0)
        {
            audioSource.PlayOneShot(deathSong);
            SceneManager.LoadScene("GameOver");
        }
    }

    void FixedUpdate()
    {
        if ((Time.time >= m_timeStamp) && (Input.GetKey(KeyCode.Mouse0)) && !isMaxHeat)
        {
            Fire();
            m_timeStamp = Time.time + TimeBetweenShots;
            fire1.Play();
            fire2.Play();
        }
        else if (!Input.GetKey(KeyCode.Mouse0))
        {
            fire1.Stop();
            fire2.Stop();
            if (isMaxHeat)
            {
                WeaponHeat = Mathf.Max(WeaponHeat - CooldownHeat * Time.fixedDeltaTime, 0f);
                if (overheatingUI != null)
                {
                    overheatingUI.UpdateWarningBar(WeaponHeat, MaxHeat);
                }
                if (WeaponHeat <= 50f)
                {
                    isMaxHeat = false;
                    //print("Tu peux tirer de nouveau");
                }
            }
            else
            {
                WeaponHeat = Mathf.Max(WeaponHeat - CooldownHeat * Time.fixedDeltaTime, 0f);
                if (overheatingUI != null)
                {
                    overheatingUI.UpdateWarningBar(WeaponHeat, MaxHeat);
                }
                isMaxHeat = false;
            }
        }
        if (playerRigidbody.velocity.y < 0)
        {
            // Obtenir la position actuelle du joueur
            Vector3 playerPosition = playerRigidbody.position;

            // Lancer un rayon vers le bas pour détecter le sol
            RaycastHit hit;
            if (Physics.Raycast(playerPosition, Vector3.down, out hit))
            {
                // Calculer la différence de hauteur entre le joueur et le sol
                float heightDifference = hit.distance - 0.5f; // La moitié de la hauteur du joueur

                // Vérifier si la différence de hauteur est inférieure à la limite autorisée
                if (heightDifference <= maxHeightDifference)
                {
                    // Désactiver temporairement la contrainte de position Y
                    playerRigidbody.constraints &= ~RigidbodyConstraints.FreezePositionY;
                }
                else
                {
                    // Réactiver la contrainte de position Y
                    playerRigidbody.constraints |= RigidbodyConstraints.FreezePositionY;
                }
            }
        }
    }

    void Fire()
    {

        var bullet = (GameObject)Instantiate(BulletPrefab, BulletSpawn.position, BulletSpawn.rotation);
        var bullet2 = (GameObject)Instantiate(BulletPrefab, BulletSpawn2.position, BulletSpawn.rotation);

        CameraShaker.Instance.ShakeOnce(1.75f, 1.5f, .1f, 1f);

        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * BulletSpeed;
        bullet2.GetComponent<Rigidbody>().velocity = bullet2.transform.forward * BulletSpeed;

        audioSource.PlayOneShot(shootSong);

        WeaponHeat = WeaponHeat + AddHeat;
        if (overheatingUI != null)
        {
            overheatingUI.UpdateWarningBar(WeaponHeat, MaxHeat);
        }

        if (WeaponHeat >= MaxHeat)
        {
            isMaxHeat = true;
            fire1.Stop();
            fire2.Stop();
        }

        GetComponent<Rigidbody>().AddForce(-transform.forward * BackwardsRecoil, ForceMode.Impulse);

        Destroy(bullet, DestroyBulletAfterSeconds);
        Destroy(bullet2, DestroyBulletAfterSeconds);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}