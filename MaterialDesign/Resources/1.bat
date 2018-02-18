set fn=C:\Users\Ricar\Downloads\ImageMagick-7.0.7-22-portable-Q16-x64\convert.exe
for /f "tokens=*" %%i in ('dir/s/b *.png') do "%fn%" "%%i" -strip "%%i"