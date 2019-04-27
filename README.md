# Project Demo2

## About project
This project is the second demonstration project for the DP-157 group.
The project includes programming, administering a web server, configuring, developing and administering databases.

## Goal project "Demo2" 
Deploy the test environment according to the structure shown in the figure.
When carrying out the project, the principles were taken into account:
- assembly of containers from original images (clean build).
- security (passwords are stored in one file in a safe place).
- before initializing the database, creating the database structure and importing data are checked.
- use sticky sessions when working with Load Balancer.

## Environment diagram

![image](https://github.com/vitalidn/Demo2/blob/master/ENVIRONMENT%20DIAGRAM.jpg)

## Technology stack
### For realise the main task has chosen next Tools
* Docker Desktop Community version 2.0.0.03(31259)
   - Engine: 18.09.2 	
   - Compose: 1.23.2 	
   - Machine: 0.16.1 	

### For realise the main task has chosen next docker images:
* SQL Server 2017 v14.00.3017 (mcr.microsoft.com/mssql/server:2017-latest-ubuntu)
* .NET Core SDK v2.2.104 (microsoft/dotnet:2.2-sdk)
* ASP.NET Core Runtime v2.2.104 (microsoft/dotnet:2.2-aspnetcore-runtime)
* HAProxy v1.9.5(haproxy:latest)

## Runbook
1. Download and install Docker Desktop for Windows and Visual Studio 2017 .
2. Create a new directory for your application.
3. Create web application in Visual Studio 2017 using Entity Framework Core.
4. Create a Dockerfile within your WebApp directory and add the following content:
		
		FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
		WORKDIR /app
		EXPOSE 80
		FROM microsoft/dotnet:2.2-sdk AS build
		WORKDIR /src
		COPY ["Shop/Shop.csproj", "Shop/"]
		RUN dotnet restore "Shop/Shop.csproj"
		COPY . .
		WORKDIR "/src/Shop"
		RUN dotnet build "Shop.csproj" -c Release -o /app
		FROM build AS publish
		RUN dotnet publish "Shop.csproj" -c Release -o /app
		FROM base AS final
		WORKDIR /app
		COPY --from=publish /app .
		ENTRYPOINT ["dotnet", "Shop.dll"]

5. Create a Dockerfile within your Load Balancer directory and add the following content:

		FROM haproxy:1.7
		COPY haproxy.cfg /usr/local/etc/haproxy/haproxy.cfg
		
Create a "haproxy.cfg" file for configuration LoadBalancer.

		global
			maxconn 2000
		defaults
			mode    http
			balance roundrobin	
			timeout connect 7000ms
			timeout client 70000ms
			timeout server 70000ms
		frontend http-in
			bind *:80
			default_backend web_browser
		backend web_browser
			#Enable sticky sessions used for sticking a client to a particular server
			cookie SRVNAME insert
			server shop1 shop1:80 cookie shop1 check 
			server shop2 shop2:80 cookie shop2 check
	
6. Create a Dockerfile within your MS SQL directory and add the following content:

        	FROM mcr.microsoft.com/mssql/server:2017-latest-ubuntu
        	ARG ACCEPT_EULA=${ACCEPT_EULA}
        	ARG USER_ID=${USER_1}
        	ARG SA_PASSWORD=${PASSWORD_1}
        	RUN mkdir -p /usr/src/app
        	WORKDIR /usr/src/app
        	COPY . /usr/src/app
        	RUN chmod +x /usr/src/app/initdb.sh
        	EXPOSE 1433
        	RUN /usr/src/app/initdb.sh
			
	
You also need to create script files to initialize the database, create table and import data.
You can use the already created files in the repository /sqlserver.
		
7. In the root folder of the project create a docker-compose.yml file and add the following content:
       
        	version: "3"
        	services:
        	haproxy:
        		build: ./loadbalancer
          		container_name: haproxy
          		ports:
            			- "80:80"
         		depends_on:
             			- shop1
             			- shop2
        	shop1:
       			build: ./webappshop
          		container_name: shop1
          		ports:
             			- "8081:80"
          		depends_on:
             			- mssqldb
        	shop2:
          		build: ./webappshop 
          		container_name: shop2
          		ports:
             			- "8082:80"
          		depends_on:
             			- mssqldb
        	mssqldb:
                	build: 
        		context: ./sqlserver
        		args: 
                		- ACCEPT_EULA=${ACCEPT_EULA}
                		- USER_ID=${USER_1}
                		- SA_PASSWORD=${PASSWORD_1}
        		env_file: 
            			- .env
			container_name: mssqldb
          		ports:
             			- "1433:1433"
			
     
For the safe transfer of usernames and passwords, you must create a file with the extension .env.
It is possible to store logins and passwords in the file, having previously encrypted it. 
For security reasons, the example does not indicate the real login and password, but you can specify any one at your discretion.

			ACCEPT_EULA=Y
			USER_1=<USER_LOGIN>
			PASSWORD_1=<USER_PASSWORD>

8. Run PowerShell as Administrator in the project root folder and execute next commands:

        		docker-compose up --build -d

9. Go to your browser and type:

        		localhost
			 
You will see the welcome page.
![image](https://github.com/vitalidn/Demo2/blob/master/PrintScreen1.jpg)
    
Then follow the link to the page with Foodstuffs.
![image](https://github.com/vitalidn/Demo2/blob/master/PrintScreen2.jpg)

## It's all!!! Good luck!
