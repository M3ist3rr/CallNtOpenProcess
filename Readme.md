# CallNtOpenProcess

This is just a simple PoC of windows syscall with the NtOpenProcess API in C#.

As we can see, we don't pass by the ntdll module (this means no EDR hooks):

<p align="center">
	<img width="550" height="200" src="https://i.imgur.com/gGc6W5g.png">
</p>