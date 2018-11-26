FROM microsoft/dotnet:sdk as builder  
 
RUN mkdir -p /root/src/app/dotnet-core-api
WORKDIR /root/src/app/dotnet-core-api
 
COPY Elasticsearch.sln . 
COPY Api/Api.csproj ./Api/Api.csproj
COPY Models/Models.csproj ./Models/Models.csproj
COPY Elastic.Repository/Elastic.Repository.csproj ./Elastic.Repository/Elastic.Repository.csproj
COPY Cached.Elastic.Repository/Cached.Elastic.Repository.csproj ./Cached.Elastic.Repository/Cached.Elastic.Repository.csproj
COPY Services/Services.csproj ./Services/Services.csproj 
COPY Tests/Tests.csproj ./Tests/Tests.csproj 
RUN dotnet restore

COPY . .
RUN dotnet build
RUN dotnet test Tests/Tests.csproj
RUN dotnet publish -c Release -o published Api/Api.csproj

FROM microsoft/dotnet:aspnetcore-runtime as dotnet-alpine

WORKDIR /root/  
COPY --from=builder /root/src/app/dotnet-core-api/Api/published .
ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000/tcp
CMD ["dotnet", "./Api.dll"]  