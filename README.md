# Free4GameDevsX2Scaler
A free pixel-art upscaler for Unity using the Creative Commons 0 license, designed specifically for game developers. Open-source and for use commercially and non-commercially, both the source and what you run through it are both completely free to modify, use and sell.

# Current Version
1.0

# Installation
Free4GameDevsX2-master must either be in the Assets root directory, or the Standard Assets
directory (the latter is recommended!). It can be named either 'Free4GameDevsX2' or 
'Free4GameDevsX2-master' (default Github name).

# How To Use
Drag your files into the 'InputFolder'. Unity will automatically change it's texture 
settings to work with the Scanner. SVG and JPG files may work, right now it's compatible with 
.PNG files mainly.

You add the script 'Free4GameDevsX2' to an inspector on a game object in your scene. You then 
hit the 'Scale!' button and it takes what's in the InputFolder, processes it, and places it 
in the same folder hierarchy in the OutputFolder with the same exact name. 

# IMPORTANT
Your image needs to be pixel perfect for the best results (not enlarged). Alpha is also not supported
as of this version.

If you drag your image into the InputFolder these settings should be set for you automatically, 
however if for whatever reason they aren't, these are the Unity settings for the texture
that you want: 

+ Advanced - > Non-Power of 2: NONE  (this is REALLY important!!)
+ Advanced - > Read/Write Enabled: TRUE
+ Filter Mode - > Point (no filter)
+ Format: Automatic

If Non-Power of 2 is set to anything but NONE, the texture will not run through the algorithm properly
and will come out blurry. 

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

So this is a new scaling algorithm that's licensed with the Creative Commons 
CC0 1.0 Universal license, the freest license available in 2020, and does not use
a shred of code or thought from previous scalers.

You can truly use this any way you want, improve it and tweak it the way you want it to 
be, sell your games and not fear legal consequences.

------------------------------------------------------------------------------------------

# How Is It Different?

F4GDX2 scaling doesn't work in the same way that xBR, HQx, Eagle or xBRZ, etc... do, but it is 
fairly similar in some ways. It converts a Unity
Texture2D into a List<HSLColor>() and takes snapshots across the texture, detecting shapes, 
changing the List, then outputting it back into a Texture2D and saving it.

I've commented the code, it's not a whole lot but it was a pain in the ass to get up
and working. Everything is there to make modifications and improve on it now, though.

If you'd like to contibute to the source code of the project, I do ask that you keep it 
free for the game development community, but I'm not going to control you with a GPL license 
in order to do that or come after you later with a twitter post or something.

# Contact
If you like it, use it, if you make changes or add to it, e-mail astralojia@gmail.com, 
I'd love to hear about it!

------------------------------------------------------------------------------------------
