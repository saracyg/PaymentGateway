FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR webapp

# copy csproj file and restore
COPY ./PaymentGateway.Contract/*.csproj ./PaymentGateway.Contract/
RUN dotnet restore ./PaymentGateway.Contract/*.csproj

COPY ./PaymentGateway/*.csproj ./PaymentGateway/
RUN dotnet restore ./PaymentGateway/*.csproj

# Copy everything else and build
COPY ./PaymentGateway/ ./PaymentGateway/
COPY ./PaymentGateway.Contract/ ./PaymentGateway.Contract/

RUN dotnet publish ./PaymentGateway/*.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /webapp
COPY --from=build /webapp/out .
ENTRYPOINT ["dotnet", "PaymentGateway.dll"]