# 🚀 DevPulse - Incident & Resilience Management System

**DevPulse** es una solución empresarial de misión crítica diseñada para centralizar, gestionar y analizar incidentes tecnológicos. El objetivo principal es minimizar el tiempo de inactividad (Downtime) y fomentar una cultura de aprendizaje continuo mediante el análisis post-incidente.

![Dashboard Principal](meta/images/main.png)

## 💼 El Negocio: ¿Qué resuelve DevPulse?

En entornos de desarrollo y operaciones, los fallos son inevitables. DevPulse profesionaliza la respuesta ante estos fallos permitiendo:
1.  **Registro Inmediato:** Capturar incidentes con evidencias visuales.
2.  **Gestión de Equipos:** Asignar responsables según el rol (Dev, Ops, QA).
3.  **Análisis de Causa Raíz:** Documentar qué falló y cómo evitarlo en el futuro (Post-Mortems).
4.  **Auditoría y Trazabilidad:** Mantener un historial íntegro de quién hizo qué y cuándo.

---

## 🛠️ Funcionalidades del Sistema

### 1. Panel de Control (Dashboard)
Visualización global del estado operativo de la plataforma.
*   **Vistas principales:**
   *   Dashboard general: ![Dash](meta/images/dash.png)

### 2. Gestión de Incidentes
El corazón del sistema. Permite el ciclo de vida completo de un fallo técnico.
*   **Listado y Búsqueda:** ![Listar](meta/images/listar-incidentes.png)
*   **Creación con Validaciones:** Control estricto de datos mediante FluentValidation.
   *   Formulario: ![Crear](meta/images/crear-incidente.png)
   *   Validaciones en tiempo real: ![Validar](meta/images/crear-incidente-validaciones.png)
*   **Asignación de Personal:** Capacidad de delegar tareas a miembros específicos.
   *   Modal de asignación: ![Asignar](meta/images/asignar-member-incidente.png)
*   **Evidencias:** Visualización de pruebas cargadas (Cloudinary).
   *   Vista de evidencia: ![Prueba](meta/images/ver-prueba-incidente.png)
*   **Mantenimiento:**
   *   Edición: ![Editar](meta/images/editar-incidente.png) | Eliminación: ![Eliminar](meta/images/eliminar-incidente.png)

### 3. Módulo de Post-Mortems
Herramienta analítica para documentar la "autopsia" de los incidentes cerrados.
*   **Módulo Principal:** ![PostMortem Modulo](meta/images/postmortem-module.png)
*   **Filtros Dinámicos:** Búsqueda avanzada por causa raíz. ![Filtros](meta/images/postmortem-symbols-dinamicos.png)
*   **Creación y Gestión:**
   *   Registro: ![Crear PM](meta/images/crear-porstmortem.png)
   *   Validaciones: ![Validar PM](meta/images/crear-postmortem-validaciones.png)
   *   Edición: ![Editar PM](meta/images/editar-portmortem.png) | Eliminación: ![Eliminar PM](meta/images/eliminar-postmortem.png)

### 4. Gestión de Equipo (Team Members)
Administración del capital humano que responde a las emergencias.
*   **Gestión de Miembros:** ![Team Module](meta/images/team-module.png)
*   **Registro de Talento:**
   *   Alta de miembro: ![Crear Team](meta/images/crear-team-member.png)
   *   Validación de roles: ![Validar Team](meta/images/crear-team-member-validaciones.png)
*   **Mantenimiento:**
   *   Edición: ![Editar Team](meta/images/editar-team-member.png) | Eliminación: ![Eliminar Team](meta/images/eliminar-team-member.png)

---

## 🏗️ Arquitectura y Lógica de Aplicación

El sistema está construido bajo una **Arquitectura de Capas (N-Tier)**, garantizando el desacoplamiento mediante el uso intensivo de Interfaces en la capa de `Application`.

### Servicios Core (Interfaces)

El comportamiento del negocio está definido por contratos estrictos:

*   **`IIncidentService`**: Orquestador del ciclo de vida del incidente (Creación, búsqueda paginada, asignación de miembros y actualizaciones).
*   **`IPostMortemService`**: Gestiona la documentación post-incidente y filtros por causa raíz.
*   **`ITeamMemberService`**: Administra el catálogo de personal y sus roles dentro de la organización.
*   **`IFileStorageService`**: Abstracción para el manejo de archivos (implementado con **Cloudinary**), permitiendo `UploadImageAsync` y `DeleteImageAsync`.

### Stack Tecnológico
*   **Core:** .NET 8 (ASP.NET Core Razor Pages)
*   **Persistencia:** Entity Framework Core con PostgreSQL.
*   **Storage:** Cloudinary API para gestión de imágenes en la nube.
*   **Validación:** FluentValidation para lógica de negocio desacoplada de la UI.
*   **Frontend:** Bootstrap 5, jQuery y validación unobtrusive.
*   **Infraestructura:** Docker & Docker Compose para portabilidad total.

---

## 🚀 Instalación y Despliegue

### Requisitos
*   .NET 8 SDK
*   Docker Desktop
*   Credenciales de Cloudinary

### Ejecución Rápida (Docker)
```bash
docker-compose up --build
```

### Aplicar Migraciones Manuales
```bash
dotnet ef database update --project Infrastructure --startup-project Web
```

---
*Desarrollado con ❤️ en JetBrains Rider.*