@echo off
SETLOCAL ENABLEDELAYEDEXPANSION

echo Deteniendo y eliminando contenedores, redes y volúmenes...
docker-compose down -v

echo Eliminando todas las imágenes usadas...
docker-compose down --rmi all

echo Eliminando volúmenes no utilizados...
docker volume prune -f

echo Levantando nuevos contenedores...
docker-compose up -d

echo Proceso completado.
