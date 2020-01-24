# _Pierre's Sweet and Savory Treats_

#### _This web app allows a bakery owner to store lists of treats, their flavors, and the relationship between the two, 1.24.2020_

#### By _**Will Quanstrom**_

## Description

_A bakery owner is greeted by a homepage. They can then follow links to see a list of treats they sell, or a list of flavors available. They owner can also add new treats and flavors, and add treats to flavors and flavors to treats. _

## Setup/Installation Requirements

* _Clone the github repository @ https://github.com/wanderlust22/PierresTreats.Solution_
* _Restore dependencies using 'dotnet restore' command_
* _Create database schema and tables with 'dotnet ef database update' command_
* _Run web app locally with 'dotnet watch run' command_
* _Navigate to localhost:5000 in web browser_

## Specifications

| Specs  | Example Input  | Example Output  | 
|---|---|---|
| Owner/user is welcomed with a splash page  | localhost:5000/  | "Welcome {user.Name}! Click here to see your list of treats"  |  
| User has ability to add new treats and flavors to database and list  | "Crossiant", "Coconut"  | "Crossiant" now displays when user looks at their list of treats. "Coconut" displays in flavors. |  
| Treats 'details' page displays its flavors while flavors 'details' page displays list of treats it belongs to  | When you click the link of 'crossiant' on the treats list --  | You're shown the flavors "chocolate" and "almond"  |  

## Known Bugs

_None at this time._

## Support and contact details

_wquanstr215@gmail.com_

## Technologies Used

_C#, ASP.NET Core MVC, Razor, Entity, MySQL_

### License

*MIT License*

Copyright (c) 2020 **_Will Quanstrom_**