cd ./Src
call npm i
call npm run build
cd ..

dotnet build

rmdir ./bin/Debug/net6.0/wwwroot
move ./wwwroot ./bin/Debug/net6.0/

echo finishing