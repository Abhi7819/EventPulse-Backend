📘 EventPulse API – README

🏷️ Overview
EventPulse API is a clean and modular event management system built using ASP.NET Core, Entity Framework Core, and SQL Server.
It includes authentication, role-based access, session management, and attendee feedback features.

🚀 Features

🔐 JWT Authentication
👤 Role-based Authorization
Admin → Full access
Organizer → Manage only own events/sessions
Attendee → Submit feedback only
📅 Event CRUD (Create, Update, Delete, View)
🕒 Sessions inside Events
⭐ Feedback on sessions
🗄 Auto Database Migration & Seed Users
📦 Repository + Service Architecture

⚙️ 1. Setup Instructions

Step 1 — Update Connection String
Open:
appsettings.Development.json
Update:
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=EventPulseDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
Save the file.

Step 2 — Run the Application
Just run the project:
Your database will be created automatically, migrations will run, and default users will be seeded.
✔ No manual steps required
✔ No migration commands required
✔ SeedData initializes automatically

🌱 Default Seed Users
The system automatically creates 3 default users:

1️⃣ Admin
Email: admin@eventpulse.com
Password: Admin@123
Role: Admin

2️⃣ Organizer
Email: organizer@eventpulse.com
Password: Organizer@123
Role: Organizer

3️⃣ Attendee
Email: attendee@eventpulse.com
Password: Attendee@123
Role: Attendee

🔑 Authentication Flow
Login Request
{
  "email": "admin@eventpulse.com",
  "password": "Admin@123"
}

Login Response
{
  "success": true,
  "message": "Login successful",
  "data": {
    "token": "JWT_TOKEN",
    "role": 0,
    "roleName": "Admin"
  }
}

Use the token in headers

🧩 Role Permissions Summary

| Role          | Create Events | View Events | Update Own Events | Delete Own Events | Create Sessions | View Sessions | Submit Feedback       |
| ------------- | ------------- | ----------- | ----------------- | ----------------- | --------------- | ------------- | --------------------- |
| **Admin**     | ✅ Yes         | ✅ All       | ✅ All             | ✅ All             | ✅ Yes           | ✅ All         | ❌ No                  |
| **Organizer** | ✅ Yes         | ✅ All       | ✅ Only Their Own  | ✅ Only Their Own  | ✅ Yes           | ✅ All         | ❌ No                  |
| **Attendee**  | ❌ No          | ✅ All       | ❌ No              | ❌ No              | ❌ No            | ✅ All         | ✅ Yes (1 per session) |


