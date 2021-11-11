# Save_TVD_exe
This is a Command Line Exe file that you can recall every time you want to save a TVD file open in EchoWave II

to use: SaveECO_MultiUS.exe filename It will save the TVD file as filename.tvd in the folder selected in the EchoWave II options. But if you set up the entire path like "C:\Users\User\Video\filename.tvd" it will save it in this path.

It works even with multiple EchoWave II windows opened. However to make it as useful and easy as possibile it will save all the file like filename_ECO1.tvd filename_ECO2.tvd filename_ECOx.tvd

To call it in Spike2 for example use ProgRun("Path\to\the\SaveECO_MultiUS.exe filename")

PS: as Spike2 function ProgRun() need an entire string, I highly reccomend that you create a dialog box where you type your filename and concatenate it to the SetFilePath() used in beginning of spike2 script. In this way you can save all TVD, Spike2 files (MAT for example), in the same folder set up in the beginning and with same name for each trial (if there are more).
