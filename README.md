# Project Setup Guide

This project consists of:

- **Frontend:** Angular 19
- **Backend:** .NET 8 API
- **Database:** PostgreSQL (running in Docker)
- **Database Management:** pgAdmin (running in Docker)

---

1. open terminal in root and run:
  docker-compose up -d
2. In DeskBooking.API application.json add:
     "ConnectionStrings": {
      "DefaultConnection": "Host=localhost;Port=5432;Database=DeskBooking;Username=postgres;Password=12345"
    },
3. Open Angular UI and install packages via npm install 
4. Start DeskBooking.API
5. Start DeskBooking.UI (provided with submodule)
