FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env

#RUN git clone https://github.com/pnazaroff/exadel-bonus-plus/
WORKDIR /exadel-bonus-plus
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/aspnet:3.1
WORKDIR /app
COPY --from=build-env exadel-bonus-plus/out/ .
EXPOSE 80
ENTRYPOINT ["dotnet", "ExadelBonusPlus.WebApi.dll"]
