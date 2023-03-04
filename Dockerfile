FROM mcr.microsoft.com/dotnet/sdk:6.0

WORKDIR /app

COPY . .

EXPOSE 5138

RUN dotnet dev-certs https --trust

CMD ["dotnet", "run"]
