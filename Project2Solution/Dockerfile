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

FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS ConsumeAPI

WORKDIR /app

COPY ConsumeAPI/ ../ConsumeAPI

COPY --from=Project.Client ./Project.Client/ ../Project.Client

RUN dotnet restore ../ConsumeAPI/*.csproj

RUN dotnet build ../ConsumeAPI/*.csproj --no-restore -c Release

RUN dotnet publish ../ConsumeAPI/*.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS deploy

WORKDIR /app

EXPOSE 80
EXPOSE 443 

COPY --from=Project.Client Project.Client/out/ ./

CMD [ "dotnet", "Project.Client.dll" ]


