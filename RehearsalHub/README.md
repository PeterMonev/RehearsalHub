# 🎸 RehearsalHub

**A professional band management platform for musicians to organize rehearsals, manage setlists, and collaborate with their bands.**

[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-8.0-512BD4?style=for-the-badge&logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-12.0-239120?style=for-the-badge&logo=csharp)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![Entity Framework](https://img.shields.io/badge/EF%20Core-8.0-512BD4?style=for-the-badge&logo=dotnet)](https://docs.microsoft.com/en-us/ef/core/)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-2022-CC2927?style=for-the-badge&logo=microsoftsqlserver)](https://www.microsoft.com/sql-server)
[![Bootstrap](https://img.shields.io/badge/Bootstrap-5.3-7952B3?style=for-the-badge&logo=bootstrap)](https://getbootstrap.com/)

[Live Demo](#) • [Report Bug](https://github.com/PeterMonev/RehearsalHub/issues) • [Request Feature](https://github.com/PeterMonev/RehearsalHub/issues)

---

## 📋 Table of Contents

- [Overview](#-overview)
- [Features](#-features)
- [Architecture](#️-architecture)
- [Tech Stack](#️-tech-stack)
- [Project Structure](#-project-structure)
- [Getting Started](#-getting-started)
- [API Endpoints](#-api-endpoints)
- [Database Schema](#️-database-schema)
- [Design Patterns](#-design-patterns)
- [Security](#-security)
- [Roadmap](#️-roadmap)
- [License](#-license)

---

## 🎯 Overview

**RehearsalHub** is a full-stack web application built with **ASP.NET Core 8 MVC** that helps bands and musicians manage their entire workflow — from scheduling rehearsals and building setlists, to managing band membership and song libraries.

The project is structured following **Clean Architecture** principles with strict **SOLID** compliance across 6 separate projects in a single solution, making the codebase production-ready, maintainable, and easily testable.

### The Problem It Solves

| Problem | RehearsalHub Solution |
|---|---|
| Scattered rehearsal schedules | Centralized scheduling with live status tracking |
| Disorganized song libraries | Structured library with genre / key / tempo filtering |
| Setlist chaos before gigs | Digital setlists with auto-calculated total duration |
| Band coordination overhead | Member invitations, role-based permissions |

---

## ✨ Features

### 🎤 Band Management
- Create and manage multiple bands simultaneously
- Assign a custom image and genre to each band
- **Role-based system:** Band Owner vs Band Member with different permissions
- Invite members by username or email; remove them at any time
- Assign instruments to each member (Guitar, Bass, Drums, Vocals, Keyboard, etc.)

### 🎵 Song Library
- Full song metadata: Title, Artist, Duration, Genre, Musical Key, Tempo (BPM)
- Public and private song visibility
- Songs can be band-specific or globally shared
- **Advanced filtering:**
  - Genre: Rock, Pop, Jazz, Blues, Metal, Classical, Country, Electronic, HipHop, Reggae, Folk, Other
  - Musical Key: C, D, E, F, G, A, B
  - Tempo category: Slow (<80 BPM), Medium (80–120 BPM), Fast (>120 BPM)
  - Real-time search by title or artist

### 📝 Setlist Management
- Create unlimited setlists per band
- Smart song picker with live client-side filtering (genre, tempo, availability)
- "Select All Visible" for quick bulk selection
- Auto-calculated total duration from all songs in the setlist
- Remove individual songs
- **Print-friendly layout** — printable setlist with clean typography
- Optional rehearsal/show date on each setlist

### 📅 Rehearsal Scheduling
- Book rehearsals with start and end datetime
- **Past-date prevention:** validated on both client (JS `min` attribute) and server (Service layer)
- Link any setlist to a rehearsal
- **Three live statuses:**
  - 🟢 Upcoming — future rehearsal
  - 🔴 Happening Now — currently in progress (animated indicator)
  - ⚫ Completed — past rehearsal
- Auto-calculated duration displayed as "Xh Ym"
- Session notes per rehearsal
- **"My Rehearsals" view** — aggregated upcoming rehearsals across all the user's bands

### 🔐 Permissions Matrix

| Action | Owner | Member | Guest |
|--------|:-----:|:------:|:-----:|
| View band / rehearsals / setlists | ✅ | ✅ | ❌ |
| Create / Edit / Delete band | ✅ | ❌ | ❌ |
| Invite / Remove members | ✅ | ❌ | ❌ |
| Create / Edit / Delete rehearsals | ✅ | ❌ | ❌ |
| Create / Edit / Delete setlists | ✅ | ❌ | ❌ |
| Add / Remove songs from setlist | ✅ | ❌ | ❌ |
| Add songs to band library | ✅ | ✅ | ❌ |
| Create public songs | ✅ | ✅ | ✅ |

---

## 🏗️ Architecture

RehearsalHub uses **Clean Architecture** with four distinct layers across six projects:

```
┌──────────────────────────────────────────────────────────┐
│                    Presentation Layer                     │
│    RehearsalHub          — MVC Controllers, Razor Views   │
│    RehearsalHub.Web.ViewModels — Pure DTO objects         │
└──────────────────────────────────────────────────────────┘
                           │
                           ▼
┌──────────────────────────────────────────────────────────┐
│                  Business Logic Layer                     │
│    RehearsalHub.Services.Data  — Service classes          │
│    RehearsalHub.GCommon        — Helpers & constants      │
└──────────────────────────────────────────────────────────┘
                           │
                           ▼
┌──────────────────────────────────────────────────────────┐
│                   Data Access Layer                       │
│    RehearsalHub.Data        — DbContext, Migrations       │
│    RehearsalHub.Data.Models — Domain entity classes       │
└──────────────────────────────────────────────────────────┘
                           │
                           ▼
┌──────────────────────────────────────────────────────────┐
│                     Database Layer                        │
│          SQL Server  +  EF Core Code-First Migrations     │
└──────────────────────────────────────────────────────────┘
```

## 🛠️ Tech Stack

### Backend
| Technology | Version | Purpose |
|---|---|---|
| ASP.NET Core MVC | 8.0 | Web framework |
| C# | 12.0 | Primary language |
| Entity Framework Core | 8.0 | ORM / data access |
| ASP.NET Core Identity | 8.0 | Authentication & user management |
| LINQ | — | Data querying |

### Frontend
| Technology | Version | Purpose |
|---|---|---|
| Bootstrap | 5.3 | Responsive UI |
| Font Awesome | 6.x | Icon library |
| Vanilla JavaScript | ES6+ | Client-side filtering & validation |
| Razor | 8.0 | Server-side HTML templating |

### Infrastructure
| Technology | Purpose |
|---|---|
| SQL Server / LocalDB | Primary relational database |
| EF Core Code-First Migrations | Schema versioning |
| ASP.NET Core Identity Tables | User, role, claim management |

---

## 📁 Project Structure

```
RehearsalHub.sln
│
├── RehearsalHub/                            # 🌐 Web Application (entry point)
│   ├── Controllers/
│   │   ├── BandsController.cs              # Band CRUD + member management
│   │   ├── SongsController.cs              # Song library management
│   │   ├── SetlistsController.cs           # Setlist CRUD + song assignment
│   │   ├── RehearsalsController.cs         # Rehearsal scheduling
│   │   └── HomeController.cs              # Landing page
│   ├── Views/
│   │   ├── Bands/                          # Index, Details, Create, Edit
│   │   ├── Songs/                          # Index, Details, Create, Edit
│   │   ├── Setlists/                       # Details, Create, Edit, AddSongs
│   │   ├── Rehearsals/                     # Index, MyRehearsals, Details, Create, Edit
│   │   └── Shared/                         # _Layout, _ValidationScripts, Error
│   ├── wwwroot/
│   │   ├── css/                            # Custom styles
│   │   └── js/                             # Custom scripts
│   └── Program.cs                          # App startup, DI registration
│
├── RehearsalHub.Web.ViewModels/            # 📦 Presentation DTOs (no logic)
│   ├── Bands/
│   ├── Songs/
│   ├── Setlist/
│   └── Rehearsal/
│
├── RehearsalHub.Services.Data/             # 💼 Business Logic Layer
│   ├── Bands/      → IBandService, BandService
│   ├── Songs/      → ISongService, SongService
│   ├── Setlists/   → ISetlistService, SetlistService
│   └── Rehearsals/ → IRehearsalService, RehearsalService
│
├── RehearsalHub.Data/                      # 🗄️ Data Access Layer
│   ├── ApplicationDbContext.cs
│   └── Migrations/
│
├── RehearsalHub.Data.Models/              # 📊 Domain Entities
│   ├── Band.cs
│   ├── BandMember.cs          # Junction: Band ↔ User
│   ├── Song.cs
│   ├── Setlist.cs
│   ├── SetlistSong.cs         # Junction: Setlist ↔ Song
│   ├── Rehearsal.cs
│   ├── ApplicationUser.cs     # Extended Identity user
│   ├── BaseEntity.cs          # Soft-delete base class
│   └── Enums/
│       ├── Genre.cs
│       ├── MusicalKey.cs
│       └── InstrumentType.cs
│
└── RehearsalHub.GCommon/                  # 🛠️ Shared Utilities
    ├── Helpers/
    │   ├── MusicHelper.cs                 # Tempo, duration, key logic
    │   └── DateTimeHelper.cs             # Date formatting, validation
    ├── DataValidation/
    │   ├── Band.cs / Song.cs / Setlist.cs / Rehearsal.cs
    └── EntityConstants.cs
```

---

## 🚀 Getting Started

### Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/sql-server) or SQL Server LocalDB
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or VS Code

### Installation

**1. Clone the repository**
```bash
git clone https://github.com/PeterMonev/RehearsalHub.git
cd RehearsalHub
```

**2. Configure the connection string**

Edit `RehearsalHub/appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=RehearsalHub;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

**3. Apply database migrations**
```bash
dotnet ef database update --project RehearsalHub.Data --startup-project RehearsalHub
```

**4. Run the application**
```bash
dotnet run --project RehearsalHub
```

**5. Open in browser**
```
https://localhost:7103
```

### First Steps
1. **Register** a new account
2. **Create a band** — you become its Owner
3. **Add songs** to the band library
4. **Create a setlist** and add songs to it
5. **Book a rehearsal** and optionally link your setlist

---

## 📡 API Endpoints

### 🎸 Bands — `BandsController`

| Method | Route | Description | Auth | Who |
|--------|-------|-------------|------|-----|
| `GET` | `/Bands` | List all user's bands | ✅ | Any |
| `GET` | `/Bands/Details/{id}` | Band dashboard | ✅ | Member |
| `GET` | `/Bands/Create` | Create form | ✅ | Any |
| `POST` | `/Bands/Create` | Submit new band | ✅ | Any |
| `GET` | `/Bands/Edit/{id}` | Edit form | ✅ | Owner |
| `POST` | `/Bands/Edit/{id}` | Save changes | ✅ | Owner |
| `POST` | `/Bands/Delete/{id}` | Soft-delete band | ✅ | Owner |
| `POST` | `/Bands/Invite` | Invite a member | ✅ | Owner |
| `POST` | `/Bands/RemoveMember` | Remove a member | ✅ | Owner |

---

### 🎵 Songs — `SongsController`

| Method | Route | Description | Auth | Who |
|--------|-------|-------------|------|-----|
| `GET` | `/Songs?bandId={id}` | Song library with filters | ✅ | Member |
| `GET` | `/Songs/Details/{id}` | Song details | ✅ | Member |
| `GET` | `/Songs/Create?bandId={id}` | Create form | ✅ | Member |
| `POST` | `/Songs/Create` | Submit new song | ✅ | Member |
| `GET` | `/Songs/Edit/{id}` | Edit form | ✅ | Creator |
| `POST` | `/Songs/Edit/{id}` | Save changes | ✅ | Creator |
| `POST` | `/Songs/Delete/{id}` | Soft-delete song | ✅ | Creator |

**Query parameters for `GET /Songs`:**

| Parameter | Values | Description |
|-----------|--------|-------------|
| `bandId` | `int` | **Required** |
| `genre` | `Rock`, `Pop`, `Jazz`, `Blues`, `Metal`, `Classical`, `Country`, `Electronic`, `HipHop`, `Reggae`, `Folk`, `Other` | Filter by genre |
| `key` | `C` `D` `E` `F` `G` `A` `B` | Filter by musical key |
| `tempo` | `slow` `medium` `fast` | <80 / 80–120 / >120 BPM |
| `search` | `string` | Search in title and artist |

---

### 📝 Setlists — `SetlistsController`

| Method | Route | Description | Auth | Who |
|--------|-------|-------------|------|-----|
| `GET` | `/Setlists/Details/{id}` | Setlist + full tracklist | ✅ | Member |
| `GET` | `/Setlists/Create?bandId={id}` | Create form | ✅ | Owner |
| `POST` | `/Setlists/Create` | Submit new setlist | ✅ | Owner |
| `GET` | `/Setlists/Edit/{id}` | Edit form | ✅ | Owner |
| `POST` | `/Setlists/Edit/{id}` | Save changes | ✅ | Owner |
| `POST` | `/Setlists/Delete/{id}` | Soft-delete setlist | ✅ | Owner |
| `GET` | `/Setlists/AddSongs/{id}` | Song picker with filters | ✅ | Owner |
| `POST` | `/Setlists/AddSongs/{id}` | Add selected songs | ✅ | Owner |
| `POST` | `/Setlists/RemoveSong` | Remove song from setlist | ✅ | Owner |

**POST `/Setlists/AddSongs/{id}` body:**
```
selectedSongIds=1&selectedSongIds=5&selectedSongIds=12
```

---

### 📅 Rehearsals — `RehearsalsController`

| Method | Route | Description | Auth | Who |
|--------|-------|-------------|------|-----|
| `GET` | `/Rehearsals?bandId={id}` | Band's rehearsal history | ✅ | Member |
| `GET` | `/Rehearsals/MyRehearsals` | All upcoming rehearsals (all bands) | ✅ | Any |
| `GET` | `/Rehearsals/Details/{id}` | Rehearsal + linked setlist songs | ✅ | Member |
| `GET` | `/Rehearsals/Create?bandId={id}` | Create form | ✅ | Owner |
| `POST` | `/Rehearsals/Create` | Book new rehearsal | ✅ | Owner |
| `GET` | `/Rehearsals/Edit/{id}` | Edit form | ✅ | Owner |
| `POST` | `/Rehearsals/Edit/{id}` | Save changes | ✅ | Owner |
| `POST` | `/Rehearsals/Delete/{id}` | Soft-delete rehearsal | ✅ | Owner |

**Validation rules for Create / Edit:**

| Field | Rule |
|-------|------|
| `Name` | Required, 3–100 characters |
| `StartRehearsal` | Required, **must not be in the past** |
| `EndRehearsal` | Required, **must be after StartRehearsal** |
| `Notes` | Optional, max 1 000 characters |
| `SetlistId` | Optional, must belong to the same band |

---

### 🏠 Home — `HomeController`

| Method | Route | Description | Auth |
|--------|-------|-------------|------|
| `GET` | `/` | Landing page | Public |
| `GET` | `/Home/Privacy` | Privacy policy | Public |

---

## 🗄️ Database Schema

### Entity Relationship Diagram

```
AspNetUsers ──(owns)──► Bands ◄──(joins)── AspNetUsers
                          │
              ┌───────────┼───────────┐
              │           │           │
           Songs       Setlists   Rehearsals
                          │           │
                     SetlistSongs  (uses Setlist)
                          │
                        Songs
```

### Tables

#### Bands
```sql
Id          INT            PK IDENTITY
Name        NVARCHAR(100)  NOT NULL
Genre       INT            NOT NULL   -- enum
ImageUrl    NVARCHAR(2000) NULL
OwnerId     NVARCHAR(450)  FK → AspNetUsers.Id
CreatedOn   DATETIME2      NOT NULL
IsDeleted   BIT            DEFAULT 0
DeletedOn   DATETIME2      NULL
```

#### BandMembers *(junction)*
```sql
BandId      INT            PK FK → Bands.Id
UserId      NVARCHAR(450)  PK FK → AspNetUsers.Id
Instrument  INT            NOT NULL   -- enum
IsLeader    BIT            NOT NULL
JoinedOn    DATETIME2      NOT NULL
```

#### Songs
```sql
Id          INT            PK IDENTITY
Title       NVARCHAR(200)  NOT NULL
Artist      NVARCHAR(200)  NOT NULL
Duration    NVARCHAR(10)   NOT NULL   -- "mm:ss"
Genre       INT            NOT NULL   -- enum
MusicalKey  INT            NOT NULL   -- enum  C=0 … B=6
Tempo       INT            NULL       -- BPM
IsPrivate   BIT            DEFAULT 0
OwnerBandId INT            NULL FK → Bands.Id
CreatorId   NVARCHAR(450)  FK → AspNetUsers.Id
CreatedOn   DATETIME2      NOT NULL
IsDeleted   BIT            DEFAULT 0
DeletedOn   DATETIME2      NULL
```

#### Setlists
```sql
Id            INT            PK IDENTITY
Name          NVARCHAR(100)  NOT NULL
BandId        INT            FK → Bands.Id
RehearsalDate DATETIME2      NULL
CreatedOn     DATETIME2      NOT NULL
IsDeleted     BIT            DEFAULT 0
DeletedOn     DATETIME2      NULL
```

#### SetlistSongs *(junction)*
```sql
SetlistId  INT  PK FK → Setlists.Id
SongId     INT  PK FK → Songs.Id
```

#### Rehearsals
```sql
Id              INT             PK IDENTITY
Name            NVARCHAR(100)   NOT NULL
StartRehearsal  DATETIME2       NOT NULL
EndRehearsal    DATETIME2       NOT NULL
Notes           NVARCHAR(1000)  NULL
BandId          INT             FK → Bands.Id
SetlistId       INT             NULL FK → Setlists.Id
CreatedOn       DATETIME2       NOT NULL
IsDeleted       BIT             DEFAULT 0
DeletedOn       DATETIME2       NULL
```

### Enumerations

```csharp
public enum Genre
{
    Rock = 0, Pop = 1, Jazz = 2, Blues = 3, Metal = 4,
    Classical = 5, Country = 6, Electronic = 7,
    HipHop = 8, Reggae = 9, Folk = 10, Other = 11
}

public enum MusicalKey   { C = 0, D = 1, E = 2, F = 3, G = 4, A = 5, B = 6 }

public enum InstrumentType
{
    Guitar = 0, Bass = 1, Drums = 2, Vocals = 3,
    Keyboard = 4, Saxophone = 5, Trumpet = 6, Other = 7
}
```

---

## 🧩 Design Patterns

### Service Layer
All business logic lives in services, never in controllers or ViewModels. Every service is accessed through an interface, enabling easy substitution and testing.

```csharp
// Program.cs
builder.Services.AddScoped<IBandService, BandService>();
builder.Services.AddScoped<ISongService, SongService>();
builder.Services.AddScoped<ISetlistService, SetlistService>();
builder.Services.AddScoped<IRehearsalService, RehearsalService>();
```

### Thin Controllers
Controllers only handle routing and ModelState — zero business logic:

```csharp
[HttpPost, ValidateAntiForgeryToken]
public async Task<IActionResult> Create(RehearsalInputModel model)
{
    if (!ModelState.IsValid) { ... return View(model); }

    if (!rehearsalService.ValidateNotInPast(model.StartRehearsal))
    {
        ModelState.AddModelError(nameof(model.StartRehearsal), "Must be in the future");
        return View(model);
    }

    int id = await rehearsalService.CreateRehearsalAsync(model, GetCurrentUserId());
    return RedirectToAction(nameof(Details), new { id });
}
```

### Soft Delete
No data is permanently lost. All main entities inherit `BaseEntity`:

```csharp
public abstract class BaseEntity
{
    public DateTime CreatedOn { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedOn { get; set; }
}
```

Every query filters with `.Where(x => !x.IsDeleted)`.

### Static Helper Classes

```csharp
// MusicHelper.cs — all music-domain logic
GetTempoCategory(int? tempo)           // → "slow" / "medium" / "fast" / "unknown"
CalculateTotalDuration(IEnumerable<string> durations)  // → "1h 23m"
ParseDurationToSeconds(string duration)
FormatSecondsToTimeString(int seconds)

// DateTimeHelper.cs — all date/time logic
IsNotInPast(DateTime start)            // → true / false
IsValidTimeRange(DateTime start, DateTime end)
IsHappeningNow(DateTime start, DateTime end)
IsUpcoming(DateTime start)
FormatDuration(TimeSpan duration)      // → "2h 30m"
FormatDateForDisplay(DateTime date)    // → "Mon, Feb 17, 2026"
FormatTimeForDisplay(DateTime date)    // → "18:00"
```

---

## 🔐 Security

| Concern | Implementation |
|---|---|
| Authentication | ASP.NET Core Identity — PBKDF2 password hashing |
| Authorization | `[Authorize]` on all controllers, service-level ownership checks |
| CSRF | `[ValidateAntiForgeryToken]` on every POST |
| SQL Injection | Fully parameterized via Entity Framework Core |
| XSS | Razor auto-encodes all rendered output |
| Past-date exploits | Client-side JS `min` attribute + server-side Service validation |
| Data integrity | Soft deletes preserve all foreign-key relationships |

---

## 🗺️ Roadmap

### ✅ Version 1.0 — Delivered
- [x] Full band management with role-based access
- [x] Song library with multi-dimension filtering
- [x] Setlist builder with live song picker
- [x] Rehearsal scheduling with past-date prevention
- [x] "My Rehearsals" cross-band aggregated view
- [x] Happening-Now live status indicator
- [x] Print-friendly setlists
- [x] Mobile-responsive design
- [x] Clean Architecture + SOLID across 6 projects

### 🚧 Version 1.5 — Planned
- [ ] Attendance RSVP per rehearsal
- [ ] Venue / location management
- [ ] Email notifications for invitations and upcoming rehearsals
- [ ] Band activity timeline

### 🔮 Version 2.0 — Future
- [ ] Real-time notifications via SignalR
- [ ] Recurring rehearsal scheduling
- [ ] Calendar view (month / week)
- [ ] Export setlist to PDF
- [ ] Public band profiles
- [ ] Mobile app (MAUI)

---

## 👤 Author

**Peter Monev**
- GitHub: [@PeterMonev](https://github.com/PeterMonev)
- LinkedIn: [https://www.linkedin.com/in/peter-monev-22582b248/]

---

## 🙏 Acknowledgements

- [Microsoft](https://dotnet.microsoft.com/) — ASP.NET Core & Entity Framework Core
- [Bootstrap](https://getbootstrap.com/) — UI framework
- [Font Awesome](https://fontawesome.com/) — Icon library

---

### ⭐ If this project helped you, please star the repository!

**Built with ❤️ for musicians, by a developer who values clean code**

🎸 *Keep Rocking!* 🎸

© 2026 Peter Monev. All rights reserved.
Do not copy, reproduce, or use this code or concept without permission.
