Write-host "Building process started..."  -ForegroundColor DarkGreen

docker-compose down

cd src\KittenGenerator\RealWorldOne.KittenGenerator.Api
docker build --no-cache -t kitten-generator-api -f dockerfile .

cd src\

cd UserManagement\RealWorldOne.UserManagement.Api
docker build --no-cache -t user-management-api -f dockerfile .

docker-compose up -d --force-recreate

Write-host "Building process completed."  -ForegroundColor DarkGreen