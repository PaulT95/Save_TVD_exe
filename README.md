# Save_TVD_exe
This is a Command Line Exe file that you can recall every time you want to save a TVD file open in EchoWave II

to use: SaveECO_MultiUS.exe filename It will save the TVD file as filename.tvd in the folder selected in the EchoWave II options. But if you set up the entire path like "C:\Users\User\Video\filename.tvd" it will save it in this path.

It works even with multiple EchoWave II windows opened. However to make it as useful and easy as possible, it will save all the file like filename_ECO1.tvd filename_ECO2.tvd filename_ECOx.tvd

To call it in Spike2 for example use ProgRun("Path\to\the\SaveECO_MultiUS.exe filename")

PS: as Spike2 function ProgRun() need an entire string, I highly reccomend that you create a dialog box where you type your filename and concatenate it to the SetFilePath() used in beginning of spike2 script. In this way you can save all TVD, Spike2 files (MAT for example), in the same folder set up in the beginning and with same name for each trial (if there are more).

PPS: it works in any scenario, however in this setup it worked with FrameLine In Sync options. If the US receives the 1000 Impulses (square wave 100Hz * 10sec) then it will stop, but  I noticed that even if it saved correctly each TVD with the correct numbers of frames, the dimension of each file got bigger and bigger, like EchoWave didn't flush the Cache\Memory with frameline in mode (I don't know whether this happens with other sync options), and it requests to tap freeze/run two times: 1 for stopping and flush 2 for re-armed for recording.

PPPS: if you want the exe file just mail me because I couldn't upload it here in GitHub due to the size

