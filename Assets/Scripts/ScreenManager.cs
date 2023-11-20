using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ScreenManager : MonoBehaviour
{
    public GameObject screenPrefab;
    public GameObject screensContainer;
    //   public string host = "10.121.36.229";
    public string host = "198.21.169.154";
    public int basePort = 9999;
    private Dictionary<int, ScreenReceiver> screens = new Dictionary<int, ScreenReceiver>();

  public void StartScreen(int monitorId)
    {
        if (screens.ContainsKey(monitorId)) return;
        Debug.Log(screensContainer.transform.childCount);
        GameObject screenObject = Instantiate(screenPrefab, screensContainer.transform);
        // Debug.Log("This is a debug message.");
        // Debug.Log(screens.Count);
        screenObject.transform.position = screensContainer.transform.position + new Vector3(screens.Count*200, 0, 0); // Set the position of the new screen
        screenObject.transform.localScale = new Vector3((float)0.1, (float)0.1, (float)0.1); 

        Transform canvasTransform = screenObject.transform.Find("Canvas");
        if (canvasTransform != null)
        {
            canvasTransform.localScale = Vector3.one;
            // Ensure a GraphicRaycaster component is attached
            GraphicRaycaster graphicRaycaster = canvasTransform.GetComponent<GraphicRaycaster>();
            if (graphicRaycaster == null)
            {
                graphicRaycaster = canvasTransform.gameObject.AddComponent<GraphicRaycaster>();
            }
        }

        // Ensure a BoxCollider component is attached to the screen object for Physics Raycasting
        BoxCollider boxCollider = screenObject.GetComponent<BoxCollider>();
        if (boxCollider == null)
        {
            boxCollider = screenObject.AddComponent<BoxCollider>();
        }
        
        ScreenReceiver screen = screenObject.GetComponent<ScreenReceiver>();
        screen.StartReceiving(host, basePort + monitorId);

        screens.Add(monitorId, screen);
        Debug.Log(screensContainer.transform.childCount);
    }


    public void StopScreen(int monitorId)
    {
        if (!screens.ContainsKey(monitorId)) return;

        ScreenReceiver screen = screens[monitorId];
        screen.StopReceiving();

        screens.Remove(monitorId);

        Destroy(screen.gameObject);
    }

    public bool IsScreenActive(int monitorId)
    {
        return screens.ContainsKey(monitorId);
    }

    public void HandleCommand(string command)
    {
        string[] parts = command.Split(' ');
        string action = parts[0];
        int monitorId = int.Parse(parts[1]);

        if (action == "start")
        {
            if (!IsScreenActive(monitorId))
            {
                StartScreen(monitorId);
            }
        }
        else if (action == "stop")
        {
            if (IsScreenActive(monitorId))
            {
                StopScreen(monitorId);
            }
        }
    }
}



// using System.Collections.Generic;
// using UnityEngine;

// public class ScreenManager : MonoBehaviour
// {
//     public GameObject screenPrefab;
//     public GameObject screensContainer;
    
//     //  public string host = "198.21.131.171";
//     public string host = "198.21.172.7";
//     //  public string host = "10.121.36.229";
//     public int basePort = 9999;
//     private Dictionary<int, ScreenReceiver> screens = new Dictionary<int, ScreenReceiver>();

//     public void StartScreen(int monitorId)
//     {
//         if (screens.ContainsKey(monitorId)) return;
//         GameObject screenObject = Instantiate(screenPrefab, screensContainer.transform);
//         screenObject.transform.position = screensContainer.transform.position + new Vector3(screens.Count*200, 0, 0);
//         screenObject.transform.localScale = new Vector3((float)0.1, (float)0.1, (float)0.1);

//         Transform canvasTransform = screenObject.transform.Find("Canvas");
//         if (canvasTransform != null)
//         {
//             canvasTransform.localScale = Vector3.one;
//         }

//         // Get the ScreenDragger component and set its monitorId
//         ScreenDragger dragger = screenObject.GetComponent<ScreenDragger>();
//         if (dragger != null)
//         {
//             dragger.monitorId = monitorId;
//         }

//         ScreenReceiver screen = screenObject.GetComponent<ScreenReceiver>();
//         screen.StartReceiving(host, basePort + monitorId);

//         screens.Add(monitorId, screen);
//     }


//     public void StopScreen(int monitorId)
//     {
//         if (!screens.ContainsKey(monitorId)) return;

//         ScreenReceiver screen = screens[monitorId];
//         screen.StopReceiving();

//         screens.Remove(monitorId);

//         Destroy(screen.gameObject);
//     }

//     public bool IsScreenActive(int monitorId)
//     {
//         return screens.ContainsKey(monitorId);
//     }

//     public void HandleCommand(string command)
//     {
//         string[] parts = command.Split(' ');
//         string action = parts[0];
//         int monitorId = int.Parse(parts[1]);

//         if (action == "start")
//         {
//             if (!IsScreenActive(monitorId))
//             {
//                 StartScreen(monitorId);
//             }
//         }
//         else if (action == "stop")
//         {
//             if (IsScreenActive(monitorId))
//             {
//                 StopScreen(monitorId);
//             }
//         }
//     }
// }



