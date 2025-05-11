using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    #region CONFIGURATION VARIABLES
    [Header("Player Configuration")]
    [SerializeField] private float xPadding = 1f;
    [SerializeField] private float yPadding = 1f;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float projectileWait = 0.25f;
    [SerializeField] private int health = 5;
    [SerializeField] private UIManager manageUI;
    [SerializeField] private float beamSpeed = 10f;
    [SerializeField] private float beamWait = 0.01f;
    [SerializeField] private int weaponDamage = 1;
    [SerializeField] private float moveSpeed; //Base move speed
    [SerializeField] private int deathTextScene = 5;


    [Header("Player Components")]
    [SerializeField] private GameObject defaultWeaponPrefab;
    [SerializeField] private GameObject beamPrefab;
    [SerializeField] private GameObject laserProjectilePrefab;
    [SerializeField] private GameObject rocketProjectilePrefab;
    [SerializeField] private Transform firePoint;
    private Animator mainAnimator;

    [Header("Player Appearance")]
    [SerializeField] private PlayerAppearance[] appearances;
    [SerializeField] private GameObject[] wingPairs; //[level][left/right]
    [SerializeField] private Sprite[] wingStateSprites; // For animation clusters
    private PlayerAppearance currentAppearance;

    [Header("Audio")]
    [SerializeField] private AudioClip beamSFX;
    [SerializeField] private AudioClip damageSFX;
    [SerializeField] private AudioClip firingSFX;
    [SerializeField] private AudioClip deathSFX;
    [SerializeField] private GameObject deathVFX;
    [SerializeField] private float explosionDuration = 1f;

    [Header("Speed Settings")]
    [SerializeField] private float speedIncrease;
    [SerializeField] private int playerSpeedLevel = 0;



    private int maxSpeedLevel = 3;
    private float currentSpeed;
    private int speedLevel = 0;
    private const string SpeedLevelKey = "PlayerSpeedLevel";

    private float deathTime;
    private SpriteRenderer spriteRenderer;
    private Weapons currentWeapon;
    private Coroutine firingCoroutine;
    private Vector3 currentPosition;
    private float xMin, xMax, yMin, yMax;
    private List<Upgrades> upgrades = new List<Upgrades>();
    #endregion

    #region UNITY LIFECYCLE METHODS

    void Start()
    {
        int savedAppearance = PlayerPrefs.GetInt("PlayerColorVariant", 0);
        ApplyAppearance(savedAppearance);
        if(currentWeapon == null)
        {
            currentWeapon = new DefaultWeapon(defaultWeaponPrefab, firePoint, projectileSpeed);
        }
        SetUpMoveBoundaries();
        LoadSpeedLevel();
        UpdatePlayerSpeed();
        UpdateWingPairs();
        UpdateAnimClipTimes();
    }

    void Update()
    {
        Move();
        Fire();
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        mainAnimator = GetComponent<Animator>();
        LoadPlayerAppearance();
    }
    #endregion

    #region MOVEMENT LOGIC
    //Boundaries for player character movement
    private void SetUpMoveBoundaries()
    {
        //selects the main camera for the game
        Camera gameCamera = Camera.main;
        xMin = -39 + xPadding;
        xMax = 39 - xPadding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + yPadding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - yPadding;
    }
    private void Move()
    {
        Vector2 inputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if(inputVector.magnitude > 1f)
        {
            inputVector = inputVector.normalized;
        }

        //DeltaX and DeltaY are set so they keep the movements relative to the player view on the camera, rather than the system's view of X and Y placement
        var deltaX = inputVector.x * Time.deltaTime * moveSpeed;
        var deltaY = inputVector.y * Time.deltaTime * moveSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);
        currentPosition = transform.position;
    }

    #endregion

    #region COMBAT LOGIC
    private void Fire()
    {
        if (Input.GetButtonDown("Fire") && firingCoroutine == null)
        {
            if(currentWeapon != null)
            {
                firingCoroutine = StartCoroutine(FireWeapon());
            }
            else
            {
                currentWeapon = new DefaultWeapon(defaultWeaponPrefab, firePoint, projectileSpeed);
                firingCoroutine = StartCoroutine(FireWeapon());
            }
        }
        if (Input.GetButtonUp("Fire") && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    private IEnumerator FireWeapon()
    {
        while (true)
        {
            currentWeapon.Fire(transform, firingSFX);
            yield return new WaitForSeconds(currentWeapon.fireRate); //Delay between shots

            if (firingCoroutine == null) break;
        }
    }

    private void ChangeWeapon(WeaponFlag weaponFlag)
    {
        switch (weaponFlag)
        {
            case WeaponFlag.Beam:
                currentWeapon = new LaserWeapon(laserProjectilePrefab);
                break;
            case WeaponFlag.Rocket:
                //currentWeapon = new RocketWeapon(rocketProjectilePrefab);
                break;
        }
    }
    #endregion

    #region APPERANCE

    private void LoadPlayerAppearance()
    {
        int savedAppearance = PlayerPrefs.GetInt("PlayerColorVariant", 0);
        ApplyAppearance(savedAppearance);
    }

    public void ApplyAppearance(int appearanceIndex)
    {
        if (appearanceIndex >= 0 && appearanceIndex < appearances.Length)
        {
            currentAppearance = appearances[appearanceIndex];

            spriteRenderer.sprite = currentAppearance.bodySprites[0];
            mainAnimator.runtimeAnimatorController = currentAppearance.animationController;

            LoadWingStates(currentAppearance.wingSprites);

            Instantiate(currentAppearance.engineEffect, transform.position, Quaternion.identity, transform);
            Instantiate(currentAppearance.dashEffect, transform.position, Quaternion.identity, transform);
        }
        else
        {
            Debug.LogWarning("Invalid appearance index");
        }
    }

    private void LoadWingStates(Sprite wingSpriteSheet)
    {
        int frameWidth = 642;
        int frameHeight = 642;

        int stateCount = 3;
        int framesPerState = wingSpriteSheet.texture.width / frameWidth;

        //wingStateSprites = new Sprite[stateCount][];

        for (int i = 0; i < framesPerState; i++)
        {
            /*wingStateSprites[i] = new Sprite[framesPerState];
            for (int j = 0; j < framesPerState; j++)
            {
                Rect frameRect = new Rect(j * frameWidth, i * frameHeight, frameWidth, frameHeight);
                wingStateSprites[i][j] = Sprite.Create(wingSpriteSheet.texture, frameRect, new Vector2(0.5f, 0.5f));
            }
            */
        }
    }

    public void UpdateWingPairs()
    {
        for (int i = 0; i < wingPairs.Length; i++)
        {
            bool shouldEnable = i < playerSpeedLevel;
            wingPairs[i].SetActive(shouldEnable);
        }
    }

    #endregion

    #region SPEED/UPGRADE LOGIC

    private void LoadSpeedLevel()
    {
        speedLevel = PlayerPrefs.GetInt(SpeedLevelKey, 0);
    }

    public void SaveSpeedLevel()
    {
        PlayerPrefs.SetInt(SpeedLevelKey, speedLevel);
        PlayerPrefs.Save();

    }

    private void UpdatePlayerSpeed()
    {
        currentSpeed = moveSpeed + (speedLevel * speedIncrease);
    }

    public void AddSpeedUpgrade()
    {
        if ( (speedLevel < maxSpeedLevel))
        {
            speedLevel++;
            SaveSpeedLevel();
            UpdatePlayerSpeed();
            UpdateWingPairs();
        }
    }

    public void ResetSpeedLevel()
    {
        speedLevel = 0;
        SaveSpeedLevel();
        UpdatePlayerSpeed();
        UpdateWingPairs();
    }
    public void ApplyUpgrade(Upgrades upgrade)
    {
        switch (upgrade.upgradeType)
        {
            case UpgradeFlag.ExtraLife:
                SetHealth(GetHealth() + 1);
                break;
            case UpgradeFlag.ExtraDamage:
                SetWeaponDamage(GetWeaponDamage() + 1);
                break;
            case UpgradeFlag.PlayerShield:
                break;
            case UpgradeFlag.WeaponChange:
                ChangeWeapon(upgrade.weaponType);
                break;
        }
    }
    #endregion

    #region DAMAGE/HEALTH LOGIC
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy Projectile")
        {
            DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
            Debug.Log("Player Damaged");
            ProcessHit(damageDealer);
        }
        if (other.tag == "Enemy")
        {
            health -= 1;
            manageUI.TookDamageUI();
            manageUI.UpdateUI();
            Debug.Log("damage Processed: Collission");
            if (health < 1)
            {
                Debug.Log("Player Killed");
                StartCoroutine(Kill());
            }
            PlaySFX(damageSFX);
        }
        if (other.tag == "Upgrade")
        {
            Upgrades upgrade = other.gameObject.GetComponent<Upgrades>();
            upgrade.AddUpgrade(this.gameObject);
            Destroy(other);
        }
    }

    private void ProcessHit(DamageDealer damage)
    {
        health -= damage.getDamage();
        manageUI.TookDamageUI();
        manageUI.UpdateUI();
        Destroy(damage.gameObject);
        Debug.Log("damage Processed");
        if (health < 1)
        {
            Debug.Log("Player Killed");
            StartCoroutine(Kill());
        }
        PlaySFX(damageSFX);
    }

    public IEnumerator Kill()
    {
        this.enabled = false; // lock player controls
        if (mainAnimator != null) PlayDeathAnimation();
        yield return new WaitForSeconds(deathTime);
        //Destroy(this.gameObject);
        yield return new WaitForSeconds(5f);
        //A check will be implemented to see if the player has more available "lives". If so, enable controls again, load in a new player, game continues.
        Debug.Log("Loading next scene");
        SceneManager.LoadScene(deathTextScene);
        Debug.Log("Scene Loaded. Did it load?");
    }
    #endregion

    #region GETTER/SETTERS

    public void SetSpeed(float speed)
    {
        moveSpeed = moveSpeed + speed;
    }

    public float GetSpeed()
    {
        return moveSpeed;
    }

    public void SetHealth(int increase)
    {
        health= health+increase;
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetWeaponDamage()
    {
        return weaponDamage;
    }

    public void SetWeaponDamage(int value)
    {
        weaponDamage = value;
    }

    public Vector3 getPosition()
    {
        return currentPosition;
    }

    public Weapons getPlayerWeapon()
    {
        return currentWeapon;
    }

    public void setPlayerWeapon(Weapons newWeapon)
    {
        currentWeapon = newWeapon;
    }

    public WeaponFlag setInitialWeapon()
    {
        return WeaponFlag.Default;
    }

    #endregion

    #region Utilities
    private void PlaySFX(AudioClip clip)
    {
        float volume = PlayerPrefs.GetFloat(PlayerPrefsKeys.SFXVolume, 0.7f);
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, volume);
    }

    public void PlayDeathAnimation()
    {
        if (mainAnimator != null)
        {
            mainAnimator.SetBool("isDead", true);
        }
    }

    public void UpdateAnimClipTimes()
    {
        AnimationClip[] clips = mainAnimator.runtimeAnimatorController.animationClips;
        foreach(AnimationClip clip in clips)
        {
            switch(clip.name)
            {
                case "PlayerDeathBlue":
                    deathTime = clip.length;
                    Debug.Log($"Time for Clip is: {deathTime}");
                    break;
            }
        }
    }
    #endregion
}
