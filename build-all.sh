#!/usr/bin/env bash
set -e

echo "Building process started..."

docker-compose down

cd src\KittenGenerator
docker build --no-cache -t kitten-generator-api -f dockerfile .

cd ..

cd UserManagement
docker build --no-cache -t user-management-api -f dockerfile .

docker-compose up -d --force-recreate

cd ..\..

echo "Building process completed."