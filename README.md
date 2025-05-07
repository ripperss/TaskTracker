## 📌 О проекте

TaskTracker - это система для управления задачами с поддержкой командной работы, комментариев и ролевой моделью доступа. Проект разрабатывается на чистой архитектуре (Clean Architecture) с использованием .NET 9.

## 🚀 Возможности

- ✅ Регистрация и аутентификация пользователей (JWT)
- ✅ Создание/редактирование задач
- ✅ Комментирование задач
- ✅ Работа в командах
- ✅ Ролевая модель (User/Manager/Admin)
- ✅ Событийная модель (Domain Events)
- ✅ Подключаться к задаче
- ✅ микросервис для увидомлений(планируется)

## 🛠 Технологический стек

### Backend
- **Язык**: C# 10
- **Фреймворк**: .NET 9
- **Архитектура**: Clean Architecture (Domain → Application → Infrastructure → API)
- **База данных**: PostgreSQL (с Entity Framework Core)
- **Аутентификация**: JWT + Identity
- **Медиатор**: MediatR
- **Валидация**: FluentValidation
- **Логирование**: Serilog

### Frontend (планируется)
- Angular/React (будет добавлен позже)

## 📂 Структура проекта

```
TaskTracker/
├── src/
│   ├── TaskTracker.Domain/        # Ядро системы (сущности, бизнес-правила)
│   ├── TaskTracker.Application/   # Use Cases, CQRS
│   ├── TaskTracker.Infrastructure/# Реализация репозиториев, Identity, БД
│   └── TaskTracker.API/          # Web API (контроллеры)
├── tests/
│   ├── Domain.Test/                 # Юнит-тесты(домен
│   └── IntegrationTests/          # Интеграционные тесты 
```

## 🔧 Установка и запуск

### Требования
- .NET 6 SDK
- PostgreSQL 14+
- Docker (опционально)

### Запуск в development
1. Клонируйте репозиторий:
   ```bash
   git clone https://github.com/yourusername/TaskTracker.git
   cd TaskTracker
   ```

2. Настройте базу данных:
   ```bash
   cd src/TaskTracker.API
   dotnet ef database update
   ```

3. Запустите проект:
   ```bash
   dotnet run
   ```

4. API будет доступно по адресу: `https://localhost:5001`

### Docker
```bash
docker-compose up -d
```

## 📝 Документация API

После запуска проекта документация Swagger доступна по адресу:  
`https://localhost:5001/swagger`

> **Note**: Этот проект находится в активной разработке. API и структура могут изменяться.
```
