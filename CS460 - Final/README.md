# CS460 Final Project
This project was to create a website to simulate coffee shop orders displayed on a web page in real-time and a section for employees to mark orders as completed.
ID_1.md, ID_2.md, items.md, order_generator.py were provided by the professor. Everything else was made by the student from scratch.

# To Run
1. Using a local SQL server, run up.sql, and then seed.sql in the data folder. You can run down.sql afterwards to delete.
2. Configure a connection string in line 23 on program.cs and appsettings.json.
3. Run ```dotnet build``` and ```dotnet run``` in the ```School-Work/CS460 - Final/HW6/HW6``` folder
4. Navigate to ``` http://localhost:5164/```  on a web browser
5. Run the python script order_generator.py to generate orders with the following cmd prompt in the data folder ```python order_generator.py http://localhost:5164/api/runOrderGenerator 0.25 40 ```