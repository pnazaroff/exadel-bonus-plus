FROM ubuntu:18.04
USER root
RUN apt-get update; \
  apt-get install -y apt-transport-https && \
  apt-get update && \
  
RUN wget -nv https://packages.microsoft.com/config/ubuntu/18.04/packages-microsoft-prod.deb \
  dpkg -i packages-microsoft-prod.deb \
  apt-get install -y aspnetcore-runtime-3.1
 
 
 #RUN and SETUP NGINX 
RUN apt-get install nginx
RUN service nginx start
COPY ./default /etc/nginx/sites-available/default
RUN nginx -s reload


#CONFIGURE DOTNET SDK
RUN wget https://packages.microsoft.com/config/ubuntu/18.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
RUN dpkg -i packages-microsoft-prod.deb
RUN sudo apt-get update; \
  sudo apt-get install -y apt-transport-https && \
  sudo apt-get update && \
  sudo apt-get install -y dotnet-sdk-3.1

# BUILD AND DEPLOY 
COPY /home/rglr/exadelBonusPlus /home/exadelBonusPlus
RUN cd /home/exadelBonusPlus
RUN dotnet publish --configuration Release

#RUN cp /home/exadelBonusPlus /var/www/exadel-bonux-plus

#RUN cp ./kestrel-test.service /etc/systemd/system/


#RUN systemctl enable kestrel-test.service
#RUN systemctl start kestrel-test.service 

EXPOSE 80


ENTRYPOINT ["dotnet", "ExadelBonusPlus.WebApi.dll"]
