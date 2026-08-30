# SafeVault - Secure .NET 10 Web API & Security Toolkit

A high-performance, secure Web API built on **.NET 10 (C# 14)** demonstrating production-grade security patterns, defensive coding, and vulnerability mitigation. Designed and developed by **Mushahid Ali Kazmi** (**@ZeroFluxAI**).

---

## 🛡️ Key Security Features

* **Authentication & Hashing:** Password hashing using **BCrypt** with dynamic salt generation (`BCrypt.Net-Next`).
* **SQL Injection Protection:** Fully parameterized SQLite queries via `Microsoft.Data.Sqlite` preventing standard and advanced SQLi vectors.
* **HTTP Hardening Middleware:** Applied custom response security headers:
  * `X-Content-Type-Options: nosniff`
  * `X-Frame-Options: DENY`
  * `X-XSS-Protection: 1; mode=block`
  * `Content-Security-Policy: default-src 'self'`
  * `Referrer-Policy: strict-origin-when-cross-origin`
* **Role-Based Authorization (RBAC):** Tiered privilege separation distinguishing standard `User` accounts from privileged `Admin` identities.
* **Automated Admin Seeding:** Idempotent database initialization ensuring default administrator provisioning upon system startup.

---

## 🧰 Tech Stack & Prerequisites

* **Framework:** .NET 10 Web API
* **Database:** SQLite (`Microsoft.Data.Sqlite`)
* **Documentation & OpenAPI:** Swashbuckle / Swagger UI
* **Testing Suite:** xUnit, `Microsoft.AspNetCore.Mvc.Testing`
* **Language:** C# 14

---

## 🚀 Quick Start Guide

### 1. Repository Setup

Clone the repository and open the solution directory:

```bash
git clone [https://github.com/ZeroFluxAI/SafeVault-Security-Enhancements.git](https://github.com/ZeroFluxAI/SafeVault-Security-Enhancements.git)
cd SafeVault-Security-Enhancements

👤 Author & Maintainer
Developer: Mushahid Ali Kazmi

GitHub Profile: @ZeroFluxAI

Project Repository: SafeVault-Security-Enhancements

📄 License
This project is open-source software licensed under the MIT License.
