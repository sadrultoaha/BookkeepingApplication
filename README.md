# Bookkeeping App
##### It's a sample app to keep record of day to day income and expenses informations.


## Tech Stacks:
### Front End: NodeJs 18.2, VueJs - 3, JavaScript;

### Backend: ASP.NET Core - 5 Web API, C# 9;

### DB: SQL Server 2019, MSSQL - 15;

### Guideline for Project Setup and Steps to Run :

## 1. Environment setup.

#### Setup and Run Sql Server *version >=2019*, .NET Core SDK *version >=5*, Visual Studio *version >=2019*, NodeJS *version >= 18.2, VueJS *version >= 3.2*.

## 2. Source Code Download.

#### Clone the repo into your local environment using the bash command -

``` Git clone https://github.com/sadrultoaha/BookkeepingApplication.git ``` .

## 3.DB and Tables Creation.

#### Run following sql script in sql server database to create db and tables, from project directory 

--> `BookkeepingApplication/Sql scripts/DBandTableCreation.sql`

#### If you want to fast preview the application without creating any predefined data form the pages, then you can run following sql script in sql server database to prepare few sample dataset from project directory

--> `BookkeepingApplication/Sql scripts/PredefinedIncomeExpenseDataEntry.sql`

## 4.ASP.NET Project Run.

#### Execute the .sln file from project directory --> `BookkeepingApplication/BookkeepingApi/BookkeepingApi.sln`

## 5.Vue Js Project Run.

#### Project setup and install package dependacies by going to the project directory then open a command prompt with this directory --> `BookkeepingApplication/BookkeepingApp` and execute following command.
```
npm install
```
### and 

#### Run frontend server using the following command:
```
npm run serve
```

## Now you can use the Bookkeeping App :) 

