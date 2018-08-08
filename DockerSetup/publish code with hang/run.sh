#~ in linux means user home dir.
#/ means root dir.
#linux is case sensitive.
#--rm is remove the container after it stop.(not show in dcoker ps -a)
docker run --rm -it -p 9000:80 -v ~/hellocore:/wwwroot/ -d microsoft/aspnetcore --name hellocoreContainer dotnet /wwwroot/HelloCore.dll
