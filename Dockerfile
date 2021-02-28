FROM mcr.microsoft.com/dotnet/aspnet:3.1
COPY ExadelBonusPlus.WebApi/bin/Release/netcoreapp3.1/ App/
WORKDIR /App
#RUN apt update
#RUN apt-get install -y nginx
#RUN service nginx start
COPY ./default /etc/nginx/sites-available/
EXPOSE 80
ENTRYPOINT ["dotnet", "ExadelBonusPlus.WebApi.dll"]
