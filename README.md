# Save_TVD_exe
This is a Command Line Exe file that you can recall every time you want to save a TVD file open in EchoWave II

to use: SaveECO_MultiUS.exe filename It will save the TVD file as filename.tvd in the folder selected in the EchoWave II options. But if you set up the entire path like "C:\Users\User\Video\filename.tvd" it will save it in this path.

It works even with multiple EchoWave II windows opened. However to make it as useful and easy as possibile it will save all the file like filename_ECO1.tvd filename_ECO2.tvd filename_ECOx.tvd

To call it in Spike2 for example use ProgRun("Path\to\the\SaveECO_MultiUS.exe filename")

PS: as Spike2 function ProgRun() need an entire string, I highly reccomend that you create a dialog box where you type your filename and concatenate it to the SetFilePath() used in beginning of spike2 script. In this way you can save all TVD, Spike2 files (MAT for example), in the same folder set up in the beginning and with same name for each trial (if there are more).

PPS: it works in this case for FrameLine In. Thus after if the USreceive the 1000Impulses (100Hz * 10sec) then it will stops, but because I noticed that even if it saved correctly each TVD with the correct numbers of frames, the dimension of each one got bigger and bigger, like EchoWave didn't flush the Cache\Memory with frameline in, and it request to tap freeze/run two times: 1 for stopping and flush 2 for re-armed for receiveng other FrameLine In.

PPPS: if you want the exe file just mail me because I couldn't upload it here in GitHub due to the size

