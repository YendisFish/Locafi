cd ./Src
npm run build
cd ..

dotnet build

rm ./bin/Debug/net6.0/wwwroot
mv ./wwwroot ./bin/Debug/net6.0/