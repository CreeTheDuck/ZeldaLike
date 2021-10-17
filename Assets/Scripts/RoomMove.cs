using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomMove : MonoBehaviour{

    public Vector2 cameraChange; // how much we want to move the camera
    public Vector3 playerChange; // how much to move trhe player when switching locations
    private CameraMovement cam;
    public bool needText;
    public string placeName;
    public GameObject text;
    public Text placeText;

    // Start is called before the first frame update
    void Start(){
        cam = Camera.main.GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void Update(){
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) { //check for the player tag and the trigger
            cam.minPosition += cameraChange;
            cam.maxPosition += cameraChange; // we will need two camera changes if the room isnt of the same size
            other.transform.position += playerChange;

            if (needText) {
                StartCoroutine(placeNameCo());
            }
        }
    }

    private IEnumerator placeNameCo() {
        text.SetActive(true);
        placeText.text = placeName;
        yield return new WaitForSeconds(4f);
        text.SetActive(false);
    }
}
