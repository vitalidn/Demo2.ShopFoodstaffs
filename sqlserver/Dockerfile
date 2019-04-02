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