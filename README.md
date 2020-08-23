# ShopBridge
A small web app with crud operations for adding items to the inventory


Pre requisites

1). Visual Studio( 17 or higher) for .Net Core Project
2). Visual Studio Code,NodeJS(v12.17.0),angular CLI for Angular Project
3). MS SQL Server for Database

Available Items(ShopBridgeUI,ShopBridgeAPI, DbScript):

1). Setup your DB with the given script or run the migration code using "update-database" command from API project.

2). ShopBridgeUI: Contains angular code without node modules. Please make sure to run "npm install" command to contain all the node packages.
   
Steps:
a). npm install
b). ng build(to check if any compile error is there ot not)
c). Make sure the end point directs to the local host your API is running on in environment.ts
d). ng serve run(to run the project)
e). Use dist folder for deployment
f). Port : http://localhost:4200/

3). ShopBridgeAPI: Contains .Net core 2.0 with 6 internal projects following DOMAIN DRIVEN PATTERN:
a). ShopBridgeAPI
b). ShopBridge.Contracts
c). ShopBridge.Service
d). ShopBridge.Repository
e). ShopBridge.DAL
f). ShopBridge.UnitTest(X unit tests)
g). Port : http://localhost:49921/

Steps:
a). Restore nuget packages and build before running
b). Change the DB connection string to your local system server in appsettings.json
c). Run the project on IIS Express making API project as startup project.
