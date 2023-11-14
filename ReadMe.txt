--# C:\xampp\php\php.ini
	{line.old} short_open_tag=Off
	{line.new} short_open_tag=On

	{line.old} ;extension=pdo_odbc
	{line.new} extension=pdo_odbc

--# Task Scheduler
	{ start a program } C:\xampp\php\php.exe -f C:\xampp\htdocs\includes\cron\cron.php
