# BlogProject

## Description
This is a simple blog API developed using ASP.NET Core 8, it implements JWT authorization, CRUD, image loading, DTO, pagination, resource protection, and convenient testing via Swagger UI.

## Technologies
- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- JWT authorization
- Swagger / Swashbuckle
- SQLite
- REST
- DTO
- IFormFile upload

## Project launch
1. Clone the repository
```bash
git clone https://github.com/VimeR-only/WebAPI
cd BlogProject
```
3. Create a database:
```bash
dotnet ef database update
```

4. Run the project:
```bash
dotnet run
```

5. Swagger UI:
```
http://localhost:xxxx/swagger
```

> **Note:** uploaded images are stored in `wwwroot/uploads`, and are accessible via `/uploads/{file}`.
---

## Query Examples

- `POST /api/Auth/register` – registration
- `POST /api/Auth/login` – login, get JWT
- `POST /api/Posts/with-image` – create a post with an image
- `GET /api/Posts?page=1&pageSize=5` – pagination
- `GET /api/Posts/{id}` – view a specific post

---

![chrome_1og3wSdHvK](https://github.com/user-attachments/assets/70b0b949-6302-4d15-8f5a-18f60c7c2c54)

## License

This project is free to use as part of a portfolio or teaching.
