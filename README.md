# K-Chat

K-Chat is a local area network chat application that operates using the UDP protocol.

## How it works
```bash
1. Begin by entering the target IP address and the corresponding port for your chat. Ensure that your IP address and port match.
2. Click "Connect" and wait for the target to follow the same steps.
3. Once both parties are connected, a confirmation message will be displayed.
```

## Known Bugs
This application represents my initial attempt at creating a chat application, and I acquired the knowledge from YouTube tutorials. As a result, there are several known bugs, including:
1. Message Delivery Issue to Target
    - This problem arises because the UDP protocol lacks an identifier number, making it unable to detect lost packets. To resolve this, consider using TCP/IP.
   - Additionally, check if the port is being utilized by another application and try changing it.
   
2. Duplicate Message Sending
   - I am actively investigating the cause of this issue. It is likely associated with the UDP protocol.


## Contributing
Feel free to submit pull requests for bug fixes or additional features. Please provide detailed information about the changes you made. Your contributions are greatly appreciated.

# Update
I have discontinued updates for this application due to time constraints. However, I have made a readme update based on my friend's bug report.
