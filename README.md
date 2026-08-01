---

# 🚀 DevPulse: Enterprise Incident Management System

**DevPulse** es una plataforma de nivel empresarial diseñada para equipos de ingeniería que necesitan gestionar el ciclo de vida completo de incidentes técnicos, desde la detección hasta el análisis post-mortem.

A diferencia de un simple CRUD, esta aplicación implementa una **Arquitectura Limpia (Clean Architecture)** y utiliza el motor de **Razor Pages** para ofrecer una experiencia SSR (Server-Side Rendering) rápida, segura y fuertemente tipada.

---

## 🛠️ Stack Tecnológico

*   **Backend:** .NET 9 (C#)
*   **Web UI:** ASP.NET Core Razor Pages (SSR)
*   **Base de Datos:** PostgreSQL
*   **ORM:** Entity Framework Core (Code First)
*   **Contenedores:** Docker & Docker Compose
*   **Validación:** FluentValidation
*   **Logging:** Serilog (Estructurado)
*   **Estilos:** Tailwind CSS / Bootstrap 5

---

## 🏗️ Arquitectura del Proyecto

El proyecto sigue los principios de **Separación de Preocupaciones (SoC)** y **SOLID**:

*   **DevPulse.Domain**: Núcleo del negocio. Contiene entidades (Incidents, PostMortems), Enums y reglas de validación de dominio. Cero dependencias externas.
*   **DevPulse.Application**: Capa de orquestación. Define las interfaces de servicios, DTOs y la lógica de negocio principal.
*   **DevPulse.Infrastructure**: Implementación de persistencia (EF Core), configuración de PostgreSQL y servicios externos (como el motor de generación de correos).
*   **DevPulse.Web (Razor Pages)**: La capa de presentación. Gestiona las rutas, el renderizado de HTML en el servidor y la interacción con el usuario.

---

## 🔥 Características Principales

### 1. Gestión de Incidentes Críticos
*   **Ciclo de Vida Realista:** Los incidentes transitan por estados: *Reported → Investigating → Identified → Resolved → Closed*.
*   **Niveles de Severidad:** Clasificación dinámica (Low, Medium, High, Critical) con impacto visual en la UI.
*   **Asignación de Responsables:** Vinculación de ingenieros a incidentes específicos.

### 2. Motor de Reportes Post-Mortem
*   Cuando un incidente se marca como "Resolved", el sistema habilita la creación de un **Post-Mortem**.
*   Permite documentar la "Causa Raíz" (Root Cause), el impacto medido y las acciones preventivas para evitar recurrencia.

### 3. Generación Dinámica de Plantillas (Razor Engine)
*   **Notificaciones HTML:** El sistema utiliza archivos `.cshtml` como plantillas para generar correos electrónicos y reportes PDF.
*   **Tipado Fuerte:** A diferencia de usar simples strings, las plantillas reciben modelos de C# (`IncidentSummaryViewModel`), garantizando que los reportes siempre tengan datos válidos.

### 4. Resiliencia y Seguridad
*   **Docker Ready:** Configuración lista para producción con Docker Compose (App + Postgres).
*   **Validación Robusta:** Uso de `FluentValidation` para asegurar que ningún incidente se registre con datos inconsistentes antes de llegar a la base de datos.

---

## 📋 El Modelo de Negocio

En el mundo empresarial, el tiempo de inactividad (downtime) cuesta miles de dólares. **DevPulse** soluciona esto mediante:
1.  **Visibilidad:** Un dashboard centralizado para que los stakeholders vean el estado de la infraestructura.
2.  **Transparencia:** Notificaciones automáticas basadas en plantillas cuando la severidad es "Critical".
3.  **Mejora Continua:** Obliga a los equipos a realizar análisis Post-Mortem para mejorar la calidad del software a largo plazo.

---

## 🚀 Instalación y Despliegue

Solo necesitas tener instalado **Docker** y **.NET 9 SDK**.

1. **Clonar el repositorio:**
   ```bash
   git clone https://github.com/tu-usuario/DevPulse.git
   cd DevPulse
   ```

2. **Levantar la infraestructura (PostgreSQL):**
   ```bash
   docker-compose up -d
   ```

3. **Ejecutar las migraciones y la App:**
   ```bash
   dotnet ef database update --project src/DevPulse.Infrastructure --startup-project src/DevPulse.Web
   dotnet run --project src/DevPulse.Web
   ```

4. **Acceder a la plataforma:**
   Abre tu navegador en `https://localhost:5001`

---

## 📸 Screenshots (Próximamente)
> *Aquí puedes añadir capturas de tu Dashboard de incidentes y el formulario de creación.*

---

## 🛡️ Roadmap de Desarrollo
- [ ] Implementar autenticación con ASP.NET Core Identity.
- [ ] Exportación de reportes Post-Mortem a PDF usando librerías de terceros.
- [ ] Dashboard de estadísticas con gráficos (Chart.js) sobre el tiempo promedio de resolución (MTTR).

---

## ✉️ Contacto
Desarrollado por **[Tu Nombre]** - [Tu LinkedIn](https://linkedin.com/in/tu-perfil) - [Tu Email](mailto:tuemail@ejemplo.com)

---

### ¿Qué hace que este proyecto destaque en tu portfolio?

1.  **Uso de PostgreSQL en Docker:** No usas una base de datos en memoria; usas una real, lo cual demuestra que entiendes entornos de desarrollo modernos.
2.  **Lógica de Negocio Real:** No es una lista de tareas (To-Do List). Es un sistema de "Incident Management", algo que los managers de ingeniería ven todos los días en herramientas como PagerDuty o Jira.
3.  **Razor Pages vs MVC:** Al usar Razor Pages, demuestras que estás al día con las recomendaciones actuales de Microsoft para aplicaciones web SSR que no requieren la complejidad de un SPA (Single Page Application).
4.  **Arquitectura Limpia:** Aunque no uses MediatR, el hecho de separar la lógica en `Application` e `Infrastructure` muestra que sabes escribir código mantenible y escalable (Enterprise-grade).