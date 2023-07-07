#!/bin/bash

cd ui
dotnet publish --output publish

cd ../

docker-compose build

# Inicia os contêineres do Docker Compose
docker-compose up -d

# Aguarda até que o contêiner do MySQL esteja pronto
echo "Aguardando o contêiner do MySQL iniciar..."
until docker exec mysql_anima_servico mysqladmin ping -uroot -pm9y5s6q3l -h localhost > /dev/null 2>&1; do
    sleep 1
done

# Pergunta ao usuário se deseja executar o comando de restauração
read -p "Deseja executar o comando de restauração? (s/n): " answer
if [ "$answer" != "${answer#[Ss]}" ]; then
    docker exec -i mysql_anima_servico sh -c 'mysql -uroot -p"m9y5s6q3l" entity_anima < /dump.sql'
fi
