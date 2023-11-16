// using UnityEngine;
// using UnityEngine.EventSystems;

// public class ScreenDragger : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
// {
//     private Vector3 screenPoint;
//     private Vector3 offset;

//     private Vector3 initialScale;
//     private float initialY;

//     public void OnBeginDrag(PointerEventData eventData)
//     {
//         // Transform position from world space into screen space
//         screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
//         offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, screenPoint.z));

//         initialScale = transform.localScale;
//         initialY = eventData.position.y;
//     }

//     public void OnDrag(PointerEventData eventData)
//     {
//         if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
//         {
//             float deltaY = eventData.position.y - initialY;
//             Vector3 newScale = initialScale * (1 + deltaY * 0.01f); // 0.01f is a scaling factor you can adjust to change the sensitivity of the scaling
//             transform.localScale = newScale;
//         }
//         else
//         {
//             Vector3 curScreenPoint = new Vector3(eventData.position.x, eventData.position.y, screenPoint.z);
//             Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
//             // Ensure the position change only affects the x and y coordinates
//             transform.position = new Vector3(curPosition.x, curPosition.y, transform.position.z);
//         }
//     }

//     public void OnEndDrag(PointerEventData eventData)
//     {
//         // You can add logic here for what should happen after the drag ends
//     }
// }







using UnityEngine;
using UnityEngine.EventSystems;
using System.IO;

public class ScreenDragger : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 screenPoint;
    private Vector3 offset;
    private Vector3 startPosition;
    private Vector3 endPosition;
    public int monitorId;

    private Vector3 initialScale;
    private float initialY;

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Transform position from world space into screen space
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, screenPoint.z));

        initialScale = transform.localScale;
        initialY = eventData.position.y;

        // Save the start position of the drag operation
        startPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            float deltaY = eventData.position.y - initialY;
            Vector3 newScale = initialScale * (1 + deltaY * 0.01f); // 0.01f is a scaling factor you can adjust to change the sensitivity of the scaling
            transform.localScale = newScale;
        }
        else
        {
            Vector3 curScreenPoint = new Vector3(eventData.position.x, eventData.position.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            // Ensure the position change only affects the x and y coordinates
            transform.position = new Vector3(curPosition.x, curPosition.y, transform.position.z);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Save the end position of the drag operation
        endPosition = transform.position;

        // Write the start and end positions to a file
        WritePositionToFile(startPosition, endPosition, "positions.txt");
    }

    private void WritePositionToFile(Vector3 startPos, Vector3 endPos, string filename)
    {
        string path = Application.persistentDataPath + "/" + filename;
        string content = "Monitor ID: " + monitorId + "\nStart Position - x: " + startPos.x + ", y: " + startPos.y + ", z: " + startPos.z;
        content += "\nEnd Position - x: " + endPos.x + ", y: " + endPos.y + ", z: " + endPos.z;
        Debug.Log(Application.persistentDataPath);

        try
        {
            // If the file exists, append a new line. Otherwise, create the file.
            if (File.Exists(path))
            {
                File.AppendAllText(path, "\n\n" + content);
            }
            else
            {
                File.WriteAllText(path, content);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to write positions to file: " + e.Message);
        }
    }
}





























