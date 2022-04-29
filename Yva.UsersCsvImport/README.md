### This repository contains Yva integration API Examples.
#### For information how to run examples - check README.md in specific project folder. 

#### Example steps:
    1. Loads file from hard driver (users-import-example-fixture.csv)
    2. Uploads it to Yva
    3. Checks in a loop for file processing to over (success/validation fail)
    4. In case of validation failure - requesting errors explanation from yva page by page

#### Before run
    1. Issue team token in your Yva admin panel
    2. Paste this token instead of {yours_teams_api_token} in appsettings.json file
    3. Change {yours_yva_address} in appsettings.json to your Yva instance address
    4. Also you can change path to csv file by changing 'CsvFilePath' value in appsettings.json

#### Requirements:
    .NET 6 SDK
#### Example build and run
Open console in example folder and write:

    dotnet build -c Release ./Yva.UsersCsvImport.csproj
    dotnet ./bin/Release/net6.0/Yva.UsersCsvImport.dll
