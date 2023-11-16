# Base image for building React app
FROM node:18-alpine as react-builder

# Set the working directory in the container for React app
WORKDIR /app/client

# Copy the package.json and package-lock.json files to the working directory
COPY client/package*.json ./

# Install dependencies for React app
RUN npm install

# Copy the entire React app directory to the working directory
COPY client/ .

# Build the React app
RUN npm run build


# Base image for building .NET project
FROM mcr.microsoft.com/dotnet/sdk:6.0  as dotnet-builder

# Set the working directory in the container for .NET project
WORKDIR /app/server

# Copy the .NET project files to the working directory
COPY MyShop/*.csproj ./

# Restore the NuGet packages
RUN dotnet restore

# Copy the entire .NET project directory to the working directory
COPY MyShop/ .

# Build the .NET project
RUN dotnet build "MyShop.csproj" -c Release -o /app/build

# Publish the .NET project
FROM dotnet-builder AS dotnet-publisher
RUN dotnet publish "MyShop.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:6.0 
WORKDIR /app
EXPOSE 80
COPY --from=dotnet-publisher /app/publish .
# Copy the built React app from the react-build stage to the wwwroot folder
COPY --from=react-builder /app/client/build ./wwwroot
ENTRYPOINT ["dotnet", "MyShop.dll"]
