# Rent A Car

Уеб приложение за управление на наемане на автомобили, разработено с ASP.NET Core Razor Pages.

## Технологии

- ASP.NET Core (.NET 10) — Razor Pages
- Entity Framework Core — SQLite
- ASP.NET Core Identity — автентикация и роли
- Bootstrap 5 — потребителски интерфейс

## Архитектура — трислойна

Проектът следва трислоен модел (3-Layer Architecture):

```
┌──────────────────────────────────┐
│     Presentation Layer           │
│     (Razor Pages / UI)           │
│     Pages/                       │
├──────────────────────────────────┤
│     Services Layer               │
│     (Бизнес логика)              │
│     Services/                    │
├──────────────────────────────────┤
│     Data Layer                   │
│     (Entity Framework + Models)  │
│     Data/                        │
└──────────────────────────────────┘
```

**Data Layer** — съдържа моделите (`ApplicationUser`, `Car`, `Reservation`) и `ApplicationDbContext`, който комуникира с SQLite базата чрез Entity Framework Core.

**Services Layer** — съдържа интерфейси и имплементации за бизнес логиката:
- `ICarService` / `CarService` — CRUD операции за автомобили
- `IReservationService` / `ReservationService` — управление на заявки + проверка за заетост на кола
- `IUserService` / `UserService` — администрация на потребители

**Presentation Layer** — Razor Pages, които показват данните на потребителя и обработват формуляри. Навигацията и достъпът се управляват чрез роли (Admin / User).

## ER Диаграма

```
┌─────────────────────┐       ┌─────────────────────┐
│   ApplicationUser   │       │        Car           │
├─────────────────────┤       ├─────────────────────┤
│ Id (PK)             │       │ Id (PK)             │
│ UserName (unique)   │       │ Make                │
│ Email (unique)      │       │ Model               │
│ FirstName           │       │ Year                │
│ LastName            │       │ Seats               │
│ Egn (unique)        │       │ Description         │
│ PhoneNumber         │       │ PricePerDay         │
│ PasswordHash        │       └─────────┬───────────┘
└─────────┬───────────┘                 │
          │                             │
          │ 1:N                    1:N  │
          │                             │
          │    ┌─────────────────────┐   │
          └────┤    Reservation      ├───┘
               ├─────────────────────┤
               │ Id (PK)            │
               │ StartDate          │
               │ EndDate            │
               │ UserId (FK)        │
               │ CarId (FK)         │
               └─────────────────────┘
```

- Един потребител може да има много резервации (1:N)
- Един автомобил може да има много резервации (1:N)
- Уникални ограничения: ЕГН, имейл, потребителско име

## Функционалности

- Регистрация и вход с парола
- Роли: Admin и User
- Публичен списък с автомобили
- Търсене на свободни коли по период
- Създаване на заявка за наем
- Преглед и отказване на собствени заявки
- Admin: CRUD за автомобили
- Admin: преглед на всички заявки
- Admin: CRUD за потребители
- Валидации: ЕГН (10 цифри), имейл, телефон, забрана за дублиране
- Проверка дали кола е свободна за избрания период

## Стартиране

```bash
dotnet run --project RentACar/RentACar.csproj
```

Приложението стартира на `http://localhost:5198`.

Администраторски акаунт по подразбиране:
- Потребител: `admin`
- Парола: `Admin123!`

## Структура на проекта

```
RentACar/
├── Data/
│   ├── Models/
│   │   ├── ApplicationUser.cs
│   │   ├── Car.cs
│   │   └── Reservation.cs
│   ├── ApplicationDbContext.cs
│   └── SeedData.cs
├── Services/
│   ├── ICarService.cs / CarService.cs
│   ├── IReservationService.cs / ReservationService.cs
│   └── IUserService.cs / UserService.cs
├── Pages/
│   ├── Account/ (Login, Register, Logout)
│   ├── Cars/ (Index, Manage/)
│   ├── Reservations/ (Create, MyReservations, Manage/)
│   └── Admin/Users/ (Index, Edit, Delete)
├── wwwroot/css/site.css
└── Program.cs
```
