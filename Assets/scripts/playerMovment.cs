using UnityEngine;
using UnityEngine.UI;

public class playerMovment : MonoBehaviour
{
    #region publicVariables
    public float speed = 200;
    public Vector3 forceVector;
    public Transform mainCamera;
    public Vector3 CameraPosition;
    public Transform ground;
    public Text score;
    public Transform playerPosition;

    public GameObject uiPanel;
    public GameObject startButton;
    public colliderSpawn allObjects;
    public GameObject[] obejcts  = new GameObject[500];
    public Quaternion playerRotation;
    
    #endregion

    #region  privateVariables
    private Rigidbody rb;
    private string baseText = "Score: ";
    private Collider playerCollider;
    private bool hit;
    private bool fell;
    private float waitTime = 3f;
    private float startTime;
    
    #endregion

    private void Start() {

        allObjects.enabled = false;
        
        playerRotation = transform.rotation;
        uiPanel.SetActive(true);
        startButton.SetActive(true);
        rb = GetComponent<Rigidbody>();
        hit = true;
        playerCollider = GetComponent<Collider>();
    }

    void FixedUpdate()
    {
        startTime += 1 * Time.deltaTime;
        obejcts = allObjects.allObjects;
        positionChecker();
        sceneResetWait();
        if (hit == false){
            forceVector.z = speed;
            CameraPosition = transform.position;
            CameraPosition.z = CameraPosition.z - 10;
            CameraPosition.y = 5;

            ground.transform.position = new Vector3(0f, 0f, transform.position.z);

            mainCamera.position = CameraPosition;

            if (Input.GetKey(KeyCode.D)){
                forceVector.x = 800;
            } else if (Input.GetKey(KeyCode.A)){
                forceVector.x = -800;
            } else {
                forceVector.x = 0;
            }

            if (rb.velocity.z <  startTime * 10){
                rb.AddForce(forceVector.x * Time.deltaTime, 0, speed * Time.deltaTime);
            } else {
                rb.AddForce(forceVector.x * Time.deltaTime, 0, 0 * Time.deltaTime);
            }
            
            score.text = baseText + Mathf.Round(transform.position.z).ToString();
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawCube(new Vector3())
    }

    void sceneResetWait(){
        if (hit == true){
            waitTime -= 1* Time.deltaTime;
            if (waitTime <= 0){
                allObjects.enabled = false;
                resetScene();
                waitTime = 3;
            }
        } else if (fell == true){
            allObjects.enabled = false;
            resetScene();
            waitTime = 3;
        }
    }

    void positionChecker(){
        if (playerPosition.position.y < -20f){
            fell = true;
        }
    }

    private void OnCollisionEnter(Collision playerCollider) {
        hit = true;
    }

    public void startButtonClick(){
        hit = false;
        waitTime = 3;
        fell = false;
        allObjects.enabled = true;
        uiPanel.SetActive(false);
        startButton.SetActive(false);
        allObjects.initalSpawn(allObjects.allObjects);
        transform.rotation = Quaternion.Euler( new Vector3(0, 0, 0));
        startTime = 0;
    }

    void resetScene(){
        transform.position = new Vector3(0, 0.5f, 0);
        uiPanel.SetActive(true);
        startButton.SetActive(true);
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, 0);

        for (int i = 0; i < obejcts.Length; i++){
            Destroy(obejcts[i]);
        }
    }
}
