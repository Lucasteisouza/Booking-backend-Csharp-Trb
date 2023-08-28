#!/bin/bash
echo "iniciando dockerfile"
echo "Dockerfile script executed from: ${PWD}"

rm dockerlog.txt

echo "Construindo imagem"
docker build --tag trybehotel:teste1 ../TrybeHotel/

echo "Construindo container"
docker run --name trybehotel_teste1 -d trybehotel:teste1

echo "Aguardando pelo container"
sleep 20

echo "Checando logs"
docker logs trybehotel_teste1 &> dockerlog.txt

echo "Removendo container"
docker rm -f trybehotel_teste1

echo "Removendo imagem"
docker image rm --force trybehotel:teste1
