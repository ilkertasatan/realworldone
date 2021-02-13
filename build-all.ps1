Write-host "Building process started..."  -ForegroundColor DarkGreen

docker-compose down

cd src\KittenGenerator
docker build --no-cache -t kitten-generator-api -f dockerfile .

cd ..

cd UserManagement
docker build --no-cache -t user-management-api -f dockerfile .

docker-compose up -d --force-recreate

cd ..\..

Write-host "Building process completed."  -ForegroundColor DarkGreen