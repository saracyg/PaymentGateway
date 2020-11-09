FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR webapp

# copy csproj file and restore
COPY ./PaymentGateway.Contract/*.csproj ./PaymentGateway.Contract/
RUN dotnet restore ./PaymentGateway.Contract/*.csproj

COPY ./BankApiMock/*.csproj ./BankApiMock/
RUN dotnet restore ./BankApiMock/*.csproj

# Copy everything else and build
COPY ./BankApiMock/ ./BankApiMock/
COPY ./PaymentGateway.Contract/ ./PaymentGateway.Contract/

RUN dotnet publish ./BankApiMock/*.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /webapp
COPY --from=build /webapp/out .
ENTRYPOINT ["dotnet", "BankApiMock.dll"]