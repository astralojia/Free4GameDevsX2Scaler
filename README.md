# Free4GameDevsX2Scaler
A free pixel-art upscaler for Unity using the MIT license, designed specifically for game developers. Open-source and for use commercially and non-commercially.

# How To Use
Drag your files into the 'InputFolder'. Unity will automatically change it's texture 
settings to work with the Scanner. SVG and JPG files may work, right now it's compatible with 
.PNG files mainly.

You add the script 'Free4GameDevsX2' to an inspector on a game object in your scene. You then 
hit the 'Scale!' button and it takes what's in the InputFolder, processes it, and places it 
in the same folder hierarchy in the OutputFolder with the same exact name. 

------------------------------------------------------------------------------------------

# Disclaimer
Hello, Astrah here! 

I created this open-source pixel art upscaling scanner so that anyone could be free
to use it in their games, free or commercial without needing to ask for an 
appropriate license or permission. 

The pixel-art scaling algorithms all have GPL licenses to protect their source code. 
They're made for the Emulation community and weren't created for the game development
scene. The problem with the GPL license is that it can be very vague. If you do use 
xBR, HQx or xBRZ, it's vague whether it would be considered a derivitive work if your
game happened to sell partially due to it having upscaled graphics by these algorithms. 

If you went and made a million dollars and your game sold mostly due to using xBRZ 
upscaling for example, and you didn't give back well....first of all, you're a douche, 
but second of all you could get sued. There's the problem today of mob law/twitter 
communities to deal with as well, so even if something isn't illegal, you want absolute
permission and to give proper credit to people.

F4GDX2 scaling doesn't work in the same way that xBR, HQx or xBRZ does, but it is 
fairly similar. It's not designed to be used as a shader, and it's much more simple
minded. It does a series of passes (13 as of writing this) that detect first
boxes then checker patterns, then try to fill in curves and lines. 

I've commented the code, it's not a whole lot but it was a pain in the ass to get up
and working, and everything is there to make modifications and improve on it.

If you'd like to contibute to the source code of the project, I do ask that you keep it 
free for the game development community, but I'm not going to control you with a GPL license 
in order to do that or come after you later with a twitter post or something.

I threw the MIT license on this upscaler, I mean for that to apply for the source code, 
the original 'algorithm' or whatever you'd call it, for if you use it to upscale or put 
fx on your graphics or textures as well.

You can truly use this any way you want, improve it and tweak it the way you want it to 
be, and not fear legal consequences, at least that's my intention.

------------------------------------------------------------------------------------------

# Contact
If you like it, use it, if you make changes or add to it, e-mail astralojia@gmail.com, 
I'd love to hear about it!

------------------------------------------------------------------------------------------
