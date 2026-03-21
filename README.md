# HR Leave Management

Educational **leave management** solution built in **ASP.NET Core** using **Clean Architecture** ideas: a modular, test-friendly layout where domain rules stay independent of infrastructure and UI.

This repository is my **learning implementation**—it is **not** an official fork of the instructor’s projects—and it follows **[ASP.NET Core – Clean Architecture](https://www.youtube.com/watch?v=gGa7SLk1-0Q)** on YouTube (clean architecture, scaling, maintainability, MVC, and **Fluent Validation** in that series).

**Trevoir Williams** published the companion source for that **older, free YouTube** track as **[HR.LeaveManagement.CleanArchitecture-dotnet5](https://github.com/trevoirwilliams/HR.LeaveManagement.CleanArchitecture-dotnet5/tree/master)** on GitHub. That repository is **archived** (read-only since April 2023) and targets the **.NET 5**-era solution layout (Api, Application, Domain, Infrastructure, Persistence, MVC, Identity, tests, etc.). **I am intentionally following this older sample** so my project stays aligned with what the YouTube course shows.

The author also offers a **newer version of the course on Udemy**. To find it, open the **[same YouTube video](https://www.youtube.com/watch?v=gGa7SLk1-0Q)** and read the **video description**—that is where the link and context are provided.

---

## Why SOLID and clean architecture?

Early architectural choices affect how easy it is to **extend** and **maintain** an app. SOLID and clean/onion-style layering help with:

| Principle | Meaning (short) |
|-----------|------------------|
| **S** | Single responsibility — each unit has one reason to change |
| **O** | Open/closed — extend behavior without modifying core code unnecessarily |
| **L** | Liskov substitution — subtypes honor contracts |
| **I** | Interface segregation — small, focused interfaces |
| **D** | Dependency inversion — depend on abstractions, not concrete infrastructure |

Together with **CQRS** and **MediatR**, this style keeps application features organized (commands/queries, handlers) and keeps the **domain** at the center.

---

## What this repository contains (current state)

| Layer | Role |
|-------|------|
| **Domain** | Entities and shared domain primitives (e.g. leave types, allocations, requests) |
| **Application** | Use cases: **CQRS** with **MediatR**, **AutoMapper** profiles, DTOs, and **persistence contracts** (repository interfaces) |

**Target framework:** .NET 10 (`net10.0`).

---

## Build

From the repository root:

```bash
dotnet build
```

---

## Attribution

- **Video:** [ASP.NET Core – Clean Architecture](https://www.youtube.com/watch?v=gGa7SLk1-0Q) (use the **description** there for the author’s newer Udemy offering)  
- **Older reference repo (archived):** [trevoirwilliams/HR.LeaveManagement.CleanArchitecture-dotnet5](https://github.com/trevoirwilliams/HR.LeaveManagement.CleanArchitecture-dotnet5/tree/master)
