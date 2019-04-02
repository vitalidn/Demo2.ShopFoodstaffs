#Start SQL Server & wait for the SQL Server to come up
/opt/mssql/bin/sqlservr & sleep 90s

#This script (into file "create-db.sql") create the DB after check
/opt/mssql-tools/bin/sqlcmd -S localhost -U ${USER_ID} -P ${SA_PASSWORD} -d master -i create-db.sql

#This script (into file "create-table.sql") create the Table after check 
/opt/mssql-tools/bin/sqlcmd -S localhost -U ${USER_ID} -P ${SA_PASSWORD} -d master -i create-table.sql

#This script (into file "import-data.sql") import data after check
/opt/mssql-tools/bin/sqlcmd -S localhost -U ${USER_ID} -P ${SA_PASSWORD} -d master -i import-data.sql

#Stop SQL Server
pkill sqlservr
