FROM mcr.microsoft.com/mssql/server:2022-latest
WORKDIR /app
EXPOSE 1433

# Install MSSQL tools dependencies
RUN apt-get update && \
    apt-get install -y curl apt-transport-https gnupg2 && \
    apt-get clean

# Import the Microsoft repository GPG keys
RUN curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add -

# Register the Microsoft SQL Server Ubuntu repository
RUN curl https://packages.microsoft.com/config/ubuntu/20.04/prod.list > /etc/apt/sources.list.d/mssql-release.list

# Update apt-get and install MSSQL tools
RUN apt-get update && \
    ACCEPT_EULA=Y apt-get install -y mssql-tools unixodbc-dev && \
    apt-get clean

# Add the sqlcmd to the PATH
ENV PATH="$PATH:/opt/mssql-tools/bin"

# Set the work directory
WORKDIR /app

# Copy your application files
COPY . .

# Set the entrypoint command
ENTRYPOINT [ "sqlcmd" ]

COPY /home/user/Schedule/init.sql /app
CMD sqlcmd -S localhost -U sa -P "P@ssw0rd" -i init.sql

