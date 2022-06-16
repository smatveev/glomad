for %%I in ( *.jpg ) do magick "%%I" -set filename: "%%t_300" -quality 50 -resize 20%% "%%[filename:].jpg"
pause