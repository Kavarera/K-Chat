# K-Chat

K-Chat is a local area network chat that work with UDP protocol, 

## How it works
```bash
1. first you need to input target ip that you want to chat and input the port, and then input your ip and the port must be same
2. Click connect and wait for the target to do the same way
3. If both of you are connected there will be a message 
```

## Bugs
This application is my first chat application. I learned how to make a chat application from YouTube, of course this application has many bugs. There are a few bugs that I know, namely:
1. Your message is not delivered to the target 
   |    This can be happen because the UDP Protocol does not use an Identifier Number and UDP cannot detect the lost packets, if you want to fix it, try using TCP / IP      |
   |    Or this can happen if the port is used by some other application. Try to change the port    |
   
2. Your message is sent twice
   |    I am still finding out why this happened, but I am pretty sure that this bug could occur due to the UDP Protocol        |

That's all i know so far....

## Contributing
Pull requests if you fix some bug or you put more feature to this app but please tell me the details. I would be very grateful if you did

# Update
I never update this app again, because I don't have time ... and I only updated Readme because my friend told me the bug
