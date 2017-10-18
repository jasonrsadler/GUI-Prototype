using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ACA
{
    public class UIManager : MonoBehaviour
    {
        public UnityEngine.UI.Text userID;
        public UnityEngine.UI.Text pwd;
        public ItemDatabase itemData;
        public UnityEngine.UI.Button logonButton;
        public UnityEngine.UI.Text logonStatus;
        public UnityEngine.UI.Button leftButton;
        public UnityEngine.UI.Button rightButton;
        public UnityEngine.UI.Button doneButton;
        public UnityEngine.UI.Button selectButton;

        Animator logonAnimator;
        bool logonActive = false;
        List<Item> item;
        List<ItemTransform> itemTransform;
        int itr = 0;
        GameObject currentObject;
        bool selected = false;


        //rotationals
        public float distance = 5.0f;
        public float xSpeed = 120.0f;
        public float ySpeed = 120.0f;
        public float yMinLimit = -20f;
        public float yMaxLimit = 80f;
        public float xMinLimit = -15f;
        public float xMaxLimit = 90f;
        public float distanceMin = .5f;
        public float distanceMax = 15f;
        public float smoothTime = 2f;
        float rotationYAxis = 0.0f;
        float rotationXAxis = 0.0f;
        float velocityX = 0.0f;
        float velocityY = 0.0f;
        public float rotationSpeed = 8f;
        //
        // Use this for initialization
        void Start()
        {

            logonActive = true;
            Debug.Log("Logging will be located in " + Application.persistentDataPath + "/log.txt");

            item = ItemData.LoadData(itemData);
            itemTransform = ItemData.LoadTransformData(itemTransform);
            if (itemTransform.Count == 0)
                itemTransform = ItemData.InitializeTransformData();
            else
                ApplyItemTransforms();

            currentObject = Instantiate(item[itr].Prefab);
            Logging.Log(currentObject.name + " was instantiated.");

            Vector3 angles = transform.eulerAngles;
            rotationYAxis = angles.y;
            rotationXAxis = angles.x;
            logonAnimator = GetComponent<Animator>();
        }

        private void ApplyItemTransforms()
        {
            for (int ix = 0; ix < itemTransform.Count; ix++)
            {
                item[ix].Prefab.transform.position = itemTransform[ix].Position;
                item[ix].Prefab.transform.rotation = itemTransform[ix].Rotation;
                item[ix].Prefab.transform.localScale = itemTransform[ix].Scale;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (logonActive)
            {
                MonitorLogon();
            }
            else
            {
                if (selected)
                {
                    MonitorPanning();
                    MonitorRotating();
                    MonitorSizing(); 
                }
            }
        }

        void MonitorLogon()
        {
            logonButton.enabled = !String.IsNullOrEmpty(userID.text);
        }

        public float mouseSensitivity = 10f;
        Vector3 lastPosition;
        void MonitorPanning()
        {
            if (Input.GetMouseButtonDown(0))
            {
                lastPosition = Input.mousePosition;
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 delta = Input.mousePosition - lastPosition;
                currentObject.transform.Translate(delta.x * mouseSensitivity, delta.y * mouseSensitivity, 0, Space.World);
                lastPosition = Input.mousePosition;
            }
            if (Input.GetMouseButtonUp(0))
            {
                Logging.Log("Current object, " + currentObject.name + " is at position V3(" + currentObject.transform.position.x.ToString() + ", " + currentObject.transform.position.y.ToString() + ", " + currentObject.transform.position.z.ToString() + ").");
                itemTransform[itr].Position = currentObject.transform.position;
                itemTransform = ItemData.UpdateItem(itemTransform[itr]);
            }

        }

        void MonitorRotating()
        {
            if (Input.GetMouseButton(1))
            {
                velocityX += xSpeed * Input.GetAxis("Mouse X") * distance * 0.02f;
                velocityY += ySpeed * Input.GetAxis("Mouse Y") * 0.02f;

                rotationYAxis += velocityX;
                rotationXAxis -= velocityY;
                Quaternion toRotation = Quaternion.Euler(rotationXAxis, rotationYAxis, 0);
                Quaternion rotation = toRotation;

                
                currentObject.transform.rotation = rotation;
                //currentObject.transform.position = position;
                velocityX = Mathf.Lerp(velocityX, 0, Time.deltaTime * smoothTime);
                velocityY = Mathf.Lerp(velocityY, 0, Time.deltaTime * smoothTime);
            }
            if (Input.GetMouseButtonUp(1))
            {
                Logging.Log("Current object, " + currentObject.name + " is at rotation V3(" + currentObject.transform.rotation.x.ToString() + ", " + currentObject.transform.rotation.y.ToString() + ", " + currentObject.transform.rotation.z.ToString() + ").");
                itemTransform[itr].Rotation = currentObject.transform.rotation;
                itemTransform = ItemData.UpdateItem(itemTransform[itr]);
            }
        }

        Vector3 minScale = new Vector3(0.1f, 0.1f, 0.1f);
        Vector3 maxScale = new Vector3(3, 3, 3);
        void MonitorSizing()
        {
            float zoomValue = Input.GetAxis("Mouse ScrollWheel");

            if (zoomValue != 0)
            {
                currentObject.transform.localScale += Vector3.one * zoomValue;
                currentObject.transform.localScale = Vector3.Max(currentObject.transform.localScale, minScale);
                currentObject.transform.localScale = Vector3.Min(currentObject.transform.localScale, maxScale);
                Logging.Log("Current object, " + currentObject.name + " is at scale V3(" + currentObject.transform.localScale.x.ToString() + ", " + currentObject.transform.localScale.y.ToString() + ", " + currentObject.transform.localScale.z.ToString() + ").");
                itemTransform[itr].Scale = currentObject.transform.localScale;
                itemTransform = ItemData.UpdateItem(itemTransform[itr]);
            }
        }

        public void OnNextClick()
        {
            Logging.Log("Clicked Next");
            Destroy(currentObject);
            itr++;
            if (itr >= item.Count)
            {
                itr = 0;
            }
            currentObject = Instantiate(item[itr].Prefab);
            currentObject.transform.position = itemTransform[itr].Position;
            currentObject.transform.rotation = itemTransform[itr].Rotation;
            currentObject.transform.localScale = itemTransform[itr].Scale;
        }

        public void OnPrevClick()
        {
            Logging.Log("Clicked Previous");
            Destroy(currentObject);
            itr--;
            if (itr < 0)
            {
                itr = item.Count - 1;
            }
            currentObject = Instantiate(item[itr].Prefab);
            currentObject.transform.position = itemTransform[itr].Position;
            currentObject.transform.rotation = itemTransform[itr].Rotation;
            currentObject.transform.localScale = itemTransform[itr].Scale;
        }

        public void OnSelectClick()
        {
            Logging.Log("Item Selected");
            doneButton.enabled = selected = true;
            leftButton.enabled = rightButton.enabled = selectButton.enabled = false;
        }

        public void OnDoneClick()
        {
            Logging.Log("Item Deselected");
            doneButton.enabled = selected = false;
            leftButton.enabled = rightButton.enabled = selectButton.enabled = true;
            ApplyItemTransforms();
        }

        public void OnLogonPressed()
        {
            Logging.Log("Logon attempted with user id " + userID + " and pwd " + pwd.text);
            if (userID.text.ToLower() == "admin") //mockup only, don't actually implement
            {
                logonStatus.enabled = false;
                Logging.Log("Logon successful");
                logonButton.enabled = false;
                logonAnimator.SetTrigger("LogonFade");
                logonActive = false;
            }
            else
            {
                logonStatus.enabled = true;
            }
        }

        public static float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360F)
                angle += 360F;
            if (angle > 360F)
                angle -= 360F;
            return Mathf.Clamp(angle, min, max);
        }
    }
}