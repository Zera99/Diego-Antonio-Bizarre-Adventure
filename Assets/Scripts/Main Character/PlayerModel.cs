using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerModel : MonoBehaviour
{
    
    Rigidbody2D _rb;
    //UpdateUI _ui;
    UpdateMiniUI _miniUI; // Cuando sepamos cual queda, se borra el otro y listo.
    PlayerController _control;

    PhysicsCalculation _physics;
    float yVelocity;

    Vector3 _checkpointPosition;

    int _currentJumps;
    bool _isInAir;
    bool _isRunning;
    bool _isFlipped;
    bool _isImmune;
    bool _isDying;
    bool _isOnCrusher;
    
    float _currentSpeed;

    public LayerMask stopRunningLayer;

    public LatinLoverStats stats;
    public Skin normalSkin, bowserSkin;
    public Transform eggSpawnPoint;
    public GameObject bowserFirePrefab;
    public Transform fireSpawnPoint;
    public Transform fusRohSpawnPoint;
    public Transform wallCheck;

    public Transform effectSpawnPoint;
    public GameObject changeSkinEffect;

    Dictionary<Skin, Func<float>> SkillsAndValues = new Dictionary<Skin, Func<float>>();
    public float currentPointsFRC, currentPointsJetPack, currentPointsJojo;
    public float recoverPointsSpeed;


    List<Skin> _mySkins = new List<Skin>();
    Skin _currentSkin;

    [Header("UpDown Movement Settings")]
    bool _canMoveDown, _canMoveUp;

    [Header("Ladder Settings")]
    public LayerMask topLadderLayer;
    public LayerMask downLadderLayer;
    GameObject _switchLadderPlatform;
    bool _onLadderZone;

    [Header("Ground Detection")]
    public Vector3 offsetPosLadderPlatform;
    public Vector2 boxSize;
    public LayerMask groundLayers;
    public Vector2 boxSizeCheck;

    bool _isControllerManipulated;

    public event Action<RuntimeAnimatorController> onChangeSkin = delegate { };
    public event Action onUpdate = delegate { };
    public event Action onFixedUpdate = delegate { };
    public event Action onHealPickUp = delegate { };
    public event Action onLifePickUp = delegate { };
    public event Action<bool> onShowRender = delegate { };
    public event Action<Color> onRenderColor = delegate { };
    public event Action normalRenderColor = delegate { };
    public event Action onLandGround = delegate { };
    public event Action<bool> onDetectGround = delegate { };
    public event Action onGroundedJump = delegate { };
    public event Action onAirJump = delegate { };
    public event Action onAttack = delegate { };
    public event Action onGetHit = delegate { };
    public event Action onDeath = delegate { };
    public event Action<bool> onFlipRender = delegate { };
    public event Action<float> onXMovement = delegate { };
    public event Action<float> onYMovement = delegate { }; // Action para el view de la escalera y todo lo que requiera moverse en vertical
    public event Action<float> onYVelocity = delegate { };
    public event Action<bool> onLadderGrab = delegate { };
    public event Action<bool> onGrabChain = delegate { };
    public event Action onBackToCheckpoint = delegate { };
	
    Action<PlayerModel, Action> _prepareAttack = delegate { };
    Action _optionalAttack = delegate { };

    Action _cancelAttackSettings;

    public Action<Action> addAttackToAnimation = delegate { };
    public Action<Action<PlayerModel>> onSkinKeys;
    
	
    Action<bool> _detectFloor;
    Action _applyPhysics; //Si no estoy en lader o agarrado, hay gravity
    Action _restrictVerticalMovement; //Detecto en el fixed update si puedo subir o no en la escalera, solo va a contener codigo si esta dentro de una escalera
    Action _setVerticalStrategy; //Seteo el Controlador y el strategy de movimiento cuando quiero moverme por primera vez en la escalera
    Action _cancelVerticalGrab; //Para que setee todo a la normalidad (devuelve gravedad, la strategy determinada si toca el piso o no, etc.)

    IController _groundKeys, _airKeys, _ladderKeys, _chainKeys;
    IMovement _groundMovementStrategy, _airMovementStrategy;
    IMovement _currentStrategy;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _physics = new PhysicsCalculation();
        _physics.CalculateGravity(stats.jumpHeight, stats.timeToApex);
        Physics.gravity = Vector3.zero;
        _rb.gravityScale = 0;
        yVelocity = 0;
        _applyPhysics = ApplyGravity;

        _detectFloor = DetectGroundFromGround;

        _groundKeys = new GroundController().SetModel(this);
        _airKeys = new AirController().SetModel(this);
        _ladderKeys = new LadderController().SetModel(this);
        _chainKeys = new ChainController().SetModel(this);
        
        stats.hp = stats.maxHP;

        _currentSpeed = stats.walkingSpeed;

        _checkpointPosition = transform.position;

        AddNewSkin(Resources.Load<Skin>("Skins/Latin_Lover_Skin"));
        SkillsAndValues.Add(_mySkins[0], () => { return -1; });
        //AddNewSkin(Resources.Load<Skin>("Skins/Bowser_Skin"));
        //SkillsAndValues.Add(_mySkins[1], () => { return -1; });
        //AddNewSkin(Resources.Load<Skin>("Skins/FusRohCuack_Skin"));
        //SkillsAndValues.Add(_mySkins[2], () => { return currentPointsFRC; });
        //AddNewSkin(Resources.Load<Skin>("Skins/Jetpack_Skin"));
        //SkillsAndValues.Add(_mySkins[3], () => { return currentPointsJetPack; });
        
    }
    // Start is called before the first frame update
    private void Start() {

        _control = new PlayerController(this, GetComponent<PlayerView>());

        _groundMovementStrategy = new GroundMovement(transform, onXMovement, DownwardsCollisionCheck);
        _airMovementStrategy = new AirMovement(transform, DownwardsCollisionCheck);

        SetNewStrategies(_groundKeys, _groundMovementStrategy);

        //_ui = GetComponent<UpdateUI>();
        _miniUI = GetComponent<UpdateMiniUI>();

        ChangeSkin(0);
        _miniUI.UpdateSkillText(_mySkins.IndexOf(_currentSkin), SkillsAndValues[_currentSkin]());

        //_ui.UpdateHPText(stats.hp);
        //_ui.UpdateLivesText(stats.lives);
		_miniUI.UpdateHPText(stats.hp);
		_miniUI.UpdateHPText(stats.lives);
    }

    private void Update() {
        
        if (_isDying)
            return;

        onUpdate();
        
    }

    private void FixedUpdate()
    {
        onFixedUpdate();

        _applyPhysics?.Invoke();
        
        if (!_isControllerManipulated)
            _detectFloor(GroundDetect());

        _restrictVerticalMovement?.Invoke(); //Es lo mismo que preguntar si es nulo antes de ejecutarlo. El invoke viene del Action, no es lo mismo que la caca del Invoke convencional
        
    }

    public void SetNewStrategies(IController newControl, IMovement newMovement)
    {
        _control.SetController(newControl);
        _currentStrategy = newMovement;
    }

    public void AddNewSkin(Skin newSkin)
    {
        _mySkins.Add(newSkin);

        switch(_mySkins.Count) {
            case 2:
                SkillsAndValues.Add(_mySkins[1], () => { return -1; }); ;
                break;
            case 3:
                SkillsAndValues.Add(_mySkins[2], () => { return currentPointsFRC; });
                break;
            case 4:
                SkillsAndValues.Add(_mySkins[3], () => { return currentPointsJetPack; });
                break;
        }
    }

    public void ChangeSkin(int index)
    {

        if (index >= _mySkins.Count) {
            return;
        }

        Skin newSkin = _mySkins[index];

        if (!newSkin)
            return;

        if (_currentSkin != newSkin)
        {
            GameObject effect = Instantiate(changeSkinEffect.gameObject, effectSpawnPoint);
            _currentSkin = newSkin;
            onChangeSkin(_currentSkin.newAnimator);
            _currentSkin.GetAttack(this);
            onSkinKeys(_currentSkin.ExtraKeys);
            _miniUI.UpdateSkillText(_mySkins.IndexOf(_currentSkin),SkillsAndValues[_currentSkin]());
        }
    }

    public void SetAttack(Action<PlayerModel, Action> preparation, Action execution, Action secondAction)
    {
        _prepareAttack = preparation;
        _optionalAttack = secondAction;
        addAttackToAnimation(execution);
    }

    #region Gravity n Floor / Wall detection

    void ApplyGravity()
    {
        float newYValue = _rb.velocity.y + _physics._jumpGravity * Time.deltaTime;
        _rb.velocity = new Vector2(_rb.velocity.x, newYValue);

        onYVelocity(newYValue);
    }

    bool DownwardsCollisionCheck()
    {
        return Physics2D.Raycast(wallCheck.position + new Vector3(0, 1f), Vector2.down, 2.5f, stopRunningLayer);
    }

    bool GroundDetect()
    {
        return Physics2D.BoxCast(transform.position - new Vector3(0, boxSize.y / 2), boxSize, 0, Vector2.right, 0, groundLayers);
    }

    public void ControlPhysics()
    {
        _isControllerManipulated = true;
        _rb.velocity = Vector3.zero;
    }

    public void RestorePhysics()
    {
        _isControllerManipulated = false;
        yVelocity = 0;

        if (GroundDetect())
        {
            GroundSettings();
            _detectFloor = DetectGroundFromGround;
        }
        else
        {
            AirSettings();
            _detectFloor = DetectGroundFromAir;
        }

    }

    void GroundSettings()
    {
        yVelocity = 0;
        _isInAir = false;
        _currentJumps = 0;
        onDetectGround(true);

        //_currentStrategy = _groundMovementStrategy;
        //_control.SetController(_groundKeys);
        SetNewStrategies(_groundKeys, _groundMovementStrategy);

        _detectFloor = DetectGroundFromGround;
        
    }

    void DetectGroundFromAir(bool groundDetected)
    {
        if (groundDetected)
        {
            GroundSettings();
            onLandGround();
        }
    }

    void AirSettings()
    {
        _isInAir = true;
        onDetectGround(false);

        if (_currentStrategy != _airMovementStrategy)
        {
            //_currentStrategy = _airMovementStrategy;
            //_control.SetController(_airKeys);
            SetNewStrategies(_airKeys, _airMovementStrategy);
        }

        _rb.velocity = new Vector2(0, yVelocity);

        _detectFloor = DetectGroundFromAir;
    }

    void DetectGroundFromGround(bool groundDetected)
    {
        if (!groundDetected)
        {
            AirSettings();
        }
    }

    void DetectMovementVerticalRestrictions()
    {
        _canMoveUp = Physics2D.BoxCast(transform.position - new Vector3(0, boxSizeCheck.y/2) + offsetPosLadderPlatform, boxSizeCheck, 0, Vector2.right, 0, topLadderLayer);
        _canMoveDown = Physics2D.BoxCast(transform.position - new Vector3(0, boxSize.y / 2), boxSize, 0, Vector2.right, 0, downLadderLayer);
    }

    #endregion
    
    #region Movement
    

    public void PressedRun()
    {
        _currentSpeed = stats.runningSpeed;
        _isRunning = true;
    }
    public void ReleaseRun()
    {
        _currentSpeed = stats.walkingSpeed;
        _isRunning = false;
    }

    public void Flip()
    {
        _isFlipped = !_isFlipped;
        onFlipRender(_isFlipped);
        Vector3 newBulletSpawnerPos = eggSpawnPoint.localPosition;
        Vector3 newWallCheckPos = wallCheck.localPosition;
        Vector3 newFireSpawnerPos = fireSpawnPoint.localPosition;
        newBulletSpawnerPos.x *= -1;
        newWallCheckPos.x *= -1;
        newFireSpawnerPos.x *= -1;
        eggSpawnPoint.localPosition = newBulletSpawnerPos;
        wallCheck.localPosition = newWallCheckPos;
        fireSpawnPoint.localPosition = newFireSpawnerPos;
        eggSpawnPoint.right = -eggSpawnPoint.right;
        fireSpawnPoint.right = -fireSpawnPoint.right;
    }

    public void MoveHorizontal(float xAxi)
    {
        _currentStrategy.MoveHorizontal(xAxi, _currentSpeed, _isRunning, ()=> 
        {
            if ((xAxi > 0 && _isFlipped) || (xAxi < 0 && !_isFlipped))
            {
                Flip();
            }
        });
    }

    public void MoveVertical(float yAxi)
    {
        if (_onLadderZone)
        {
            if ((yAxi > 0 && _canMoveUp) || (yAxi < 0 && _canMoveDown))
            {
                if (_setVerticalStrategy == null)
                {
                    _cancelVerticalGrab();
                    Debug.Log("Cancel");
                    
                }
            }
            else
            {
                if (yAxi != 0)
                {
                    _setVerticalStrategy?.Invoke();
                    Debug.Log("Invoke");
                }
                
                _currentStrategy.MoveVertical(yAxi, stats.climbingSpeed);

            }
        }
        
    }

    public void EnterChain(float dirX)
    {

        if (_isControllerManipulated) return;

        if (dirX > 0 && _isFlipped || dirX < 0 && !_isFlipped)
        {
            Flip();
        }


        SetNewStrategies(_ladderKeys, new ChainMovement());
        _applyPhysics = null;
        _rb.velocity = Vector3.zero;
        _cancelVerticalGrab = () => 
        {
            onGrabChain(false);
            _applyPhysics = ApplyGravity;
            SetNewStrategies(_airKeys, _airMovementStrategy);
            _cancelVerticalGrab = null;
        };
        onGrabChain(true);
    }

    public void ExitChain()
    {
        _cancelVerticalGrab?.Invoke();
    }

    void SetLadderSettings(Action moveUp, Action moveDown)
    {
        
        SetNewStrategies(_ladderKeys, new LadderMovement(transform, moveUp, moveDown, onYMovement));
        
        _applyPhysics = null;

        ControlPhysics();
        _cancelVerticalGrab = () => CancelLadderSettings(moveUp, moveDown);
        _setVerticalStrategy = null;
        onLadderGrab(true);
    }
    

    void CancelLadderSettings(Action moveUp, Action moveDown)
    {
        Debug.Log("cancelExecuted");

        _cancelVerticalGrab = null;
        

        RestorePhysics();


        if (_onLadderZone)
            _setVerticalStrategy = () => SetLadderSettings(moveUp, moveDown);

        _applyPhysics = ApplyGravity;

        onLadderGrab(false);
    }
    
    public void EnterLadderZone(GameObject switchPlatform)
    {
        _onLadderZone = true;

        Action onMovingVerticalPositive = () => { if (!switchPlatform.activeSelf) switchPlatform.SetActive(true); };

        Action onMovingVerticalNegative = () => { if (switchPlatform.activeSelf) switchPlatform.SetActive(false); };

        _setVerticalStrategy = () =>
        {
            SetLadderSettings(onMovingVerticalPositive, onMovingVerticalNegative);
        };
        

        _restrictVerticalMovement = DetectMovementVerticalRestrictions;
        
    }
    public void ExitLadderZone()
    {
        _onLadderZone = false;
        _cancelVerticalGrab?.Invoke();
        _restrictVerticalMovement = null;
    }
    #endregion

    #region Jumps

    void JumpLogic()
    {
        yVelocity = _physics.CalculateJumpVelocity(stats.timeToApex);
        _rb.velocity = new Vector2(_rb.velocity.x, yVelocity);
    }

    public void GroundedJump()
    {
        _cancelVerticalGrab?.Invoke();

        SetNewStrategies(_airKeys, _airMovementStrategy);
        JumpLogic();
        onGroundedJump();
    }

    public void AirJump()
    {
        if (_currentJumps < stats.maxJumps)
        {
            _currentJumps++;
            JumpLogic();
            onAirJump();
        }
    }
    #endregion

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.GetComponent<IHazardCollider>() != null) {
            collision.gameObject.GetComponent<IHazardCollider>().MakeCollisionDamage(this);
        }

        

        if (collision.gameObject.tag == "Wall" && _isOnCrusher) {
            TakeDamage(100);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        IPickupable pickupable = collision.gameObject.GetComponent<IPickupable>();
        //Debug.Log("Trigger: " + collision.gameObject.name);

        if (pickupable != null)
        {
            pickupable.OnPickUp(this);
        }
        else if (collision.gameObject.GetComponent<IHazard>() != null)
        {
            collision.gameObject.GetComponent<IHazard>().MakeDamage(this);
        }
        else if (collision.GetComponent<Checkpoint>())
        {
            _checkpointPosition = collision.GetComponent<Checkpoint>().GetCheckpointPosition();
            collision.GetComponent<Checkpoint>().ActivateCheckpoint();
        } else if (collision.gameObject.GetComponent<IMovingPlatform>() != null) {
            collision.gameObject.GetComponent<IMovingPlatform>().ParentToPlatform(this.gameObject.transform);
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.GetComponent<Spike>() != null)
        {
            collision.gameObject.GetComponent<Spike>().MakeDamage(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.GetComponent<IMovingPlatform>() != null) {
            this.gameObject.transform.parent = null;
        }
    }

    // -------------------------------------------- Attacks --------------------------------------------

    public void Attack() {
        _prepareAttack(this, onAttack);
        // TODO: Hay que hacer que esto se actualice solo, O en su defecto updatear el diccionario en todos los lados que se toca el valor, sumado o restado

        //SkillsAndValues[_currentSkin] -= 20;
        //_miniUI.UpdateSkillText(_mySkins.IndexOf(_currentSkin), SkillsAndValues[_currentSkin]);
    }

    public void SecondAttack()
    {
        _optionalAttack();
        //_miniUI.UpdateSkillText(_mySkins.IndexOf(_currentSkin), SkillsAndValues[_currentSkin]);
    }
    
    public void ChangePointsValue(Skin skinCalling, float value)
    {
        if (_currentSkin == skinCalling)
            _miniUI.UpdateSkillText(_mySkins.IndexOf(_currentSkin), value);
    }

    //// TODO: Super simple, habría que spawnear feedback o detallar mas como los elimina
    //public void FusRoQuack() {
    //    Debug.Log("FusRo Enter");
    //    Collider2D[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy").Select(x => x.GetComponent<Collider2D>()).ToArray();
    //    Debug.Log("Enemies: " + allEnemies.Length.ToString());
    //    List<GameObject> enemiesToDestroy = new List<GameObject>();
    //    Debug.Log("To Destroy: " + enemiesToDestroy.Count.ToString());
    //    Camera cam = Camera.main;
    //    Plane[] planes = GeometryUtility.CalculateFrustumPlanes(cam);

    //    for (int i = 0; i < allEnemies.Length; i++) {
    //        Debug.Log("Name: " + allEnemies[i].name);
    //        if (GeometryUtility.TestPlanesAABB(planes, allEnemies[i].bounds)) { // Chequea si el collider esta dentro de los planos de vision de la camara.
    //            enemiesToDestroy.Add(allEnemies[i].gameObject);
    //        }
    //    }

    //    foreach (GameObject e in enemiesToDestroy) {
    //        Destroy(e);
    //    }
    //    enemiesToDestroy.Clear();
    //}

    // -------------------------------------------- End Attacks --------------------------------------------
    // -------------------------------------------- Damage --------------------------------------------

    public void CancelAttackActionsOnDmg(Action action)
    {
        _cancelAttackSettings = action;
        if (action != null)
            _cancelAttackSettings += () => _cancelAttackSettings = null;
    }

    public void TakeDamage(int dmg) {

        if (_isImmune || _isDying) return;
        
        stats.hp = Mathf.Max(stats.hp -= dmg, 0);

        _miniUI.UpdateHPText(stats.hp);

        _cancelVerticalGrab?.Invoke();
        _cancelAttackSettings?.Invoke();

        if (stats.hp == 0) {
            Die();
            return;
        }

        onGetHit();
        SetImmunity(true);
        StartCoroutine(GetHitEffect());

        if (!_isFlipped) {
            _rb.AddForce(new Vector2(-1, 1).normalized * stats.damageForce, ForceMode2D.Impulse);
        } else {
            _rb.AddForce(new Vector2(1, 1).normalized * stats.damageForce, ForceMode2D.Impulse);
        }
    }

    public void SetImmunity(bool i)
    {
        _isImmune = i;
    }

    public void SetOnCrusher(bool c) {
        _isOnCrusher = c;
    }

    public void Die()
    {
        _isDying = true;
        yVelocity = 0;

        stats.lives--;
		_miniUI.UpdateLivesText(stats.lives);

        if (stats.lives <= 0)
        {
            Debug.Log("Obligado a reiniciar. No hay mas vidas");
            stats.lives = stats.maxLives;
            StartCoroutine(RestartLevel());
        }
        else
        {
            StartCoroutine(DieToCheckpoint());
        }

        onDeath();
        
    } 

    IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(1.3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator DieToCheckpoint()
    {
        yield return new WaitForSeconds(1.3f);
        transform.position = _checkpointPosition;
        _isDying = false;
        stats.hp = stats.maxHP;
		_miniUI.UpdateHPText(stats.hp);
        onBackToCheckpoint();
    }

    IEnumerator GetHitEffect()
    {
        onRenderColor(new Color(175, 0, 0, 0.8f));
        for (int i = 0; i < 5; i++)
        {
            onShowRender(false);
            yield return new WaitForSeconds(0.1f);
            onShowRender(true);
            yield return new WaitForSeconds(0.05f);
        }
        normalRenderColor();
        yield return new WaitForSeconds(1f);
        SetImmunity(false);
    }

    IEnumerator PickUpColorRender(Color newCol)
    {
        onRenderColor(newCol);
        
        yield return new WaitForSeconds(0.2f);
        normalRenderColor();
    }
    // -------------------------------------------- End Damage --------------------------------------------

    // -------------------------------------------- Pickups -----------------------------------------------

    public void HealthPickUp(int hp)
    {
        onHealPickUp();
        stats.hp = Mathf.Min(stats.hp + hp, stats.maxHP);
        //_ui.UpdateHPText(stats.hp);
        _miniUI.UpdateHPText(stats.hp);
        StartCoroutine(PickUpColorRender(Color.green));
    }

    public void LivesPickUp()
    {
        onLifePickUp();
        stats.lives++;
        _miniUI.UpdateLivesText(stats.lives);
        StartCoroutine(PickUpColorRender(Color.yellow));
    }


    // -------------------------------------------- End Pickups --------------------------------------------

    public Coroutine RechargeBar(Func<bool> condition, Action recharge, Action end)
    {
        return StartCoroutine(Recharge(condition, recharge, end));
    }

    public void StopRecharge(Coroutine cor)
    {
        if (cor != null)
            StopCoroutine(cor);
    }

    IEnumerator Recharge(Func<bool> condition, Action recharge, Action end)
    {
        while (condition())
        {
            recharge();
            yield return null;
        }

        end();
    }


    // -------------------------------------------- Debugs --------------------------------------------
    private void OnDrawGizmos() {

        Gizmos.color = Color.red;
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + Vector3.right * 0.3f);
        Gizmos.DrawLine(wallCheck.position, wallCheck.position - Vector3.right * 0.3f);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(wallCheck.position + new Vector3(0, 1f), wallCheck.position + Vector3.down * 2f);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position - new Vector3(0, boxSize.y / 2), boxSize);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position - new Vector3(0, boxSizeCheck.y/2) + offsetPosLadderPlatform, boxSizeCheck);
    }
}


