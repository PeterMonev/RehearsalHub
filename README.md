# 🎸 RehearsalHub

**A professional band management platform for musicians to organize rehearsals, manage setlists, and collaborate with their bands.**

[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-8.0-512BD4?style=for-the-badge&logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-12.0-239120?style=for-the-badge&logo=csharp)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![Entity Framework](https://img.shields.io/badge/EF%20Core-8.0-512BD4?style=for-the-badge&logo=dotnet)](https://docs.microsoft.com/en-us/ef/core/)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-2022-CC2927?style=for-the-badge&logo=microsoftsqlserver)](https://www.microsoft.com/sql-server)
[![Bootstrap](https://img.shields.io/badge/Bootstrap-5.3-7952B3?style=for-the-badge&logo=bootstrap)](https://getbootstrap.com/)
[![SignalR](https://img.shields.io/badge/SignalR-Real--time-512BD4?style=for-the-badge&logo=dotnet)](https://dotnet.microsoft.com/apps/aspnet/signalr)
[![xUnit](https://img.shields.io/badge/xUnit-Tests-green?style=for-the-badge)](https://xunit.net/)

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
- [Testing](#-testing)
- [Roadmap](#️-roadmap)
- [License](#-license)

---

## 🎯 Overview

**RehearsalHub** is a full-stack web application built with **ASP.NET Core 8 MVC** that helps bands and musicians manage their entire workflow — from scheduling rehearsals and building setlists, to managing band membership and song libraries.

The project is structured following **Clean Architecture** principles with strict **SOLID** compliance across 7 separate projects in a single solution, making the codebase production-ready, maintainable, and easily testable.

### The Problem It Solves

| Problem | RehearsalHub Solution |
|---|---|
| Scattered rehearsal schedules | Centralized scheduling with live status tracking |
| Disorganized song libraries | Structured library with genre / key / tempo filtering |
| Setlist chaos before gigs | Digital setlists with auto-calculated total duration |
| Band coordination overhead | Member invitations, role-based permissions |
| No platform oversight | Full admin panel for content moderation and user management |
| Missed invitations | Real-time SignalR notifications for pending band invitations |

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

### 🔔 Real-Time Notifications (SignalR)
- Live invitation badge counter in the navigation bar — updates instantly without page refresh
- Powered by **ASP.NET Core SignalR** (`NotificationsHub`)
- On connect, the hub fetches the user's pending invitation count and pushes it to the client
- Supports `RefreshInvitations` — the client can request a full updated list at any time
- Per-user notification system: create, retrieve, delete, and mark-all-as-read
- Notifications carry an optional URL for direct deep-linking

### 🛡️ Admin Panel
- Accessible only to users with the **Admin** role (`[Authorize(Roles = "Admin")]`)
- Role seeded automatically via EF Core migration (`SeedRolesAndAdminRole`)

#### Dashboard
- Live aggregate statistics: total users, bands, songs, rehearsals, setlists
- New users registered this month
- Count of currently active (non-deleted) bands

#### User Management
- Paginated, searchable list of all registered users
- **Promote** any user to the Admin role
- **Demote** any admin back to a regular user — self-demotion is blocked
- **Soft-delete** user accounts (cannot delete yourself)

#### Band Oversight
- View all bands in the system regardless of ownership
- **Edit** any band's name, genre, and image — no ownership check
- **Soft-delete** any band — cascades to its rehearsals and setlists

#### Song Moderation
- View **all** songs including private ones (admin bypasses visibility rules)
- **Create** new songs directly from the admin panel
- **Edit** any song — no ownership check
- **Hard-delete** any song from the system

### 🔐 Permissions Matrix

| Action | Admin | Owner | Member | Guest |
|--------|:-----:|:-----:|:------:|:-----:|
| Access Admin Panel | ✅ | ❌ | ❌ | ❌ |
| Promote / Demote users | ✅ | ❌ | ❌ | ❌ |
| Delete any user | ✅ | ❌ | ❌ | ❌ |
| Edit / Delete any band | ✅ | ❌ | ❌ | ❌ |
| View / Edit / Delete any song | ✅ | ❌ | ❌ | ❌ |
| View band / rehearsals / setlists | ✅ | ✅ | ✅ | ❌ |
| Create / Edit / Delete band | ✅ | ✅ | ❌ | ❌ |
| Invite / Remove members | ✅ | ✅ | ❌ | ❌ |
| Create / Edit / Delete rehearsals | ✅ | ✅ | ❌ | ❌ |
| Create / Edit / Delete setlists | ✅ | ✅ | ❌ | ❌ |
| Add / Remove songs from setlist | ✅ | ✅ | ❌ | ❌ |
| Add songs to band library | ✅ | ✅ | ✅ | ❌ |
| Create public songs | ✅ | ✅ | ✅ | ✅ |

---

## 🏗️ Architecture

RehearsalHub uses **Clean Architecture** with four distinct layers across seven projects:

```
┌──────────────────────────────────────────────────────────┐
│                    Presentation Layer                     │
│    RehearsalHub          — MVC Controllers, Razor Views   │
│    RehearsalHub/Areas/Admin — Admin Area (MVC + Razor)    │
│    RehearsalHub.Web.ViewModels — Pure DTO objects         │
└──────────────────────────────────────────────────────────┘
                           │
                           ▼
┌──────────────────────────────────────────────────────────┐
│                  Business Logic Layer                     │
│    RehearsalHub.Services.Data  — Service classes          │
│    RehearsalHub.Services.Data/Admin — AdminService        │
│    RehearsalHub.Services.Data/Hubs  — SignalR Hub         │
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

---

## 🛠️ Tech Stack

### Backend
| Technology | Version | Purpose |
|---|---|---|
| ASP.NET Core MVC | 8.0 | Web framework |
| C# | 12.0 | Primary language |
| Entity Framework Core | 8.0 | ORM / data access |
| ASP.NET Core Identity | 8.0 | Authentication & user management |
| ASP.NET Core SignalR | 8.0 | Real-time WebSocket notifications |
| LINQ | — | Data querying |

### Frontend
| Technology | Version | Purpose |
|---|---|---|
| Bootstrap | 5.3 | Responsive UI |
| Font Awesome | 6.x | Icon library |
| Vanilla JavaScript | ES6+ | Client-side filtering, validation & SignalR client |
| Razor | 8.0 | Server-side HTML templating |

### Testing
| Technology | Purpose |
|---|---|
| xUnit | Unit test framework |
| FluentAssertions | Readable assertion syntax |
| In-memory DbContext (EF Core) | Isolated database per test |
| Coverlet / Cobertura | Code coverage reporting |

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
│   ├── Areas/
│   │   └── Admin/                          # 🛡️ Admin Area
│   │       ├── Controllers/
│   │       │   └── AdminController.cs      # Dashboard, Users, Bands, Songs
│   │       └── Views/Admin/
│   │           ├── Index.cshtml            # Dashboard with stats
│   │           ├── Users.cshtml            # User list + promote/demote/delete
│   │           ├── Bands.cshtml            # Band list + edit/delete
│   │           ├── Songs.cshtml            # All songs + edit/delete
│   │           ├── EditBand.cshtml
│   │           ├── EditSong.cshtml
│   │           └── CreateSong.cshtml
│   ├── Controllers/
│   │   ├── BandsController.cs              # Band CRUD + member management
│   │   ├── SongsController.cs              # Song library management
│   │   ├── SetlistsController.cs           # Setlist CRUD + song assignment
│   │   ├── RehearsalsController.cs         # Rehearsal scheduling
│   │   ├── NotificationsController.cs      # Notification read/delete
│   │   ├── InvitationsController.cs        # Band invitation accept/decline
│   │   └── HomeController.cs              # Landing page
│   ├── Views/
│   │   ├── Bands/
│   │   ├── Songs/
│   │   ├── Setlists/
│   │   ├── Rehearsals/
│   │   └── Shared/
│   ├── wwwroot/
│   │   ├── css/
│   │   └── js/
│   │       ├── site.js                     # General UI logic
│   │       ├── site-confirm.js             # SweetAlert delete confirmation
│   │       ├── site-messages.js            # Toast/flash messages
│   │       ├── site-search.js              # Live search helpers
│   │       └── site-search-addsong.js      # Setlist song picker filtering
│   └── Program.cs                          # App startup, DI, SignalR mapping
│
├── RehearsalHub.Web.ViewModels/            # 📦 Presentation DTOs (no logic)
│   ├── Admin/
│   │   ├── AdminDashboardViewModel.cs      # Stats for admin dashboard
│   │   ├── AdminUserViewModel.cs
│   │   ├── AdminBandViewModel.cs
│   │   └── AdminSongViewModel.cs
│   ├── Bands/
│   ├── Songs/
│   ├── Setlist/
│   └── Rehearsal/
│
├── RehearsalHub.Services.Data/             # 💼 Business Logic Layer
│   ├── Admin/      → IAdminService, AdminService
│   ├── Bands/      → IBandService, BandService
│   ├── Songs/      → ISongService, SongService
│   ├── Setlists/   → ISetlistService, SetlistService
│   ├── Rehearsals/ → IRehearsalService, RehearsalService
│   ├── Invitation/ → IInvitationService, InvitationService
│   ├── Notifications/ → INotificationService, NotificationService
│   ├── Users/      → IUserService, UserService
│   └── Hubs/
│       └── NotificationsHub.cs             # SignalR hub (invitations)
│
├── RehearsalHub.Data/                      # 🗄️ Data Access Layer
│   ├── ApplicationDbContext.cs
│   └── Migrations/
│       └── SeedRolesAndAdminRole           # Seeds Admin role on first run
│
├── RehearsalHub.Data.Models/              # 📊 Domain Entities
│   ├── Band.cs, BandMember.cs, Song.cs
│   ├── Setlist.cs, SetlistSong.cs
│   ├── Rehearsal.cs
│   ├── Notification.cs
│   ├── ApplicationUser.cs
│   ├── BaseEntity.cs
│   └── Enums/
│
├── RehearsalHub.GCommon/                  # 🛠️ Shared Utilities
│   ├── Helpers/
│   │   ├── MusicHelper.cs
│   │   └── DateTimeHelper.cs
│   ├── DataValidation/
│   └── EntityConstants.cs
│
└── RehearsalHub.Tests/                    # 🧪 Unit Tests
    ├── Helpers/
    │   ├── TestDataBuilder.cs              # Reusable test entity factory
    │   └── TestDbContextFactory.cs         # In-memory DbContext factory
    ├── Services/
    │   ├── AdminServiceTests.cs
    │   ├── BandServiceTests.cs
    │   ├── InvitationServiceTests.cs
    │   ├── NotificationServiceTests.cs
    │   ├── RehearsalServiceTests.cs
    │   ├── SetlistServiceTests.cs
    │   ├── SongServiceTest.cs
    │   └── UserServiceTests.cs
    └── TestResults/
        └── coverage.cobertura.xml          # Code coverage report
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

This also seeds the `Admin` role automatically.
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
6. To access the **Admin Panel**, promote a user to Admin via the database or seed script, then navigate to `/Admin`

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
| `GET` | `/Songs/Edit/{id}` | Edit form | ✅ | Creator / Band Owner |
| `POST` | `/Songs/Edit/{id}` | Save changes | ✅ | Creator / Band Owner |
| `POST` | `/Songs/Delete/{id}` | Delete song | ✅ | Creator / Band Owner |

**Query parameters for `GET /Songs`:**

| Parameter | Values | Description |
|-----------|--------|-------------|
| `bandId` | `int` | Filter by band |
| `genre` | `Rock`, `Pop`, `Jazz`, `Blues`, `Metal`, `Classical`, `Country`, `Electronic`, `HipHop`, `Reggae`, `Folk`, `Other` | Filter by genre |
| `key` | `C` `D` `E` `F` `G` `A` `B` | Filter by musical key |
| `tempo` | `slow` `medium` `fast` | <80 / 80–120 / >120 BPM |
| `searchTerm` | `string` | Search in title and artist |

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

---

### 🔔 Notifications — `NotificationsController` + SignalR Hub

| Method | Route | Description | Auth |
|--------|-------|-------------|------|
| `GET` | `/Notifications` | List all notifications | ✅ |
| `POST` | `/Notifications/Delete/{id}` | Delete a notification | ✅ |
| `POST` | `/Notifications/MarkAllRead` | Mark all as read | ✅ |
| **WS** | `/hubs/notifications` | SignalR hub endpoint | ✅ |

**SignalR Hub methods (`NotificationsHub`):**

| Hub Method | Direction | Description |
|---|---|---|
| `GetInitialCount` | Client → Server | Fetches pending invitation count on connect |
| `UpdateInviteCount` | Server → Client | Pushes live invitation badge number |
| `RefreshInvitations` | Client → Server | Requests full updated invitation list |
| `UpdateInvitations` | Server → Client | Pushes full invitation list to caller |

---

### 🛡️ Admin — `AdminController` (`/Admin`)

| Method | Route | Description |
|--------|-------|-------------|
| `GET` | `/Admin` | Dashboard with aggregate stats |
| `GET` | `/Admin/Users` | Paginated user list |
| `POST` | `/Admin/PromoteUser` | Promote user to Admin |
| `POST` | `/Admin/DemoteUser` | Remove Admin role (self-demotion blocked) |
| `POST` | `/Admin/DeleteUser` | Soft-delete a user account |
| `GET` | `/Admin/Bands` | Paginated band list |
| `POST` | `/Admin/DeleteBand` | Soft-delete any band |
| `GET` | `/Admin/EditBand/{id}` | Edit band form |
| `POST` | `/Admin/EditBand` | Save band changes |
| `GET` | `/Admin/Songs` | All songs (public + private) |
| `GET` | `/Admin/CreateSong` | Create song form |
| `POST` | `/Admin/CreateSong` | Submit new song |
| `GET` | `/Admin/EditSong/{id}` | Edit song form |
| `POST` | `/Admin/EditSong` | Save song changes |
| `POST` | `/Admin/DeleteSong` | Hard-delete any song |

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

AspNetUsers ──► Notifications
AspNetUsers ──► Invitations ──► Bands
```

### Key Tables

#### Bands
```sql
Id          INT            PK IDENTITY
Name        NVARCHAR(100)  NOT NULL
Genre       INT            NOT NULL
ImageUrl    NVARCHAR(2000) NULL
OwnerId     NVARCHAR(450)  FK → AspNetUsers.Id
CreatedOn   DATETIME2      NOT NULL
IsDeleted   BIT            DEFAULT 0
DeletedOn   DATETIME2      NULL
```

#### Songs
```sql
Id          INT            PK IDENTITY
Title       NVARCHAR(200)  NOT NULL
Artist      NVARCHAR(200)  NOT NULL
Duration    NVARCHAR(10)   NOT NULL   -- "mm:ss"
Genre       INT            NOT NULL
MusicalKey  INT            NOT NULL
Tempo       INT            NULL       -- BPM
IsPrivate   BIT            DEFAULT 0
OwnerBandId INT            NULL FK → Bands.Id
CreatorId   NVARCHAR(450)  FK → AspNetUsers.Id
CreatedOn   DATETIME2      NOT NULL
IsDeleted   BIT            DEFAULT 0
DeletedOn   DATETIME2      NULL
```

#### Notifications
```sql
Id          INT            PK IDENTITY
UserId      NVARCHAR(450)  FK → AspNetUsers.Id
Message     NVARCHAR(MAX)  NOT NULL
Url         NVARCHAR(2000) NULL
IsRead      BIT            DEFAULT 0
CreatedOn   DATETIME2      NOT NULL
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
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IInvitationService, InvitationService>();
builder.Services.AddSignalR();
```

### Thin Controllers
Controllers only handle routing and ModelState — zero business logic.

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

Every query filters with `.Where(x => !x.IsDeleted)`. Admin hard-delete for songs is the only exception.

### Static Helper Classes

```csharp
// MusicHelper.cs
GetTempoCategory(int? tempo)
CalculateTotalDuration(IEnumerable<string> durations)
ParseDurationToSeconds(string duration)
FormatSecondsToTimeString(int seconds)

// DateTimeHelper.cs
IsNotInPast(DateTime start)
IsValidTimeRange(DateTime start, DateTime end)
IsHappeningNow(DateTime start, DateTime end)
IsUpcoming(DateTime start)
FormatDuration(TimeSpan duration)
FormatDateForDisplay(DateTime date)
FormatTimeForDisplay(DateTime date)
```

---

## 🔐 Security

| Concern | Implementation |
|---|---|
| Authentication | ASP.NET Core Identity — PBKDF2 password hashing |
| Authorization | `[Authorize]` on all controllers, service-level ownership checks |
| Admin access | `[Authorize(Roles = "Admin")]` on the entire Admin area |
| CSRF | `[ValidateAntiForgeryToken]` on every POST |
| SQL Injection | Fully parameterized via Entity Framework Core |
| XSS | Razor auto-encodes all rendered output |
| Past-date exploits | Client-side JS `min` attribute + server-side Service validation |
| Self-demotion / self-delete | Blocked in `AdminService` — compares target ID to current admin ID |
| SignalR identity | Hub uses `Context.UserIdentifier` (ASP.NET Identity claim) |
| Data integrity | Soft deletes preserve all foreign-key relationships |

---

## 🧪 Testing

RehearsalHub includes a dedicated `RehearsalHub.Tests` project with comprehensive unit tests covering all service classes.

### Test Stack
- **xUnit** — test framework
- **FluentAssertions** — human-readable assertions (`result.Should().Be(...)`)
- **EF Core InMemory** — isolated in-memory database per test (via `TestDbContextFactory`)
- **Coverlet** — code coverage with Cobertura XML output

### Test Coverage

| Test File | Service Tested | Scenarios Covered |
|---|---|---|
| `SongServiceTests.cs` | `SongService` | Create, paged list, details, edit permissions, delete |
| `BandServiceTests.cs` | `BandService` | Create, member invite, remove, ownership checks |
| `RehearsalServiceTests.cs` | `RehearsalService` | Create, past-date validation, status (upcoming/now/done), delete |
| `SetlistServiceTests.cs` | `SetlistService` | Create, add/remove songs, duration calculation |
| `InvitationServiceTests.cs` | `InvitationService` | Send, accept, decline, pending count |
| `NotificationServiceTests.cs` | `NotificationService` | Create, get, delete, mark-all-read |
| `AdminServiceTests.cs` | `AdminService` | Dashboard stats, promote/demote, delete user/band/song |
| `UserServiceTests.cs` | `UserService` | Profile retrieval, update |

### Running Tests

```bash
# Run all tests
dotnet test

# Run with code coverage
dotnet test --collect:"XPlat Code Coverage"

# Run a specific test file
dotnet test --filter "FullyQualifiedName~SongServiceTests"
```

### Test Helpers

```csharp
// TestDbContextFactory — creates a fresh in-memory DB for each test
using var context = TestDbContextFactory.Create();

// TestDataBuilder — factory methods for common entities
var user = TestDataBuilder.CreateUser();
var band = TestDataBuilder.CreateBand(user.Id);
var song = TestDataBuilder.CreateSong(user.Id, band.Id);
```

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
- [x] Clean Architecture + SOLID across 7 projects

### ✅ Version 1.5 — Delivered
- [x] Real-time notifications via SignalR (invitation badge)
- [x] Full notification system (create, read, delete, mark-all-read)
- [x] Admin Panel with dashboard, user/band/song management
- [x] Role seeding via EF Core migration
- [x] Comprehensive unit test suite (8 test classes, xUnit + FluentAssertions)
- [x] Code coverage reporting (Cobertura XML)

### 🔮 Version 2.0 — Future
- [ ] Attendance RSVP per rehearsal
- [ ] Venue / location management
- [ ] Email notifications for upcoming rehearsals
- [ ] Band activity timeline
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

- [Microsoft](https://dotnet.microsoft.com/) — ASP.NET Core, EF Core & SignalR
- [Bootstrap](https://getbootstrap.com/) — UI framework
- [Font Awesome](https://fontawesome.com/) — Icon library
- [xUnit](https://xunit.net/) — Testing framework
- [FluentAssertions](https://fluentassertions.com/) — Assertion library

---

### ⭐ If this project helped you, please star the repository!

**Built with ❤️ for musicians, by a developer who values clean code**

🎸 *Keep Rocking!* 🎸

© 2026 Peter Monev. All rights reserved.
Do not copy, reproduce, or use this code or concept without permission.
