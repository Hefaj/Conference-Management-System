
# 📅 Roadmap: Modular Monolith Conference System

Projekt budowany w oparciu o architekturę **Modular Monolith** z wykorzystaniem:
* **.NET 10**
* **FastEndpoints** (Pattern REPR)
* **PostgreSQL** (Schema-per-module)
* **Docker**

---

## 🏁 Sprint 0: Fundamenty i Infrastruktura
**Cel:** Działająca solucja, kontener bazy danych i szkielet architektury.

- [x] **Inicjalizacja Solucji**
    - [x] Utworzenie pustej solucji `.sln`.
    - [x] Utworzenie folderów dla Backend i Frontend.
- [x] **Infrastruktura Docker**
    - [x] Plik `.env` z konfiguracją bazy danych (np. `POSTGRES_USER`, `POSTGRES_PASSWORD`, `POSTGRES_DB`).
    - [x] Plik `docker-compose.yml` z usługą PostgreSQL.
    - [x] (Opcjonalnie) Konfiguracja pgAdmin.
- [x] **Host Project**
    - [x] Projekt `CMS.Web` (ASP.NET Core).
    - [x] Instalacja i podstawowa konfiguracja **FastEndpoints**.
- [x] **Szkielet Modułów**
    - [x] Class Library: `CMS.Shared`.
    - [x] Class Library: `CMS.Modules.Cfp`.
    - [x] Class Library: `CMS.Modules.Ticketing`.
    - [x] Extension methods do rejestracji modułów w DI.
        - [x] `CMS.Modules.Cfp`
        - [x] `CMS.Modules.Ticketing`
- [x] **Baza Danych (EF Core)**
    - [x] Konfiguracja `DbContext` dla każdego modułu.
        - [x] `CMS.Modules.Cfp`
        - [x] `CMS.Modules.Ticketing`
    - [x] **Ważne:** Ustawienie osobnych schematów (Schema) w Postgres (np. `cfp`, `ticketing`).
- [x] **DDD**
    - [x] Aggregate Root (Entity)
    - [x] Value Object
    - [x] IDomainEvent
    - [x] Result
- [x] **DDD Sheard**
    - [x] EmailAddress – z walidacją formatu.
    - [x] Money (Kwota, Waluta) – z logiką zaokrągleń i operacji matematycznych.
    - [x] PersonName (Imię, Nazwisko).
    - [x] DateTimeRange (Start, Koniec) – z walidacją Start < Koniec.
- [x] IUnitOfWork
- [x] IRepository

---

## 🚀 Sprint 1: Moduł CFP (Call For Papers) - Vertical Slice
Odpowiedzialność: Obsługa procesu zgłaszania tematów przez prelegentów, 
proces recenzji (Call for Papers) oraz selekcja ostatecznej listy wystąpień.

**Cel:** Pierwsza działająca logika biznesowa, walidacja i API.

Ten moduł odpowiada za proces zgłaszania propozycji, ich recenzowania oraz wyboru prelegentów.
Rola: Zarządzanie cyklem życia zgłoszenia od "Draftu" do "Akceptacji/Odrzucenia".

- [x] **Model Domenowy**
    - [x] Agregat `Conference` (Entity).
    - [x] Value Objects (np. `Speaker`, `ConferenceStatus`).
- [ ] **Infrastructure**
    - [x] Mapowanie encji w EF Core.
    - [x] CfpDbContext
    - [x] CfpUnitOfWork
    - [ ] Utworzenie i wykonanie pierwszej migracji.
- [ ] **Endpoint: Zgłoszenie (POST)**
    - [ ] Implementacja `CreateConferenceEndpoint`.
    - [ ] DTO Request/Response.
    - [ ] Walidacja przy użyciu `FluentValidation` (wbudowane w FastEndpoints).
- [ ] **Endpoint: Przeglądanie (GET)**
    - [ ] Implementacja `GetConferenceEndpoint`.
- [ ] **Endpoint: Akceptacja (PUT)**
    - [ ] Implementacja `ApproveConferenceEndpoint` (zmiana statusu).
- [ ] **Testy**
    - [ ] Podstawowy test integracyjny (np. z użyciem `FastEndpoints.Testing`).

---

## 💎 Sprint 2: Moduł Ticketing - Logika i Współbieżność
**Cel:** Obsługa limitów, rezerwacji i problemów współbieżności (Concurrency).

- [ ] **Model Domenowy**
    - [ ] Agregat `Order`.
    - [ ] Encje `Ticket`, `TicketType`.
    - [ ] Value Object `Money`.
- [ ] **Logika Biznesowa**
    - [ ] Implementacja reguł (limity biletów, wygasanie rezerwacji).
- [ ] **Concurrency Control**
    - [ ] Konfiguracja tokena `xmin` w EF Core (Optimistic Concurrency).
- [ ] **Endpoint: Rezerwacja**
    - [ ] `ReserveTicketEndpoint` (logika sprawdzania dostępności).
- [ ] **Endpoint: Płatność**
    - [ ] `MarkOrderPaidEndpoint` (finalizacja zamówienia).
- [ ] **Obsługa Błędów**
    - [ ] Global Exception Handler / Problem Details.

---

## 🔗 Sprint 3: Komunikacja Asynchroniczna (Events)
**Cel:** Luźne powiązania między modułami.

- [ ] **Infrastruktura Eventów**
    - [ ] Abstrakcja `IEventBus` (lub konfiguracja MediatR/MassTransit).
- [ ] **Zdarzenia Domenowe**
    - [ ] Definicja `OrderPaidEvent` (Moduł Ticketing).
    - [ ] Definicja `SubmissionApprovedEvent` (Moduł CFP).
- [ ] **Outbox Pattern** (Zalecane)
    - [ ] Tabela `OutboxMessages` w bazie.
    - [ ] Zapis eventu w transakcji biznesowej.
    - [ ] Background Worker do publikacji eventów.
- [ ] **Moduł Conference (Integracja)**
    - [ ] Utworzenie modułu `MyConf.Modules.Conference`.
    - [ ] Listener: Tworzenie sesji w agendzie po odebraniu `SubmissionApprovedEvent`.

---

## ✨ Sprint 4: Moduł Access i Finalizacja
**Cel:** Dopięcie procesów biznesowych i dokumentacja.

- [ ] **Moduł Access**
    - [ ] Tabela `Attendees`.
    - [ ] Handler: `OrderPaidEventHandler` -> Utworzenie uczestnika i wejściówki.
- [ ] **Endpoint: Check-in**
    - [ ] `VerifyTicketEndpoint` (symulacja bramki).
- [ ] **Dokumentacja API**
    - [ ] Uzupełnienie opisów `Summary` w FastEndpoints dla Swaggera.
- [ ] **Cleanup & Docs**
    - [ ] Refaktoryzacja i usunięcie zbędnego kodu.
    - [ ] Przygotowanie `README.md` z instrukcją uruchomienia.

---

## 💡 Backlog / Pomysły na przyszłość
- [ ] Autoryzacja (JWT / API Keys).
- [ ] Integracja z prawdziwym systemem płatności (np. Stripe - tryb testowy).
- [ ] Prosty frontend (Blazor lub React, Angular).
- [ ] Generowanie biletów PDF.




1. Moduł: Nabór Prelekcji (Call for Papers / CFP)
Ten moduł odpowiada za proces zgłaszania propozycji, ich recenzowania oraz wyboru prelegentów.
Rola: Zarządzanie cyklem życia zgłoszenia od "Draftu" do "Akceptacji/Odrzucenia".

Zgłoszenie prelekcji.
