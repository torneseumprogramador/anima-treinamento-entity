# Imagem base
FROM mcr.microsoft.com/dotnet/aspnet:7.0

# Define o diretório de trabalho
WORKDIR /app

# Copia a pasta publish para dentro do contêiner Rodar o comando no terminal 'dotnet publish -c Release -o out'
COPY ./out .

# Copia o arquivo appsettings.docker.json e renomeia para appsettings.json
COPY appsettings.docker.json appsettings.json

# Define o comando de entrada
ENTRYPOINT ["dotnet", "entity.dll"]
