FROM mcr.microsoft.com/dotnet/sdk:5.0 AS builder
WORKDIR /src
COPY ./OrleansTest.csproj .
RUN dotnet restore OrleansTest.csproj
COPY . .
RUN dotnet build OrleansTest.csproj -c Debug -o /src/out

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=builder /src/out .

EXPOSE 80
ENTRYPOINT ["dotnet", "OrleansTest.dll"]
