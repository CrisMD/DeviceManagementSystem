# DeviceManagement

A Device management system, which will keep track of all the mobile devices the
company owns, all the details of the device, location, and who is using it at the moment.

Device details:
  - Name
  - Manufacturer
  - Type (phone, tablet)
  - Operating System (OS)
  - OS version
  - Processor
  - RAM amount
  
Users:
  - Name
  - Role
  - Location
  
Technologies used:
- DB: Oracle
- Programming language: C#
- UI: AngularJS
- Web Services: WCF 

Functionalities:
  - Login 
  - Register
  - Show all devices in a list (with the user who is using it), 
  - Select a device to view its details
  - Create a new device
  - Update an existing device
  - Delete a device directly from the list
  - A user will be able to assign a device to himself if it is not used by another user
  - A user will be able to unassign a device previously assigned to himself
