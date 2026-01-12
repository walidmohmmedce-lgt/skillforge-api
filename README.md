# SkillForge API

SkillForge is a backend Web API built with ASP.NET Core that models learning progression using skill trees, dependencies, and user progress.

The system simulates real-world logic such as:
- Skills having prerequisites  
- Users completing skills  
- New skills unlocking dynamically based on progress  

This project demonstrates practical backend engineering concepts, not just basic CRUD.

---

## Features

- Skill Trees (e.g. ".NET Backend Roadmap")
- Skills inside each tree
- Skill dependencies (prerequisites system)
- User progress tracking
- Dynamic skill unlocking logic
- RESTful API
- Swagger API documentation

---

## Tech Stack

- ASP.NET Core Web API  
- Entity Framework Core  
- SQL Server  
- C#  
- Swagger (OpenAPI)

---

## Example Logic

C# Basics → OOP → ASP.NET Core  

- If the user completes **C# Basics**, then **OOP** becomes available  
- If the user completes **OOP**, then **ASP.NET Core** becomes available  

This simulates real progression systems like learning platforms or game skill trees.

---

## How to Run Locally

1. Clone the repository:
```bash
git clone https://github.com/walidmohmmedce-lgt/skillforge-api.git
cd skillforge-api

2. Update the connection string in appsettings.json if needed:
"Server=.\\SQLEXPRESS;Database=SkillForgeDb;Trusted_Connection=True;TrustServerCertificate=True"

3. Run database migrations:
dotnet ef database update

4. Run the project:
dotnet run

5. Open Swagger in your browser:
https://localhost:XXXX/swagger



Quick Test Guide (Swagger)

After opening Swagger, you can test the API like this:

1. Create a skill tree

POST /api/skilltrees

{ "name": ".NET Backend Roadmap" }

2. Add skills

POST /api/skills

{ "name": "C# Basics", "skillTreeId": 1 }


POST /api/skills

{ "name": "OOP", "skillTreeId": 1 }

3. Add dependency (OOP requires C# Basics)

POST /api/skilldependencies

{ "skillId": 2, "requiredSkillId": 1 }

4. Mark skill as completed

POST /api/progress/complete/1

5. Check available skills

GET /api/skills/available/1

You will see that skills unlock dynamically based on completed prerequisites.

Author

Walid Mohmmed
.NET Backend Developer
GitHub: https://github.com/walidmohmmedce-lgt