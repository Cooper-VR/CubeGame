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
    #endregion

    #region  privateVariables
    private Rigidbody rb;
    private string baseText = "Score: ";
    private Collider playerCollider;
    private bool hit;
    public Quaternion playerRotation;
    #endregion

    private void Start() {
        obejcts = allObjects.allObjects;
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
        positionChecker();
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

            if (rb.velocity.z < 50f){
                rb.AddForce(forceVector.x * Time.deltaTime, 0, speed * Time.deltaTime);
            } else {
                rb.AddForce(forceVector.x * Time.deltaTime, 0, 0 * Time.deltaTime);
            }
            
            score.text = baseText + Mathf.Round(transform.position.z).ToString();
        }
    }

    void positionChecker(){
        if (playerPosition.position.y < -50f){
            hit = true;
            allObjects.enabled = false;
            Debug.Log(playerPosition.position.y);
            Debug.Log("fell");
            resetScene();
        }
    }

    private void OnCollisionEnter(Collision playerCollider) {
        if (Time.time > 1){
            hit = true;
            allObjects.enabled = false;
            Debug.Log("hit");
            resetScene();
        }
    }

    public void startButtonClick(){
        hit = false;
        allObjects.enabled = true;
        uiPanel.SetActive(false);
        startButton.SetActive(false);
        allObjects.initalSpawn();
        transform.rotation = Quaternion.Euler( new Vector3(0, 0, 0));
        
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
