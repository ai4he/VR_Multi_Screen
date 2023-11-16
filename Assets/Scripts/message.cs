using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class message : MonoBehaviour
{
    private TcpListener listener;
    private const int port = 9999;

    private void Start()
    {
        IPAddress localAddr = IPAddress.Parse("172.22.48.216");
        listener = new TcpListener(localAddr, port);
        listener.Start();
        Debug.Log("Server started on port " + port);

        // Wait for a client to connect in a separate thread
        Thread t = new Thread(new ThreadStart(WaitForClient));
        t.Start();
    }

    private void WaitForClient()
    {
        // Block until a client connects
        TcpClient client = listener.AcceptTcpClient();
        Debug.Log("Connected to client " + client.Client.RemoteEndPoint);

        NetworkStream stream = client.GetStream();
        byte[] buffer = new byte[1024];
        int bytesRead;

        while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
        {
            string receivedData = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            Debug.Log("Received: " + receivedData);

            // Send the same data back to the client
            stream.Write(buffer, 0, bytesRead);
        }

        client.Close();
        Debug.Log("Connection closed.");
    }

    private void OnApplicationQuit()
    {
        listener.Stop();
    }
}


