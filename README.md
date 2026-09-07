# Ignite

Ignite is a C# church platform: a public website plus a simple CMS so a church team can manage campuses, sermons, events, pages, ministries, and connect requests.

It is a starting point for a site in the same family as [kingdomcity.com](https://kingdomcity.com) — locations, watch online, give, and connect — built in ASP.NET Core so you can keep growing it yourself.

## Run it

You need the [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0).

```bash
cd src/Web
dotnet run
```

Then open:

- Public site: `http://localhost:5208`
- Admin CMS: `http://localhost:5208/admin`
- API docs: `http://localhost:5208/swagger`

Demo admin login (change this before any real church use):

- Email: `admin@ignitechurch.local`
- Password: `IgniteAdmin!23`

SQLite creates `src/Web/ignite.db` on first run and seeds sample campuses, sermons, and events.

```bash
dotnet test
```

## What is included

Public website:

- Home, locations, watch/sermons, events, give, connect form, about/ministries

Admin CMS (`/admin`):

- Campuses and service times
- Sermons
- Events
- Pages
- Ministries
- Connect inbox
- Church settings (name, hero, livestream, giving URL)

API (`/api/...`) for a future app or kiosk.

## Solution layout

This follows a four-layer ASP.NET Core layout:

- `src/Core` — church entities and enums
- `src/Application` — DTOs, `Result<T>`, services, repository contracts
- `src/Infrastructure` — EF Core, SQLite, Identity, seed data
- `src/WebApi` — HTTP controllers and `ApiResponse<T>`
- `src/Web` — Blazor public site + admin UI (the runnable host)
- `tests/Ignite.Application.Tests`

## Why C# is a good fit here

You already write C#, so you can own the whole stack: website, CMS, and later an API for a mobile app. Blazor keeps the admin UI in C# as well. For giving, livestream, and email, plug in tools churches already use (Tithely/Pushpay, YouTube, Planning Center) instead of rebuilding them.

## Sensible next steps

1. Replace the demo church name, locations, and copy with your church.
2. Change the admin password and keep `appsettings` secrets out of git.
3. Point **Give** at Tithely, Pushpay, or your bank.
4. Point **Watch** at your YouTube or Subsplash livestream.
5. Host on a VPS, Azure App Service, or a Windows/Linux box your church already has.
6. Later: volunteer roles, kids check-in, groups, and a branded app consuming `/api`.

Building a full custom platform like a large international church is a long project. This repo gives you a real, organised Ignite foundation you can run this week and extend as the church needs it.
