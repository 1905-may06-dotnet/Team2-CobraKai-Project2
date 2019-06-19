FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS Project.Domain

WORKDIR /app

COPY Project.Domain/ ../Project.Domain

RUN dotnet restore ../Project.Domain/Project.Domain.csproj --no-dependencies

RUN dotnet build ../Project.Domain/Project.Domain.csproj --no-restore -c Release 

FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS Project.Data

WORKDIR /app

COPY Project.Data/ ../Project.Data

COPY --from=Project.Domain ./Project.Domain/ ../Project.Domain

RUN dotnet restore ../Project.Data/*.csproj

RUN dotnet build ../Project.Data/*.csproj --no-restore -c Release

FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS Project.Client

WORKDIR /app

COPY Project.Client/ ../Project.Client

COPY --from=Project.Domain ./Project.Domain/ ../Project.Domain

COPY --from=Project.Data ./Project.Data/ ../Project.Data

RUN dotnet restore ../Project.Client/*.csproj

RUN dotnet build ../Project.Client/*.csproj --no-restore -c Release

RUN dotnet publish ../Project.Client/*.csproj -c Release -o out

#FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS deploy

#WORKDIR /app

#EXPOSE 80
#EXPOSE 443 

#COPY --from=[insert project to deploy]

#CMD [ "dotnet", "[select .dll to deploy]" ]

#to update for future added projects

#use the following commands:
#dotnet build
#dotnet publish -o out (TODO: create out folders)


