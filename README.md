# 🔥 BlazingQuiz

**BlazingQuiz** es una plataforma de cuestionarios (quizzes) multiplataforma.  
El proyecto está en **fase inicial de desarrollo** – todavía no hay funcionalidades completas, pero la estructura base ya está definida.

## 🎯 Objetivo técnico

Construir una solución completa para crear, compartir y responder cuestionarios, con:

- **API** (ASP.NET Core) – Gestión de quizzes, preguntas, respuestas, usuarios y puntuaciones.
- **Web** (Blazor) – Interfaz para administradores y jugadores.
- **Mobile** (.NET MAUI o Blazor Hybrid) – Experiencia nativa en iOS/Android.
- **Shared** (librería de clases) – Modelos y DTOs reutilizables.

## 🧩 Estado actual

- [x] Estructura de solución creada (`.sln` con 4 proyectos).
- [ ] Definición del modelo de datos (entidades `Quiz`, `Question`, `Option`, `UserAnswer`, etc.).
- [ ] Endpoints básicos de la API.
- [ ] Autenticación / autorización.
- [ ] Implementación de la lógica de corrección.
- [ ] Interfaces de usuario (web y móvil).

## 📌 Próximos pasos (planeados)

1. Diseñar el esquema de base de datos (Entity Framework Core).
2. Implementar CRUD de quizzes en la API.
3. Crear un flujo simple para responder quizzes desde la web.
4. Migrar la lógica compartida a `BlazingQuiz.Shared`.
5. Añadir soporte móvil.

## 🤝 Contribuciones

Por ahora el proyecto es solo un esqueleto, pero cualquier idea o ayuda es bienvenida.  
Abre un *issue* si quieres discutir algo técnico.

## 📄 Licencia

MIT (se añadirá más adelante).
