%windows docker%
%build image first use "docker build . -t imageName" in location of dockerfile%
%set workfolder must be the same as dockerfile%
%must set start dll, due to the enterypoint has been set to 'dotnet' in dockerfile%
%-e is set system enviroment variable, if wanna to change port can set here or can be omit%

docker run --restart always -p 8080:80 -v D:\Site\EFChallenge\publish:c:\app -e "ASPNETCORE_URLS=http://+:80" -d efvideo --name efvideoContainer EFVideo.dll
